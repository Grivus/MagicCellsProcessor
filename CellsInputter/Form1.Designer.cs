namespace CellsInputter
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tableView = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cbPlayerId = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cbCellType = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tbCellLife = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.tbFieldSize = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.tableView)).BeginInit();
			this.SuspendLayout();
			// 
			// tableView
			// 
			this.tableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.tableView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
			this.tableView.Location = new System.Drawing.Point(12, 3);
			this.tableView.Name = "tableView";
			this.tableView.Size = new System.Drawing.Size(760, 462);
			this.tableView.TabIndex = 1;
			this.tableView.SelectionChanged += new System.EventHandler(this.tableView_SelectionChanged);
			// 
			// Column1
			// 
			this.Column1.HeaderText = "Column1";
			this.Column1.Name = "Column1";
			// 
			// Column2
			// 
			this.Column2.HeaderText = "Column2";
			this.Column2.Name = "Column2";
			// 
			// Column3
			// 
			this.Column3.HeaderText = "Column3";
			this.Column3.Name = "Column3";
			// 
			// cbPlayerId
			// 
			this.cbPlayerId.FormattingEnabled = true;
			this.cbPlayerId.Location = new System.Drawing.Point(856, 28);
			this.cbPlayerId.Name = "cbPlayerId";
			this.cbPlayerId.Size = new System.Drawing.Size(121, 21);
			this.cbPlayerId.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(856, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Игрок номер:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(853, 78);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Тип клетки";
			// 
			// cbCellType
			// 
			this.cbCellType.FormattingEnabled = true;
			this.cbCellType.Location = new System.Drawing.Point(856, 107);
			this.cbCellType.Name = "cbCellType";
			this.cbCellType.Size = new System.Drawing.Size(121, 21);
			this.cbCellType.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(862, 159);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(83, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Жизнь клетки:";
			// 
			// tbCellLife
			// 
			this.tbCellLife.Location = new System.Drawing.Point(856, 175);
			this.tbCellLife.Name = "tbCellLife";
			this.tbCellLife.Size = new System.Drawing.Size(100, 20);
			this.tbCellLife.TabIndex = 7;
			this.tbCellLife.Text = "5";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(790, 417);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(127, 48);
			this.button1.TabIndex = 8;
			this.button1.Text = "Сохранить";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(790, 366);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(124, 45);
			this.button2.TabIndex = 9;
			this.button2.Text = "Открыть";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(862, 211);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(81, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Размеры поля";
			this.label4.Click += new System.EventHandler(this.label4_Click);
			// 
			// tbFieldSize
			// 
			this.tbFieldSize.Location = new System.Drawing.Point(865, 227);
			this.tbFieldSize.Name = "tbFieldSize";
			this.tbFieldSize.Size = new System.Drawing.Size(42, 20);
			this.tbFieldSize.TabIndex = 12;
			this.tbFieldSize.Text = "20";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(923, 414);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(124, 45);
			this.button3.TabIndex = 13;
			this.button3.Text = "Очистить";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1050, 471);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.tbFieldSize);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tbCellLife);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cbCellType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cbPlayerId);
			this.Controls.Add(this.tableView);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.tableView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView tableView;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.ComboBox cbPlayerId;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbCellType;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbCellLife;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbFieldSize;
		private System.Windows.Forms.Button button3;
	}
}

