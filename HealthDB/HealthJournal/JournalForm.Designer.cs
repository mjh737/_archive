namespace HealthJournal
{
    partial class JournalForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JournalForm));
            this.Tabs = new System.Windows.Forms.TabControl();
            this.tabRuns = new System.Windows.Forms.TabPage();
            this.listRuns = new System.Windows.Forms.ListView();
            this.runsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.routesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabWalks = new System.Windows.Forms.TabPage();
            this.listWalks = new System.Windows.Forms.ListView();
            this.walksContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.routesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabStats = new System.Windows.Forms.TabPage();
            this.listStats = new System.Windows.Forms.ListView();
            this.statsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Tabs.SuspendLayout();
            this.tabRuns.SuspendLayout();
            this.runsContextMenu.SuspendLayout();
            this.tabWalks.SuspendLayout();
            this.walksContextMenu.SuspendLayout();
            this.tabStats.SuspendLayout();
            this.statsContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.tabRuns);
            this.Tabs.Controls.Add(this.tabWalks);
            this.Tabs.Controls.Add(this.tabStats);
            this.Tabs.Location = new System.Drawing.Point(12, 12);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(762, 401);
            this.Tabs.TabIndex = 0;
            // 
            // tabRuns
            // 
            this.tabRuns.Controls.Add(this.listRuns);
            this.tabRuns.Location = new System.Drawing.Point(4, 22);
            this.tabRuns.Name = "tabRuns";
            this.tabRuns.Padding = new System.Windows.Forms.Padding(3);
            this.tabRuns.Size = new System.Drawing.Size(754, 375);
            this.tabRuns.TabIndex = 0;
            this.tabRuns.Text = "Runs";
            this.tabRuns.UseVisualStyleBackColor = true;
            // 
            // listRuns
            // 
            this.listRuns.ContextMenuStrip = this.runsContextMenu;
            this.listRuns.FullRowSelect = true;
            this.listRuns.GridLines = true;
            this.listRuns.Location = new System.Drawing.Point(3, 6);
            this.listRuns.Name = "listRuns";
            this.listRuns.Size = new System.Drawing.Size(745, 363);
            this.listRuns.TabIndex = 0;
            this.listRuns.UseCompatibleStateImageBehavior = false;
            this.listRuns.DoubleClick += new System.EventHandler(this.EditRunEntry);
            // 
            // runsContextMenu
            // 
            this.runsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.routesToolStripMenuItem});
            this.runsContextMenu.Name = "RunsContextMenu";
            this.runsContextMenu.Size = new System.Drawing.Size(111, 92);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewRunEntry);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.EditRunEntry);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteRunEntry);
            // 
            // routesToolStripMenuItem
            // 
            this.routesToolStripMenuItem.Name = "routesToolStripMenuItem";
            this.routesToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.routesToolStripMenuItem.Text = "Routes";
            this.routesToolStripMenuItem.Click += new System.EventHandler(this.EditRoutes);
            // 
            // tabWalks
            // 
            this.tabWalks.Controls.Add(this.listWalks);
            this.tabWalks.Location = new System.Drawing.Point(4, 22);
            this.tabWalks.Name = "tabWalks";
            this.tabWalks.Padding = new System.Windows.Forms.Padding(3);
            this.tabWalks.Size = new System.Drawing.Size(754, 375);
            this.tabWalks.TabIndex = 2;
            this.tabWalks.Text = "Walks";
            this.tabWalks.UseVisualStyleBackColor = true;
            // 
            // listWalks
            // 
            this.listWalks.ContextMenuStrip = this.walksContextMenu;
            this.listWalks.Location = new System.Drawing.Point(3, 6);
            this.listWalks.Name = "listWalks";
            this.listWalks.Size = new System.Drawing.Size(745, 363);
            this.listWalks.TabIndex = 0;
            this.listWalks.UseCompatibleStateImageBehavior = false;
            // 
            // walksContextMenu
            // 
            this.walksContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem2,
            this.editToolStripMenuItem2,
            this.deleteToolStripMenuItem2,
            this.routesToolStripMenuItem1});
            this.walksContextMenu.Name = "WalksContextMenu";
            this.walksContextMenu.Size = new System.Drawing.Size(111, 92);
            // 
            // newToolStripMenuItem2
            // 
            this.newToolStripMenuItem2.Name = "newToolStripMenuItem2";
            this.newToolStripMenuItem2.Size = new System.Drawing.Size(110, 22);
            this.newToolStripMenuItem2.Text = "New";
            this.newToolStripMenuItem2.Click += new System.EventHandler(this.NewWalkEntry);
            // 
            // editToolStripMenuItem2
            // 
            this.editToolStripMenuItem2.Name = "editToolStripMenuItem2";
            this.editToolStripMenuItem2.Size = new System.Drawing.Size(110, 22);
            this.editToolStripMenuItem2.Text = "Edit";
            // 
            // deleteToolStripMenuItem2
            // 
            this.deleteToolStripMenuItem2.Name = "deleteToolStripMenuItem2";
            this.deleteToolStripMenuItem2.Size = new System.Drawing.Size(110, 22);
            this.deleteToolStripMenuItem2.Text = "Delete";
            this.deleteToolStripMenuItem2.Click += new System.EventHandler(this.DeleteWalkEntry);
            // 
            // routesToolStripMenuItem1
            // 
            this.routesToolStripMenuItem1.Name = "routesToolStripMenuItem1";
            this.routesToolStripMenuItem1.Size = new System.Drawing.Size(110, 22);
            this.routesToolStripMenuItem1.Text = "Routes";
            this.routesToolStripMenuItem1.Click += new System.EventHandler(this.EditRoutes);
            // 
            // tabStats
            // 
            this.tabStats.Controls.Add(this.listStats);
            this.tabStats.Location = new System.Drawing.Point(4, 22);
            this.tabStats.Name = "tabStats";
            this.tabStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabStats.Size = new System.Drawing.Size(754, 375);
            this.tabStats.TabIndex = 1;
            this.tabStats.Text = "Stats";
            this.tabStats.UseVisualStyleBackColor = true;
            // 
            // listStats
            // 
            this.listStats.ContextMenuStrip = this.statsContextMenu;
            this.listStats.FullRowSelect = true;
            this.listStats.GridLines = true;
            this.listStats.Location = new System.Drawing.Point(3, 6);
            this.listStats.Name = "listStats";
            this.listStats.Size = new System.Drawing.Size(745, 363);
            this.listStats.TabIndex = 0;
            this.listStats.UseCompatibleStateImageBehavior = false;
            this.listStats.DoubleClick += new System.EventHandler(this.EditStatEntry);
            // 
            // statsContextMenu
            // 
            this.statsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem1,
            this.editToolStripMenuItem1,
            this.deleteToolStripMenuItem1});
            this.statsContextMenu.Name = "statsContextMenu";
            this.statsContextMenu.Size = new System.Drawing.Size(108, 70);
            // 
            // newToolStripMenuItem1
            // 
            this.newToolStripMenuItem1.Name = "newToolStripMenuItem1";
            this.newToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.newToolStripMenuItem1.Text = "New";
            this.newToolStripMenuItem1.Click += new System.EventHandler(this.NewStatEntry);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.editToolStripMenuItem1.Text = "Edit";
            this.editToolStripMenuItem1.Click += new System.EventHandler(this.EditStatEntry);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.DeleteStatEntry);
            // 
            // JournalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 425);
            this.Controls.Add(this.Tabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "JournalForm";
            this.Text = "Health Journal";
            this.Load += new System.EventHandler(this.JournalForm_Load);
            this.Tabs.ResumeLayout(false);
            this.tabRuns.ResumeLayout(false);
            this.runsContextMenu.ResumeLayout(false);
            this.tabWalks.ResumeLayout(false);
            this.walksContextMenu.ResumeLayout(false);
            this.tabStats.ResumeLayout(false);
            this.statsContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage tabRuns;
        private System.Windows.Forms.TabPage tabStats;
        private System.Windows.Forms.ListView listRuns;
        private System.Windows.Forms.ContextMenuStrip runsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ListView listStats;
        private System.Windows.Forms.ContextMenuStrip statsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.TabPage tabWalks;
        private System.Windows.Forms.ToolStripMenuItem routesToolStripMenuItem;
        private System.Windows.Forms.ListView listWalks;
        private System.Windows.Forms.ContextMenuStrip walksContextMenu;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem routesToolStripMenuItem1;
    }
}