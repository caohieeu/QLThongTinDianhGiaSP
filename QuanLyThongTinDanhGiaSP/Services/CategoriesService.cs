using QuanLyThongTinDanhGiaSP.DAL;
using QuanLyThongTinDanhGiaSP.Models;
using QuanLyThongTinDanhGiaSP.Repository;
using QuanLyThongTinDanhGiaSP.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThongTinDanhGiaSP.Services
{
    public class CategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = new CategoriesRepository(new CassandraContext(Utils.KeySpace));
        }
        public List<Categories> GetAllCategory()
        {
            return _categoriesRepository.GetAll().ToList();
        }
        public Categories GetById(string id)
        {
            return _categoriesRepository.GetById(id);
        }
        public bool AddCategory(Categories category)
        {
            return _categoriesRepository.Add(category);
        }
        public bool DeleteCategory(string id)
        {
            return _categoriesRepository.Remove(id);
        }
        public bool UpdateCategory(Categories category)
        {
            return _categoriesRepository.Update(category);
        }
    }
}
