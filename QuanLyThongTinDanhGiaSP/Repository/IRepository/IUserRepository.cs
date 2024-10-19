using QuanLyThongTinDanhGiaSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThongTinDanhGiaSP.Repository.IRepository
{
    public interface IUserRepository : IRepository<users>
    {
        bool UpdateUser(users user);
        bool RemoveUser(string userId);
    }
}
