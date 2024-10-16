using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThongTinDanhGiaSP.Models
{
    public class Product_Review
    {
        public Guid ProductId { get; set; }

        public Guid ReviewId { get; set; }

        public Guid UserId { get; set; }

        public string Username { get; set; }

        public decimal Rating { get; set; }

        public string ReviewText { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public string Status { get; set; }

        public string ConfirmText { get; set; }

        public bool IsConfirm { get; set; }

        public DateTime? ConfirmDate { get; set; }
    }
}
