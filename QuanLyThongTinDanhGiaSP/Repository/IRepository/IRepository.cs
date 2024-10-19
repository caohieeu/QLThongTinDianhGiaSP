using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThongTinDanhGiaSP.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        bool Add(T entity);
        bool Add2(T entity);
        bool Remove(string id);
        bool Update(T entity);
        bool Update2(T entity);
        IEnumerable<T> FilterByDate(DateTime startDate, DateTime endDate,string date);
        IEnumerable<T> FilterByName(string columnName, string name);
        IEnumerable<string> SearchSuggestions(string columnName, string inputValue);
    }
}
