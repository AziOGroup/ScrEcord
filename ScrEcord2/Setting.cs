using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrEcord2
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }

        private INIControl settings = new INIControl("Settings.ini");

        private void Setting_Load(object sender, EventArgs e)
        {
            browseTextbox.Text = settings["FilePath"];
            filetypeCombobox.Text = settings["FileType"];

            if (settings["clipboard"] == "true") clipboardCheckbox.CheckState = CheckState.Checked;
            if (settings["explore"] == "true") exploreCheckbox.CheckState = CheckState.Checked;
            if (settings["SplitDirectory"] == "false") splitCheckbox.CheckState = CheckState.Unchecked;
            if (settings["override"] == "true") overrideCheckbox.CheckState = CheckState.Checked;
        }

        private void saveCloseButton_Click(object sender, EventArgs e)
        {
            settings["FilePath"] = browseTextbox.Text;
            settings["FileType"] = filetypeCombobox.Text;
            settings["clipboard"] = clipboardCheckbox.Checked ? "true" : "false";
            settings["explore"] = exploreCheckbox.Checked ? "true" : "false";
            settings["SplitDirectory"] = splitCheckbox.Checked ? "true" : "false";
            settings["override"] = overrideCheckbox.Checked ? "true" : "false";
            Close();
        }

        private void exploreButton_Click(object sender, EventArgs e)
        {
                System.Diagnostics.Process.Start(browseTextbox.Text);
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.SelectedPath = browseTextbox.Text;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                browseTextbox.Text = fbd.SelectedPath;
            }

        }
    }
}
