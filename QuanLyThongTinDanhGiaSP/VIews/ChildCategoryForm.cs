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

namespace QuanLyThongTinDanhGiaSP.VIews
{
    public partial class ChildCategoryForm : Form
    {
        private readonly CategoriesService _categoriesService;
        public bool IsSave;
        public string _mode;
        public string _categoryId;
        public ChildCategoryForm(string mode, string categoryId)
        {
            InitializeComponent();
            _categoriesService = new CategoriesService();
            _categoryId = categoryId;
            _mode = mode;
            LoadButton();
            if (mode == "edit")
            {
                LoadInfor();
            }
        }
        void LoadInfor()
        {
            Categories categorie = _categoriesService.GetById(_categoryId);
            txtName.Text = categorie.name;
        }
        public void LoadButton()
        {
            if (_mode == "add")
            {
                btnDelete.Visible = false;
            }
        }
        void executeAdd()
        {
            string textName = txtName.Text;

            if (_categoriesService.AddCategory(new Categories()
            {
                category_id = Guid.NewGuid(),
                name = textName,
                create_at = DateTime.Now,
            }))
            {
                MessageBox.Show("Add category successfully");
                this.Close();
                IsSave = true;
            }
            else
            {
                MessageBox.Show("Add category failed");
            }
        }
        void executeUpdate()
        {
            Categories categorie = new Categories()
            {
                category_id = Guid.Parse(_categoryId),
                name = txtName.Text,
                create_at = DateTime.Now,
            };
            if (_categoriesService.UpdateCategory(categorie))
            {
                MessageBox.Show("Update successfully");
                this.Close();
                IsSave = true;
            }
            else
            {
                MessageBox.Show("Update fail");
            }
        }
        void executeDelete()
        {
            if (_categoriesService.DeleteCategory(_categoryId))
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_mode == "add")
            {
                executeAdd();
            }
            else
            {
                executeUpdate();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            executeDelete();
        }
    }
}
