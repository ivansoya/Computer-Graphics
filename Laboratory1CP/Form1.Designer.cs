namespace Laboratory1CP
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
            this.OGL = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OGL
            // 
            this.OGL.AccumBits = ((byte)(0));
            this.OGL.AutoCheckErrors = false;
            this.OGL.AutoFinish = false;
            this.OGL.AutoMakeCurrent = true;
            this.OGL.AutoSwapBuffers = true;
            this.OGL.BackColor = System.Drawing.Color.Black;
            this.OGL.ColorBits = ((byte)(32));
            this.OGL.DepthBits = ((byte)(16));
            this.OGL.Location = new System.Drawing.Point(12, 12);
            this.OGL.Name = "OGL";
            this.OGL.Size = new System.Drawing.Size(607, 550);
            this.OGL.StencilBits = ((byte)(0));
            this.OGL.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(625, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(188, 45);
            this.button1.TabIndex = 1;
            this.button1.Text = "Отобразить";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 574);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.OGL);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl OGL;
        private System.Windows.Forms.Button button1;
    }
}

