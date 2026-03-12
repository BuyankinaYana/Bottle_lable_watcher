namespace Bottle_lable_watcher
{
    partial class StartWin
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
            label1 = new Label();
            L_camera_1 = new Label();
            L_camera_2 = new Label();
            L_module = new Label();
            B_cancel = new Button();
            B_update = new Button();
            button3 = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ControlLightLight;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 204);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Location = new Point(165, 29);
            label1.Name = "label1";
            label1.Size = new Size(377, 46);
            label1.TabIndex = 0;
            label1.Text = "Контроль этикеток";
            // 
            // L_camera_1
            // 
            L_camera_1.AutoSize = true;
            L_camera_1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            L_camera_1.ForeColor = Color.FromArgb(0, 0, 192);
            L_camera_1.Location = new Point(225, 100);
            L_camera_1.Name = "L_camera_1";
            L_camera_1.Size = new Size(116, 31);
            L_camera_1.TabIndex = 1;
            L_camera_1.Text = "Камера 1";
            // 
            // L_camera_2
            // 
            L_camera_2.AutoSize = true;
            L_camera_2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            L_camera_2.ForeColor = Color.FromArgb(0, 0, 192);
            L_camera_2.Location = new Point(225, 150);
            L_camera_2.Name = "L_camera_2";
            L_camera_2.Size = new Size(116, 31);
            L_camera_2.TabIndex = 2;
            L_camera_2.Text = "Камера 2";
            // 
            // L_module
            // 
            L_module.AutoSize = true;
            L_module.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            L_module.ForeColor = Color.FromArgb(0, 0, 192);
            L_module.Location = new Point(225, 200);
            L_module.Name = "L_module";
            L_module.Size = new Size(206, 31);
            L_module.TabIndex = 3;
            L_module.Text = "Модуль owen I/O";
            // 
            // B_cancel
            // 
            B_cancel.BackColor = Color.Red;
            B_cancel.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            B_cancel.ForeColor = Color.White;
            B_cancel.Location = new Point(12, 289);
            B_cancel.Name = "B_cancel";
            B_cancel.Size = new Size(168, 45);
            B_cancel.TabIndex = 4;
            B_cancel.Text = "Отмена";
            B_cancel.UseVisualStyleBackColor = false;
            // 
            // B_update
            // 
            B_update.BackColor = SystemColors.Highlight;
            B_update.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            B_update.ForeColor = Color.White;
            B_update.Location = new Point(186, 289);
            B_update.Name = "B_update";
            B_update.Size = new Size(168, 45);
            B_update.TabIndex = 5;
            B_update.Text = "Перезапуск";
            B_update.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.ForestGreen;
            button3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.White;
            button3.Location = new Point(360, 289);
            button3.Name = "button3";
            button3.Size = new Size(168, 45);
            button3.TabIndex = 6;
            button3.Text = "Продолжить";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.logo;
            pictureBox1.Location = new Point(9, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(150, 120);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = Properties.Resources.logo_2;
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(9, 138);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(150, 120);
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // StartWin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(542, 346);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(button3);
            Controls.Add(B_update);
            Controls.Add(B_cancel);
            Controls.Add(L_module);
            Controls.Add(L_camera_2);
            Controls.Add(L_camera_1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StartWin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ООО Квантрон Групп \"Контроль этикеток\"";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label L_camera_1;
        private Label L_camera_2;
        private Label L_module;
        private Button B_cancel;
        private Button B_update;
        private Button button3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}