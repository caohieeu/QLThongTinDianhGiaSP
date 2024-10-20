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

        public bool UpdateReviewReply(Guid productId, Guid reviewId, string reply)
        {
            string query = $@"
                    UPDATE product_reviews 
                    SET confirm_text = '{reply}', 
                        is_confirm = true, 
                        confirm_date = toDate(now()) 
                    WHERE review_id = {reviewId} AND product_id = {productId}";
            try
            {
                // Sử dụng ExecuteQuery với parameters để tránh SQL injection
                _context.executeQuery(query);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating review reply: {ex.Message}");
                return false;
            }
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
                        if (!row.IsNull("confirm_text"))
                        {
                            a.ConfirmText = row.GetValue<string>("confirm_text");
                        }

                        // Xử lý is_confirm
                        if (!row.IsNull("is_confirm"))
                        {
                            a.IsConfirm = row.GetValue<bool>("is_confirm");
                        }

                        // Xử lý confirm_date
                        if (!row.IsNull("confirm_date"))
                        {
                            var confirmDateValue = row.GetValue<object>("confirm_date");
                            if (confirmDateValue is Cassandra.LocalDate confirmLocalDate)
                            {
                                // Chuyển đổi LocalDate thành DateTime
                                a.ConfirmDate = new DateTime(confirmLocalDate.Year, confirmLocalDate.Month, confirmLocalDate.Day);
                            }
                            else if (confirmDateValue is DateTime confirmDateTime)
                            {
                                a.ConfirmDate = confirmDateTime;
                            }
                            else
                            {
                                a.ConfirmDate = null; // Hoặc xử lý khác nếu cần
                            }
                        }
                        else
                        {
                            a.ConfirmDate = null;
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

        public IEnumerable<Product_Review> GetProductReviews()
        {
            string query = $"SELECT * FROM product_reviews";

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

        public IEnumerable<Product_Review> GetProductReviewsByUsername(string username)
        {
            string query = $"SELECT * FROM product_reviews WHERE username = '{username}' ALLOW FILTERING";

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
        public bool RemoveProduct(string productId, string categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
