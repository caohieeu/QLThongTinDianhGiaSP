using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using QuanLyThongTinDanhGiaSP.Models;
using QuanLyThongTinDanhGiaSP.DAL;
using QuanLyThongTinDanhGiaSP.Models;
using QuanLyThongTinDanhGiaSP.Repository;
using QuanLyThongTinDanhGiaSP.Repository.IRepository;

namespace QuanLyThongTinDanhGiaSP
{
    public partial class ProductReviewsControl : UserControl
    {
        private Product_Review _review;
        private Button replyButton;
        private TextBox replyTextBox;
        private Button sendButton;

        private readonly IProductReviewsReponsitory _productReviewsReponsitory;
        private readonly CassandraContext _cassandraContext = new CassandraContext(Utils.KeySpace);


        public ProductReviewsControl(Product_Review review)
        {
            InitializeComponent();
            _productReviewsReponsitory = new ProductReviewsReponsitory(_cassandraContext);
            _review = review;
            DisplayReview();
        }

        private void DisplayReview()
        {
            this.Height = 200;

            Label usernameLabel = new Label
            {
                Text = $"{_review.Username} - Đánh giá: {_review.Rating:F1}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            this.Controls.Add(usernameLabel);

            // Nội dung đánh giá
            Label reviewTextLabel = new Label
            {
                Text = _review.ReviewText,
                Font = new Font("Arial", 10),
                Location = new Point(10, 40),
                Width = this.Width - 20,
                Height = 80
            };
            this.Controls.Add(reviewTextLabel);

            // Ngày mua và trạng thái
            Label infoLabel = new Label
            {
                Text = $"Ngày mua: {_review.PurchaseDate?.ToShortDateString() ?? "N/A"} | Trạng thái: {_review.Status}",
                Font = new Font("Arial", 9, FontStyle.Italic),
                Location = new Point(10, 130),
                AutoSize = true
            };
            this.Controls.Add(infoLabel);


            if (_review.IsConfirm)
            {
                Label confirmLabel = new Label
                {
                    Text = $"Phản hồi: {_review.ConfirmText}",
                    Font = new Font("Arial", 9),
                    Location = new Point(10, 150),
                    ForeColor = Color.Green,
                    AutoSize = true
                };
                this.Controls.Add(confirmLabel);
            }
            else
            {
                replyButton = new Button
                {
                    Text = "Trả lời",
                    Location = new Point(10, 150),
                    Size = new Size(75, 23)
                };
                replyButton.Click += ReplyButton_Click;
                this.Controls.Add(replyButton);
            }

            this.Paint += (sender, e) =>
            {
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.LightBlue, Color.White, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            };
        }

        private void ReplyButton_Click(object sender, EventArgs e)
        {
            // Ẩn nút "Trả lời"
            replyButton.Visible = false;

            // Hiển thị TextBox để nhập phản hồi
            replyTextBox = new TextBox
            {
                Location = new Point(10, 150),
                Size = new Size(this.Width - 100, 23),
                Multiline = true
            };
            this.Controls.Add(replyTextBox);

            // Hiển thị nút "Gửi"
            sendButton = new Button
            {
                Text = "Gửi",
                Location = new Point(this.Width - 85, 150),
                Size = new Size(75, 23)
            };
            sendButton.Click += SendButton_Click;
            this.Controls.Add(sendButton);

            // Điều chỉnh chiều cao của control
            this.Height += 30;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            // Xử lý logic gửi phản hồi ở đây
            string reply = replyTextBox.Text;
            // TODO: Cập nhật phản hồi vào cơ sở dữ liệu
            _productReviewsReponsitory.UpdateReviewReply(_review.ProductId, _review.ReviewId, reply);

            // Hiển thị phản hồi
            Label confirmLabel = new Label
            {
                Text = $"Phản hồi: {reply}",
                Font = new Font("Arial", 9),
                Location = new Point(10, 150),
                ForeColor = Color.Green,
                AutoSize = true
            };

            // Xóa TextBox và nút Gửi
            this.Controls.Remove(replyTextBox);
            this.Controls.Remove(sendButton);

            // Thêm nhãn phản hồi
            this.Controls.Add(confirmLabel);

            // Điều chỉnh lại chiều cao của control
            this.Height -= 30;
        }
    }
}