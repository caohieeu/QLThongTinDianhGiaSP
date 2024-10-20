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
            ResizeImage();
        }

        private void ResizeImage()
        {
            pictureBox1.BackColor = Color.Transparent;

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Size = new Size(150, 100);

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            if (pictureBox1.Image != null)
            {
                Image originalImage = pictureBox1.Image;
                Image resizedImage = new Bitmap(originalImage, new Size(150, 100));
                pictureBox1.Image = resizedImage;
                originalImage.Dispose();
            }


            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Size = new Size(636, 460);

            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            if (pictureBox2.Image != null)
            {
                Image originalImage = pictureBox2.Image;
                Image resizedImage = new Bitmap(originalImage, new Size(636, 460));
                pictureBox2.Image = resizedImage;
                originalImage.Dispose();
            }
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
            MainReviewForm productForm = new MainReviewForm();
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

        private void btnSProduct_Click(object sender, EventArgs e)
        {
            ProductForm frm = new ProductForm();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;

            frm.Size = new Size(panel1.Width, panel1.Height);

            panel1.Controls.Clear();
            panel1.Controls.Add(frm);
            frm.Show();
        }

        public void btnSProduct_Click()
        {
            ProductForm frm = new ProductForm();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;

            frm.Size = new Size(panel1.Width, panel1.Height);

            panel1.Controls.Clear();
            panel1.Controls.Add(frm);
            frm.Show();
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            AnalyticsForm frm = new AnalyticsForm();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;

            frm.Size = new Size(panel1.Width, panel1.Height);

            panel1.Controls.Clear();
            panel1.Controls.Add(frm);
            frm.Show();
        }
    }
}
