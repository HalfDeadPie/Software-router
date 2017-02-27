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
            this.textboxIP1 = new System.Windows.Forms.TextBox();
            this.buttonStart1 = new System.Windows.Forms.Button();
            this.buttonReqARP1 = new System.Windows.Forms.Button();
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
            // textboxIP1
            // 
            this.textboxIP1.Location = new System.Drawing.Point(64, 17);
            this.textboxIP1.Name = "textboxIP1";
            this.textboxIP1.Size = new System.Drawing.Size(267, 20);
            this.textboxIP1.TabIndex = 1;
            // 
            // buttonStart1
            // 
            this.buttonStart1.Location = new System.Drawing.Point(339, 15);
            this.buttonStart1.Name = "buttonStart1";
            this.buttonStart1.Size = new System.Drawing.Size(75, 23);
            this.buttonStart1.TabIndex = 2;
            this.buttonStart1.Text = "Start";
            this.buttonStart1.UseVisualStyleBackColor = true;
            this.buttonStart1.Click += new System.EventHandler(this.buttonStart1_Click);
            // 
            // buttonReqARP1
            // 
            this.buttonReqARP1.Location = new System.Drawing.Point(64, 551);
            this.buttonReqARP1.Name = "buttonReqARP1";
            this.buttonReqARP1.Size = new System.Drawing.Size(103, 23);
            this.buttonReqARP1.TabIndex = 3;
            this.buttonReqARP1.Text = "ARP Request";
            this.buttonReqARP1.UseVisualStyleBackColor = true;
            this.buttonReqARP1.Click += new System.EventHandler(this.buttonReqARP1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 637);
            this.Controls.Add(this.buttonReqARP1);
            this.Controls.Add(this.buttonStart1);
            this.Controls.Add(this.textboxIP1);
            this.Controls.Add(this.tableARP);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView tableARP;
        private System.Windows.Forms.ColumnHeader ipAddress;
        private System.Windows.Forms.ColumnHeader macAddress;
        private System.Windows.Forms.ColumnHeader ttl;
        private System.Windows.Forms.TextBox textboxIP1;
        private System.Windows.Forms.Button buttonStart1;
        private System.Windows.Forms.Button buttonReqARP1;
    }
}