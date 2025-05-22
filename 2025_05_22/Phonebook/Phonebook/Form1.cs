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

namespace Phonebook
{
    // 結構體：用來儲存電話簿的每一筆資料，包括姓名與電話號碼
    struct PhoneBookEntry
    {
        public string name;   // 姓名
        public string phone;  // 電話號碼
    }

    public partial class Form1 : Form
    {
        // 欄位：用來儲存所有電話簿資料的清單
        private List<PhoneBookEntry> phoneList =
            new List<PhoneBookEntry>();

        public Form1()
        {
            InitializeComponent();
        }

        // ReadFile 方法：讀取 PhoneList.txt 檔案內容，
        // 並將每一筆資料轉換為 PhoneBookEntry 物件，存入 phoneList 清單中。
        private void ReadFile()
        {
            StreamReader inputFile;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                
                    
                    
                    inputFile = File.OpenText(openFileDialog1.FileName);// 開啟檔案
                    string line;
                    while (!inputFile.EndOfStream)
                    {
                        line = inputFile.ReadLine();
                        string[] parts = line.Split(',');
                        PhoneBookEntry entry;
                        entry.name = parts[0];
                        entry.phone = parts[1];
                        phoneList.Add(entry);
                    }
                    inputFile.Close();

                
                
            }
            else
            {
                MessageBox.Show("檔案未選擇或無法開啟。");
            }

               
        }

        // DisplayNames 方法：將 phoneList 清單中的所有姓名
        // 顯示在 nameListBox 控制項中，供使用者選擇。
        private void DisplayNames()
        {
            foreach (PhoneBookEntry entry in phoneList)//遍歷所有電話簿資料
            {
                nameListBox.Items.Add(entry.name);// 將姓名加入 nameListBox 控制項
            }
        }

        // Form1_Load 事件：當表單載入時執行，
        // 通常用來初始化資料，例如讀取檔案與顯示姓名清單。
        private void Form1_Load(object sender, EventArgs e)
        {
            ReadFile();// 讀取檔案

            DisplayNames();// 顯示姓名在 nameListBox 控制項中
        }

        // nameListBox_SelectedIndexChanged 事件：
        // 當使用者在 nameListBox 中選擇不同的姓名時觸發，
        // 用來顯示對應的電話號碼。
        private void nameListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = nameListBox.SelectedIndex;// 取得選擇的索引
            if(index!=-1)
            {
                // 顯示對應的電話號碼
                phoneLabel.Text = phoneList[index].phone;
            }
            else
            {
                // 如果沒有選擇任何項目，則清空電話號碼顯示
                phoneLabel.Text = "無資料";
            }
        }

        // exitButton_Click 事件：當使用者按下「離開」按鈕時執行，
        // 關閉目前的表單，結束程式。
        private void exitButton_Click(object sender, EventArgs e)
        {
            // 關閉表單，結束程式。
            this.Close();

            //把phoneList的資料寫進原來檔案
            if (openFileDialog1.FileName != null && File.Exists(openFileDialog1.FileName))
            {
                try
                {
                    using (StreamWriter outputFile = new StreamWriter(openFileDialog1.FileName))
                    {
                        foreach (PhoneBookEntry entry in phoneList)
                        {
                            outputFile.WriteLine($"{entry.name},{entry.phone}");
                        }
                    }
                    MessageBox.Show("資料已成功儲存至檔案！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"儲存檔案時發生錯誤：{ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("無法儲存資料，檔案不存在或未選擇檔案。");
            }
            //把phoneList的資料寫進原來檔案
        }

private void button1_Click(object sender, EventArgs e)
        {
            // 檢查 NameBox 和 PhoneBox 是否有輸入資料  
            if (!string.IsNullOrWhiteSpace(Namebox.Text) && !string.IsNullOrWhiteSpace(PhoneBox.Text))
            {
                // 建立新的 PhoneBookEntry 物件  
                PhoneBookEntry newEntry;
                newEntry.name = Namebox.Text.Trim();
                newEntry.phone = PhoneBox.Text.Trim();

                // 將新資料加入 phoneList  
                phoneList.Add(newEntry);

                // 將新姓名顯示在 nameListBox  
                nameListBox.Items.Add(newEntry.name);

                // 清空輸入框  
                Namebox.Clear();
                PhoneBox.Clear();

                // 提示使用者資料已新增  
                MessageBox.Show("資料已成功新增！");
            }
            else
            {
                // 提示使用者輸入資料  
                MessageBox.Show("請輸入完整的姓名和電話號碼！");
            }
        }
    }
}
