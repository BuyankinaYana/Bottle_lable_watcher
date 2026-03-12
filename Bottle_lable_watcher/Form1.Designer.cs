namespace Bottle_lable_watcher
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel11 = new Panel();
            tableLayoutPanel4 = new TableLayoutPanel();
            b_start = new Button();
            b_stop = new Button();
            l_work = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            b_camera1 = new Button();
            b_camera2 = new Button();
            l_panelcontrol = new Label();
            panel10 = new Panel();
            l_camera1_status = new Label();
            l_camera2_status = new Label();
            label4 = new Label();
            l_status = new Label();
            panel12 = new Panel();
            l_logs = new Label();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            panel2 = new Panel();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            panel3 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            cb_detect_lable = new CheckBox();
            cb_lable_offset = new CheckBox();
            cb_lable_defect = new CheckBox();
            comboBox1 = new ComboBox();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel11.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel10.SuspendLayout();
            panel12.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel3.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1050, 635);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tableLayoutPanel2);
            tabPage1.ForeColor = Color.FromArgb(0, 0, 192);
            tabPage1.Location = new Point(4, 37);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1042, 594);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "   Главная   ";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tableLayoutPanel2.Controls.Add(panel11, 2, 1);
            tableLayoutPanel2.Controls.Add(panel10, 1, 1);
            tableLayoutPanel2.Controls.Add(panel12, 0, 1);
            tableLayoutPanel2.Controls.Add(panel1, 0, 0);
            tableLayoutPanel2.Controls.Add(panel2, 1, 0);
            tableLayoutPanel2.Controls.Add(panel3, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel2.Size = new Size(1036, 588);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // panel11
            // 
            panel11.Controls.Add(tableLayoutPanel4);
            panel11.Controls.Add(l_work);
            panel11.Controls.Add(tableLayoutPanel3);
            panel11.Controls.Add(l_panelcontrol);
            panel11.Dock = DockStyle.Fill;
            panel11.Location = new Point(693, 414);
            panel11.Name = "panel11";
            panel11.Size = new Size(340, 171);
            panel11.TabIndex = 5;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(b_start, 0, 0);
            tableLayoutPanel4.Controls.Add(b_stop, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Top;
            tableLayoutPanel4.Location = new Point(0, 130);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(340, 90);
            tableLayoutPanel4.TabIndex = 3;
            // 
            // b_start
            // 
            b_start.BackColor = Color.ForestGreen;
            b_start.Dock = DockStyle.Fill;
            b_start.Font = new Font("Segoe UI Black", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            b_start.ForeColor = Color.White;
            b_start.Location = new Point(3, 3);
            b_start.Name = "b_start";
            b_start.Size = new Size(164, 84);
            b_start.TabIndex = 0;
            b_start.Text = "Старт";
            b_start.UseVisualStyleBackColor = false;
            // 
            // b_stop
            // 
            b_stop.BackColor = Color.Red;
            b_stop.Dock = DockStyle.Fill;
            b_stop.Font = new Font("Segoe UI Black", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            b_stop.ForeColor = Color.White;
            b_stop.Location = new Point(173, 3);
            b_stop.Name = "b_stop";
            b_stop.Size = new Size(164, 84);
            b_stop.TabIndex = 1;
            b_stop.Text = "Стоп";
            b_stop.UseVisualStyleBackColor = false;
            // 
            // l_work
            // 
            l_work.BackColor = SystemColors.Highlight;
            l_work.Dock = DockStyle.Top;
            l_work.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            l_work.ForeColor = Color.White;
            l_work.Location = new Point(0, 95);
            l_work.Name = "l_work";
            l_work.Size = new Size(340, 35);
            l_work.TabIndex = 2;
            l_work.Text = "Обработка";
            l_work.TextAlign = ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(b_camera1, 0, 0);
            tableLayoutPanel3.Controls.Add(b_camera2, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Top;
            tableLayoutPanel3.Location = new Point(0, 35);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(340, 60);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // b_camera1
            // 
            b_camera1.BackColor = Color.Navy;
            b_camera1.Dock = DockStyle.Fill;
            b_camera1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            b_camera1.ForeColor = Color.White;
            b_camera1.Location = new Point(3, 3);
            b_camera1.Name = "b_camera1";
            b_camera1.Size = new Size(164, 54);
            b_camera1.TabIndex = 0;
            b_camera1.Text = "Камера 1";
            b_camera1.UseVisualStyleBackColor = false;
            // 
            // b_camera2
            // 
            b_camera2.BackColor = Color.Navy;
            b_camera2.Dock = DockStyle.Fill;
            b_camera2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            b_camera2.ForeColor = Color.White;
            b_camera2.Location = new Point(173, 3);
            b_camera2.Name = "b_camera2";
            b_camera2.Size = new Size(164, 54);
            b_camera2.TabIndex = 1;
            b_camera2.Text = "Камера 2";
            b_camera2.UseVisualStyleBackColor = false;
            // 
            // l_panelcontrol
            // 
            l_panelcontrol.BackColor = SystemColors.Highlight;
            l_panelcontrol.Dock = DockStyle.Top;
            l_panelcontrol.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            l_panelcontrol.ForeColor = Color.White;
            l_panelcontrol.Location = new Point(0, 0);
            l_panelcontrol.Name = "l_panelcontrol";
            l_panelcontrol.Size = new Size(340, 35);
            l_panelcontrol.TabIndex = 0;
            l_panelcontrol.Text = "Панель контроля";
            l_panelcontrol.TextAlign = ContentAlignment.TopCenter;
            // 
            // panel10
            // 
            panel10.Controls.Add(l_camera1_status);
            panel10.Controls.Add(l_camera2_status);
            panel10.Controls.Add(label4);
            panel10.Controls.Add(l_status);
            panel10.Dock = DockStyle.Fill;
            panel10.Location = new Point(348, 414);
            panel10.Name = "panel10";
            panel10.Size = new Size(339, 171);
            panel10.TabIndex = 4;
            // 
            // l_camera1_status
            // 
            l_camera1_status.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            l_camera1_status.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            l_camera1_status.ForeColor = Color.FromArgb(0, 0, 192);
            l_camera1_status.Location = new Point(0, 49);
            l_camera1_status.Name = "l_camera1_status";
            l_camera1_status.Size = new Size(339, 35);
            l_camera1_status.TabIndex = 3;
            l_camera1_status.Text = "Камера 1";
            l_camera1_status.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // l_camera2_status
            // 
            l_camera2_status.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            l_camera2_status.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            l_camera2_status.ForeColor = Color.FromArgb(0, 0, 192);
            l_camera2_status.Location = new Point(0, 84);
            l_camera2_status.Name = "l_camera2_status";
            l_camera2_status.Size = new Size(339, 35);
            l_camera2_status.TabIndex = 2;
            l_camera2_status.Text = "Камера 2";
            l_camera2_status.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(0, 0, 192);
            label4.Location = new Point(0, 119);
            label4.Name = "label4";
            label4.Size = new Size(339, 35);
            label4.TabIndex = 1;
            label4.Text = "Модуль I/O";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // l_status
            // 
            l_status.BackColor = SystemColors.Highlight;
            l_status.Dock = DockStyle.Top;
            l_status.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            l_status.ForeColor = Color.White;
            l_status.Location = new Point(0, 0);
            l_status.Name = "l_status";
            l_status.Size = new Size(339, 35);
            l_status.TabIndex = 0;
            l_status.Text = "Статус";
            l_status.TextAlign = ContentAlignment.TopCenter;
            // 
            // panel12
            // 
            panel12.Controls.Add(l_logs);
            panel12.Dock = DockStyle.Fill;
            panel12.Location = new Point(3, 414);
            panel12.Name = "panel12";
            panel12.Size = new Size(339, 171);
            panel12.TabIndex = 3;
            // 
            // l_logs
            // 
            l_logs.BackColor = SystemColors.Highlight;
            l_logs.Dock = DockStyle.Top;
            l_logs.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            l_logs.ForeColor = Color.White;
            l_logs.Location = new Point(0, 0);
            l_logs.Name = "l_logs";
            l_logs.Size = new Size(339, 35);
            l_logs.TabIndex = 0;
            l_logs.Text = "Логи";
            l_logs.TextAlign = ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(339, 405);
            panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.GradientInactiveCaption;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 35);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(339, 370);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.BackColor = SystemColors.Highlight;
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.White;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(339, 35);
            label1.TabIndex = 0;
            label1.Text = "Камера 1";
            // 
            // panel2
            // 
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(348, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(339, 405);
            panel2.TabIndex = 1;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = SystemColors.GradientInactiveCaption;
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.Location = new Point(0, 35);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(339, 370);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.BackColor = SystemColors.Highlight;
            label2.BorderStyle = BorderStyle.Fixed3D;
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.ForeColor = Color.White;
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(339, 35);
            label2.TabIndex = 0;
            label2.Text = "Камера 2";
            // 
            // panel3
            // 
            panel3.Controls.Add(tableLayoutPanel1);
            panel3.Controls.Add(comboBox1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(693, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(340, 405);
            panel3.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(cb_detect_lable, 0, 0);
            tableLayoutPanel1.Controls.Add(cb_lable_offset, 0, 1);
            tableLayoutPanel1.Controls.Add(cb_lable_defect, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 36);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
            tableLayoutPanel1.Size = new Size(340, 369);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // cb_detect_lable
            // 
            cb_detect_lable.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_detect_lable.BackColor = SystemColors.Highlight;
            cb_detect_lable.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            cb_detect_lable.ForeColor = Color.White;
            cb_detect_lable.Location = new Point(3, 3);
            cb_detect_lable.Name = "cb_detect_lable";
            cb_detect_lable.Size = new Size(334, 35);
            cb_detect_lable.TabIndex = 0;
            cb_detect_lable.Text = "Обнаружение этикетки";
            cb_detect_lable.UseVisualStyleBackColor = false;
            // 
            // cb_lable_offset
            // 
            cb_lable_offset.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_lable_offset.BackColor = SystemColors.Highlight;
            cb_lable_offset.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            cb_lable_offset.ForeColor = Color.White;
            cb_lable_offset.Location = new Point(3, 125);
            cb_lable_offset.Name = "cb_lable_offset";
            cb_lable_offset.Size = new Size(334, 35);
            cb_lable_offset.TabIndex = 1;
            cb_lable_offset.Text = "Смещение этикетки";
            cb_lable_offset.UseVisualStyleBackColor = false;
            // 
            // cb_lable_defect
            // 
            cb_lable_defect.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_lable_defect.BackColor = SystemColors.Highlight;
            cb_lable_defect.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            cb_lable_defect.ForeColor = Color.White;
            cb_lable_defect.Location = new Point(3, 247);
            cb_lable_defect.Name = "cb_lable_defect";
            cb_lable_defect.Size = new Size(334, 35);
            cb_lable_defect.TabIndex = 2;
            cb_lable_defect.Text = "Дефекты этикетки";
            cb_lable_defect.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            comboBox1.Dock = DockStyle.Top;
            comboBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            comboBox1.ForeColor = Color.FromArgb(0, 0, 192);
            comboBox1.FormattingEnabled = true;
            comboBox1.ItemHeight = 28;
            comboBox1.Items.AddRange(new object[] { "Камера 1", "Камера 2" });
            comboBox1.Location = new Point(0, 0);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(340, 36);
            comboBox1.TabIndex = 5;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 37);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1042, 594);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "   Настройки   ";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 37);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1042, 594);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "   Помощь   ";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 37);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(1042, 594);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "   О нас   ";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1050, 635);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "ООО Квантрон Групп \"Контроль этикеток\"";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panel11.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            panel10.ResumeLayout(false);
            panel12.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel3.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel1;
        private PictureBox pictureBox1;
        private Label label1;
        private Panel panel2;
        private PictureBox pictureBox2;
        private Label label2;
        private Panel panel3;
        private ComboBox comboBox1;
        private Panel panel10;
        private Label l_camera1_status;
        private Label l_camera2_status;
        private Label label4;
        private Label l_status;
        private Panel panel12;
        private Label l_logs;
        private Panel panel11;
        private TableLayoutPanel tableLayoutPanel4;
        private Button b_start;
        private Button b_stop;
        private Label l_work;
        private TableLayoutPanel tableLayoutPanel3;
        private Button b_camera1;
        private Button b_camera2;
        private Label l_panelcontrol;
        private TableLayoutPanel tableLayoutPanel1;
        private CheckBox cb_detect_lable;
        private CheckBox cb_lable_offset;
        private CheckBox cb_lable_defect;
    }
}
