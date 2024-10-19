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
using System.Xml.Linq;

namespace QuanLyThongTinDanhGiaSP.VIews
{
    public partial class ChildUserForm : Form
    {
        private readonly UsersService _usersService;
        public bool IsSave;
        public string _mode;
        public string _userId;
        public ChildUserForm(string mode, string userId)
        {
            InitializeComponent();
            _mode = mode;
            _userId = userId;
            _usersService = new UsersService();
            if (mode == "edit")
            {
                LoadInfor();
            }
            LoadButton();
            IsSave = false;
        }
        void LoadInfor()
        {
            users user = _usersService.GetUserById(_userId);
            txtUsername.Text = user.username;
            txtEmail.Text = user.email;
            dtpDob.Value = user.dob;
        }
        public void LoadButton()
        {
            if (_mode == "add")
            {
                btnDelete.Visible = false;
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
        void executeAdd()
        {
            string textUsername = txtUsername.Text;
            string textEmail = txtEmail.Text;
            DateTime textDob = dtpDob.Value;

            if (_usersService.AddUser(new users()
            {
                user_id = Guid.NewGuid(),
                username = textUsername,
                email = textEmail,
                dob = textDob
            }))
            {
                MessageBox.Show("Add user successfully");
                this.Close();
                IsSave = true;
            }
            else
            {
                MessageBox.Show("Add user failed");
            }
        }
        void executeUpdate()
        {
            users user = new users()
            {
                user_id = Guid.Parse(_userId),
                username = txtUsername.Text,
                email = txtEmail.Text,
                dob = dtpDob.Value
            };
            if (_usersService.UpadateUser(user))
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
            if (_usersService.RemoveUser(_userId))
            {
                MessageBox.Show("Delete Successfully");
                this.Close();
                IsSave = true;
            }
            else
            {
                MessageBox.Show("Delete failed");
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            executeDelete();
        }
    }
}
