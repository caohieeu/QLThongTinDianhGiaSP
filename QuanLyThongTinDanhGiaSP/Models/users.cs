﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThongTinDanhGiaSP.Models
{
    public class users
    {
        public Guid user_id { get; set; }
        public string username { get; set; }
        public DateTime dob { get; set; }
        public string email { get; set; }
    }
}
