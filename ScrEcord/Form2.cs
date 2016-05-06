using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace ScrEcord
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            if (File.Exists("Path.ini"))
            {
                StreamReader sr = new StreamReader("Path.ini");
                string type = "", path = "", sline = "";
                while (!sr.EndOfStream)
                {
                    sline = sr.ReadLine();

                    if (sline.IndexOf("Type=") >= 0) type = sline.Replace("Type=", "");
                    if (sline.IndexOf("Path=") >= 0) path = sline.Replace("Path=", "");
                }
                comboBox1.Text = type;
                textBox1.Text = path;

                sr.Close();
            }
            else
            {
                //初期設定
                textBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                comboBox1.Text = "png";

                StreamWriter sw = new StreamWriter("Path.ini", false);
                sw.WriteLine($"Path={textBox1.Text}");
                sw.WriteLine($"Type={comboBox1.Text}");
                sw.Close();


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.SelectedPath = textBox1.Text;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!File.Exists("Path.ini"))
            {
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("Path.ini", false);
            sw.WriteLine($"Path={textBox1.Text}");
            sw.WriteLine($"Type={comboBox1.Text}");
            sw.Close();
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //フォルダを開く
            if (Directory.Exists(textBox1.Text))
                System.Diagnostics.Process.Start(textBox1.Text);
        }
    }
}
