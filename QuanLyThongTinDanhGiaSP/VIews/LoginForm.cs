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
using QuanLyThongTinDanhGiaSP.DAL;
using QuanLyThongTinDanhGiaSP.Repository;
using QuanLyThongTinDanhGiaSP.Repository.IRepository;

namespace QuanLyThongTinDanhGiaSP.VIews
{
    public partial class LoginForm : Form
    {
        private readonly CassandraContext _cassandraContext = new CassandraContext(Utils.KeySpace);
        public LoginForm()
        {
            InitializeComponent();
        }

        private void checkViewPassWord_CheckedChanged(object sender, EventArgs e)
        {
            if (checkViewPassWord.Checked)
            {
                txtPassWoud.PasswordChar = '\0';
            }
            else
            {
                txtPassWoud.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassWoud.Text))
                {
                    MessageBox.Show("Thông tin tài khoản hoặc mật khẩu không được để trống !");
                    return;
                }

                string query = $"SELECT * FROM users WHERE username = '{txtUserName.Text}' ALLOW FILTERING";
                var result = _cassandraContext.executeQuery(query).FirstOrDefault();

                if (result != null)
                {
                    string storedPassword = result.GetValue<string>("password");
                    if (storedPassword == txtPassWoud.Text)
                    {
                        Main a = new Main();
                        a.Show();
                        this.Hide();
                        a.BringToFront();
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu không chính xác hãy nhập lại !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản không tồn tại !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
    }
}
