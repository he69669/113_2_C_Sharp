using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

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
    // 定義 TeamData 結構，包含球隊名稱、獲勝次數、獲勝年份清單
    public struct TeamData
    {
        public string Name;           // 球隊名稱
        public int WinCount;          // 獲勝次數
        public List<int> WinYears;    // 獲勝年份清單

        public TeamData(string name)
        {
            Name = name;
            WinCount = 0;
            WinYears = new List<int>();
        }
    }

    public partial class Form1 : Form
    {
        // 新增「新增資料」與「離開」按鈕
        private Button btnAddData;
        private Button btnExit;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomButtons();
        }

        // 使用 List 來存放球隊與冠軍資料
        List<string> teamList = new List<string>();
        List<string> winnerList = new List<string>();

        // 使用 Dictionary 來存放 TeamData，key 為球隊名稱
        Dictionary<string, TeamData> teamDataDict = new Dictionary<string, TeamData>();

        // 新增 teamDataList，儲存所有球隊與冠軍資料的清單
        List<TeamData> teamDataList = new List<TeamData>();

        // 儲存檔案路徑
        string teamsFilePath = "";
        string winnersFilePath = "";

        /// <summary>
        /// 初始化自訂按鈕（新增資料、離開）
        /// </summary>
        private void InitializeCustomButtons()
        {
            btnAddData = new Button();
            btnAddData.Text = "新增資料";
            btnAddData.Location = new Point(30, 220);
            btnAddData.Size = new Size(100, 30);
            btnAddData.Click += BtnAddData_Click;
            this.Controls.Add(btnAddData);

            btnExit = new Button();
            btnExit.Text = "離開";
            btnExit.Location = new Point(150, 220);
            btnExit.Size = new Size(100, 30);
            btnExit.Click += BtnExit_Click;
            this.Controls.Add(btnExit);
        }

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
            BuildTeamData();
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
        /// 建立 teamDataDict 與 teamDataList，計算每隊獲勝次數與年份
        /// </summary>
        private void BuildTeamData()
        {
            teamDataDict.Clear();
            teamDataList.Clear();
            foreach (string team in teamList)
            {
                teamDataDict[team] = new TeamData(team);
            }

            int startYear = 1903;
            int endYear = 2009;
            HashSet<int> skipYears = new HashSet<int> { 1904, 1994 };

            // 處理1903~2009年
            for (int i = 0, y = startYear; i < winnerList.Count && y <= endYear; y++)
            {
                if (skipYears.Contains(y))
                    continue;
                string winner = winnerList[i];
                if (teamDataDict.ContainsKey(winner))
                {
                    TeamData td = teamDataDict[winner];
                    td.WinCount++;
                    td.WinYears.Add(y);
                    teamDataDict[winner] = td;
                }
                i++;
            }

            // 處理2010年以後
            int extraStartIndex = 0;
            if (winnerList.Count > 0)
            {
                int countBefore2010 = 0;
                for (int y = startYear, i = 0; y <= endYear && i < winnerList.Count; y++)
                {
                    if (skipYears.Contains(y))
                        continue;
                    countBefore2010++;
                    i++;
                }
                extraStartIndex = countBefore2010;
            }
            int extraYear = 2010;
            for (int i = extraStartIndex; i < winnerList.Count; i++, extraYear++)
            {
                string winner = winnerList[i];
                if (!teamDataDict.ContainsKey(winner))
                {
                    teamDataDict[winner] = new TeamData(winner);
                }
                TeamData td = teamDataDict[winner];
                td.WinCount++;
                td.WinYears.Add(extraYear);
                teamDataDict[winner] = td;
            }

            // 將 teamDataDict 內容同步到 teamDataList
            teamDataList.Clear();
            foreach (var td in teamDataDict.Values)
            {
                teamDataList.Add(td);
            }
        }

        /// <summary>
        /// 當使用者在 listBox1 選取球隊時，顯示該隊伍的 TeamData 結果（資料來源為 teamDataList）
        /// </summary>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = listBox1.SelectedItem.ToString();
            // 從 teamDataList 取得該隊伍資料
            TeamData? td = teamDataList.FirstOrDefault(t => t.Name == str);
            if (td == null || string.IsNullOrEmpty(td.Value.Name))
            {
                label1.Text = str + " 無資料。";
                return;
            }
            string yearsText = td.Value.WinYears.Count > 0
                ? string.Join("、", td.Value.WinYears)
                : "無";
            int lastYear = td.Value.WinYears.Count > 0 ? td.Value.WinYears.Max() : 2009;
            label1.Text = str + $" 自 1903 年至 {lastYear} 年共獲得冠軍 {td.Value.WinCount} 次。\n奪冠年份：" + yearsText;
        }

        /// <summary>
        /// 按下「新增資料」按鈕，開啟對話方塊讀取2010年以後MLB冠軍隊伍資料，更新list與介面
        /// </summary>
        private void BtnAddData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openNewWinnersDialog = new OpenFileDialog();
            openNewWinnersDialog.Title = "請選擇2010年以後MLB冠軍資料檔";
            openNewWinnersDialog.Filter = "文字檔案 (*.txt)|*.txt|所有檔案 (*.*)|*.*";
            if (openNewWinnersDialog.ShowDialog() == DialogResult.OK)
            {
                string newWinnersFilePath = openNewWinnersDialog.FileName;
                List<string> newWinners = new List<string>();
                try
                {
                    using (StreamReader inputFile = File.OpenText(newWinnersFilePath))
                    {
                        string line;
                        while ((line = inputFile.ReadLine()) != null)
                        {
                            newWinners.Add(line);
                        }
                    }

                    // 將新資料加入 winnerList
                    winnerList.AddRange(newWinners);

                    // 將新隊伍加入 teamList（避免重複）
                    foreach (string team in newWinners.Distinct())
                    {
                        if (!teamList.Contains(team))
                        {
                            teamList.Add(team);
                        }
                    }

                    // 重新整理 listBox1 顯示
                    listBox1.Items.Clear();
                    foreach (string team in teamList)
                    {
                        listBox1.Items.Add(team);
                    }

                    // 重新建立 teamDataDict 與 teamDataList
                    BuildTeamData();

                    MessageBox.Show("已成功匯入2010年以後MLB冠軍資料！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("讀取2010年以後冠軍資料時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("未選擇2010年以後冠軍資料檔。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 按下「離開」按鈕，將更新過後的資料存回原始檔案，然後結束程式
        /// </summary>
        private void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                // 將 teamList 寫回 Teams.txt
                File.WriteAllLines(teamsFilePath, teamList, Encoding.UTF8);
                // 將 winnerList 寫回 WorldSeries.txt
                File.WriteAllLines(winnersFilePath, winnerList, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("儲存資料時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}
