using QuanLyThongTinDanhGiaSP.VIews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThongTinDanhGiaSP
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm();
            categoryForm.TopLevel = false;
            categoryForm.FormBorderStyle = FormBorderStyle.None;

            categoryForm.Size = new Size(panel1.Width, panel1.Height);

            panel1.Controls.Clear(); 
            panel1.Controls.Add(categoryForm); 
            categoryForm.Show();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            productForm.TopLevel = false;
            productForm.FormBorderStyle = FormBorderStyle.None;

            productForm.Size = new Size(panel1.Width, panel1.Height);

            panel1.Controls.Clear();
            panel1.Controls.Add(productForm);
            productForm.Show();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm();
            userForm.TopLevel = false;
            userForm.FormBorderStyle = FormBorderStyle.None;

            userForm.Size = new Size(panel1.Width, panel1.Height);

            panel1.Controls.Clear();
            panel1.Controls.Add(userForm);
            userForm.Show();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    ReviewForm frm = new UserForm();
        //    frm.TopLevel = false;
        //    frm.FormBorderStyle = FormBorderStyle.None;

        //    frm.Size = new Size(panel1.Width, panel1.Height);

        //    panel1.Controls.Clear();
        //    panel1.Controls.Add(frm);
        //    frm.Show();
        //}
    }
}
