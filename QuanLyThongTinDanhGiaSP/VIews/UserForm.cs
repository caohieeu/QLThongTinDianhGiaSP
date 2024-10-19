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
            var userId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            ChildUserForm frm = new ChildUserForm("edit", userId);
            frm.ShowDialog();
            if (frm.IsSave)
            {
                LoadData();
            }
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            string inputText = txt_Search.Text.Trim();

            if (!string.IsNullOrEmpty(inputText))
            {

                var filteredCategories = _usersService.FilterUsersByName("username", inputText);

                dataGridView1.DataSource = filteredCategories.ToList();
            }
            else
            {

                dataGridView1.DataSource = _usersService.GetAllUser();
            }
        }
    }
}
