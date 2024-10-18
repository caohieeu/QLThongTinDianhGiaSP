namespace QuanLyThongTinDanhGiaSP.VIews
{
    partial class CategoryForm
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
            this.cqlDataAdapter1 = new Cassandra.Data.CqlDataAdapter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Search = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.Label();
            this.dateTime_end = new System.Windows.Forms.DateTimePicker();
            this.dateTime_start = new System.Windows.Forms.DateTimePicker();
            this.btn_Loc = new System.Windows.Forms.Button();
            this.cbb_NameCategories = new System.Windows.Forms.ComboBox();
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
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(680, 466);
            this.dataGridView1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txt_Search);
            this.panel1.Controls.Add(this.name);
            this.panel1.Controls.Add(this.dateTime_end);
            this.panel1.Controls.Add(this.dateTime_start);
            this.panel1.Controls.Add(this.btn_Loc);
            this.panel1.Controls.Add(this.cbb_NameCategories);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 340);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 126);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Tìm kiếm tên thương hiệu:";
            // 
            // txt_Search
            // 
            this.txt_Search.Location = new System.Drawing.Point(279, 17);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Size = new System.Drawing.Size(200, 20);
            this.txt_Search.TabIndex = 13;
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Location = new System.Drawing.Point(21, 67);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(70, 13);
            this.name.TabIndex = 11;
            this.name.Text = "Thương hiệu:";
            // 
            // dateTime_end
            // 
            this.dateTime_end.Location = new System.Drawing.Point(279, 81);
            this.dateTime_end.Name = "dateTime_end";
            this.dateTime_end.Size = new System.Drawing.Size(200, 20);
            this.dateTime_end.TabIndex = 10;
            // 
            // dateTime_start
            // 
            this.dateTime_start.Location = new System.Drawing.Point(279, 55);
            this.dateTime_start.Name = "dateTime_start";
            this.dateTime_start.Size = new System.Drawing.Size(200, 20);
            this.dateTime_start.TabIndex = 9;
            // 
            // btn_Loc
            // 
            this.btn_Loc.Location = new System.Drawing.Point(516, 64);
            this.btn_Loc.Name = "btn_Loc";
            this.btn_Loc.Size = new System.Drawing.Size(75, 23);
            this.btn_Loc.TabIndex = 8;
            this.btn_Loc.Text = "Lọc";
            this.btn_Loc.UseVisualStyleBackColor = true;
            // 
            // cbb_NameCategories
            // 
            this.cbb_NameCategories.FormattingEnabled = true;
            this.cbb_NameCategories.Location = new System.Drawing.Point(97, 64);
            this.cbb_NameCategories.Name = "cbb_NameCategories";
            this.cbb_NameCategories.Size = new System.Drawing.Size(121, 21);
            this.cbb_NameCategories.TabIndex = 7;
            // 
            // CategoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 466);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CategoryForm";
            this.Text = "ProductForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private Cassandra.Data.CqlDataAdapter cqlDataAdapter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.DateTimePicker dateTime_end;
        private System.Windows.Forms.DateTimePicker dateTime_start;
        private System.Windows.Forms.Button btn_Loc;
        private System.Windows.Forms.ComboBox cbb_NameCategories;
        private System.Windows.Forms.TextBox txt_Search;
        private System.Windows.Forms.Label label1;
    }
}