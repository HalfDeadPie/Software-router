namespace WindowsFormsApplication1
{
    partial class Form2
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
            this.tableARP = new System.Windows.Forms.ListView();
            this.ipAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.macAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ttl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // tableARP
            // 
            this.tableARP.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ipAddress,
            this.macAddress,
            this.ttl});
            this.tableARP.FullRowSelect = true;
            this.tableARP.GridLines = true;
            this.tableARP.Location = new System.Drawing.Point(64, 43);
            this.tableARP.MinimumSize = new System.Drawing.Size(350, 4);
            this.tableARP.Name = "tableARP";
            this.tableARP.Size = new System.Drawing.Size(350, 502);
            this.tableARP.TabIndex = 0;
            this.tableARP.UseCompatibleStateImageBehavior = false;
            this.tableARP.View = System.Windows.Forms.View.Details;
            // 
            // ipAddress
            // 
            this.ipAddress.Text = "Internet address";
            this.ipAddress.Width = 150;
            // 
            // macAddress
            // 
            this.macAddress.Text = "Physical address";
            this.macAddress.Width = 150;
            // 
            // ttl
            // 
            this.ttl.Text = "Time";
            this.ttl.Width = 50;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 637);
            this.Controls.Add(this.tableARP);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView tableARP;
        private System.Windows.Forms.ColumnHeader ipAddress;
        private System.Windows.Forms.ColumnHeader macAddress;
        private System.Windows.Forms.ColumnHeader ttl;
    }
}