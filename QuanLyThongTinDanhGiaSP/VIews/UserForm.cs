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
    public partial class UserForm : Form
    {
        private readonly UsersService _usersService;
        public bool IsSave;
        public string _mode;
        public string _productId;
        public string _categoryId;
        public UserForm()
        {
            InitializeComponent();
            _usersService = new UsersService();
            dateTime_start.Format = DateTimePickerFormat.Custom;
            dateTime_start.CustomFormat = "dd/MM/yyyy";
            dateTime_end.Format = DateTimePickerFormat.Custom;
            dateTime_end.CustomFormat = "dd/MM/yyyy";
            LoadData();
            LoadUsersIntoComboBox();
            this.btn_Loc.Click += Btn_Loc_Click;
            DisplayUsers(_usersService.GetAllUser());
        }

        private void DisplayUsers(IEnumerable<users> users)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.AutoScroll = true;

            foreach (var user in users)
            {
                UserItemControl userControl = new UserItemControl(user);
                userControl.EditUserRequested += (sender, userId) =>
                {
                    OpenEditUserForm(userId);
                };
                flowLayoutPanel1.Controls.Add(userControl);
            }

            flowLayoutPanel1.Refresh();
        }

        private void OpenEditUserForm(Guid userId)
        {
            ChildUserForm frm = new ChildUserForm("edit", userId.ToString());
            frm.ShowDialog();
            if (frm.IsSave)
            {
                LoadData();
            }
        }


        public void LoadData()
        {
        }
        private void LoadUsersIntoComboBox()
        {
            var allUsers = _usersService.GetAllUser();

            var userNames = new List<string> { "" };

            userNames.AddRange(allUsers.Select(c => c.username).ToList());

            cbb_NameUsers.DataSource = userNames;

            cbb_NameUsers.SelectedIndex = 0;
        }
        private void Btn_Loc_Click(object sender, EventArgs e)
        {
            string selectedUser = cbb_NameUsers.SelectedItem?.ToString(); 
            DateTime selectedDate_Start = dateTime_start.Value;
            DateTime selectedDate_End = dateTime_end.Value;

            IEnumerable<users> filteredUsers;

            // Lọc theo vai trò
            if (!string.IsNullOrWhiteSpace(selectedUser))
            {
                filteredUsers = _usersService.FilterUsersByName("username", selectedUser).ToList();

                if (dateTime_start.Value != null && dateTime_end.Value != null)
                {
                    filteredUsers = filteredUsers
                        .Where(user => user.dob >= selectedDate_Start && user.dob <= selectedDate_End)
                        .ToList();
                }
            }
            // Nếu chỉ lọc theo ngày
            else if (dateTime_start.Value != null && dateTime_end.Value != null)
            {
                filteredUsers = _usersService.FilterUsersByDate(selectedDate_Start, selectedDate_End, "dob").ToList();
            }
            else
            {
                filteredUsers = _usersService.GetAllUser();
            }

            DisplayUsers(filteredUsers);
        }


        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            ChildUserForm frm = new ChildUserForm("add", null);
            frm.ShowDialog();
            if (frm.IsSave)
            {
                LoadData();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            string inputText = txt_Search.Text.Trim();

            if (!string.IsNullOrEmpty(inputText))
            {
                var filteredUsers = _usersService.FilterUsersByName("username", inputText).ToList();

                DisplayUsers(filteredUsers);
            }
            else
            {
                var allUsers = _usersService.GetAllUser();
                DisplayUsers(allUsers);
            }
        }
    }
}
