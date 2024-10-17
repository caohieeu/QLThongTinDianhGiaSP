using QuanLyThongTinDanhGiaSP.DAL;
using QuanLyThongTinDanhGiaSP.Models;
using QuanLyThongTinDanhGiaSP.Repository;
using QuanLyThongTinDanhGiaSP.Repository.IRepository;
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
    public partial class ReviewForm : Form
    {
        private products _product;
        private readonly IProductReviewsReponsitory _productReviewsReponsitory;
        private readonly CassandraContext _cassandraContext = new CassandraContext(Utils.KeySpace);
        
        public ReviewForm(products product)
        {
            InitializeComponent();
            _productReviewsReponsitory = new ProductReviewsReponsitory(_cassandraContext);
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
    }
}
