using QuanLyThongTinDanhGiaSP.Services;
using System;
using System.Windows.Forms;
using QuanLyThongTinDanhGiaSP.Models;
using System.Runtime.InteropServices;
using System.Linq;

namespace QuanLyThongTinDanhGiaSP.VIews
{
    public partial class ChildProductForm : Form
    {
        private readonly ProductService _productService;
        private readonly CategoriesService _categoriesService;
        public bool IsSave;
        public string _mode;
        public string _productId;
        public string _categoryId;
        public ChildProductForm(string mode, string productId, string categoryId)
        {
            InitializeComponent();
            _mode = mode;
            _productId = productId;
            _categoryId = categoryId;
            _productService = new ProductService();
            _categoriesService = new CategoriesService();

            LoadCombobox();
            LoadButton();
            if(mode == "edit")
            {
                LoadInfor();
            }
            IsSave = false;
        }

        public void LoadCombobox()
        {
            cbCategory.Items.Clear();
            cbCategory.DataSource = _categoriesService.GetAllCategory();
            cbCategory.DisplayMember = "name";
            cbCategory.ValueMember = "category_id";
        }

        public void LoadButton()
        {
            if(_mode == "add")
            {
                btnDelete.Visible = false;
            }
        }
        void LoadInfor()
        {
            products product = _productService.GetProduct(_productId, _categoryId);
            txtName.Text = product.name;
            txtManufacturer.Text = product.manufacturer;
            cbCategory.SelectedItem = cbCategory.Items
                .Cast<Categories>()
                .FirstOrDefault(c => c.name == product.category_name);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(_mode == "add")
            {
                executeAdd();
            }
            else
            {
                executeUpdate();
            }
        }

        void executeAdd()
        {
            string textName = txtName.Text;
            string textManufacturer = txtManufacturer.Text;
            string textIdCategory = cbCategory.SelectedValue.ToString();
            var obj = (Categories)cbCategory.SelectedItem;
            string textNameCategory = obj.name;

            if (_productService.AddProduct(new products()
            {
                product_id = Guid.NewGuid(),
                name = textName,
                manufacturer = textManufacturer,
                category_id = Guid.Parse(textIdCategory),
                create_at = DateTime.Now,
                category_name = textNameCategory,
            }))
            {
                MessageBox.Show("Add product successfully");
                this.Close();
                IsSave = true;
            }
            else
            {
                MessageBox.Show("Add product failed");
            }
        }
        void executeUpdate()
        {
            products product = new products()
            {
                product_id = Guid.Parse(_productId),
                name = txtName.Text,
                manufacturer = txtManufacturer.Text,
                category_id = Guid.Parse(_categoryId),
                create_at = DateTime.Now,
                category_name = ((Categories)cbCategory.SelectedItem).name,
            };
            if(_productService.UpdateProduct(product)) {
                MessageBox.Show("Update successfully");
                this.Close();
                IsSave=true;
            }
            else
            {
                MessageBox.Show("Update fail");
            }
        }
        void executeDelete()
        {
            if(_productService.DeleteProduct(_productId, _categoryId))
            {
                MessageBox.Show("Delete Successfully");
                this.Close();
                IsSave = true;
            }
            else
            {
                MessageBox.Show("Delete fail");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            executeDelete();
        }
    }
}
