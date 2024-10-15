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
        }
        public void LoadData()
        {
            dataGridView1.DataSource = _productService.GetAllProduct();
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
            var productId = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            var categoryId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            ChildProductForm frm = new ChildProductForm("edit", productId, categoryId);
            frm.ShowDialog();
            if (frm.IsSave)
            {
                LoadData();
            }
        }
    }
}