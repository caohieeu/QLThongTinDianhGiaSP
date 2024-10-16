﻿using QuanLyThongTinDanhGiaSP.Models;
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
        private readonly ProductService _productService;
        public ProductForm()
        {
            InitializeComponent();
            _productService = new ProductService();
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

            // Hiển thị lại các sản phẩm đã lọc trong flowLayoutPanel
            DisplayProducts(filteredProducts);
        }
        private void Txt_Search_TextChanged(object sender, EventArgs e)
        {
            string inputText = txt_Search.Text.Trim();

            if (!string.IsNullOrEmpty(inputText))
            {
                // Lọc sản phẩm theo tên trong _productService
                var filteredProducts = _productService.FilterProductByName("name", inputText);

                // Hiển thị sản phẩm đã lọc trong flowLayoutPanel
                DisplayProducts(filteredProducts);
            }
            else
            {
                LoadData(); // Gọi lại phương thức LoadData để lấy tất cả sản phẩm
            }
        }

    }
}