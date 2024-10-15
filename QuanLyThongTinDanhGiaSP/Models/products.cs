using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThongTinDanhGiaSP.Models
{
    public class products
    {
        public Guid category_id { get; set; }
        public Guid product_id { get; set; }
        public string name { get; set; }
        public string manufacturer { get; set; }
        public DateTime create_at { get; set; }
        public string category_name { get; set; }
    }
}
