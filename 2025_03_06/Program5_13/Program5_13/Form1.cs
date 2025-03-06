using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Program5_13
{
    public partial class Form1 : Form
    {
        private int count;
        private SaveFileDialog saveFile; // 修改這一行

        public Form1()
        {
            InitializeComponent();
            // 修改元件的 Text 屬性
            this.Text = "表單1";
            button1.Text = "按鈕1";
            // 如果有其他元件，請依照需要添加
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            StreamWriter outputFile;//宣告StreamWriter物件
            int num;//宣告整數變數    


            try
            {
                if (saveFile.ShowDialog() == DialogResult.OK) 
                { 
                outputFile = File.CreateText("numbers.txt");//開啟檔案,寫入數字
                if (int.TryParse(textBox1.Text, out count))//判斷是否為數字
                {
                    for (int i = 0; i < count; i++)//迴圈產生亂數
                    {
                        outputFile.WriteLine(rand.Next(100));//寫入亂數
                    }
                    outputFile.Close();//關閉檔案
                    MessageBox.Show("檔案已經建立");//顯示訊息
                }
                else
                {
                    MessageBox.Show("請輸入數字");//顯示訊息
             
                }

                }
                else
                {
                    MessageBox.Show("你按下取消");//顯示訊息
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);//顯示錯誤訊息
            }
        }
    }
}