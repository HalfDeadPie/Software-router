namespace WindowsFormsApplication1
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
            this.listInterfaces1 = new System.Windows.Forms.ListView();
            this.listInterfaces2 = new System.Windows.Forms.ListView();
            this.buttonStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listInterfaces1
            // 
            this.listInterfaces1.Location = new System.Drawing.Point(83, 54);
            this.listInterfaces1.Name = "listInterfaces1";
            this.listInterfaces1.Size = new System.Drawing.Size(1078, 292);
            this.listInterfaces1.TabIndex = 0;
            this.listInterfaces1.UseCompatibleStateImageBehavior = false;
            // 
            // listInterfaces2
            // 
            this.listInterfaces2.Location = new System.Drawing.Point(83, 362);
            this.listInterfaces2.Name = "listInterfaces2";
            this.listInterfaces2.Size = new System.Drawing.Size(1078, 315);
            this.listInterfaces2.TabIndex = 1;
            this.listInterfaces2.UseCompatibleStateImageBehavior = false;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(615, 683);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1309, 769);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.listInterfaces2);
            this.Controls.Add(this.listInterfaces1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listInterfaces1;
        private System.Windows.Forms.ListView listInterfaces2;
        private System.Windows.Forms.Button buttonStart;
    }
}

