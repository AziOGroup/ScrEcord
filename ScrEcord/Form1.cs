using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

using HongliangSoft.Utilities.Gui;

namespace ScrEcord
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 初クリック
        /// </summary>
        bool firstClick = true;
        bool captureMode = false;

        /// <summary>
        /// キャプチャー位置
        /// </summary>
        Rectangle captureRect = new Rectangle(0, 0, 1, 1);

        Bitmap canvas;
        Graphics g;
        Pen p;
        Point cursor;

        KeyboardHook kbh = new KeyboardHook();

        string path = "", type = "";

        public Form1()
        {
            InitializeComponent();

            //プライマリスクリーンの解像度に合わせる
            Size = Screen.PrimaryScreen.Bounds.Size;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y);

            canvas = new Bitmap(Size.Width, Size.Height);
            g = Graphics.FromImage(canvas);
            p = new Pen(Color.White, 1);

            this.TransparencyKey = Color.White;
            this.Opacity = 0.7;

            TopMost = true;

            ReLoad();

            kbh.KeyboardHooked += new KeyboardHookedEventHandler(KeyHook);

        }

        /// <summary>
        /// マウスが押されたときに動作します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (captureMode)
            {
                //初めてマウスが押されたか
                if (firstClick && e.Button == MouseButtons.Left)
                {
                    cursor = PointToClient(MousePosition);
                    captureRect.X = cursor.X;
                    captureRect.Y = cursor.Y;
                    firstClick = false;
                }

                if (e.Button == MouseButtons.Right)
                {
                    firstClick = true;
                    Reset();
                }
            }

            else if (e.Button == MouseButtons.Right)
            {
                Reset();
            }
        }


        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (captureMode)
            {
                //マウス押下状態をリセット
                if (!firstClick && e.Button == MouseButtons.Left)
                {
                    firstClick = true;

                    if (captureRect.Width <= 0 || captureRect.Height <= 0)
                    {
                        return;
                    }


                    Bitmap bmp = new Bitmap(captureRect.Width, captureRect.Height);
                    Graphics grp = Graphics.FromImage(bmp);
                    grp.CopyFromScreen(
                        new Point(Screen.PrimaryScreen.Bounds.X + captureRect.X, Screen.PrimaryScreen.Bounds.Y + captureRect.Y),
                        new Point(0, 0),
                        bmp.Size);

                    grp.Dispose();

                    ReLoad();
                    if (MessageBox.Show("保存しますか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DateTime nowTime = DateTime.Now;
                        bmp.Save($"{path}\\{nowTime.ToString("yyyy-MM-dd hh-mm-ss")}.{type}");

                        (new ToastText($"{path}\\{nowTime.ToString("yyyy-MM-dd hh-mm-ss")}.{type} に保存しました", ToastTemplateType.ToastText01)).Show();
                    }

                    Reset();
                }
            }
        }

        //切り取り領域コピー
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!firstClick && captureMode)
            {

                g.Clear(Color.Transparent);
                cursor = PointToClient(MousePosition);

                captureRect.Width = cursor.X - captureRect.X;
                captureRect.Height = cursor.Y - captureRect.Y;
                g.FillRectangle(Brushes.White, captureRect);
                pictureBox1.Image = canvas;

            }
        }

        //ESC押下時アプリケーション終了
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                captureMode = false;
            }
        }

        private void 終了EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Dispose();
            this.Close();
            Application.Exit();
        }



        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon1.Dispose();
            Application.Exit();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                captureMode = true;
                timer1.Enabled = true;
                Show();
            }
        }

        private void 保存先の変更ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Form2()).Show();
        }


        /// <summary>
        /// 一度キャプチャをリセットします
        /// </summary>
        private void Reset()
        {
            firstClick = true;
            canvas = new Bitmap(Size.Width, Size.Height);
            g = Graphics.FromImage(canvas);
            p = new Pen(Color.White, 1);
            Rectangle captureRect = new Rectangle(0, 0, 1, 1);
            pictureBox1.Image = canvas;
            captureMode = false;
            timer1.Enabled = false;

            Update();
            Hide();
        }

        /// <summary>
        /// プロファイルをロードし変数に格納します
        /// </summary>
        private void ReLoad()
        {
            Reset();
            try
            {
                if (System.IO.File.Exists("Path.ini"))
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader("Path.ini");
                    string sline = "";
                    while (!sr.EndOfStream)
                    {
                        sline = sr.ReadLine();
                        if (sline.IndexOf("Type=") >= 0) type = sline.Replace("Type=", "");
                        if (sline.IndexOf("Path=") >= 0) path = sline.Replace("Path=", "");
                    }
                    if (type == string.Empty) type = "png";
                    if (path == string.Empty) path = $"{System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)}\\ScrEcord";
                    sr.Close();
                }
                else
                {
                    //Path.iniがなかったため新規作成
                    MessageBox.Show("初回起動または設定ファイルが見つからなかったため、初期設定を行ってください");
                    (new Form2()).ShowDialog();
                    ReLoad();
                }

                if (!System.IO.Directory.Exists(path))
                {
                    //ディレクトリがない場合
                    System.IO.Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しましたマイピクチャ下に保存します\r\n" + ex.Message);
                path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\ScrEcord";
                if (!System.IO.Directory.Exists(path))
                {
                    //ディレクトリがない場合
                    System.IO.Directory.CreateDirectory(path);
                }

            }

        }

        private void KeyHook(object sender, KeyboardHookedEventArgs e)
        {

            if (e.KeyCode == Keys.PrintScreen && !captureMode)
            {
                captureMode = true;
                timer1.Enabled = true;
                Show();

            }

            if (captureMode && e.KeyCode == Keys.Escape)
            {
                Reset();
            }

        }

    }



    //トースト用XML生成クラス
    public class ToastText
    {
        XmlDocument xmlDoc;
        public ToastText(string message, ToastTemplateType type)
        {
            xmlDoc = ToastNotificationManager.GetTemplateContent(type);
            var textTag = xmlDoc.GetElementsByTagName("text").First();
            textTag.AppendChild(xmlDoc.CreateTextNode(message));

        }

        public void Show()
        {
            var notifier = ToastNotificationManager.CreateToastNotifier("Notice");
            notifier.Show(new ToastNotification(xmlDoc));
        }
    }
}
