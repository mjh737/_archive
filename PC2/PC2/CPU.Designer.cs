namespace PC2
{
    partial class CPU
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.AXTextBox = new System.Windows.Forms.TextBox();
            this.BXTextBox = new System.Windows.Forms.TextBox();
            this.CXTextBox = new System.Windows.Forms.TextBox();
            this.DXTextBox = new System.Windows.Forms.TextBox();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DataBusTextBox = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.DisplayTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "AX";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "DX";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "IP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "CX";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "BX";
            // 
            // AXTextBox
            // 
            this.AXTextBox.Location = new System.Drawing.Point(68, 12);
            this.AXTextBox.Name = "AXTextBox";
            this.AXTextBox.Size = new System.Drawing.Size(40, 20);
            this.AXTextBox.TabIndex = 6;
            // 
            // BXTextBox
            // 
            this.BXTextBox.Location = new System.Drawing.Point(68, 38);
            this.BXTextBox.Name = "BXTextBox";
            this.BXTextBox.Size = new System.Drawing.Size(40, 20);
            this.BXTextBox.TabIndex = 7;
            // 
            // CXTextBox
            // 
            this.CXTextBox.Location = new System.Drawing.Point(68, 64);
            this.CXTextBox.Name = "CXTextBox";
            this.CXTextBox.Size = new System.Drawing.Size(40, 20);
            this.CXTextBox.TabIndex = 8;
            // 
            // DXTextBox
            // 
            this.DXTextBox.Location = new System.Drawing.Point(68, 90);
            this.DXTextBox.Name = "DXTextBox";
            this.DXTextBox.Size = new System.Drawing.Size(40, 20);
            this.DXTextBox.TabIndex = 9;
            // 
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(68, 116);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(40, 20);
            this.IPTextBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Data Bus";
            // 
            // DataBusTextBox
            // 
            this.DataBusTextBox.Location = new System.Drawing.Point(68, 142);
            this.DataBusTextBox.Name = "DataBusTextBox";
            this.DataBusTextBox.Size = new System.Drawing.Size(40, 20);
            this.DataBusTextBox.TabIndex = 13;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(233, 279);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 14;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.Start);
            // 
            // DisplayTextBox
            // 
            this.DisplayTextBox.Location = new System.Drawing.Point(233, 12);
            this.DisplayTextBox.Multiline = true;
            this.DisplayTextBox.Name = "DisplayTextBox";
            this.DisplayTextBox.Size = new System.Drawing.Size(236, 183);
            this.DisplayTextBox.TabIndex = 16;
            // 
            // CPU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 314);
            this.Controls.Add(this.DisplayTextBox);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.DataBusTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.IPTextBox);
            this.Controls.Add(this.DXTextBox);
            this.Controls.Add(this.CXTextBox);
            this.Controls.Add(this.BXTextBox);
            this.Controls.Add(this.AXTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CPU";
            this.Text = "CPU";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox AXTextBox;
        private System.Windows.Forms.TextBox BXTextBox;
        private System.Windows.Forms.TextBox CXTextBox;
        private System.Windows.Forms.TextBox DXTextBox;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DataBusTextBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox DisplayTextBox;
    }
}

