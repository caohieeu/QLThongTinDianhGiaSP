using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThongTinDanhGiaSP.Models
{
    public class Categories
    {
        public Guid category_id { get; set; }
        public string name { get; set; }
        public DateTime create_at { get; set; }
    }
}
