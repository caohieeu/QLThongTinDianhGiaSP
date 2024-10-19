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
            this.btn_Loc.Click += Btn_Loc_Click;
            txt_Search.TextChanged += Txt_Search_TextChanged;
            LoadCategoriesIntoComboBox();
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

        private void LoadCategoriesIntoComboBox()
        {
            var allCategories = _categoriesService.GetAllCategory();

            var categoryNames = new List<string> { "" };

            categoryNames.AddRange(allCategories.Select(c => c.name).ToList());

            cbb_NameCategories.DataSource = categoryNames;

            cbb_NameCategories.SelectedIndex = 0;

        }
        private void Btn_Loc_Click(object sender, EventArgs e)
        {
            string selectedCategoryName = cbb_NameCategories.SelectedItem?.ToString(); 
            DateTime selectedDate_Start = dateTime_start.Value;
            DateTime selectedDate_End = dateTime_end.Value;

            IEnumerable<Categories> filteredCategories;

            if (!string.IsNullOrWhiteSpace(selectedCategoryName))
            {
                filteredCategories = _categoriesService.FilterCategoriesByName("name",selectedCategoryName);

                if (dateTime_start.Value != null && dateTime_end.Value != null)
                {
                    filteredCategories = filteredCategories
                        .Where(category => category.create_at >= selectedDate_Start && category.create_at <= selectedDate_End);
                }
            }
            else if (dateTime_start.Value != null && dateTime_end.Value != null)
            {
                filteredCategories = _categoriesService.FilterCategoriesByDate(selectedDate_Start, selectedDate_End, "create_at");
            }
            else
            {
              
                filteredCategories = _categoriesService.GetAllCategory();
            }

            dataGridView1.DataSource = filteredCategories.ToList();
        }
        private void Txt_Search_TextChanged(object sender, EventArgs e)
        {
            string inputText = txt_Search.Text.Trim();

            if (!string.IsNullOrEmpty(inputText))
            {

                var filteredCategories = _categoriesService.FilterCategoriesByName("name", inputText);

                dataGridView1.DataSource = filteredCategories.ToList();
            }
            else
            {

                dataGridView1.DataSource = _categoriesService.GetAllCategory();
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            ChildCategoryForm frm = new ChildCategoryForm("add", null);
            frm.ShowDialog();
            if (frm.IsSave)
            {
                LoadData();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var categoryId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            ChildCategoryForm frm = new ChildCategoryForm("edit", categoryId);
            frm.ShowDialog();
            if (frm.IsSave)
            {
                LoadData();
            }
        }
    }
}
