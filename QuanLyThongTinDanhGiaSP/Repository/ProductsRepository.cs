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

        public IEnumerable<Product_Review> GetProductReviewsByUsername(string username)
        {
            string query = $"SELECT * FROM product_reviews WHERE username = '{username}'";

            try
            {
                var resultSet = _context.executeQuery(query);
                var reviews = new List<Product_Review>();

                foreach (var row in resultSet)
                {
                    Product_Review a = new Product_Review();

                    try
                    {
                        a.ProductId = row.GetValue<Guid>("product_id");
                        a.ReviewId = row.GetValue<Guid>("review_id");
                        a.UserId = row.GetValue<Guid>("user_id");
                        a.Username = row.GetValue<string>("username");
                        a.Rating = row.GetValue<decimal>("rating");
                        a.ReviewText = row.GetValue<string>("review_text");

                        var purchaseDateValue = row.GetValue<object>("purchase_date");
                        if (purchaseDateValue is Cassandra.LocalDate localDate)
                        {
                            a.PurchaseDate = new DateTime(localDate.Year, localDate.Month, localDate.Day);
                        }
                        else
                        {
                            a.PurchaseDate = (DateTime?)null;
                        }

                        a.Status = row.GetValue<string>("status");
                        if (!row.IsNull("confirm_text"))
                        {
                            a.ConfirmText = row.GetValue<string>("confirm_text");
                        }

                        if (!row.IsNull("is_confirm"))
                        {
                            a.IsConfirm = row.GetValue<bool>("is_confirm");
                        }

                        if (!row.IsNull("confirm_date"))
                        {
                            var confirmDateValue = row.GetValue<object>("confirm_date");
                            if (confirmDateValue is Cassandra.LocalDate confirmLocalDate)
                            {
                                a.ConfirmDate = new DateTime(confirmLocalDate.Year, confirmLocalDate.Month, confirmLocalDate.Day);
                            }
                            else if (confirmDateValue is DateTime confirmDateTime)
                            {
                                a.ConfirmDate = confirmDateTime;
                            }
                            else
                            {
                                a.ConfirmDate = null;
                            }
                        }
                        else
                        {
                            a.ConfirmDate = null;
                        }
                    }
                    catch (InvalidCastException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }

                    reviews.Add(a);
                }

                return reviews;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
                return new List<Product_Review>();
            }
        }
    }
}
