using QuanLyThongTinDanhGiaSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuanLyThongTinDanhGiaSP.Repository.IRepository
{
    public interface IProductRepository : IRepository<products>
    {
        products GetProductById(string productId, string categoryId);
        bool UpdateProduct(products product);
        bool RemoveProduct(string productId, string categoryId);
    }
}
