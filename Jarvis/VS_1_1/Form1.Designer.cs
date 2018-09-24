namespace VS_1
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.b_random = new System.Windows.Forms.Button();
            this.numbers_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reset = new System.Windows.Forms.Button();
            this.but_graph = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tb_diametr = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(15, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 500);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // b_random
            // 
            this.b_random.Location = new System.Drawing.Point(631, 127);
            this.b_random.Name = "b_random";
            this.b_random.Size = new System.Drawing.Size(119, 28);
            this.b_random.TabIndex = 1;
            this.b_random.Text = "Рандом";
            this.b_random.UseVisualStyleBackColor = true;
            this.b_random.Click += new System.EventHandler(this.button1_Click);
            // 
            // numbers_tb
            // 
            this.numbers_tb.Location = new System.Drawing.Point(726, 42);
            this.numbers_tb.Name = "numbers_tb";
            this.numbers_tb.Size = new System.Drawing.Size(100, 20);
            this.numbers_tb.TabIndex = 2;
            this.numbers_tb.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(546, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Сколько нужно элементов?";
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(740, 203);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(68, 27);
            this.reset.TabIndex = 12;
            this.reset.Text = "Очистить";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click_1);
            // 
            // but_graph
            // 
            this.but_graph.Location = new System.Drawing.Point(719, 459);
            this.but_graph.Name = "but_graph";
            this.but_graph.Size = new System.Drawing.Size(107, 38);
            this.but_graph.TabIndex = 13;
            this.but_graph.Text = "График";
            this.toolTip1.SetToolTip(this.but_graph, "мямямя");
            this.but_graph.UseVisualStyleBackColor = true;
            this.but_graph.Click += new System.EventHandler(this.but_graph_Click);
            // 
            // tb_diametr
            // 
            this.tb_diametr.FormattingEnabled = true;
            this.tb_diametr.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.tb_diametr.Location = new System.Drawing.Point(558, 207);
            this.tb_diametr.Name = "tb_diametr";
            this.tb_diametr.Size = new System.Drawing.Size(98, 21);
            this.tb_diametr.TabIndex = 14;
            this.tb_diametr.Text = "1";
         
            this.tb_diametr.SelectedIndexChanged += new System.EventHandler(this.tb_diametr_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 526);
            this.Controls.Add(this.tb_diametr);
            this.Controls.Add(this.b_random);
            this.Controls.Add(this.but_graph);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numbers_tb);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Нахождение выпуклой оболочки набора точек методом Джарвиса";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button b_random;
        private System.Windows.Forms.TextBox numbers_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.Button but_graph;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox tb_diametr;
    }
}

