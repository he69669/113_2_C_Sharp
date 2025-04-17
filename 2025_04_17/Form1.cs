using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seating_Chart
{
    public partial class Form1 : Form
    {
        private TextBox columnTextBox; // 修正：將 'object' 更改為 'TextBox'

        public Form1()
        {
            InitializeComponent();
        }

        private void displayPriceButton_Click(object sender, EventArgs e)
        {
            // 定義座位的行數與列數
            const int ROWS = 6; // 座位的總行數
            const int COLS = 4; // 座位的總列數

            int row; // 儲存使用者輸入的行號
            int col; // 儲存使用者輸入的列號

            // 定義一個二維陣列來儲存每個座位的價格
            decimal[,] prices = { 
                {450m, 450m, 450m, 450m}, // 第一行的座位價格
                {425m, 425m, 425m, 425m}, // 第二行的座位價格
                {400m, 400m, 400m, 400m}, // 第三行的座位價格
                {375m, 375m, 375m, 375m}, // 第四行的座位價格
                {375m, 375m, 375m, 375m}, // 第五行的座位價格
                {350m, 350m, 350m, 350m}  // 第六行的座位價格
            };

            // 嘗試將使用者輸入的行號轉換為整數
            if (int.TryParse(rowTextBox.Text, out row))
            {
                // 嘗試將使用者輸入的列號轉換為整數
                if (int.TryParse(columnTextBox.Text, out col))
                {
                    // 驗證行號是否在有效範圍內
                    if (row >= 0 && row < ROWS)
                    {
                        // 驗證列號是否在有效範圍內
                        if (col >= 0 && col < COLS)
                        {
                            // 在標籤中顯示對應座位的價格
                            priceLabel.Text = prices[row, col].ToString("C");
                        }
                        else
                        {
                            // 如果列號無效，顯示錯誤訊息並將焦點設置到列號輸入框
                            MessageBox.Show("請輸入有效的行列.");
                            colTextBox.Focus();
                            return;
                        }
                    }
                    else
                    {
                        // 如果行號無效，顯示錯誤訊息並將焦點設置到行號輸入框
                        MessageBox.Show("請輸入有效的列號.");
                        rowTextBox.Focus();
                        return;
                    }
                }
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // 關閉表單
            this.Close();
        }
    }
}
