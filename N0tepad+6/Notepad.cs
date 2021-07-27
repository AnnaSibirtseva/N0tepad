using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace N0tepad_6
{
    public partial class Notepad : Form
    {
        int Counter = 1;
        bool Сheck;
        string FileName = "";
        Color theme = Color.White;
        // Fields for storing the time for auto-saving and saving the backup.
        int Save_time, Log_time;
        ToolStripMenuItem darkTheme, Sec30, Min1, Min5, Min15, Sec30log, Min1log, Min5log, Min15log;
        // A dictionary for storing all open files.
        Dictionary<TabPage, RichTextBox> Saved_Files = new Dictionary<TabPage, RichTextBox>();
        // A dictionary where value is the tab, and keys are true if the file is saved and false otherwise.
        Dictionary<TabPage, bool> Checking = new Dictionary<TabPage, bool>();
        // A dictionary with tabs and paths to them.
        Dictionary<TabPage, string> Names = new Dictionary<TabPage, string>();
        // Array with all saved settings from the file.
        string[] Saved_Settings;

        /// <summary>
        /// A method for basic actions before a full-fledged launch.
        /// </summary>
        public Notepad()
        {
            InitializeComponent();

            // Adding two types of file extensions.
            OpenFileDialog.Filter = "Text files(*.txt)|*.txt|RichText files(*.rtf)|*.rtf|C# Code files(*.cs)|*.cs";
            SaveFileDialog.Filter = "Text files(*.txt)|*.txt|RichText files(*.rtf)|*.rtf|C# Code files(*.cs)|*.cs";

            // Reading the saved settings.
            Saved_Settings = File.ReadAllLines("Settings.txt");
            Open_Closed();
            // If there were no open files, then create an empty tab, otherwise open these files.
            if (Counter == 1)
            {
                Saved_Files.Add(New, TextBox);
                Checking.Add(New, true);
            }
            else
            {
                Checking.Remove(New);
                Saved_Files.Remove(New);
                this.FirstPage.TabPages.RemoveAt(0);
            }
            // Creating a menu.
            Menu();
        }
        /// <summary>
        /// This method opens the tabs that were opened before.
        /// </summary>
        
        void Open_Closed()
        {
            try
            {
                // Adding all the files to the saved paths.
                for (int i = 11; i < Saved_Settings.Length; i++)
                {
                    Open_Closed_Font(i);
                    string fileText = File.ReadAllText(Saved_Settings[i]);
                    var separator = Path.DirectorySeparatorChar;
                    string[] all_name = Saved_Settings[i].Split(separator);
                    string[] formatic = Saved_Settings[i].Split('.');
                    FirstPage.SelectedTab.ForeColor = Color.Black;
                    FirstPage.SelectedTab.Text = all_name[all_name.Length - 1] + "    ";
                    // We open the file in different ways, depending on its format.
                    if (formatic[formatic.Length - 1] == "rtf")
                    {
                        (FirstPage.SelectedTab.Controls[0] as RichTextBox).LoadFile(Saved_Settings[i]);
                    }
                    else if (formatic[formatic.Length - 1] == "cs")
                    {
                        (FirstPage.SelectedTab.Controls[0] as RichTextBox).LoadFile(Saved_Settings[i], RichTextBoxStreamType.PlainText);
                    }
                    else if (formatic[formatic.Length - 1] == "txt")
                    {
                        Saved_Files[FirstPage.SelectedTab].Text = fileText;
                    }
                    if (!Checking.ContainsKey(FirstPage.SelectedTab))
                    {
                        Checking.Add(FirstPage.SelectedTab, true);
                    }
                    if (!Names.ContainsValue(Saved_Settings[i]) && !Names.ContainsKey(FirstPage.SelectedTab))
                    {
                        Names.Add(FirstPage.SelectedTab, Saved_Settings[i]);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong", "ERROR",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error,
                                                    MessageBoxDefaultButton.Button1,
                                                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        /// <summary>
        /// Method for formatting settings on previously opened files.
        /// </summary>
        void Open_Closed_Font(int i)
        {
            Counter++;
            string[] formatic = Saved_Settings[i].Split('.');
            var textBox = new RichTextBox();
            TabPage newTabPage = new TabPage();
            FirstPage.TabPages.Add(newTabPage);
            // Setting up everything relative to the theme.
            if (theme == Color.White)
            {
                textBox.ForeColor = Color.Black;
            }
            else
            {
                textBox.ForeColor = Color.White;
            }
            textBox.Dock = DockStyle.Fill;
            textBox.Font = new Font("Segoe UI", 13.8F);
            textBox.BackColor = theme;
            textBox.TextChanged += TextBox_TextChanged;
            FirstPage.SelectedTab = newTabPage;
            newTabPage.Controls.Add(textBox);
            Saved_Files.Add(FirstPage.SelectedTab, textBox);
        }
        /// <summary>
        /// Method for saving the file.
        /// </summary>
        void SaveItem_Click(object sender, EventArgs e)
        {
            // Сhecking whether it was saved before.
            if (!Checking[FirstPage.SelectedTab])
            {
                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Saving();
                }
                else
                {
                    MessageBox.Show("Something went wrong" + "\n" + "The file wasn't saved.", "ERROR",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error,
                                                        MessageBoxDefaultButton.Button1,
                                                        MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            else
            {
                string[] formatic = Names[FirstPage.SelectedTab].Split('.');
                if (formatic[formatic.Length - 1] == "rtf")
                {
                    (FirstPage.SelectedTab.Controls[0] as RichTextBox).SaveFile(Names[FirstPage.SelectedTab]);
                }
                else if (formatic[formatic.Length - 1] == "cs")
                {
                    (FirstPage.SelectedTab.Controls[0] as RichTextBox).SaveFile(Names[FirstPage.SelectedTab], RichTextBoxStreamType.PlainText);
                }
                else
                {
                    File.WriteAllText(Names[FirstPage.SelectedTab], Saved_Files[FirstPage.SelectedTab].Text);
                }
            }
        }
        /// <summary>
        /// The method that opens the file.
        /// </summary>
        void OpenItem_Click(object sender, EventArgs e)
        {
            try
            {
                // If there are open tabs, then open the file there.
                if (Saved_Files.Count != 0)
                {
                    if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filename = OpenFileDialog.FileName;
                        string fileText = File.ReadAllText(filename);
                        var separator = Path.DirectorySeparatorChar;
                        string[] all_name = filename.Split(separator);
                        string[] formatic = filename.Split('.');
                        FirstPage.SelectedTab.ForeColor = Color.Black;
                        FirstPage.SelectedTab.Text = all_name[all_name.Length - 1] + "    ";
                        if (formatic[formatic.Length - 1] == "rtf")
                        {
                            (FirstPage.SelectedTab.Controls[0] as RichTextBox).LoadFile(filename);
                        }
                        else if (formatic[formatic.Length - 1] == "cs")
                        {
                            (FirstPage.SelectedTab.Controls[0] as RichTextBox).LoadFile(filename, RichTextBoxStreamType.PlainText);
                        }
                        else
                        {
                            Saved_Files[FirstPage.SelectedTab].Text = fileText;
                        }
                        if (!Checking.ContainsKey(FirstPage.SelectedTab))
                        {
                            Checking.Add(FirstPage.SelectedTab, true);
                        }
                        // If the file has not yet been added to the dictionary, where the full paths to the file are stored, then we do this.
                        if (!Names.ContainsValue(filename) && !Names.ContainsKey(FirstPage.SelectedTab))
                        {
                            Names.Add(FirstPage.SelectedTab, filename);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Unfortunately, you can't open the file \nbecause you don't have any tabs open.", "OOPS",
                                                            MessageBoxButtons.OK,
                                                            MessageBoxIcon.Information,
                                                            MessageBoxDefaultButton.Button1,
                                                            MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong", "ERROR",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error,
                                                        MessageBoxDefaultButton.Button1,
                                                        MessageBoxOptions.DefaultDesktopOnly);
            }

        }
        /// <summary>
        /// A method that saves all open files.
        /// </summary>
        void SaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage i in Saved_Files.Keys)
                {
                    // If there are unsaved changes in the file and it has already been saved before, then simply overwrite it.
                    if (!Checking[i] || !Names.ContainsKey(i))
                    {
                        if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            Saving();
                        }
                    }
                    else
                    {
                        string[] formatic = Names[i].Split('.');
                        if (formatic[formatic.Length - 1] == "rtf")
                        {
                            (i.Controls[0] as RichTextBox).SaveFile(Names[i]);
                        }
                        else if (formatic[formatic.Length - 1] == "cs")
                        {
                            (FirstPage.SelectedTab.Controls[0] as RichTextBox).SaveFile(Names[FirstPage.SelectedTab], RichTextBoxStreamType.PlainText);
                        }
                        else
                        {
                            File.WriteAllText(Names[i], Saved_Files[i].Text);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong", "ERROR",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error,
                                                        MessageBoxDefaultButton.Button1,
                                                        MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        /// <summary>
        /// Method that opens the selected file in a new form / window.
        /// </summary>
        void NewWindow_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                Notepad new_doc = new Notepad();
                new_doc.Show();
                TextBox.Font = new Font("Segoe UI", 13.8F);
                string filename = OpenFileDialog.FileName;
                string fileText = File.ReadAllText(filename);
                var separator = Path.DirectorySeparatorChar;
                string[] all_name = filename.Split(separator);

                new_doc.FirstPage.SelectedTab.Text = all_name[all_name.Length - 1] + "    ";
                new_doc.TextBox.Text = fileText;
            }

        }
        /// <summary>
        /// A method that saves a file as something.
        /// </summary>
        void SaveAsItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Saving();
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong" + "\n" + "The file wasn't saved.", "ERROR",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error,
                                                    MessageBoxDefaultButton.Button1,
                                                    MessageBoxOptions.DefaultDesktopOnly);

            }
        }
        /// <summary>
        /// Method that adds a new tab to the form.
        /// </summary>
        void AddItem_Click(object sender, EventArgs e)
        {
            // Setting a limit of possible open tabs.
            if (Counter <= 28)
            {
                Counter++;
                TabPage newTabPage = new TabPage();
                newTabPage.Text = $"New file {Counter}     ";
                FirstPage.TabPages.Add(newTabPage);
                var textBox = new RichTextBox();
                if (theme == Color.White)
                {
                    textBox.ForeColor = Color.Black;
                }
                else
                {
                    textBox.ForeColor = Color.White;
                }
                textBox.Dock = DockStyle.Fill;
                textBox.Font = new Font("Segoe UI", 13.8F);
                textBox.BackColor = theme;
                textBox.TextChanged += TextBox_TextChanged;
                FirstPage.SelectedTab = newTabPage;
                newTabPage.Controls.Add(textBox);
                Saved_Files.Add(FirstPage.SelectedTab, textBox);
                Checking.Add(FirstPage.SelectedTab, true);
                (FirstPage.SelectedTab.Controls[0] as RichTextBox).ContextMenuStrip = ContextMenu;
            }
            else
            {
                MessageBox.Show("Sorry, but you can't open more than 29 files at a time.", "Information",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information,
                                                    MessageBoxDefaultButton.Button1,
                                                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        /// <summary>
        /// Method for changing the formatting of the selected text.
        /// </summary>
        void Fotmat_Click(object sender, EventArgs e)
        {
            FontDialog.ShowColor = true;
            if (FontDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (TabPage i in Saved_Files.Keys)
                {
                    (FirstPage.SelectedTab.Controls[0] as RichTextBox).SelectionFont = FontDialog.Font;
                    (FirstPage.SelectedTab.Controls[0] as RichTextBox).SelectionColor = FontDialog.Color;
                }
            }
        }
        /// <summary>
        /// A method that allows you to exit the application.
        /// </summary>
        void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// Method, for final saving.
        /// </summary>
        /// <param name="SelectedTab">The specific tab that will be saved.</param>
        void End_Saving(TabPage SelectedTab)
        {
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = SaveFileDialog.FileName;
                if (!Names.ContainsKey(FirstPage.SelectedTab))
                {
                    Names.Add(SelectedTab, filename);
                }
                string[] formatic = Names[FirstPage.SelectedTab].Split('.');
                if (formatic[formatic.Length - 1] == "rtf" || formatic[formatic.Length - 1] == "cs")
                {
                    (FirstPage.SelectedTab.Controls[0] as RichTextBox).SaveFile(Names[FirstPage.SelectedTab]);
                }
                else
                {
                    File.WriteAllText(Names[FirstPage.SelectedTab], Saved_Files[FirstPage.SelectedTab].Text);
                }
            }
            else
            {
                MessageBox.Show("Something went wrong" + "\n" + "The file wasn't saved.", "ERROR",
                                                                            MessageBoxButtons.OK,
                                                                            MessageBoxIcon.Error,
                                                                            MessageBoxDefaultButton.Button1,
                                                                            MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        /// <summary>
        /// The method of inserting a piece of text saved in the buffer.
        /// </summary>
        private void Paste_Click(object sender, EventArgs e)
        {
            try
            {
                if (Saved_Files.Count != 0)
                {
                    Saved_Files[FirstPage.SelectedTab].Paste();
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong", "ERROR",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error,
                                                        MessageBoxDefaultButton.Button1,
                                                        MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        /// <summary>
        /// A method that encodes the selected text fragment to the buffer.
        /// </summary>
        private void Copy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(Saved_Files[FirstPage.SelectedTab].SelectedText);
            }
            catch
            {
                MessageBox.Show("Something went wrong", "ERROR",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error,
                                                        MessageBoxDefaultButton.Button1,
                                                        MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        /// <summary>
        /// A method that cuts out a piece of text and saves it to the buffer for further insertion.
        /// </summary>
        private void Cut_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(Saved_Files[FirstPage.SelectedTab].SelectedText);
                Saved_Files[FirstPage.SelectedTab].SelectedText = "";
            }
            catch
            {
                MessageBox.Show("Something went wrong", "ERROR",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error,
                                                        MessageBoxDefaultButton.Button1,
                                                        MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        /// <summary>
        /// Method that selects all the text in the selected tab.
        /// </summary>
        private void AllSelected_Click(object sender, EventArgs e)
        {
            (FirstPage.SelectedTab.Controls[0] as RichTextBox).SelectAll();
        }
        /// <summary>
        /// Creating a menu.
        /// </summary>
        void Menu()
        {
            File_Menu();
            Edit_Menu();
            Context_Menu();

            ToolStripMenuItem formatItem = new ToolStripMenuItem("Format");
            formatItem.Image = Image.FromFile("Format.ico");
            formatItem.Click += Formating;
            menu.Items.Add(formatItem);

            Settings_Menu();

            ToolStripMenuItem backupItem = new ToolStripMenuItem("Backup");
            backupItem.Image = Image.FromFile("Backup1.ico");
            backupItem.Click += Backup;
            menu.Items.Add(backupItem);
        }
        /// <summary>
        /// A method for creating a context menu for tabs.
        /// </summary>
        void Context_Menu()
        {
            ToolStripMenuItem selectAll = new ToolStripMenuItem("Select All");
            selectAll.Image = Image.FromFile("SelectAll.ico");
            selectAll.Click += AllSelected_Click;
            selectAll.ShortcutKeys = Keys.Control | Keys.A;

            ToolStripMenuItem cut = new ToolStripMenuItem("Cut");
            cut.Image = Image.FromFile("Cut.ico");
            cut.Click += Cut_Click;
            cut.ShortcutKeys = Keys.Control | Keys.X;

            ToolStripMenuItem copy = new ToolStripMenuItem("Copy");
            copy.Image = Image.FromFile("Copy.ico");
            copy.Click += Copy_Click;
            copy.ShortcutKeys = Keys.Control | Keys.C;

            ToolStripMenuItem paste = new ToolStripMenuItem("Paste");
            paste.Image = Image.FromFile("Paste.ico");
            paste.Click += Paste_Click;
            paste.ShortcutKeys = Keys.Control | Keys.V;

            ToolStripMenuItem format = new ToolStripMenuItem("Format");
            format.Image = Image.FromFile("Format.ico");
            format.Click += Fotmat_Click;
            format.ShortcutKeys = Keys.Control | Keys.F;

            ContextMenu.Items.AddRange(new[] { selectAll, cut, copy, paste, format });
            foreach (TabPage i in Saved_Files.Keys)
            {
                (i.Controls[0] as RichTextBox).ContextMenuStrip = ContextMenu;
            }
        }
        /// <summary>
        /// Create a menu for files.
        /// </summary>
        void File_Menu()
        {
            ToolStripMenuItem fileItem = new ToolStripMenuItem("File");

            fileItem.Image = Image.FromFile("File.ico");
            ToolStripMenuItem openItem = new ToolStripMenuItem("Open");
            openItem.Image = Bitmap.FromFile("OpenDoc.ico");
            openItem.Click += OpenItem_Click;
            openItem.ShortcutKeys = Keys.Control | Keys.O;
            fileItem.DropDownItems.Add(openItem);

            ToolStripMenuItem addItem = new ToolStripMenuItem("New");
            addItem.Image = Bitmap.FromFile("NewDoc.ico");
            addItem.Click += AddItem_Click;
            addItem.ShortcutKeys = Keys.Control | Keys.N;
            fileItem.DropDownItems.Add(addItem);

            ToolStripMenuItem saveItem = new ToolStripMenuItem("Save");
            saveItem.Image = Bitmap.FromFile("SaveDoc.ico");
            saveItem.Click += SaveItem_Click;
            saveItem.ShortcutKeys = Keys.Control | Keys.S;
            fileItem.DropDownItems.Add(saveItem);
            menu.Items.Add(fileItem);

            ToolStripMenuItem saveAsItem = new ToolStripMenuItem("Save As...");
            saveAsItem.Image = Bitmap.FromFile("SaveAsDoc.ico");
            saveAsItem.Click += SaveAsItem_Click;
            saveAsItem.ShortcutKeys = Keys.Alt | Keys.S;
            fileItem.DropDownItems.Add(saveAsItem);
            menu.Items.Add(fileItem);

            ToolStripMenuItem saveAllItem = new ToolStripMenuItem("Save All");
            saveAllItem.Image = Bitmap.FromFile("SaveAll.ico");
            saveAllItem.Click += SaveAll_Click;
            saveAllItem.ShortcutKeys = Keys.Control | Keys.L;
            fileItem.DropDownItems.Add(saveAllItem);
            menu.Items.Add(fileItem);

            ToolStripMenuItem newItem = new ToolStripMenuItem("New Window");
            newItem.Image = Bitmap.FromFile("NewWindow.ico");
            newItem.Click += NewWindow_Click;
            newItem.ShortcutKeys = Keys.Control | Keys.W;
            fileItem.DropDownItems.Add(newItem);
            menu.Items.Add(fileItem);

            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit");
            exitItem.Image = Bitmap.FromFile("Exit.ico");
            exitItem.Click += Exit_Click;
            exitItem.ShortcutKeys = Keys.Control | Keys.E;
            fileItem.DropDownItems.Add(exitItem);
        }
        /// <summary>
        /// Create a settings menu.
        /// </summary>
        void Settings_Menu()
        {
            ToolStripMenuItem settingsItem = new ToolStripMenuItem("Settings");
            settingsItem.Image = Image.FromFile("Settings.ico");
            menu.Items.Add(settingsItem);
            Save_time = Convert.ToInt32(Saved_Settings[5]);
            Log_time = Convert.ToInt32(Saved_Settings[10]);
            Сheck = Convert.ToBoolean(Saved_Settings[0]);
            darkTheme = new ToolStripMenuItem("Dark Theme") { Checked = Сheck, CheckOnClick = true };
            if (Сheck)
            {
                Dark_Theme();
            }
            darkTheme.CheckedChanged += The_Theme;
            settingsItem.DropDownItems.Add(darkTheme);
            ToolStripMenuItem AutoSave = new ToolStripMenuItem("Autosave");
            Sec30 = new ToolStripMenuItem("Every 30 seconds") { Checked = Convert.ToBoolean(Saved_Settings[1]), CheckOnClick = true };
            Sec30.CheckedChanged += Autosave_Changed;
            Min1 = new ToolStripMenuItem("Every 1 minute") { Checked = Convert.ToBoolean(Saved_Settings[2]), CheckOnClick = true };
            Min1.CheckedChanged += Autosave_Changed;
            Min5 = new ToolStripMenuItem("Every 5 minutes") { Checked = Convert.ToBoolean(Saved_Settings[3]), CheckOnClick = true };
            Min5.CheckedChanged += Autosave_Changed;
            Min15 = new ToolStripMenuItem("Every 15 minutes") { Checked = Convert.ToBoolean(Saved_Settings[4]), CheckOnClick = true };
            Min15.CheckedChanged += Autosave_Changed;
            Save_Timer.Interval = Save_time;
            Save_Timer.Start();
            AutoSave.DropDownItems.Add(Sec30);
            AutoSave.DropDownItems.Add(Min1);
            AutoSave.DropDownItems.Add(Min5);
            AutoSave.DropDownItems.Add(Min15);
            settingsItem.DropDownItems.Add(AutoSave);

            ToolStripMenuItem Logging = new ToolStripMenuItem("Backup");
            Sec30log = new ToolStripMenuItem("Every 30 seconds") { Checked = Convert.ToBoolean(Saved_Settings[6]), CheckOnClick = true };
            Sec30log.CheckedChanged += Logging_Changed;
            Min1log = new ToolStripMenuItem("Every 1 minute") { Checked = Convert.ToBoolean(Saved_Settings[7]), CheckOnClick = true };
            Min1log.CheckedChanged += Logging_Changed;
            Min5log = new ToolStripMenuItem("Every 5 minutes") { Checked = Convert.ToBoolean(Saved_Settings[8]), CheckOnClick = true };
            Min5log.CheckedChanged += Logging_Changed;
            Min15log = new ToolStripMenuItem("Every 15 minutes") { Checked = Convert.ToBoolean(Saved_Settings[9]), CheckOnClick = true };
            Min15log.CheckedChanged += Logging_Changed;
            Logging_Timer.Interval = Log_time;
            Logging_Timer.Start();
            Logging.DropDownItems.Add(Sec30log);
            Logging.DropDownItems.Add(Min1log);
            Logging.DropDownItems.Add(Min5log);
            Logging.DropDownItems.Add(Min15log);
            settingsItem.DropDownItems.Add(Logging);
        }
        /// <summary>
        /// Create a menu for editing text.
        /// </summary>
        void Edit_Menu()
        {
            ToolStripMenuItem editItem = new ToolStripMenuItem("Edit");
            editItem.Image = Image.FromFile("Edit.ico");

            ToolStripMenuItem selectAll = new ToolStripMenuItem("Select All");
            selectAll.Image = Image.FromFile("SelectAll.ico");
            selectAll.Click += AllSelected_Click;
            selectAll.ShortcutKeys = Keys.Control | Keys.A;
            editItem.DropDownItems.Add(selectAll);

            ToolStripMenuItem cut = new ToolStripMenuItem("Cut");
            cut.Image = Image.FromFile("Cut.ico");
            cut.Click += Cut_Click;
            cut.ShortcutKeys = Keys.Control | Keys.Z;
            editItem.DropDownItems.Add(cut);

            ToolStripMenuItem copy = new ToolStripMenuItem("Copy");
            copy.Image = Image.FromFile("Copy.ico");
            copy.Click += Copy_Click;
            copy.ShortcutKeys = Keys.Control | Keys.C;
            editItem.DropDownItems.Add(copy);

            ToolStripMenuItem paste = new ToolStripMenuItem("Paste");
            paste.Image = Image.FromFile("Paste.ico");
            paste.Click += Paste_Click;
            paste.ShortcutKeys = Keys.Control | Keys.V;
            editItem.DropDownItems.Add(paste);

            ToolStripMenuItem undo = new ToolStripMenuItem("Undo");
            undo.Image = Image.FromFile("Undo.ico");
            undo.Click += Undo_Click;
            undo.ShortcutKeys = Keys.Control | Keys.Z;
            editItem.DropDownItems.Add(undo);

            ToolStripMenuItem redo = new ToolStripMenuItem("Redo");
            redo.Image = Image.FromFile("Redo.ico");
            redo.Click += Redo_Click;
            redo.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Z;
            editItem.DropDownItems.Add(redo);

            menu.Items.Add(editItem);
        }
        /// <summary>
        /// Method for repeating the previous action.
        /// </summary>
        private void Redo_Click(object sender, EventArgs e)
        {
            (FirstPage.SelectedTab.Controls[0] as RichTextBox).Redo();
        }
        /// <summary>
        /// Method for canceling an action.
        /// </summary>
        private void Undo_Click(object sender, EventArgs e)
        {
            (FirstPage.SelectedTab.Controls[0] as RichTextBox).Undo();
        }
        /// <summary>
        /// Method for setting the app theme.
        /// </summary>
        void The_Theme(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem.CheckState == CheckState.Checked)
            {
                Dark_Theme();
            }
            else if (menuItem.CheckState == CheckState.Unchecked)
            {
                // By storing the user's choice in a file.
                File.WriteAllText("Settings.txt", $"false\n{Saved_Settings[1]}\n{Saved_Settings[2]}\n{Saved_Settings[3]}\n{Saved_Settings[4]}\n{Save_time}\n{Saved_Settings[6]}\n{Saved_Settings[7]}\n{Saved_Settings[8]}\n{Saved_Settings[9]}\n{Log_time}\n");
                theme = Color.White;
                menu.BackColor = Color.AliceBlue;
                menu.ForeColor = Color.Black;
                tableLayoutPanel1.BackColor = Color.AliceBlue;
                foreach (TabPage i in Saved_Files.Keys)
                {
                    (i.Controls[0] as RichTextBox).BackColor = theme;
                    (i.Controls[0] as RichTextBox).ForeColor = Color.Black;
                }
                New.BackColor = Color.AliceBlue;
                TextBox.BackColor = Color.White;
                TextBox.ForeColor = Color.Black;
                this.ForeColor = Color.Black;
                this.ForeColor = Color.AliceBlue;
            }
        }
        /// <summary>
        /// Method for setting the app's dark theme.
        /// </summary>
        void Dark_Theme()
        {
            File.WriteAllText("Settings.txt", $"true\n{Saved_Settings[1]}\n{Saved_Settings[2]}\n{Saved_Settings[3]}\n{Saved_Settings[4]}\n{Save_time}\n{Saved_Settings[6]}\n{Saved_Settings[7]}\n{Saved_Settings[8]}\n{Saved_Settings[9]}\n{Log_time}\n");
            theme = Color.FromArgb(32, 32, 32);
            this.BackColor = Color.Black;
            menu.BackColor = Color.Black;
            menu.ForeColor = Color.White;
            tableLayoutPanel1.BackColor = Color.Black;
            foreach (TabPage i in Saved_Files.Keys)
            {
                (i.Controls[0] as RichTextBox).BackColor = theme;
                (i.Controls[0] as RichTextBox).ForeColor = Color.White;
            }
            TextBox.ForeColor = Color.White;
        }
        /// <summary>
        /// Method for auto-saving.
        /// </summary>
        void Autosave_Changed(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem.CheckState == CheckState.Checked)
            {
                if (menuItem.Text == "Every 30 seconds")
                {
                    if (menuItem.Text == "Every 30 seconds")
                    {
                        Min1.CheckState = CheckState.Unchecked;
                        Min5.CheckState = CheckState.Unchecked;
                        Min15.CheckState = CheckState.Unchecked;
                        File.WriteAllText("Settings.txt", $"{Сheck}\ntrue\nfalse\nfalse\nfalse\n30000\n{Saved_Settings[6]}\n{Saved_Settings[7]}\n{Saved_Settings[8]}\n{Saved_Settings[9]}\n{Log_time}\n");
                        Save_time = 1000 * 30;
                    }
                }
                else if (menuItem.Text == "Every 1 minute")
                {
                    Sec30.CheckState = CheckState.Unchecked;
                    Min5.CheckState = CheckState.Unchecked;
                    Min15.CheckState = CheckState.Unchecked;
                    File.WriteAllText("Settings.txt", $"{Сheck}\nfalse\ntrue\nfalse\nfalse\n60000\n{Saved_Settings[6]}\n{Saved_Settings[7]}\n{Saved_Settings[8]}\n{Saved_Settings[9]}\n{Log_time}\n");
                    Save_time = 1000 * 60;
                }
                else if (menuItem.Text == "Every 5 minutes")
                {
                    Sec30.CheckState = CheckState.Unchecked;
                    Min1.CheckState = CheckState.Unchecked;
                    Min15.CheckState = CheckState.Unchecked;
                    File.WriteAllText("Settings.txt", $"{Сheck}\nfalse\nfalse\ntrue\nfalse\n300000\n{Saved_Settings[6]}\n{Saved_Settings[7]}\n{Saved_Settings[8]}\n{Saved_Settings[9]}\n{Log_time}\n");
                    Save_time = 1000 * 60 * 5;
                }
                else if (menuItem.Text == "Every 15 minutes")
                {
                    Sec30.CheckState = CheckState.Unchecked;
                    Min1.CheckState = CheckState.Unchecked;
                    Min5.CheckState = CheckState.Unchecked;
                    File.WriteAllText("Settings.txt", $"{Сheck}\nfalse\nfalse\nfalse\ntrue\n900000\n{Saved_Settings[6]}\n{Saved_Settings[7]}\n{Saved_Settings[8]}\n{Saved_Settings[9]}\n{Log_time}\n");
                    Save_time = 1000 * 60 * 15;
                }
                else
                {
                    Sec30.CheckState = CheckState.Unchecked;
                    Min1.CheckState = CheckState.Unchecked;
                    Min5.CheckState = CheckState.Unchecked;
                    Min15.CheckState = CheckState.Unchecked;
                    Save_time = 0;
                }
            }
        }
        /// <summary>
        /// Method for determining the time for backup.
        /// </summary>
        void Logging_Changed(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem.CheckState == CheckState.Checked)
            {
                if (menuItem.Text == "Every 30 seconds")
                {
                    if (menuItem.Text == "Every 30 seconds")
                    {
                        Min1log.CheckState = CheckState.Unchecked;
                        Min5log.CheckState = CheckState.Unchecked;
                        Min15log.CheckState = CheckState.Unchecked;
                        File.WriteAllText("Settings.txt", $"{Сheck}\n{Saved_Settings[1]}\n{Saved_Settings[2]}\n{Saved_Settings[3]}\n{Saved_Settings[4]}\n{Save_time}\ntrue\nfalse\nfalse\nfalse\n30000\n");
                        Log_time = 1000 * 30;
                    }
                }
                else if (menuItem.Text == "Every 1 minute")
                {
                    Sec30log.CheckState = CheckState.Unchecked;
                    Min5log.CheckState = CheckState.Unchecked;
                    Min15log.CheckState = CheckState.Unchecked;
                    File.WriteAllText("Settings.txt", $"{Сheck}\n{Saved_Settings[1]}\n{Saved_Settings[2]}\n{Saved_Settings[3]}\n{Saved_Settings[4]}\n{Save_time}\nfalse\ntrue\nfalse\nfalse\n60000\n");
                    Log_time = 1000 * 60;
                }
                else if (menuItem.Text == "Every 5 minutes")
                {
                    Sec30log.CheckState = CheckState.Unchecked;
                    Min1log.CheckState = CheckState.Unchecked;
                    Min15log.CheckState = CheckState.Unchecked;
                    File.WriteAllText("Settings.txt", $"{Сheck}\n{Saved_Settings[1]}\n{Saved_Settings[2]}\n{Saved_Settings[3]}\n{Saved_Settings[4]}\n{Save_time}\nfalse\nfalse\ntrue\nfalse\n300000\n");
                    Log_time = 1000 * 60 * 5;
                }
                else if (menuItem.Text == "Every 15 minutes")
                {
                    Sec30log.CheckState = CheckState.Unchecked;
                    Min1log.CheckState = CheckState.Unchecked;
                    Min5log.CheckState = CheckState.Unchecked;
                    File.WriteAllText("Settings.txt", $"{Сheck}\n{Saved_Settings[1]}\n{Saved_Settings[2]}\n{Saved_Settings[3]}\n{Saved_Settings[4]}\n{Save_time}\nfalse\nfalse\nfalse\ntrue\n900000\n");
                    Log_time = 1000 * 60 * 15;
                }
                else
                {
                    Sec30log.CheckState = CheckState.Unchecked;
                    Min1log.CheckState = CheckState.Unchecked;
                    Min5log.CheckState = CheckState.Unchecked;
                    Min15log.CheckState = CheckState.Unchecked;
                    Log_time = 0;
                }
            }
        }
        /// <summary>
        /// Method that changes the format of the entire text in the tab.
        /// </summary>
        void Formating(object sender, EventArgs e)
        {
            //FontDialog font_dialog = new FontDialog();
            FontDialog.ShowColor = true;
            if (FontDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (TabPage i in Saved_Files.Keys)
                {
                    (i.Controls[0] as RichTextBox).Font = FontDialog.Font;
                    (i.Controls[0] as RichTextBox).ForeColor = FontDialog.Color;
                }
            }
        }
        /// <summary>
        /// Method for saving backups.
        /// </summary>
        void Backup(object sender, EventArgs e)
        {
            OpenFileDialog BackupFileDialog = new OpenFileDialog();
            string path = Directory.GetCurrentDirectory();
            if (Directory.Exists(Path.Combine(path, @"Logging")))
            {
                BackupFileDialog.InitialDirectory = Path.Combine(path, @"Logging");
            }
            if (BackupFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = BackupFileDialog.FileName;
                string fileText = File.ReadAllText(filename);
                var separator = Path.DirectorySeparatorChar;
                string[] all_name = filename.Split(separator);
                string[] formatic = filename.Split('.');
                FirstPage.SelectedTab.ForeColor = Color.Black;
                FirstPage.SelectedTab.Text = all_name[all_name.Length - 1] + "    ";
                if (formatic[formatic.Length - 1] == "rtf")
                {
                    (FirstPage.SelectedTab.Controls[0] as RichTextBox).LoadFile(filename);
                }
                else if (formatic[formatic.Length - 1] == "cs")
                {
                    (FirstPage.SelectedTab.Controls[0] as RichTextBox).LoadFile(filename, RichTextBoxStreamType.PlainText);
                }
                else
                {
                    Saved_Files[FirstPage.SelectedTab].Text = fileText;
                }
                if (!Checking.ContainsKey(FirstPage.SelectedTab))
                {
                    Checking.Add(FirstPage.SelectedTab, true);
                }
                // If the file has not yet been added to the dictionary, where the full paths to the file are stored, then we do this.
                if (!Names.ContainsValue(filename) && !Names.ContainsKey(FirstPage.SelectedTab))
                {
                    Names.Add(FirstPage.SelectedTab, filename);
                }

            }
        }
        /// <summary>
        /// Part of the actions to save the file.
        /// </summary>
        private void Saving()
        {
            FileName = SaveFileDialog.FileName;
            if (!Names.ContainsKey(FirstPage.SelectedTab))
            {
                Names.Add(FirstPage.SelectedTab, FileName);
            }
            var separator = Path.DirectorySeparatorChar;
            string[] all_name = FileName.Split(separator);
            string[] formatic = FileName.Split('.');
            FirstPage.SelectedTab.Text = all_name[all_name.Length - 1] + "  ";
            if (formatic[^1] == "rtf")
            {
                (FirstPage.SelectedTab.Controls[0] as RichTextBox).SaveFile(Names[FirstPage.SelectedTab]);
            }
            else if (formatic[^1] == "cs")
            {
                (FirstPage.SelectedTab.Controls[0] as RichTextBox).SaveFile(Names[FirstPage.SelectedTab], RichTextBoxStreamType.PlainText);
            }
            else
            {
                File.WriteAllText(Names[FirstPage.SelectedTab], Saved_Files[FirstPage.SelectedTab].Text);
            }
            Checking[FirstPage.SelectedTab] = true;
        }
        /// <summary>
        /// Last actions before closing the program.
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                foreach (TabPage i in Saved_Files.Keys)
                {
                    if (!Checking[i])
                    {
                        var dr = MessageBox.Show($"Wanna save this file: {i.Text}?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                        if (dr == DialogResult.Yes)
                        {
                            End_Saving(i);
                            if (!Saved_Settings.Contains(Names[i]))
                            {
                                using (StreamWriter writer = File.AppendText("Settings.txt"))
                                {
                                    writer.WriteLine(Names[i]);
                                }
                            }
                        }
                        else if (dr == DialogResult.No)
                        {
                            if (Names.ContainsKey(i) && Names.ContainsValue(Names[i]))
                            {
                                if (!Saved_Settings.Contains(Names[i]))
                                {
                                    using (StreamWriter writer = File.AppendText("Settings.txt"))
                                    {
                                        writer.WriteLine(Names[i]);
                                    }
                                }
                            }
                            continue;
                        }
                        else
                        {
                            // Cancel the closing of the application.
                            e.Cancel = true;
                        }

                    }
                    else if (!Checking[i])
                    {
                        if (!Saved_Settings.Contains(Names[i]))
                        {
                            using (StreamWriter writer = File.AppendText("Settings.txt"))
                            {
                                writer.WriteLine(Names[i]);
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong", "ERROR",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error,
                                                        MessageBoxDefaultButton.Button1,
                                                        MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        /// <summary>
        /// When the timer runs out of time, save the file.
        /// </summary>
        private void Save_Timer_Tick(object sender, EventArgs e)
        {
            Save_Timer.Stop();
            foreach (TabPage i in Saved_Files.Keys)
            {
                if (Names.ContainsKey(i))
                {
                    string[] formatic = Names[i].Split('.');
                    if (formatic[formatic.Length - 1] == "rtf")
                    {
                        (i.Controls[0] as RichTextBox).SaveFile(Names[i]);
                    }
                    else if (formatic[formatic.Length - 1] == "cs")
                    {
                        (FirstPage.SelectedTab.Controls[0] as RichTextBox).SaveFile(Names[FirstPage.SelectedTab], RichTextBoxStreamType.PlainText);
                    }
                    else
                    {
                        File.WriteAllText(Names[i], Saved_Files[i].Text);
                    }
                    Checking[i] = true;
                }
            }
            // Start the countdown for a new one.
            Save_Timer.Start();
        }
        /// <summary>
        /// When the timer runs out of time, creating back up files.
        /// </summary>
        private void Logging_Timer_Tick(object sender, EventArgs e)
        {
            Logging_Timer.Stop();
            foreach (TabPage i in Saved_Files.Keys)
            {
                if (Names.ContainsKey(i))
                {
                    string[] formatic = Names[i].Split('.');
                    string name = Path.GetFileNameWithoutExtension(Names[i]);
                    string date = ((DateTime.Now.ToString()).Replace(".", "")).Replace(":", "");
                    if (formatic[formatic.Length - 1] == "rtf")
                    {
                        (i.Controls[0] as RichTextBox).SaveFile($"Logging/{name}{date}.rtf");
                    }
                    else if (formatic[formatic.Length - 1] == "cs")
                    {
                        (i.Controls[0] as RichTextBox).SaveFile($"Logging/{name}{date}.cs", RichTextBoxStreamType.PlainText);
                    }
                    else
                    {
                        using (StreamWriter streamWriter = new StreamWriter($"Logging/{name}{date}.txt"))
                        {
                            streamWriter.WriteLine(Saved_Files[i].Text);
                        }
                    }
                    Checking[i] = true;
                }
            }
            // Start the countdown for a new one.
            Logging_Timer.Start();
        }
        /// <summary>
        /// Method for closing the tab.
        /// </summary>
        private void FirstPage_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.FirstPage.TabPages.Count; i++)
            {
                Rectangle r = FirstPage.GetTabRect(i);
                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
                if (closeButton.Contains(e.Location))
                {
                    if (Counter > 1 && MessageBox.Show($"Would you like to сlose {this.FirstPage.SelectedTab.Text}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var tapPage = this.FirstPage.TabPages;
                        Checking.Remove(this.FirstPage.SelectedTab);
                        Saved_Files.Remove(this.FirstPage.SelectedTab);
                        Names.Remove(this.FirstPage.SelectedTab);
                        tapPage.RemoveAt(i);
                        Counter--;
                        break;
                    }
                    else if (Counter <= 1)
                    {
                        MessageBox.Show("You can't close the last tab.", "ERROR",
                                                                                MessageBoxButtons.OK,
                                                                                MessageBoxIcon.Error,
                                                                                MessageBoxDefaultButton.Button1,
                                                                                MessageBoxOptions.DefaultDesktopOnly);

                    }
                }
            }
        }
        /// <summary>
        /// Method for creating a cross on tabs.
        /// </summary>
        private void FirstPage_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Add the icon of the cross as the closing element.
            Image image = Image.FromFile("cross.ico");
            e.Graphics.DrawImage(image, e.Bounds.Right - 20, e.Bounds.Top + 3);
            e.Graphics.DrawString(this.FirstPage.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 7, e.Bounds.Top + 3);
            e.DrawFocusRectangle();
        }
        /// <summary>
        /// The method that is called when the text in the RichTextBox changes.
        /// </summary>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            // Saying that there are unsaved changes in the file.
            Checking[FirstPage.SelectedTab] = false;
        }
    }
}
