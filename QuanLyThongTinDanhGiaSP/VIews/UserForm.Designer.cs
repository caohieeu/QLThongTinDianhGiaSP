namespace QuanLyThongTinDanhGiaSP.VIews
{
    partial class UserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.username = new System.Windows.Forms.Label();
            this.btn_Loc = new System.Windows.Forms.Button();
            this.dateTime_end = new System.Windows.Forms.DateTimePicker();
            this.dateTime_start = new System.Windows.Forms.DateTimePicker();
            this.cbb_NameUsers = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(667, 459);
            this.dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.username);
            this.panel1.Controls.Add(this.btn_Loc);
            this.panel1.Controls.Add(this.dateTime_end);
            this.panel1.Controls.Add(this.dateTime_start);
            this.panel1.Controls.Add(this.cbb_NameUsers);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 337);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(667, 122);
            this.panel1.TabIndex = 1;
            // 
            // username
            // 
            this.username.AutoSize = true;
            this.username.Location = new System.Drawing.Point(19, 38);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(63, 13);
            this.username.TabIndex = 13;
            this.username.Text = "User Name:";
            // 
            // btn_Loc
            // 
            this.btn_Loc.Location = new System.Drawing.Point(134, 80);
            this.btn_Loc.Name = "btn_Loc";
            this.btn_Loc.Size = new System.Drawing.Size(75, 23);
            this.btn_Loc.TabIndex = 12;
            this.btn_Loc.Text = "Lọc";
            this.btn_Loc.UseVisualStyleBackColor = true;
            // 
            // dateTime_end
            // 
            this.dateTime_end.Location = new System.Drawing.Point(252, 52);
            this.dateTime_end.Name = "dateTime_end";
            this.dateTime_end.Size = new System.Drawing.Size(200, 20);
            this.dateTime_end.TabIndex = 11;
            // 
            // dateTime_start
            // 
            this.dateTime_start.Location = new System.Drawing.Point(252, 17);
            this.dateTime_start.Name = "dateTime_start";
            this.dateTime_start.Size = new System.Drawing.Size(200, 20);
            this.dateTime_start.TabIndex = 10;
            // 
            // cbb_NameUsers
            // 
            this.cbb_NameUsers.FormattingEnabled = true;
            this.cbb_NameUsers.Location = new System.Drawing.Point(88, 35);
            this.cbb_NameUsers.Name = "cbb_NameUsers";
            this.cbb_NameUsers.Size = new System.Drawing.Size(121, 21);
            this.cbb_NameUsers.TabIndex = 9;
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 459);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UserForm";
            this.Text = "UserForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label username;
        private System.Windows.Forms.Button btn_Loc;
        private System.Windows.Forms.DateTimePicker dateTime_end;
        private System.Windows.Forms.DateTimePicker dateTime_start;
        private System.Windows.Forms.ComboBox cbb_NameUsers;
    }
}