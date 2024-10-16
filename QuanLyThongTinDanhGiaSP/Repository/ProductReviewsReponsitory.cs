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
    public class ProductReviewsReponsitory : Repository<Product_Review>, IProductReviewsReponsitory
    {
        private readonly CassandraContext _context;
       
        public ProductReviewsReponsitory(CassandraContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Product_Review> GetProductReviews(Guid productId)
        {
            string query = $"SELECT * FROM product_reviews WHERE product_id = {productId}";

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

                        // Xử lý purchase_date
                        var purchaseDateValue = row.GetValue<object>("purchase_date");
                        if (purchaseDateValue is Cassandra.LocalDate localDate)
                        {
                            // Chuyển đổi LocalDate thành DateTime
                            a.PurchaseDate = new DateTime(localDate.Year, localDate.Month, localDate.Day);
                        }
                        else
                        {
                            a.PurchaseDate = (DateTime?)null; // Hoặc xử lý khác nếu cần
                        }

                        a.Status = row.GetValue<string>("status");
                        a.ConfirmText = row.GetValue<string>("confirm_text");
                        a.IsConfirm = row.GetValue<bool>("is_confirm");

                        // Xử lý confirm_date
                        var confirmDateValue = row.GetValue<object>("confirm_date");
                        if (confirmDateValue is Cassandra.LocalDate confirmLocalDate)
                        {
                            // Chuyển đổi LocalDate thành DateTime
                            a.ConfirmDate = new DateTime(confirmLocalDate.Year, confirmLocalDate.Month, confirmLocalDate.Day);
                        }
                        else
                        {
                            a.ConfirmDate = (DateTime?)null; // Hoặc xử lý khác nếu cần
                        }
                    }
                    catch (InvalidCastException ex)
                    {
                        // Log the error
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

        public bool RemoveProduct(string productId, string categoryId)
        {
            throw new NotImplementedException();
        }

        public void ReplyReview(string reply, string idReview)
        {
            throw new NotImplementedException();
        }
    }
}
