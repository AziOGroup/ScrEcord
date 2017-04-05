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
    public partial class Form1 : Form
    {
        /// <summary>
        /// ディスプレイ座標のカーソルを取得します
        /// </summary>
        public Point DisplayCursor
        {
            get
            {
                return PointToScreen(Cursor.Position);
            }
        }

        /// <summary>
        /// 現在マウスカーソルがあるディスプレイのRectを取得します
        /// </summary>
        public Rectangle CurrentDisplayRect
        {
            get
            {
                return Screen.FromPoint(DisplayCursor).Bounds;
            }
        }

        /// <summary>
        /// マウスが一度だけ押下された状態を記録します。押下しなおすとfalseに戻ります
        /// </summary>
        private bool isClick = false;

        private INIControl settings = new INIControl("Settings.ini");

        public Form1()
        {
            InitializeComponent();

            this.TopMost = true;


            CheckINI();
            Reset();
        }

        /// <summary>
        /// 各種初期設定
        /// </summary>
        /// <param name="isHide">falseを渡した場合ウィンドウを隠しません</param>
        private void Reset(bool isHide = true)
        {
            //PictureBoxの初期化
            pictureBox1.BackColor = Color.Pink;
            pictureBox1.Size = new Size(0, 0);
            pictureBox1.Location = new Point(0, 0);

            //Formの初期化
            this.TransparencyKey = Color.Pink;
            this.Location = new Point(0, 0);
            this.Size = CurrentDisplayRect.Size;
            this.Location = CurrentDisplayRect.Location;
            this.Opacity = 0.5f;
            this.BackColor = Color.FromArgb(255, 30, 30, 30);
            this.Refresh();

            if (isHide) this.Hide();


            //変数の初期化
            isClick = false;
            firstPoint = new Point(0, 0);
        }

        /// <summary>
        /// INIファイルをチェックし、INI管理クラスを準備します
        /// </summary>
        private void CheckINI()
        {
            if (!settings.INIData.ContainsKey("FileType"))
            {
                settings["FileType"] = "png";
            }

            if (!settings.INIData.ContainsKey("FilePath"))
            {
                settings["FilePath"] = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)}\\ScrEcord";
            }

            if (!settings.INIData.ContainsKey(""))
            {
                settings["SplitDirectory"] = "true";
            }

        }

        /// <summary>
        /// マウス押下時の処理、起点座標の記録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !isClick)
            {
                isClick = true;
                pictureBox1.Location = PointToClient(Cursor.Position);

                firstPoint = Cursor.Position;
            }
        }

        Point firstPoint = new Point();

        /// <summary>
        /// マウスアップ時の処理、isClickの初期化とキャプチャ処理の開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isClick)
            {
                isClick = false;

                //左クリックじゃない場合キャンセル処理を行う
                if (e.Button != MouseButtons.Left)
                {
                    Reset();
                    return;
                }

                //保存処理↓
                CaptureScreen();
                Reset();

                //クリップボード転送 or エクスプローラを開く
                if (settings["clipboard"] == "true") Clipboard.SetText(settings["FilePath"]);

                if (settings["explore"] == "true") System.Diagnostics.Process.Start(settings["FilePath"]);


            }
        }

        /// <summary>
        /// クリック押下時にPictureBoxのリサイズを行います
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveTimer_Tick(object sender, EventArgs e)
        {
            if (isClick)
            {
                if (Cursor.Position.X > firstPoint.X && Cursor.Position.Y > firstPoint.Y)
                {
                    pictureBox1.Size = new Size(
                    PointToClient(Cursor.Position).X - pictureBox1.Location.X,
                    PointToClient(Cursor.Position).Y - pictureBox1.Location.Y);
                }
                if (Cursor.Position.X < firstPoint.X || Cursor.Position.Y < firstPoint.Y)
                {
                    pictureBox1.Location = new Point(PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y);
                    pictureBox1.Size = new Size(
                        Math.Abs(pictureBox1.Location.X - PointToClient(firstPoint).X),
                        Math.Abs(pictureBox1.Location.Y - PointToClient(firstPoint).Y));
                }
            }
        }

        /// <summary>
        /// 現在のPictureBoxの状態をもとに画面キャプチャを行います
        /// </summary>
        private void CaptureScreen()
        {
            //サイズが0の場合はreturn
            if (pictureBox1.Width <= 0 || pictureBox1.Height <= 0) return;

            //出力処理
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics grp = Graphics.FromImage(bmp);
            grp.CopyFromScreen(
                new Point(CurrentDisplayRect.X + pictureBox1.Location.X, CurrentDisplayRect.Y + pictureBox1.Location.Y),
                new Point(0, 0),
                bmp.Size);

            grp.Dispose();

            Reset();


            //保存処理
            if (MessageBox.Show($"保存しますか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DateTime nowTime = DateTime.Now;

                if (settings["SplitDirectory"] == "false")
                {
                    if (!System.IO.Directory.Exists($"{settings["FilePath"]}")) System.IO.Directory.CreateDirectory($"{settings["FilePath"]}");
                    bmp.Save($"{settings["FilePath"]}\\{nowTime.ToString("yyyyMMdd_HHmmss")}.{settings["FileType"]}");
                }
                else
                {
                    if (!System.IO.Directory.Exists($"{settings["FilePath"]}\\{nowTime.ToString("yyyy-MM")}")) System.IO.Directory.CreateDirectory($"{settings["FilePath"]}\\{nowTime.ToString("yyyy-MM")}");
                    bmp.Save($"{settings["FilePath"]}\\{nowTime.ToString("yyyy-MM")}\\{nowTime.ToString("yyyyMMdd_HHmmss")}.{settings["FileType"]}");
                }
            }


        }

        /// <summary>
        /// グローバルキーフック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keyboardHook1_KeyboardHooked(object sender, HongliangSoft.Utilities.Gui.KeyboardHookedEventArgs e)
        {
            if (e.KeyCode == Keys.PrintScreen)
            {
                Reset(false);
                this.Show();
            }

            if (e.KeyCode == Keys.Escape)
            {
                Reset();
            }
        }

        /// <summary>
        /// アプリケーションを終了します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 終了EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Dispose();
            Application.Exit();
        }

        /// <summary>
        /// フォームが閉じられるときに呼び出されます（notifyの後処理等）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon1.Dispose();
            Application.Exit();
        }

        /// <summary>
        /// 通知アイコンがダブルクリックされた時にキャプチャ開始します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Reset(false);
            this.Show();
        }

        private void 環境設定SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Setting().ShowDialog();

            //設定ファイルの見直し
            settings = new INIControl("Settings.ini");
        }
    }
}
