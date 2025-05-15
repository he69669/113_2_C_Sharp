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

namespace CSV_Reader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void getScoresButton_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader inputFile = null; // Initialize the variable to avoid CS0165  
                                               //顯示檔案選擇對話方塊,讓使用者選擇要啟動的csv檔案  
                string line; //儲存讀取的每一行資料  
                int count = 0;
                int total = 0;
                double average;

                char[] delim = { ',', ':' }; // 定義分隔符號為逗號  
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //使用者選擇檔案後,開啟該檔案已供後讀取  
                    inputFile = File.OpenText(openFileDialog1.FileName);

                    while (!inputFile.EndOfStream)
                    {
                        //讀取每一列資料  
                        line = inputFile.ReadLine();
                        line = line.Trim(); //去除行首行尾的空白字元  
                        string[] tokens = line.Split(delim); //將讀取的行資料分割成字串陣列  

                        for (int i = 1; i < tokens.Length; i++)
                        {
                            //將字串陣列中的每一個字串轉換成整數,並累加計算總和與計數  
                            total += int.Parse(tokens[i]);
                        }
                        average = (double)total / (tokens.Length-1); //計算平均值  
                                                                 //將計算的平均值顯示在Label上  
                        averagesListBox.Items.Add(tokens[0] +"的平均分數為:" + average.ToString("F2")); //顯示平均值,保留兩位小數  
                    }
                }
                else
                {
                    //使用者未選擇任何檔案時,顯示提示訊息  
                    MessageBox.Show("未選擇任何檔案");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("發生錯誤: " + ex.Message);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Close the form.  
            this.Close();
        }
    }
}
