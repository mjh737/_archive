namespace WealthDB
{
    partial class LedgerPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listTransactions = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listTransactions
            // 
            this.listTransactions.FullRowSelect = true;
            this.listTransactions.GridLines = true;
            this.listTransactions.Location = new System.Drawing.Point(3, 3);
            this.listTransactions.Name = "listTransactions";
            this.listTransactions.Size = new System.Drawing.Size(605, 255);
            this.listTransactions.TabIndex = 0;
            this.listTransactions.UseCompatibleStateImageBehavior = false;
            this.listTransactions.View = System.Windows.Forms.View.Details;
            // 
            // LedgerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listTransactions);
            this.Name = "LedgerPanel";
            this.Size = new System.Drawing.Size(611, 261);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listTransactions;
    }
}
