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
        public users GetUserById(string id)
        {
            return _userRepository.GetById(id);
        }
        public bool AddUser(users user)
        {
            return _userRepository.Add2(user);
        }
        public bool UpadateUser(users user)
        {
            return _userRepository.Update2(user);
        }
        public bool RemoveUser(string userId)
        {
            return _userRepository.RemoveUser(userId);
        }
        public List<users> FilterUsersByName(string columnName, string name)
        {
            return _userRepository.FilterByName(columnName, name).ToList();
        }
        public List<users> FilterUsersByDate(DateTime startDate, DateTime endDate, string date)
        {
            return _userRepository.FilterByDate(startDate, endDate, date).ToList();
        }
    }
}
