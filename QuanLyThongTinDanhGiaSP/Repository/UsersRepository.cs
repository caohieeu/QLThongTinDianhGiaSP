using QuanLyThongTinDanhGiaSP.DAL;
using QuanLyThongTinDanhGiaSP.Models;
using QuanLyThongTinDanhGiaSP.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThongTinDanhGiaSP.Repository
{
    public class UsersRepository : Repository<users>, IUserRepository
    {
        private readonly CassandraContext _context;
        public UsersRepository(CassandraContext context) : base(context)
        {
            _context = context;
        }
        public bool RemoveUser(string userId)
        {
            string sql = $"delete from users where user_id = {userId}";
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
        public bool UpdateUser(users user)
        {
            string sql = $"update users " +
                $"set username='{user.username}', email='{user.email}', dob='{user.dob}' " +
                $"where user_id = {user.user_id}";
            try
            {
                if (GetById(user.user_id.ToString()) == null)
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
