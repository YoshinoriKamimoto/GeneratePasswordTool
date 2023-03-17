using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace パスワード自動生成ツール
{
    public partial class Form1 : Form
    {
        // コンストラクタ
        public Form1()
        {
            InitializeComponent();
        }

        // パスワード生成ボタンClickイベント
        private void GeneratePasswordButton_Click(object sender, EventArgs e)
        {
            NewPasswordTextBox.Clear();

            // 生成するパスワードの文字数をテキストボックスから取得
            string wordCntStr = WordCountTextBox.Text;

            // 値を整数変換
            int wordCnt;
            if (int.TryParse(wordCntStr, out wordCnt) == false)
            {
                MessageBox.Show("文字数は数字で入力してください。",
                                "入力値不正",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                WordCountTextBox.Focus();
                WordCountTextBox.SelectAll();
                return;
            }

            // 数値をチェック
            if (wordCnt < 4 || wordCnt > 32)
            {
                MessageBox.Show("文字数は4～32文字で入力してください。",
                                "入力値不正",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                WordCountTextBox.Focus();
                WordCountTextBox.SelectAll();
                return;
            }

            // パスワードを生成
            StringBuilder sb = GeneratePassword(wordCnt);


            // 画面に生成したパスワードを表示
            NewPasswordTextBox.Text = sb.ToString();
            MessageBox.Show("パスワードを生成しました。",
                            "完了",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            WordCountTextBox.Clear();

        }


        // パスワード生成メソッド
        private StringBuilder GeneratePassword(int wordCnt)
        {
            // 生成したパスワードを保持する
            StringBuilder sb = new StringBuilder();

            // 数字、大文字英字、小文字英字、記号の最低文字数を設定
            int numberCnt = 1;
            int upperAlphabetCnt = 1;
            int lowrAlphabetCnt = 1;
            int symbolAlphabetCnt = 1;

            // 入力された文字数から余剰文字数を計算
            int extraCnt = wordCnt - (numberCnt + upperAlphabetCnt + lowrAlphabetCnt + symbolAlphabetCnt);

            // 余剰文字数を、乱数を使用して各要素にランダムに配分
            if (extraCnt > 0)
            {
                for (int i = 0; i < extraCnt; i++)
                {
                    int n = GetRandomNumbers(4);
                    if (n == 0)
                    {
                        numberCnt++;
                    }
                    else if (n == 1)
                    {

                        upperAlphabetCnt++;
                    }
                    else if (n == 2)
                    {
                        lowrAlphabetCnt++;
                    }
                    else if (n == 3)
                    {
                        symbolAlphabetCnt++;
                    }
                }
            }

            // 設定された文字数分、各要素の値をランダムに取得
            // 数字を取得
            for (int i = 0; i < numberCnt; i++)
            {
                int n = GetRandomNumbers(10);

                // 値を追加
                sb.Append(n.ToString());
            }

            // 大文字英字を取得
            for (int i = 0; i < upperAlphabetCnt; i++)
            {
                string word = GetRandomUppperAlphabet();

                // 値を追加
                sb.Append(word);
            }

            // 小文字英字を取得
            for (int i = 0; i < lowrAlphabetCnt; i++)
            {
                string word = GetRandomLowerAlphabet();

                // 値を追加
                sb.Append(word);
            }

            // 記号を取得
            for (int i = 0; i < symbolAlphabetCnt; i++)
            {
                string word = GetRandomSymbol();

                // 値を追加
                sb.Append(word);
            }


            // 生成したパスワードを返却
            return sb;
        }


        // ランダムな数字を取得するメソッド
        private int GetRandomNumbers(int maxValue)
        {
            // ランダムオブジェクトをインスタンス化
            Random random = new Random();

            // 0~指定された上限値までのランダムな数字を取得
            int num = random.Next(maxValue);

            return num;
        }


        // ランダムな大文字アルファベットを取得するメソッド
        private string GetRandomUppperAlphabet()
        {
            // 使用する大文字アルファベットを定義
            string[] array = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            // 配列にアクセスするための添え字を乱数で取得
            int index = GetRandomNumbers(array.Length);

            // 配列から値を取得
            string alphabet = array[index];

            return alphabet;
        }


        // ランダムな小文字アルファベットを取得するメソッド
        private string GetRandomLowerAlphabet()
        {
            // 使用する小文字アルファベットを定義
            string[] array = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

            // 配列にアクセスするための添え字を乱数で取得
            int index = GetRandomNumbers(array.Length);

            // 配列から値を取得
            string alphabet = array[index];

            return alphabet;
        }


        // ランダムな記号を取得するメソッド
        private string GetRandomSymbol()
        {
            // 使用する記号を定義
            string[] array = { "_", "!", "&", "%", "?", "$" };

            // 配列にアクセスするための添え字を乱数で取得
            int index = GetRandomNumbers(array.Length);

            // 配列から値を取得
            string symbol = array[index];

            return symbol;
        }
    }
}
