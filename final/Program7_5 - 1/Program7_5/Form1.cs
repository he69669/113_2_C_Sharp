using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Program7_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 使用 List 來存放球隊與冠軍資料
        List<string> teamList = new List<string>();
        List<string> winnerList = new List<string>();

        // 儲存檔案路徑
        string teamsFilePath = "";
        string winnersFilePath = "";

        /// <summary>
        /// 表單載入事件，初始化並讀取球隊與冠軍資料
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // 提示使用者先選擇球隊資料檔案
            MessageBox.Show("請先選擇球隊資料檔案", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            OpenFileDialog openTeamsDialog = new OpenFileDialog();
            openTeamsDialog.Title = "請選擇球隊資料檔 (Teams.txt)";
            openTeamsDialog.Filter = "文字檔案 (*.txt)|*.txt|所有檔案 (*.*)|*.*";
            if (openTeamsDialog.ShowDialog() == DialogResult.OK)
            {
                teamsFilePath = openTeamsDialog.FileName;
            }
            else
            {
                MessageBox.Show("未選擇球隊資料檔，程式將結束。");
                this.Close();
                return;
            }

            // 提示使用者再選擇世界大賽資料檔案
            MessageBox.Show("再選擇世界大賽冠軍資料檔案", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            OpenFileDialog openWinnersDialog = new OpenFileDialog();
            openWinnersDialog.Title = "請選擇冠軍資料檔 (WorldSeries.txt)";
            openWinnersDialog.Filter = "文字檔案 (*.txt)|*.txt|所有檔案 (*.*)|*.*";
            if (openWinnersDialog.ShowDialog() == DialogResult.OK)
            {
                winnersFilePath = openWinnersDialog.FileName;
            }
            else
            {
                MessageBox.Show("未選擇冠軍資料檔，程式將結束。");
                this.Close();
                return;
            }

            // 讀取檔案內容
            readTeams();
            readWinner();
        }

        /// <summary>
        /// 讀取 Teams.txt 檔案，將所有球隊名稱加入 listBox1，並存入 teamList
        /// </summary>
        private void readTeams()
        {
            try
            {
                teamList.Clear();
                listBox1.Items.Clear();
                using (StreamReader inputFile = File.OpenText(teamsFilePath))
                {
                    string line;
                    // 逐行讀取 Teams.txt，將每一行加入 teamList 與 listBox1
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        teamList.Add(line);
                        listBox1.Items.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                // 發生例外時，顯示錯誤訊息（繁體中文）
                MessageBox.Show("讀取球隊資料時發生錯誤：" + ex.Message);
            }
        }

        /// <summary>
        /// 讀取 WorldSeries.txt 檔案，將所有冠軍球隊名稱存入 winnerList
        /// </summary>
        private void readWinner()
        {
            try
            {
                winnerList.Clear();
                using (StreamReader inputFile = File.OpenText(winnersFilePath))
                {
                    string line;
                    // 逐行讀取 WorldSeries.txt，將每一行加入 winnerList
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        winnerList.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                // 發生例外時，顯示錯誤訊息（繁體中文）
                MessageBox.Show("讀取冠軍資料時發生錯誤：" + ex.Message);
            }
        }

        /// <summary>
        /// 當使用者在 listBox1 選取球隊時，計算該球隊奪冠次數並顯示於 label1，並列出奪冠年份
        /// </summary>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = listBox1.SelectedItem.ToString();
            int numWin = 0;
            List<int> winYears = new List<int>();
            int startYear = 1903;
            int year = startYear;

            // MLB 世界大賽未舉辦的年份（1918、1994）
            HashSet<int> skipYears = new HashSet<int> { 1904, 1994 };

            // 計算選取球隊在 winnerList 中出現的次數，並記錄奪冠年份
            for (int i = 0, y = startYear; i < winnerList.Count && y <= 2009; y++)
            {
                if (skipYears.Contains(y))
                {
                    continue;
                }
                if (winnerList[i] == str)
                {
                    numWin++;
                    winYears.Add(y);
                }
                i++;
            }

            // 組合奪冠年份字串
            string yearsText = winYears.Count > 0
                ? string.Join("、", winYears)
                : "無";

            // 顯示該球隊自 1903 年至 2009 年共獲得冠軍的次數及年份（繁體中文）
            label1.Text = str + " 自 1903 年至 2009 年共獲得冠軍 " + numWin + " 次。\n奪冠年份：" + yearsText;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
