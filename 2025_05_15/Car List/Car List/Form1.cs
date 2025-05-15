using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_List
{
    // 定義汽車結構，包含廠牌、年份與里程數
    struct Automobile
    {
        public string make;    // 汽車廠牌
        public int year;       // 汽車年份
        public double mileage; // 汽車里程數
    }

    public partial class Form1 : Form
    {
        // 建立一個汽車清單作為欄位，用來儲存所有輸入的汽車資料
        private List<Automobile> carList = new List<Automobile>();

        public Form1()
        {
            InitializeComponent();

            // 設定所有元件的 Text 屬性為繁體中文
            this.Text = "汽車清單管理";
            addButton.Text = "新增";
            displayButton.Text = "顯示";
            makeLabel.Text = "廠牌：";
            yearLabel.Text = "年份：";
            mileageLabel.Text = "里程數：";
            carListBox.Items.Clear();
        }

        /// <summary>
        /// 取得使用者輸入的資料，並將其指派給傳入的汽車物件欄位
        /// </summary>
        /// <param name="auto">傳入的汽車物件，會被填入使用者輸入的資料</param>
        private void GetData(ref Automobile auto)
        {
            try
            {
                // 從文字方塊取得使用者輸入的資料，並轉換成對應型別
                auto.make = makeTextBox.Text;
                auto.year = int.Parse(yearTextBox.Text);
                auto.mileage = double.Parse(mileageTextBox.Text);
            }
            catch (Exception ex)
            {
                // 若發生例外，顯示錯誤訊息（以繁體中文顯示）
                MessageBox.Show("輸入資料有誤，請檢查您的輸入。\n詳細資訊：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 新增按鈕的事件處理函式，將使用者輸入的汽車資料新增到清單中
        /// </summary>
        private void addButton_Click(object sender, EventArgs e)
        {
            // 建立一個新的汽車結構實例
            Automobile car = new Automobile();

            // 取得使用者輸入的資料，並填入汽車物件
            GetData(ref car);

            // 將汽車物件加入汽車清單
            carList.Add(car);

            // 清空所有輸入用的文字方塊
            makeTextBox.Clear();
            yearTextBox.Clear();
            mileageTextBox.Clear();

            // 將游標焦點設回廠牌輸入框，方便繼續輸入
            makeTextBox.Focus();
        }

        /// <summary>
        /// 顯示按鈕的事件處理函式，將所有汽車資料顯示在清單方塊中
        /// </summary>
        private void displayButton_Click(object sender, EventArgs e)
        {
            // 宣告一個字串用來儲存每一行的輸出內容
            string output;

            // 清空清單方塊目前的內容
            carListBox.Items.Clear();

            // 逐一將汽車清單中的資料格式化後顯示在清單方塊
            foreach (Automobile aCar in carList)
            {
                // 組合每一筆汽車資料的顯示字串（以繁體中文顯示）
                output = aCar.year + " 年 " + aCar.make +
                    "，里程數：" + aCar.mileage + " 公里";

                // 將組合好的字串加入清單方塊
                carListBox.Items.Add(output);
            }
        }
    }
}
