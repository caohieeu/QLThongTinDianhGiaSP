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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThongTinDanhGiaSP.VIews
{
    public partial class ProductForm : Form
    {
        private readonly CassandraContext _cassandraContext = new CassandraContext(Utils.KeySpace);
        private readonly IProductReviewsReponsitory _productReviewsReponsitory;
        private readonly ProductService _productService;

        public ProductForm()
        {
            InitializeComponent();
            _productService = new ProductService();
            _productReviewsReponsitory = new ProductReviewsReponsitory(_cassandraContext);
            dateTime_start.Format = DateTimePickerFormat.Custom;
            dateTime_start.CustomFormat = "dd/MM/yyyy";
            dateTime_end.Format = DateTimePickerFormat.Custom;
            dateTime_end.CustomFormat = "dd/MM/yyyy";
            LoadData();
            LoadProductIntoComboBox();
            this.btn_Loc.Click += Btn_Loc_Click;
            txt_Search.TextChanged += Txt_Search_TextChanged;
        }
        public void LoadData()
        {
            var products = _productService.GetAllProduct();
            DisplayProducts(products);
        }

         private void DisplayProducts(IEnumerable<products> products)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.AutoScroll = true;
            foreach (var product in products)
            {
                var productControl = new ProductItemControl(product);
                productControl.Width = flowLayoutPanel1.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 20;
                productControl.DoubleClick += (sender, e) => ProductControl_DoubleClick(product);
                flowLayoutPanel1.Controls.Add(productControl);
            }
        }

        private void ProductControl_DoubleClick(products product)
        {
            ChildProductForm frm = new ChildProductForm("edit", product.product_id.ToString(), product.category_id.ToString());
            frm.ShowDialog();
            if (frm.IsSave)
            {
                LoadData();
            }
        }

        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            ChildProductForm frm = new ChildProductForm("add", null, null);
            frm.ShowDialog();
            if(frm.IsSave)
            {
                LoadData();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //var productId = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            //var categoryId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            //ChildProductForm frm = new ChildProductForm("edit", productId, categoryId);
            //frm.ShowDialog();
            //if (frm.IsSave)
            //{
            //    LoadData();
            //}
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void LoadProductIntoComboBox()
        {
            var allProducts = _productService.GetAllProduct();

            var productNames = new List<string> { "" };

            productNames.AddRange(allProducts.Select(c => c.category_name).ToList());

            cbb_NameProducts.DataSource = productNames;

            cbb_NameProducts.SelectedIndex = 0;
        }
        private void Btn_Loc_Click(object sender, EventArgs e)
        {
            string selectedProductsName = cbb_NameProducts.SelectedItem?.ToString();
            DateTime selectedDate_Start = dateTime_start.Value;
            DateTime selectedDate_End = dateTime_end.Value;

            IEnumerable<products> filteredProducts;

            if (!string.IsNullOrWhiteSpace(selectedProductsName))
            {
                filteredProducts = _productService.FilterProductByName("category_name", selectedProductsName);

                if (dateTime_start.Value != null && dateTime_end.Value != null)
                {
                    filteredProducts = filteredProducts
                        .Where(product => product.create_at >= selectedDate_Start && product.create_at <= selectedDate_End);
                }
            }
            else if (dateTime_start.Value != null && dateTime_end.Value != null)
            {
                filteredProducts = _productService.FilterProductByDate(selectedDate_Start, selectedDate_End, "create_at");
            }
            else
            {
                filteredProducts = _productService.GetAllProduct();
            }

            DisplayProducts(filteredProducts);
        }
        private void Txt_Search_TextChanged(object sender, EventArgs e)
        {
            string inputText = txt_Search.Text.Trim();

            if (!string.IsNullOrEmpty(inputText))
            {
                var filteredProducts = _productService.FilterProductByName("name", inputText);

                DisplayProducts(filteredProducts);
            }
            else
            {
                LoadData(); 
            }
        }
        private void ExportReviewsToExcel()
        {
            var reviews = _productReviewsReponsitory.GetProductReviews(new Guid()).ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Danh sách đánh giá");

                // Tiêu đề cột
                worksheet.Cells[1, 1].Value = "Mã đánh giá";
                worksheet.Cells[1, 2].Value = "Tên người dùng";
                worksheet.Cells[1, 3].Value = "Điểm đánh giá";
                worksheet.Cells[1, 4].Value = "Nội dung đánh giá";
                worksheet.Cells[1, 5].Value = "Ngày mua";
                worksheet.Cells[1, 6].Value = "Trạng thái";
                worksheet.Cells[1, 7].Value = "Ngày xác nhận";

                // Dữ liệu
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

                // Định dạng
                worksheet.Cells.AutoFitColumns();

                // Lưu file
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Lưu file đánh giá",
                    //FileName = $"{_product.name}_DanhSachDanhGia.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(saveFileDialog.FileName, package.GetAsByteArray());
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExportReviewsToExcel();
        }
    }
}