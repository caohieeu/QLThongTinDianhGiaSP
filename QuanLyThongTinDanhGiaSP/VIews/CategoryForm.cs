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
            dateTime_start.Format = DateTimePickerFormat.Custom;
            dateTime_start.CustomFormat = "dd/MM/yyyy";
            dateTime_end.Format = DateTimePickerFormat.Custom;
            dateTime_end.CustomFormat = "dd/MM/yyyy";
           
            GetById();
            this.btn_Loc.Click += Btn_Loc_Click;
            txt_Search.TextChanged += Txt_Search_TextChanged;
            LoadCategoriesIntoComboBox();

            DisplayCategories(_categoriesService.GetAllCategory());

        }

        private void DisplayCategories(List<Categories> categories)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.AutoScroll = true;
            foreach (var category in categories)
            {
                CategoryItemControl categoryControl = new CategoryItemControl(category);
                categoryControl.Size = new Size(flowLayoutPanel1.Width - 40, 100); 
                flowLayoutPanel1.Controls.Add(categoryControl);
            }
            flowLayoutPanel1.Refresh();
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

            List<Categories> filteredCategories;

            if (!string.IsNullOrWhiteSpace(selectedCategoryName))
            {
                filteredCategories = _categoriesService.FilterCategoriesByName("name", selectedCategoryName).ToList();

                if (dateTime_start.Value != null && dateTime_end.Value != null)
                {
                    filteredCategories = filteredCategories
                        .Where(category => category.create_at >= selectedDate_Start && category.create_at <= selectedDate_End)
                        .ToList();
                }
            }
            else if (dateTime_start.Value != null && dateTime_end.Value != null)
            {
                filteredCategories = _categoriesService.FilterCategoriesByDate(selectedDate_Start, selectedDate_End, "create_at").ToList();
            }
            else
            {
                filteredCategories = _categoriesService.GetAllCategory().ToList();
            }
            DisplayCategories(filteredCategories);
        }


        private void Txt_Search_TextChanged(object sender, EventArgs e)
        {
            string inputText = txt_Search.Text.Trim();

            if (!string.IsNullOrEmpty(inputText))
            {
                var filteredCategories = _categoriesService.FilterCategoriesByName("name", inputText).ToList();

                DisplayCategories(filteredCategories);
            }
            else
            {             
                var allCategories = _categoriesService.GetAllCategory().ToList();
                DisplayCategories(allCategories);
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            ChildCategoryForm frm = new ChildCategoryForm("add", null);
            frm.ShowDialog();
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
