using QuanLyThongTinDanhGiaSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThongTinDanhGiaSP.Repository.IRepository
{
    public interface IProductReviewsReponsitory : IRepository<Product_Review>
    {
        IEnumerable<Product_Review> GetProductReviews(Guid productId);
        bool RemoveProduct(string productId, string categoryId);
        bool UpdateReviewReply(Guid productId, Guid reviewId, string reply);
    }
}
