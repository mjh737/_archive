namespace ComputerSimulator
{
    partial class Computer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTickCounter = new Label();
            lblIP = new Label();
            SuspendLayout();
            // 
            // lblTickCounter
            // 
            lblTickCounter.AutoSize = true;
            lblTickCounter.Location = new Point(794, 9);
            lblTickCounter.Name = "lblTickCounter";
            lblTickCounter.Size = new Size(41, 20);
            lblTickCounter.TabIndex = 0;
            lblTickCounter.Text = "Ticks";
            // 
            // lblIP
            // 
            lblIP.AutoSize = true;
            lblIP.Location = new Point(814, 29);
            lblIP.Name = "lblIP";
            lblIP.Size = new Size(21, 20);
            lblIP.TabIndex = 1;
            lblIP.Text = "IP";
            // 
            // Computer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(985, 463);
            Controls.Add(lblIP);
            Controls.Add(lblTickCounter);
            Name = "Computer";
            Text = "Computer Simulator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTickCounter;
        private Label lblIP;
    }
}
