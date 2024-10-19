using Cassandra.Data.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.IO.Image;
using OfficeOpenXml;
using QuanLyThongTinDanhGiaSP.DAL;
using QuanLyThongTinDanhGiaSP.Models;
using QuanLyThongTinDanhGiaSP.Repository;
using QuanLyThongTinDanhGiaSP.Repository.IRepository;
using QuanLyThongTinDanhGiaSP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QuanLyThongTinDanhGiaSP.VIews
{
    public partial class ReviewForm : Form
    {
        private products _product;
        private readonly IProductReviewsReponsitory _productReviewsReponsitory;
        private readonly ProductService _productService;
        private readonly CassandraContext _cassandraContext = new CassandraContext(Utils.KeySpace);
        
        public ReviewForm(products product)
        {
            InitializeComponent();
            _productReviewsReponsitory = new ProductReviewsReponsitory(_cassandraContext);
            _productService = new ProductService();
            _product = product;

            LoadReviews();
        }

        private void LoadReviews()
        {
            this.Text = $"Đánh giá cho {_product.name}";
            var reviews = _productReviewsReponsitory.GetProductReviews(_product.product_id);

            DisplayReviews(reviews);
        }

        private void DisplayReviews(IEnumerable<Product_Review> reviews)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.AutoScroll = true;

            foreach (var review in reviews)
            {
                var reviewControl = new ProductReviewsControl(review);
                flowLayoutPanel1.Controls.Add(reviewControl);
            }
        }
        private void ExportReviewsToExcel()
        {
            var reviews = _productReviewsReponsitory.GetProductReviews(_product.product_id).ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Danh sách đánh giá");

                worksheet.Cells[1, 1].Value = "Mã đánh giá";
                worksheet.Cells[1, 2].Value = "Tên người dùng";
                worksheet.Cells[1, 3].Value = "Điểm đánh giá";
                worksheet.Cells[1, 4].Value = "Nội dung đánh giá";
                worksheet.Cells[1, 5].Value = "Ngày mua";
                worksheet.Cells[1, 6].Value = "Trạng thái";
                worksheet.Cells[1, 7].Value = "Ngày xác nhận";

                for (int i = 0; i < reviews.Count; i++)
                {
                    var review = reviews[i];
                    worksheet.Cells[i + 2, 1].Value = review.ReviewId.ToString();
                    worksheet.Cells[i + 2, 2].Value = review.Username;
                    worksheet.Cells[i + 2, 3].Value = review.Rating;
                    worksheet.Cells[i + 2, 4].Value = review.ReviewText;
                    worksheet.Cells[i + 2, 5].Value = review.PurchaseDate?.ToString("dd/MM/yyyy");
                    worksheet.Cells[i + 2, 6].Value = review.Status;
                    worksheet.Cells[i + 2, 7].Value = review.ConfirmDate?.ToString("dd/MM/yyyy");
                }

                worksheet.Cells.AutoFitColumns();

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Lưu file đánh giá",
                    FileName = $"{_product.name}_DanhSachDanhGia.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(saveFileDialog.FileName, package.GetAsByteArray());
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void ExportReviewsToPdf()
        {
            var reviews = _productReviewsReponsitory.GetProductReviews(_product.product_id).ToList();

            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                document.Add(new Paragraph($"Danh sách đánh giá cho sản phẩm: {_product.name}").SetFontSize(18));
                document.Add(new Paragraph(" "));

                var table = new Table(7);
                table.AddHeaderCell("Mã đánh giá");
                table.AddHeaderCell("Tên người dùng");
                table.AddHeaderCell("Điểm đánh giá");
                table.AddHeaderCell("Nội dung đánh giá");
                table.AddHeaderCell("Ngày mua");
                table.AddHeaderCell("Trạng thái");
                table.AddHeaderCell("Ngày xác nhận");

                foreach (var review in reviews)
                {
                    table.AddCell(review.ReviewId.ToString());
                    table.AddCell(review.Username);
                    table.AddCell(review.Rating.ToString());
                    table.AddCell(review.ReviewText);
                    table.AddCell(review.PurchaseDate?.ToString("dd/MM/yyyy") ?? "N/A");
                    table.AddCell(review.Status);
                    table.AddCell(review.ConfirmDate?.ToString("dd/MM/yyyy") ?? "N/A");
                }

                document.Add(table);
                document.Close();

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    Title = "Lưu file đánh giá",
                    FileName = $"{_product.name}_DanhSachDanhGia.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(saveFileDialog.FileName, stream.ToArray());
                    MessageBox.Show("Xuất file PDF thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExportReviewsToExcel();
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            ExportReviewsToPdf();
        }
    }
}
