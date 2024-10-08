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
    public class CategoriesRepository : Repository<Categories>, ICategoriesRepository
    {
        private readonly CassandraContext _context;
        public CategoriesRepository(CassandraContext context) : base(context)
        {
            _context = new CassandraContext(Utils.KeySpace);
        }
    }
}
