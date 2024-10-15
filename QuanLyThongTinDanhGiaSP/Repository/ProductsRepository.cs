using Cassandra;
using Newtonsoft.Json.Linq;
using QuanLyThongTinDanhGiaSP.DAL;
using QuanLyThongTinDanhGiaSP.Models;
using QuanLyThongTinDanhGiaSP.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThongTinDanhGiaSP.Repository
{
    public class ProductsRepository : Repository<products>, IProductRepository
    {
        private readonly CassandraContext _context;
        public ProductsRepository(CassandraContext context) : base(context)
        {
            _context = context;
        }

        public products GetProductById(string productId, string categoryId)
        {
            string sql = $"select * from products where product_id = {productId} AND category_id = {categoryId}";
            try
            {
                var rs = _context.executeQuery(sql);
                products product = new products();
                foreach(var item in rs)
                {
                    product.product_id = item.GetValue<Guid>("product_id");
                    product.category_id = item.GetValue<Guid>("category_id");
                    product.category_name = item.GetValue<string>("category_name");
                    product.create_at = item.GetValue<DateTime>("create_at");
                    product.manufacturer = item.GetValue<string>("manufacturer");
                    product.name = item.GetValue<string>("name");
                }
                return product;
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveProduct(string productId, string categoryId)
        {
            string sql = $"delete from products where product_id = {productId} AND category_id = {categoryId}";
            try
            {
                _context.executeQuery(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateProduct(products product)
        {
            string sql = $"update products " +
                $"set category_name='{product.category_name}', manufacturer='{product.manufacturer}', name='{product.name}' " +
                $"where product_id = {product.product_id} AND category_id = {product.category_id}";
            try
            {
                if(GetProductById(product.product_id.ToString(), product.category_id.ToString()) == null)
                {
                    return false;
                }
                _context.executeQuery(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
