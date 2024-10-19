using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QuanLyThongTinDanhGiaSP.DAL;
using QuanLyThongTinDanhGiaSP.Models;
using QuanLyThongTinDanhGiaSP.Repository;
using QuanLyThongTinDanhGiaSP.Repository.IRepository;
using QuanLyThongTinDanhGiaSP.Services;

namespace QuanLyThongTinDanhGiaSP.VIews
{
    public partial class AnalyticsForm : Form
    {
        private readonly IProductReviewsReponsitory _productReviewsReponsitory;
        private readonly ProductService _productService;
        private readonly CassandraContext _cassandraContext;
        public AnalyticsForm()
        {
            InitializeComponent();
            _cassandraContext = new CassandraContext(Utils.KeySpace);
            _productReviewsReponsitory = new ProductReviewsReponsitory(_cassandraContext);
            _productService = new ProductService();

            LoadProducts();
        }
        private void LoadProducts()
        {
            var products = _productService.GetAllProduct();
            cbProduct.DataSource = products;
            cbProduct.DisplayMember = "Name";
            cbProduct.ValueMember = "product_id";
        }

        private void btnAnalyst_Click(object sender, EventArgs e)
        {
            var selectedProductId = (Guid)cbProduct.SelectedValue;
            var reviews = _productReviewsReponsitory.GetProductReviews(selectedProductId).ToList();

            var positiveReviews = reviews.Count(r => r.Rating >= 4);
            var negativeReviews = reviews.Count(r => r.Rating < 4);

            lblPositiveRate.Text = $"{((double)positiveReviews / reviews.Count * 100).ToString("F2")}%";
            lblNegativeRate.Text = $"{((double)negativeReviews / reviews.Count * 100).ToString("F2")}%";

            chart1.Series.Clear();
            var series = new Series("Đánh giá");
            series.ChartType = SeriesChartType.Column;
            series.Points.AddXY("Tích cực", positiveReviews);
            series.Points.AddXY("Tiêu cực", negativeReviews);
            chart1.Series.Add(series);

            // Hiển thị thống kê đánh giá
            dataGridView1.DataSource = reviews.Select(r => new
            {
                r.ReviewId,
                r.Username,
                r.Rating,
                r.ReviewText,
                r.PurchaseDate,
                r.Status,
                r.ConfirmDate
            }).ToList();
        }
    }
}
