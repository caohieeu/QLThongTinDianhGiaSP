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
         
            dateTime_start.Format = DateTimePickerFormat.Custom;
            dateTime_start.CustomFormat = "dd/MM/yyyy";
            dateTime_end.Format = DateTimePickerFormat.Custom;
            dateTime_end.CustomFormat = "dd/MM/yyyy";
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
        }

        private void btn_Loc_Click_1(object sender, EventArgs e)
        {
            DateTime selectedDate_Start = dateTime_start.Value;
            DateTime selectedDate_End = dateTime_end.Value;

            IEnumerable<Product_Review> filteredReviews = _productReviewsReponsitory.GetProductReviews();

            if (dateTime_start.Value != null && dateTime_end.Value != null)
            {
                filteredReviews = filteredReviews
                    .Where(review => review.PurchaseDate >= selectedDate_Start && review.PurchaseDate <= selectedDate_End);
            }

            if (radioButton1.Checked)
            {
                filteredReviews = filteredReviews.Where(review => review.Rating >= 1 && review.Rating < 2);
            }
            else if (radioButton2.Checked)
            {
                filteredReviews = filteredReviews.Where(review => review.Rating >= 2 && review.Rating < 3);
            }
            else if (radioButton3.Checked)
            {
                filteredReviews = filteredReviews.Where(review => review.Rating >= 3 && review.Rating < 4);
            }
            else if (radioButton4.Checked)
            {
                filteredReviews = filteredReviews.Where(review => review.Rating >= 4 && review.Rating < 5);
            }
            else if (radioButton5.Checked)
            {
                filteredReviews = filteredReviews.Where(review => review.Rating == 5);
            }

            DisplayReviews(filteredReviews);
        }

    }
}
