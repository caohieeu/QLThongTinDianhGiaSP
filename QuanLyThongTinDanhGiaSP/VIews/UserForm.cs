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
        public UserForm()
        {
            InitializeComponent();
            _usersService = new UsersService();
            LoadData();
            LoadUsersIntoComboBox();
            this.btn_Loc.Click += Btn_Loc_Click;
        }

       
        public void LoadData()
        {
            dataGridView1.DataSource = _usersService.GetAllUser();
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
            string selectedUsersName = cbb_NameUsers.SelectedItem?.ToString();
            DateTime selectedDate_Start = dateTime_start.Value;
            DateTime selectedDate_End = dateTime_end.Value;

            IEnumerable<users> filteredUsers;

            if (!string.IsNullOrWhiteSpace(selectedUsersName))
            {
                filteredUsers = _usersService.FilterUsersByName("username",selectedUsersName);

                if (dateTime_start.Value != null && dateTime_end.Value != null)
                {
                    filteredUsers = filteredUsers
                        .Where(category => category.dob >= selectedDate_Start && category.dob <= selectedDate_End);
                }
            }
            else if (dateTime_start.Value != null && dateTime_end.Value != null)
            {
                filteredUsers = _usersService.FilterUsersByDate(selectedDate_Start, selectedDate_End, "dob");
            }
            else
            {

                filteredUsers = _usersService.GetAllUser();
            }

            dataGridView1.DataSource = filteredUsers.ToList();
        }

    }
}
