namespace WealthDB
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
            this.ledgerPanel1 = new WealthDB.LedgerPanel();
            this.cboAccount = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ledgerPanel1
            // 
            this.ledgerPanel1.Location = new System.Drawing.Point(139, 12);
            this.ledgerPanel1.Name = "ledgerPanel1";
            this.ledgerPanel1.Size = new System.Drawing.Size(611, 261);
            this.ledgerPanel1.TabIndex = 0;
            // 
            // cboAccount
            // 
            this.cboAccount.FormattingEnabled = true;
            this.cboAccount.Location = new System.Drawing.Point(12, 12);
            this.cboAccount.Name = "cboAccount";
            this.cboAccount.Size = new System.Drawing.Size(121, 21);
            this.cboAccount.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 286);
            this.Controls.Add(this.cboAccount);
            this.Controls.Add(this.ledgerPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private LedgerPanel ledgerPanel1;
        private System.Windows.Forms.ComboBox cboAccount;

    }
}

