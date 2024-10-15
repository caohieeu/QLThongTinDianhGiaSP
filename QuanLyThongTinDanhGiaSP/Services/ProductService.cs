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
    public class ProductService
    {
        private readonly CassandraContext _cassandraContext;
        private readonly IProductRepository _productRepository;
        public ProductService()
        {
            _cassandraContext = new CassandraContext(Utils.KeySpace);
            _productRepository = new ProductsRepository(_cassandraContext);
        }
        public IEnumerable<products> GetAllProduct()
        {
            return _productRepository.GetAll();
        }
        public products GetProduct(string proId, string cateId)
        {
            return _productRepository.GetProductById(proId, cateId);
        }
        public bool AddProduct(products product)
        {
            return _productRepository.Add(product);
        }
        public bool UpdateProduct(products product)
        {
            return _productRepository.UpdateProduct(product);
        }
        public bool DeleteProduct(string proId, string cateId)
        {
            return _productRepository.RemoveProduct(proId, cateId);
        }
        public List<products> FilterProductByName(string columnName, string name)
        {
            return _productRepository.FilterByName(columnName, name).ToList();
        }
        public List<products> FilterProductByDate(DateTime startDate, DateTime endDate, string date)
        {
            return _productRepository.FilterByDate(startDate, endDate, date).ToList();
        }
    }
}
