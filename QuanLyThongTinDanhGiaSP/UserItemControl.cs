using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyThongTinDanhGiaSP.Models;

namespace QuanLyThongTinDanhGiaSP
{
    public partial class UserItemControl : UserControl
    {
        private users _user;
        public event EventHandler<Guid> EditUserRequested;

        public UserItemControl(users user)
        {
            InitializeComponent();
            _user = user;
            DisplayUserInfo();
        }

        private void DisplayUserInfo()
        {
            this.Size = new Size(300, 120);
            this.Margin = new Padding(10);
            this.BackColor = Color.White;
            this.Padding = new Padding(1);

            this.BorderStyle = BorderStyle.None;
            this.Paint += (sender, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid);
            };

            TableLayoutPanel table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(10)
            };

            Label lblUsername = new Label
            {
                Text = _user.username,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185)
            };

            Label lblEmail = new Label
            {
                Text = $"Email: {_user.email}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 10),
                ForeColor = Color.FromArgb(100, 100, 100)
            };

            Label lblDob = new Label
            {
                Text = $"DOB: {_user.dob.ToString("dd/MM/yyyy")}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 10),
                ForeColor = Color.FromArgb(100, 100, 100)
            };

            Button btnEdit = new Button
            {
                Text = "Chỉnh sửa",
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btnEdit.Click += (sender, e) => EditUser();

            table.Controls.Add(lblUsername, 0, 0);
            table.Controls.Add(lblEmail, 0, 1);
            table.Controls.Add(lblDob, 0, 2);
            table.Controls.Add(btnEdit, 1, 1);

            table.SetRowSpan(btnEdit, 2);

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));

            this.Controls.Add(table);

            this.MouseEnter += (sender, e) =>
            {
                this.BackColor = Color.FromArgb(245, 245, 245);
                this.Invalidate();
            };
            this.MouseLeave += (sender, e) =>
            {
                this.BackColor = Color.White;
                this.Invalidate();
            };
        }

        private void EditUser()
        {
            EditUserRequested?.Invoke(this, _user.user_id);
        }
    }
}