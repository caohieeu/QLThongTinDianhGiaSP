using Cassandra;
using Microsoft.SqlServer.Server;
using QuanLyThongTinDanhGiaSP.DAL;
using QuanLyThongTinDanhGiaSP.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThongTinDanhGiaSP.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CassandraContext _context;
        public Repository(CassandraContext context)
        {
            _context = new CassandraContext(Utils.KeySpace);
        }
        public bool Add(T entity)
        {
            string typeName = typeof(T).Name.ToLower();
            var columns = string.Join(", ", typeof(T).GetProperties().Select(x => x.Name.ToLower()));
            var values = typeof(T).GetProperties()
                .Select(prop =>
                {
                    var value = prop.GetValue(entity);
                    if (value is Guid || prop.PropertyType == typeof(Guid))
                        return "uuid()";
                    else if (value is DateTime || prop.PropertyType == typeof(DateTime))
                        return $"'{((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss")}'";
                    else
                        return value != null ? $"'{value}'" : "null";
                });
            var valueString = string.Join(", ", values);
            string query = $"INSERT INTO {typeName} ({columns}) VALUES ({valueString})";
            try
            {
                _context.executeQuery(query);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public IEnumerable<T> GetAll()
        {
            string typeName = typeof(T).Name.ToLower();
            string sql = $"SELECT * FROM {typeName}";
            var result = _context.executeQuery(sql);
            
            var list = new List<T>();

            foreach(var row in  result)
            {
                var instance = Activator.CreateInstance<T>();
                foreach (var prop in typeof(T).GetProperties())
                {
                    var value = row.GetValue<object>(prop.Name.ToLower());
                    if(value != null)
                    {
                        if (prop.PropertyType == typeof(string))
                        {
                            prop.SetValue(instance, value.ToString());
                        }
                        else if (prop.PropertyType == typeof(Guid))
                        {
                            prop.SetValue(instance, Guid.Parse(value.ToString()));
                        }
                        else if (prop.PropertyType == typeof(DateTime))
                        {
                            if (DateTime.TryParse(value.ToString(), out DateTime dateValue))
                            {
                                prop.SetValue(instance, dateValue);
                            }
                        }
                    }
                }
                list.Add(instance);
            }
            return list;
        }

        public T GetById(string id)
        {
            var instance = Activator.CreateInstance<T>();
            string idName = typeof(T).GetProperties()[0].Name.ToLower();

            string typeName = typeof(T).Name.ToLower();
            string sql = $"SELECT * FROM {typeName} WHERE {idName}={id}";
            var result = _context.executeQuery(sql);

            foreach(var row in result)
            {
                foreach (var prop in typeof(T).GetProperties())
                {
                    var value = row.GetValue<object>(prop.Name.ToLower());
                    if (value != null)
                    {
                        if (prop.PropertyType == typeof(string))
                        {
                            prop.SetValue(instance, value.ToString());
                        }
                        else if (prop.PropertyType == typeof(Guid))
                        {
                            prop.SetValue(instance, Guid.Parse(value.ToString()));
                        }
                        else if (prop.PropertyType == typeof(DateTime))
                        {
                            if (DateTime.TryParse(value.ToString(), out DateTime dateValue))
                            {
                                prop.SetValue(instance, dateValue);
                            }
                        }
                    }
                }
            }

            return instance;
        }

        public bool Update(T entity)
        {
            var instance = Activator.CreateInstance<T>();
            string idName = typeof(T).GetProperties()[0].Name.ToLower();
            var idValue = typeof(T).GetProperties()[0].GetValue(entity);

            string typeName = typeof(T).Name.ToLower();
            var condition = string.Join(", ", typeof(T).GetProperties().Skip(1).Select(prop =>
            {
                var value = prop.GetValue(entity);
                if (value is DateTime || prop.PropertyType == typeof(DateTime))
                    value = $"{((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss")}";
                return value != null ? $"{prop.Name.ToLower()} = '{value}'" : $"{prop.Name.ToLower()} = null";
            }));
            try
            {
                string sql = $"UPDATE {typeName} SET {condition} WHERE {idName}={idValue}";
                var result = _context.executeQuery(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Remove(string id)
        {
            try
            {
                string idName = typeof(T).GetProperties()[0].Name.ToLower();
                string typeName = typeof(T).Name.ToLower();
                string sql = $"DELETE FROM {typeName} WHERE {idName}={id}";
                var result = _context.executeQuery(sql);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public IEnumerable<T> FilterByName(string columnName,string name)
        {
            string typeName = typeof(T).Name.ToLower();

            string query = $"SELECT * FROM {typeName} WHERE {columnName} = '{name}' ALLOW FILTERING";

            try
            {
                var result = _context.executeQuery(query);
                var list = new List<T>();

                foreach (var row in result)
                {
                    var instance = Activator.CreateInstance<T>();
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        var value = row.GetValue<object>(prop.Name.ToLower());
                        if (value != null)
                        {
                            if (prop.PropertyType == typeof(string))
                            {
                                prop.SetValue(instance, value.ToString());
                            }
                            else if (prop.PropertyType == typeof(Guid))
                            {
                                prop.SetValue(instance, Guid.Parse(value.ToString()));
                            }
                            else if (prop.PropertyType == typeof(DateTime))
                            {
                                if (DateTime.TryParse(value.ToString(), out DateTime dateValue))
                                {
                                    prop.SetValue(instance, dateValue);
                                }
                            }
                        }
                    }
                    list.Add(instance);
                }
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public IEnumerable<T> FilterByDate(DateTime startDate, DateTime endDate,string date)
        {
            string typeName = typeof(T).Name.ToLower();
            string sql = $"SELECT * FROM {typeName} WHERE {date} >= '{startDate:yyyy-MM-dd}' AND {date} <= '{endDate:yyyy-MM-dd}' ALLOW FILTERING";
            var result = _context.executeQuery(sql);

            var list = new List<T>();

            foreach (var row in result)
            {
                var instance = Activator.CreateInstance<T>();
                foreach (var prop in typeof(T).GetProperties())
                {
                    var value = row.GetValue<object>(prop.Name.ToLower());
                    if (value != null)
                    {
                        if (prop.PropertyType == typeof(string))
                        {
                            prop.SetValue(instance, value.ToString());
                        }
                        else if (prop.PropertyType == typeof(Guid))
                        {
                            prop.SetValue(instance, Guid.Parse(value.ToString()));
                        }
                        else if (prop.PropertyType == typeof(DateTime))
                        {
                            if (DateTime.TryParse(value.ToString(), out DateTime dateValue))
                            {
                                prop.SetValue(instance, dateValue);
                            }
                        }
                    }
                }
                list.Add(instance);
            }
            return list;
        }
    }
}
