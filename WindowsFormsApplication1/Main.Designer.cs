﻿namespace WindowsFormsApplication1
{
    partial class Main
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
            this.textboxARPtarget = new System.Windows.Forms.TextBox();
            this.tableRoutes = new System.Windows.Forms.ListView();
            this.rtType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rtPrefix = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rtMask = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rtInterface = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rtNexthop = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonStatic = new System.Windows.Forms.Button();
            this.buttonEnable1 = new System.Windows.Forms.Button();
            this.buttonPing = new System.Windows.Forms.Button();
            this.rtMetric = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textboxMask1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tableARP
            // 
            this.tableARP.BackColor = System.Drawing.SystemColors.InfoText;
            this.tableARP.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ipAddress,
            this.macAddress,
            this.ttl});
            this.tableARP.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tableARP.ForeColor = System.Drawing.SystemColors.Window;
            this.tableARP.FullRowSelect = true;
            this.tableARP.Location = new System.Drawing.Point(64, 43);
            this.tableARP.MinimumSize = new System.Drawing.Size(350, 4);
            this.tableARP.Name = "tableARP";
            this.tableARP.Size = new System.Drawing.Size(350, 508);
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
            this.textboxIP1.BackColor = System.Drawing.SystemColors.MenuText;
            this.textboxIP1.ForeColor = System.Drawing.SystemColors.Window;
            this.textboxIP1.Location = new System.Drawing.Point(64, 17);
            this.textboxIP1.Name = "textboxIP1";
            this.textboxIP1.Size = new System.Drawing.Size(117, 20);
            this.textboxIP1.TabIndex = 1;
            // 
            // buttonStart1
            // 
            this.buttonStart1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonStart1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonStart1.Location = new System.Drawing.Point(339, 15);
            this.buttonStart1.Name = "buttonStart1";
            this.buttonStart1.Size = new System.Drawing.Size(75, 23);
            this.buttonStart1.TabIndex = 2;
            this.buttonStart1.Text = "Start";
            this.buttonStart1.UseVisualStyleBackColor = false;
            this.buttonStart1.Click += new System.EventHandler(this.buttonStart1_Click);
            // 
            // buttonReqARP1
            // 
            this.buttonReqARP1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonReqARP1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonReqARP1.Location = new System.Drawing.Point(64, 572);
            this.buttonReqARP1.Name = "buttonReqARP1";
            this.buttonReqARP1.Size = new System.Drawing.Size(103, 23);
            this.buttonReqARP1.TabIndex = 3;
            this.buttonReqARP1.Text = "ARP Request";
            this.buttonReqARP1.UseVisualStyleBackColor = false;
            this.buttonReqARP1.Click += new System.EventHandler(this.buttonReqARP1_Click);
            // 
            // textboxARPtarget
            // 
            this.textboxARPtarget.BackColor = System.Drawing.SystemColors.MenuText;
            this.textboxARPtarget.ForeColor = System.Drawing.SystemColors.Window;
            this.textboxARPtarget.Location = new System.Drawing.Point(64, 551);
            this.textboxARPtarget.Name = "textboxARPtarget";
            this.textboxARPtarget.Size = new System.Drawing.Size(350, 20);
            this.textboxARPtarget.TabIndex = 4;
            // 
            // tableRoutes
            // 
            this.tableRoutes.BackColor = System.Drawing.SystemColors.MenuText;
            this.tableRoutes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableRoutes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.rtType,
            this.rtPrefix,
            this.rtMask,
            this.rtInterface,
            this.rtNexthop,
            this.rtMetric});
            this.tableRoutes.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tableRoutes.ForeColor = System.Drawing.SystemColors.Window;
            this.tableRoutes.FullRowSelect = true;
            this.tableRoutes.Location = new System.Drawing.Point(479, 43);
            this.tableRoutes.Name = "tableRoutes";
            this.tableRoutes.Size = new System.Drawing.Size(542, 508);
            this.tableRoutes.TabIndex = 5;
            this.tableRoutes.UseCompatibleStateImageBehavior = false;
            this.tableRoutes.View = System.Windows.Forms.View.Details;
            // 
            // rtType
            // 
            this.rtType.Text = "Type";
            this.rtType.Width = 40;
            // 
            // rtPrefix
            // 
            this.rtPrefix.Text = "Prefix";
            this.rtPrefix.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rtPrefix.Width = 110;
            // 
            // rtMask
            // 
            this.rtMask.Text = "Mask";
            this.rtMask.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rtMask.Width = 110;
            // 
            // rtInterface
            // 
            this.rtInterface.Text = "Interface";
            this.rtInterface.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rtInterface.Width = 70;
            // 
            // rtNexthop
            // 
            this.rtNexthop.Text = "Nexthop";
            this.rtNexthop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rtNexthop.Width = 150;
            // 
            // buttonStatic
            // 
            this.buttonStatic.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonStatic.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonStatic.Location = new System.Drawing.Point(479, 551);
            this.buttonStatic.Name = "buttonStatic";
            this.buttonStatic.Size = new System.Drawing.Size(75, 23);
            this.buttonStatic.TabIndex = 6;
            this.buttonStatic.Text = "Static";
            this.buttonStatic.UseVisualStyleBackColor = false;
            this.buttonStatic.Click += new System.EventHandler(this.buttonStatic_Click);
            // 
            // buttonEnable1
            // 
            this.buttonEnable1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonEnable1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonEnable1.Location = new System.Drawing.Point(840, 14);
            this.buttonEnable1.Name = "buttonEnable1";
            this.buttonEnable1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonEnable1.Size = new System.Drawing.Size(75, 23);
            this.buttonEnable1.TabIndex = 7;
            this.buttonEnable1.Text = "Start";
            this.buttonEnable1.UseVisualStyleBackColor = false;
            this.buttonEnable1.Click += new System.EventHandler(this.buttonEnable1_Click);
            // 
            // buttonPing
            // 
            this.buttonPing.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonPing.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonPing.Location = new System.Drawing.Point(173, 572);
            this.buttonPing.Name = "buttonPing";
            this.buttonPing.Size = new System.Drawing.Size(100, 23);
            this.buttonPing.TabIndex = 8;
            this.buttonPing.Text = "Ping";
            this.buttonPing.UseVisualStyleBackColor = false;
            this.buttonPing.Click += new System.EventHandler(this.buttonPing_Click);
            // 
            // rtMetric
            // 
            this.rtMetric.Text = "Metric";
            this.rtMetric.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textboxMask1
            // 
            this.textboxMask1.BackColor = System.Drawing.SystemColors.MenuText;
            this.textboxMask1.ForeColor = System.Drawing.SystemColors.Window;
            this.textboxMask1.Location = new System.Drawing.Point(187, 17);
            this.textboxMask1.Name = "textboxMask1";
            this.textboxMask1.Size = new System.Drawing.Size(121, 20);
            this.textboxMask1.TabIndex = 9;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1209, 637);
            this.Controls.Add(this.textboxMask1);
            this.Controls.Add(this.buttonPing);
            this.Controls.Add(this.buttonEnable1);
            this.Controls.Add(this.buttonStatic);
            this.Controls.Add(this.tableRoutes);
            this.Controls.Add(this.textboxARPtarget);
            this.Controls.Add(this.buttonReqARP1);
            this.Controls.Add(this.buttonStart1);
            this.Controls.Add(this.textboxIP1);
            this.Controls.Add(this.tableARP);
            this.Name = "Main";
            this.Text = "Software Router";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
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
        private System.Windows.Forms.TextBox textboxARPtarget;
        private System.Windows.Forms.ListView tableRoutes;
        private System.Windows.Forms.ColumnHeader rtType;
        private System.Windows.Forms.ColumnHeader rtPrefix;
        private System.Windows.Forms.ColumnHeader rtNexthop;
        private System.Windows.Forms.Button buttonStatic;
        private System.Windows.Forms.ColumnHeader rtMask;
        private System.Windows.Forms.ColumnHeader rtInterface;
        private System.Windows.Forms.Button buttonEnable1;
        private System.Windows.Forms.Button buttonPing;
        private System.Windows.Forms.ColumnHeader rtMetric;
        private System.Windows.Forms.TextBox textboxMask1;
    }
}