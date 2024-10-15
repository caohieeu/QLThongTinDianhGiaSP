using QuanLyThongTinDanhGiaSP.DAL;
using QuanLyThongTinDanhGiaSP.Models;
using QuanLyThongTinDanhGiaSP.Repository.IRepository;
using QuanLyThongTinDanhGiaSP.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThongTinDanhGiaSP.Services
{
    public class UsersService
    {
        private readonly CassandraContext _cassandraContext;
        private readonly IUserRepository _userRepository;
        public UsersService()
        {
            _cassandraContext = new CassandraContext(Utils.KeySpace);
            _userRepository = new UsersRepository(_cassandraContext);
        }
        public IEnumerable<users> GetAllUser ()
        {
            return _userRepository.GetAll();
        }
    }
}
