
namespace CidDupFinder
{
    partial class MainForm
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
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnSelectDirectory = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listFilters = new System.Windows.Forms.ListBox();
            this.btnAddFilter = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnRemoveFIlter = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(32, 30);
            this.txtPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(1004, 28);
            this.txtPath.TabIndex = 0;
            this.txtPath.Text = "\\\\WDMyCloud\\SmartWare\\WD SmartWare.swstor\\DESKTOP-EUVHA9N\\Volume.7357c394.16d2.4f" +
    "c9.8113.e1b8f285f38d\\Nueva Carpeta";
            // 
            // btnSelectDirectory
            // 
            this.btnSelectDirectory.Location = new System.Drawing.Point(1044, 30);
            this.btnSelectDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectDirectory.Name = "btnSelectDirectory";
            this.btnSelectDirectory.Size = new System.Drawing.Size(37, 28);
            this.btnSelectDirectory.TabIndex = 1;
            this.btnSelectDirectory.Text = "...";
            this.btnSelectDirectory.UseVisualStyleBackColor = true;
            this.btnSelectDirectory.Click += new System.EventHandler(this.btnSelectDirectory_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Directory to Scan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "File Filters";
            // 
            // listFilters
            // 
            this.listFilters.FormattingEnabled = true;
            this.listFilters.ItemHeight = 22;
            this.listFilters.Location = new System.Drawing.Point(32, 89);
            this.listFilters.Name = "listFilters";
            this.listFilters.Size = new System.Drawing.Size(452, 202);
            this.listFilters.TabIndex = 4;
            // 
            // btnAddFilter
            // 
            this.btnAddFilter.Location = new System.Drawing.Point(518, 123);
            this.btnAddFilter.Name = "btnAddFilter";
            this.btnAddFilter.Size = new System.Drawing.Size(121, 48);
            this.btnAddFilter.TabIndex = 5;
            this.btnAddFilter.Text = "Add";
            this.btnAddFilter.UseVisualStyleBackColor = true;
            this.btnAddFilter.Click += new System.EventHandler(this.btnAddFilter_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(518, 89);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(121, 28);
            this.txtFilter.TabIndex = 6;
            // 
            // btnRemoveFIlter
            // 
            this.btnRemoveFIlter.Location = new System.Drawing.Point(518, 243);
            this.btnRemoveFIlter.Name = "btnRemoveFIlter";
            this.btnRemoveFIlter.Size = new System.Drawing.Size(121, 48);
            this.btnRemoveFIlter.TabIndex = 7;
            this.btnRemoveFIlter.Text = "Remove";
            this.btnRemoveFIlter.UseVisualStyleBackColor = true;
            this.btnRemoveFIlter.Click += new System.EventHandler(this.btnRemoveFIlter_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(32, 297);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(1049, 48);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start Duplicates Search";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(32, 389);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(1049, 338);
            this.treeView1.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 362);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 24);
            this.label3.TabIndex = 10;
            this.label3.Text = "Results";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 739);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnRemoveFIlter);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.btnAddFilter);
            this.Controls.Add(this.listFilters);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectDirectory);
            this.Controls.Add(this.txtPath);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnSelectDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listFilters;
        private System.Windows.Forms.Button btnAddFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnRemoveFIlter;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label3;
    }
}

