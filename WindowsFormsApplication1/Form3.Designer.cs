namespace WindowsFormsApplication1
{
    partial class Form3
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
            this.textboxStaticPrefix = new System.Windows.Forms.TextBox();
            this.textboxStaticMask = new System.Windows.Forms.TextBox();
            this.textboxStaticNexthop = new System.Windows.Forms.TextBox();
            this.numericDevice = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2T = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonAddStaticRoute = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericDevice)).BeginInit();
            this.SuspendLayout();
            // 
            // textboxStaticPrefix
            // 
            this.textboxStaticPrefix.Location = new System.Drawing.Point(52, 3);
            this.textboxStaticPrefix.Name = "textboxStaticPrefix";
            this.textboxStaticPrefix.Size = new System.Drawing.Size(142, 20);
            this.textboxStaticPrefix.TabIndex = 0;
            this.textboxStaticPrefix.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textboxStaticMask
            // 
            this.textboxStaticMask.Location = new System.Drawing.Point(52, 38);
            this.textboxStaticMask.Name = "textboxStaticMask";
            this.textboxStaticMask.Size = new System.Drawing.Size(142, 20);
            this.textboxStaticMask.TabIndex = 1;
            // 
            // textboxStaticNexthop
            // 
            this.textboxStaticNexthop.Location = new System.Drawing.Point(52, 73);
            this.textboxStaticNexthop.Name = "textboxStaticNexthop";
            this.textboxStaticNexthop.Size = new System.Drawing.Size(142, 20);
            this.textboxStaticNexthop.TabIndex = 2;
            // 
            // numericDevice
            // 
            this.numericDevice.Location = new System.Drawing.Point(160, 108);
            this.numericDevice.Name = "numericDevice";
            this.numericDevice.Size = new System.Drawing.Size(34, 20);
            this.numericDevice.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 23);
            this.label1.TabIndex = 0;
            // 
            // label2T
            // 
            this.label2T.AutoSize = true;
            this.label2T.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2T.Location = new System.Drawing.Point(10, 6);
            this.label2T.Name = "label2T";
            this.label2T.Size = new System.Drawing.Size(33, 13);
            this.label2T.TabIndex = 4;
            this.label2T.Text = "Prefix";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(10, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mask";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(-1, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nexthop";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(105, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Interface";
            // 
            // buttonAddStaticRoute
            // 
            this.buttonAddStaticRoute.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonAddStaticRoute.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonAddStaticRoute.Location = new System.Drawing.Point(52, 135);
            this.buttonAddStaticRoute.Name = "buttonAddStaticRoute";
            this.buttonAddStaticRoute.Size = new System.Drawing.Size(142, 23);
            this.buttonAddStaticRoute.TabIndex = 8;
            this.buttonAddStaticRoute.Text = "Add";
            this.buttonAddStaticRoute.UseVisualStyleBackColor = false;
            this.buttonAddStaticRoute.Click += new System.EventHandler(this.buttonAddStaticRoute_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(212, 173);
            this.Controls.Add(this.buttonAddStaticRoute);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label2T);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericDevice);
            this.Controls.Add(this.textboxStaticNexthop);
            this.Controls.Add(this.textboxStaticMask);
            this.Controls.Add(this.textboxStaticPrefix);
            this.Name = "Form3";
            this.Text = "Add static route";
            ((System.ComponentModel.ISupportInitialize)(this.numericDevice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textboxStaticPrefix;
        private System.Windows.Forms.TextBox textboxStaticMask;
        private System.Windows.Forms.TextBox textboxStaticNexthop;
        private System.Windows.Forms.NumericUpDown numericDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2T;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonAddStaticRoute;
    }
}