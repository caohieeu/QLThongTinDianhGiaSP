using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThongTinDanhGiaSP.Models;
using QuanLyThongTinDanhGiaSP.Services;
using QuanLyThongTinDanhGiaSP.VIews;


namespace QuanLyThongTinDanhGiaSP
{
    public partial class ProductItemControl : UserControl
    {
        private products _product;

        public ProductItemControl(products product)
        {
            InitializeComponent();
            _product = product;
            DisplayProductInfo();
        }

        private void DisplayProductInfo()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Size = new System.Drawing.Size(this.Width, 130);

            TableLayoutPanel table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4
            };

            Label lblName = new Label
            {
                Text = $"Name: {_product.name}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblCategory = new Label
            {
                Text = $"Category: {_product.category_name}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblCreatedAt = new Label
            {
                Text = $"Created: {_product.create_at}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            Button btnViewReviews = new Button
            {
                Text = "Xem đánh giá",
                Dock = DockStyle.Fill
            };
            btnViewReviews.Click += BtnViewReviews_Click;

            table.Controls.Add(lblName, 0, 0);
            table.Controls.Add(lblCategory, 0, 1);
            table.Controls.Add(lblCreatedAt, 0, 2);
            table.Controls.Add(btnViewReviews, 0, 3);

            this.Controls.Add(table);
        }

        private void BtnViewReviews_Click(object sender, EventArgs e)
        {
            ReviewForm reviewForm = new ReviewForm(_product);
            reviewForm.Show();
        }
    }
}
