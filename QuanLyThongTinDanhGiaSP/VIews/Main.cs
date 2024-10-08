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

            panel1.Controls.Clear(); 
            panel1.Controls.Add(categoryForm); 
            categoryForm.Show();
        }
    }
}
