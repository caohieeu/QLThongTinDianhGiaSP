using QuanLyThongTinDanhGiaSP.Models;
using QuanLyThongTinDanhGiaSP.Repository;
using QuanLyThongTinDanhGiaSP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThongTinDanhGiaSP.VIews
{
    public partial class CategoryForm : Form
    {
        private readonly CategoriesService _categoriesService;
        public CategoryForm()
        {
            InitializeComponent();
            _categoriesService = new CategoriesService(new CategoriesRepository(new DAL.CassandraContext(Utils.KeySpace)));
            LoadData();
            //AddData();
            //DeleteData();
            //UpdateData();
            GetById();
        }
        void LoadData()
        {
            dataGridView1.DataSource = _categoriesService.GetAllCategory();
        }
        void AddData()
        {
            _categoriesService.AddCategory(new Categories()
            {
                category_id = Guid.NewGuid(),
                name = "category test",
                create_at = DateTime.Now,
            });
        }
        void DeleteData()
        {
            _categoriesService.DeleteCategory("67260eac-ab6f-49ea-9c71-df948c9a7693");
        }
        void UpdateData()
        {
            Categories categorie = new Categories()
            {
                category_id = new Guid("4687fdd5-2189-4b1f-8477-85a40361c37f"),
                name = "caohieeu",
                create_at = DateTime.Now,
            };
            _categoriesService.UpdateCategory(categorie);
        }
        void GetById()
        {
            var a = _categoriesService.GetById("4687fdd5-2189-4b1f-8477-85a40361c37f");
            var b = 1;  
        }
    }
}
