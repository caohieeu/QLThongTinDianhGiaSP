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
    public partial class CategoryItemControl : UserControl
    {
        private Categories _category;
        public event EventHandler<Guid> ViewProductsRequested;

        public CategoryItemControl(Categories category)
        {
            InitializeComponent();
            _category = category;
            DisplayCategoryInfo();
        }

        private void DisplayCategoryInfo()
        {
            this.Size = new Size(300, 100);
            this.Margin = new Padding(10);
            this.BackColor = Color.White;
            this.Padding = new Padding(1);

            this.BorderStyle = BorderStyle.None;
            this.Paint += (sender, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid);
            };

            TableLayoutPanel table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(10)
            };

            Label lblName = new Label
            {
                Text = _category.name,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185)
            };

            Label lblCreatedAt = new Label
            {
                Text = $"Ngày tạo: {_category.create_at.ToString("dd/MM/yyyy HH:mm")}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 10),
                ForeColor = Color.FromArgb(100, 100, 100)
            };

            Button btnViewProducts = new Button
            {
                Text = "Xem sản phẩm",
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
    
            btnViewProducts.Click += (sender, e) => ViewProducts();

            table.Controls.Add(lblName, 0, 0);
            table.Controls.Add(lblCreatedAt, 0, 1);
            table.Controls.Add(btnViewProducts, 1, 0);
            table.SetRowSpan(btnViewProducts, 2);

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            this.Controls.Add(table);

            this.MouseEnter += (sender, e) => 
            {
                this.BackColor = Color.FromArgb(245, 245, 245);
                this.Invalidate();
            };
            this.MouseLeave += (sender, e) => 
            {
                this.BackColor = Color.White;
                this.Invalidate();
            };
        }

        private void ViewProducts()
        {
            ProductForm frm = new ProductForm();
            frm.Show();
        }
    }
}
