namespace Serpent
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.text = new System.Windows.Forms.TextBox();
            this.key = new System.Windows.Forms.TextBox();
            this.answerbyte = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.encrypt = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.decrypt = new System.Windows.Forms.Button();
            this.c128 = new System.Windows.Forms.CheckBox();
            this.c192 = new System.Windows.Forms.CheckBox();
            this.c256 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.answer = new System.Windows.Forms.RichTextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // text
            // 
            this.text.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.text.Location = new System.Drawing.Point(12, 110);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(769, 30);
            this.text.TabIndex = 0;
            // 
            // key
            // 
            this.key.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.key.Location = new System.Drawing.Point(12, 183);
            this.key.Name = "key";
            this.key.Size = new System.Drawing.Size(769, 30);
            this.key.TabIndex = 1;
            // 
            // answerbyte
            // 
            this.answerbyte.BackColor = System.Drawing.SystemColors.Window;
            this.answerbyte.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.answerbyte.ForeColor = System.Drawing.SystemColors.WindowText;
            this.answerbyte.Location = new System.Drawing.Point(377, 254);
            this.answerbyte.Name = "answerbyte";
            this.answerbyte.ReadOnly = true;
            this.answerbyte.Size = new System.Drawing.Size(533, 70);
            this.answerbyte.TabIndex = 2;
            this.answerbyte.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(60, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(643, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "Криптографический алгоритм SERPENT";
            // 
            // encrypt
            // 
            this.encrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.encrypt.Location = new System.Drawing.Point(12, 335);
            this.encrypt.Name = "encrypt";
            this.encrypt.Size = new System.Drawing.Size(336, 37);
            this.encrypt.TabIndex = 4;
            this.encrypt.Text = "Зашифровать";
            this.encrypt.UseVisualStyleBackColor = true;
            this.encrypt.Click += new System.EventHandler(this.encrypt_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(932, 27);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(113, 24);
            this.toolStripButton1.Text = "Об Алгоритме";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(7, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(246, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Введите текст в байтах: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(7, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Ключ в байтах:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(78, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Размер ключа (бит):";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // decrypt
            // 
            this.decrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.decrypt.Location = new System.Drawing.Point(12, 390);
            this.decrypt.Name = "decrypt";
            this.decrypt.Size = new System.Drawing.Size(336, 37);
            this.decrypt.TabIndex = 11;
            this.decrypt.Text = "Расшифровать";
            this.decrypt.UseVisualStyleBackColor = true;
            this.decrypt.Click += new System.EventHandler(this.decrypt_Click);
            // 
            // c128
            // 
            this.c128.AutoSize = true;
            this.c128.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.c128.Location = new System.Drawing.Point(290, 239);
            this.c128.Name = "c128";
            this.c128.Size = new System.Drawing.Size(58, 24);
            this.c128.TabIndex = 12;
            this.c128.Text = "128";
            this.c128.UseVisualStyleBackColor = true;
            this.c128.CheckedChanged += new System.EventHandler(this.c128_CheckedChanged);
            // 
            // c192
            // 
            this.c192.AutoSize = true;
            this.c192.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.c192.Location = new System.Drawing.Point(290, 260);
            this.c192.Name = "c192";
            this.c192.Size = new System.Drawing.Size(58, 24);
            this.c192.TabIndex = 13;
            this.c192.Text = "192";
            this.c192.UseVisualStyleBackColor = true;
            this.c192.CheckedChanged += new System.EventHandler(this.c192_CheckedChanged);
            // 
            // c256
            // 
            this.c256.AutoSize = true;
            this.c256.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.c256.Location = new System.Drawing.Point(290, 281);
            this.c256.Name = "c256";
            this.c256.Size = new System.Drawing.Size(58, 24);
            this.c256.TabIndex = 14;
            this.c256.Text = "256";
            this.c256.UseVisualStyleBackColor = true;
            this.c256.CheckedChanged += new System.EventHandler(this.c256_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(372, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 25);
            this.label5.TabIndex = 15;
            this.label5.Text = "Ответ в байтах:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(372, 345);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 25);
            this.label6.TabIndex = 17;
            this.label6.Text = "Ответ:";
            // 
            // answer
            // 
            this.answer.BackColor = System.Drawing.SystemColors.Window;
            this.answer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.answer.Location = new System.Drawing.Point(377, 380);
            this.answer.Name = "answer";
            this.answer.ReadOnly = true;
            this.answer.Size = new System.Drawing.Size(533, 70);
            this.answer.TabIndex = 16;
            this.answer.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 469);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.answer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.c256);
            this.Controls.Add(this.c192);
            this.Controls.Add(this.c128);
            this.Controls.Add(this.decrypt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.encrypt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.answerbyte);
            this.Controls.Add(this.key);
            this.Controls.Add(this.text);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text;
        private System.Windows.Forms.TextBox key;
        private System.Windows.Forms.RichTextBox answerbyte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button encrypt;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button decrypt;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.CheckBox c128;
        private System.Windows.Forms.CheckBox c192;
        private System.Windows.Forms.CheckBox c256;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox answer;
    }
}

