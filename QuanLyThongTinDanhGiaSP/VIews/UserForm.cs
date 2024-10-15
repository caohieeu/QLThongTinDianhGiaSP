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
        }
        public void LoadData()
        {
            dataGridView1.DataSource = _usersService.GetAllUser();
        }
    }
}
