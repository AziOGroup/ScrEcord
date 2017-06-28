using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace ScrEcord2
{
    /// <summary>
    /// INIを操作、管理するクラスです
    /// </summary>
    class INIControl
    {
        public Dictionary<string, string> INIData = new Dictionary<string, string>();

        /// <summary>
        /// 引数なしコンストラクタは禁止されています
        /// </summary>
        private INIControl() { }

        /// <summary>
        /// 操作対象のファイル名を取得します
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// 指定したINIファイルを読み込みまたは作成します、["Name"]で読み込み、["Name"] = data で追記します
        /// </summary>
        /// <param name="IniName">拡張子を含めたINIファイル名、相対・絶対パスどちらも使用できます。</param>
        public INIControl(string IniName)
        {
            //ファイルチェック
            if (!File.Exists(IniName))
            {
                //ディレクトリチェック
                if (!Directory.Exists(Path.GetDirectoryName(IniName)) && Path.GetDirectoryName(IniName) != string.Empty)
                {
                    //該当ディレクトリがない場合作成
                    Directory.CreateDirectory(Path.GetDirectoryName(IniName));
                }
                //該当INIがない場合作成
                var sw = new StreamWriter(IniName);
                sw.Close();
            }

            //INIファイル読み込み開始
            var sr = new StreamReader(IniName);
            FileName = IniName;

            while (!sr.EndOfStream)
            {
                string sline = sr.ReadLine();
                if (sline.IndexOf("=") >= 0)
                {
                    string[] splitStr = sline.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                    //要素数によって処理を変更
                    switch (splitStr.Length)
                    {
                        case 0:
                            break;
                        case 1:
                            INIData.Add(splitStr[0].ToLower(), "");
                            break;
                        case 2:
                        case 3:
                            INIData.Add(splitStr[0].ToLower(), splitStr[1]);
                            break;
                        default:
                            break;
                    }
                }
            }
            sr.Close();

        }

        /// <summary>
        /// 格納されたKeyからデータを取得します、書き込んだ場合は即座ファイルに反映されます
        /// </summary>
        /// <param name="keyName">キーの名前を指定します</param>
        /// <returns>存在しないキーを指定した場合はString.Emptyが返されます</returns>
        public string this[object keyName]
        {
            set
            {
                //INIData.Add(keyName.ToString(), value.ToString());

                INIData[keyName.ToString().ToLower()] = value.ToString();
                Save();
            }
            get
            {
                if (INIData.ContainsKey(keyName.ToString().ToLower()))
                {
                    return INIData[keyName.ToString().ToLower()];
                }

                return "";
            }
        }

        /// <summary>
        /// [内部用関数です]現在のINIの状態をファイルに保存します。
        /// </summary>
        /// <returns>正常に終了した場合true, 失敗した場合falseを返します</returns>
        protected bool Save()
        {
            try
            {
                StreamWriter sw = new StreamWriter(FileName, false);

                foreach (var item in INIData)
                {
                    sw.WriteLine($"{item.Key.ToLower()}={item.Value}");
                }

                sw.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 現在操作対象のINIから該当するキーを削除します。
        /// </summary>
        /// <returns>正常に終了した場合true, 失敗した場合falseを返します</returns>
        public bool Remove(string keyName)
        {
            bool result = INIData.Remove(keyName.ToLower());
            Save();

            return result;
        }

    }
}
