namespace N0tepad_6
{
    partial class Notepad
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notepad));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.FirstPage = new System.Windows.Forms.TabControl();
            this.New = new System.Windows.Forms.TabPage();
            this.TextBox = new System.Windows.Forms.RichTextBox();
            this.FontDialog = new System.Windows.Forms.FontDialog();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.Save_Timer = new System.Windows.Forms.Timer(this.components);
            this.ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Logging_Timer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.FirstPage.SuspendLayout();
            this.New.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.AliceBlue;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.menu, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.FirstPage, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.918239F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.08176F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(894, 501);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.AliceBlue;
            this.menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(894, 34);
            this.menu.TabIndex = 1;
            this.menu.Text = "menuStrip1";
            // 
            // FirstPage
            // 
            this.FirstPage.Controls.Add(this.New);
            this.FirstPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FirstPage.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.FirstPage.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FirstPage.Location = new System.Drawing.Point(3, 37);
            this.FirstPage.Name = "FirstPage";
            this.FirstPage.SelectedIndex = 0;
            this.FirstPage.Size = new System.Drawing.Size(888, 461);
            this.FirstPage.TabIndex = 0;
            this.FirstPage.Tag = "";
            this.FirstPage.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.FirstPage_DrawItem);
            this.FirstPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FirstPage_MouseDown);
            // 
            // New
            // 
            this.New.BackColor = System.Drawing.Color.AliceBlue;
            this.New.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.New.Controls.Add(this.TextBox);
            this.New.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.New.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.New.Location = new System.Drawing.Point(4, 34);
            this.New.Name = "New";
            this.New.Padding = new System.Windows.Forms.Padding(3);
            this.New.Size = new System.Drawing.Size(880, 423);
            this.New.TabIndex = 0;
            this.New.Text = "New";
            // 
            // TextBox
            // 
            this.TextBox.BackColor = System.Drawing.Color.White;
            this.TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox.Location = new System.Drawing.Point(3, 3);
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(874, 417);
            this.TextBox.TabIndex = 0;
            this.TextBox.Text = "";
            this.TextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // Save_Timer
            // 
            this.Save_Timer.Tick += new System.EventHandler(this.Save_Timer_Tick);
            // 
            // ContextMenu
            // 
            this.ContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ContextMenu.Name = "ContextMenu";
            this.ContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // Logging_Timer
            // 
            this.Logging_Timer.Tick += new System.EventHandler(this.Logging_Timer_Tick);
            // 
            // Notepad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(894, 501);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.MinimumSize = new System.Drawing.Size(900, 535);
            this.Name = "Notepad";
            this.Text = "N0tepad+6";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.FirstPage.ResumeLayout(false);
            this.New.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl FirstPage;
        private System.Windows.Forms.TabPage New;
        private System.Windows.Forms.RichTextBox TextBox;
        private System.Windows.Forms.FontDialog FontDialog;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.Timer Save_Timer;
        private System.Windows.Forms.ContextMenuStrip ContextMenu;
        private System.Windows.Forms.Timer Logging_Timer;
    }
}

