using QuanLyThongTinDanhGiaSP.Models;
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
using OfficeOpenXml;
using QuanLyThongTinDanhGiaSP.DAL;
using QuanLyThongTinDanhGiaSP.Repository;
using QuanLyThongTinDanhGiaSP.Repository.IRepository;

namespace QuanLyThongTinDanhGiaSP.VIews
{
    public partial class MainReviewForm : Form
    {
        private readonly ProductService _productService;
        private products _product;
        private readonly IProductReviewsReponsitory _productReviewsReponsitory;
        private readonly CassandraContext _cassandraContext = new CassandraContext(Utils.KeySpace);

        public MainReviewForm()
        {
            InitializeComponent();
            _productReviewsReponsitory = new ProductReviewsReponsitory(_cassandraContext);
            _productService = new ProductService();
            LoadData();
            LoadProductIntoComboBox();

            DisplayReviews(_productReviewsReponsitory.GetProductReviews());
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

        public void LoadData()
        {
        }

        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            ChildProductForm frm = new ChildProductForm("add", null, null);
            frm.ShowDialog();
            if (frm.IsSave)
            {
                LoadData();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

        private void btn_Loc_Click_1(object sender, EventArgs e)
        {
            string selectedProductsName = cbb_NameProducts.SelectedItem?.ToString();
            DateTime selectedDate_Start = dateTime_start.Value;
            DateTime selectedDate_End = dateTime_end.Value;

            IEnumerable<products> filteredProducts;

            // Lọc theo tên sản phẩm
            if (!string.IsNullOrWhiteSpace(selectedProductsName))
            {
                filteredProducts = _productService.FilterProductByName("category_name", selectedProductsName);

                // Lọc thêm theo ngày nếu đã chọn
                if (dateTime_start.Value != null && dateTime_end.Value != null)
                {
                    filteredProducts = filteredProducts
                        .Where(product => product.create_at >= selectedDate_Start && product.create_at <= selectedDate_End);
                }
            }
            // Lọc theo ngày nếu không chọn tên sản phẩm
            else if (dateTime_start.Value != null && dateTime_end.Value != null)
            {
                filteredProducts = _productService.FilterProductByDate(selectedDate_Start, selectedDate_End, "create_at");
            }
            else
            {
                // Nếu không có điều kiện lọc nào thì lấy tất cả sản phẩm
                filteredProducts = _productService.GetAllProduct();
            }
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            string inputText = txt_Search.Text.Trim();

            if (!string.IsNullOrEmpty(inputText))
            {
                // Lọc sản phẩm theo tên trong _productService
                var filteredProducts = _productService.FilterProductByName("name", inputText);
            }
            else
            {
                LoadData(); // Gọi lại phương thức LoadData để lấy tất cả sản phẩm
            }
        }
    }
}
