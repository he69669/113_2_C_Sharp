using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell_Phone_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 設定表單標題為「手機測試」
            this.Text = "手機測試";

            // 設定所有控制項的 Text 屬性為繁體中文
            SetControlText();
        }

        /// <summary>
        /// 設定表單上所有控制項的 Text 屬性為繁體中文。
        /// 根據常見的控制項名稱進行設定，若名稱不同請依實際情況調整。
        /// </summary>
        private void SetControlText()
        {
            Control c;

            // 設定品牌標籤的文字
            c = this.Controls["labelBrand"];
            if (c != null) c.Text = "品牌：";

            // 設定型號標籤的文字
            c = this.Controls["labelModel"];
            if (c != null) c.Text = "型號：";

            // 設定價格標籤的文字
            c = this.Controls["labelPrice"];
            if (c != null) c.Text = "價格：";

            // 設定建立物件按鈕的文字
            c = this.Controls["createObjectButton"];
            if (c != null) c.Text = "建立物件";

            // 設定離開按鈕的文字
            c = this.Controls["exitButton"];
            if (c != null) c.Text = "離開";

            // 設定顯示結果的標籤（如有）
            c = this.Controls["brandLabel"];
            if (c != null) c.Text = "品牌結果";
            c = this.Controls["modelLabel"];
            if (c != null) c.Text = "型號結果";
            c = this.Controls["priceLabel"];
            if (c != null) c.Text = "價格結果";
        }

        /// <summary>
        /// 取得使用者輸入的手機資料，並指派給 CellPhone 物件的屬性。
        /// </summary>
        /// <param name="phone">要指派資料的 CellPhone 物件</param>
        private void GetPhoneData(CellPhone phone)
        {
            decimal price;

            // 從品牌文字框取得品牌
            phone.Brand = brandTextBox.Text;

            // 從型號文字框取得型號
            phone.Model = modelTextBox.Text;

            // 從價格文字框取得價格，並嘗試轉換為 decimal
            if (decimal.TryParse(priceTextBox.Text, out price))
            {
                phone.Price = price; // 設定手機價格
            }
            else
            {
                // 顯示錯誤訊息，提示使用者輸入有效的價格
                MessageBox.Show("請輸入有效的價格。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // 如果價格無效，則不繼續執行
            }
        }

        /// <summary>
        /// 當使用者按下「建立物件」按鈕時執行。
        /// 會建立一個新的 CellPhone 物件，並顯示其屬性值。
        /// </summary>
        private void createObjectButton_Click(object sender, EventArgs e)
        {
            CellPhone myPhone = new CellPhone();

            // 呼叫 GetPhoneData 方法來取得使用者輸入的手機資料
            GetPhoneData(myPhone);

            // 將 myPhone 的屬性值顯示在標籤中
            brandLabel.Text = myPhone.Brand;
            modelLabel.Text = myPhone.Model;
            priceLabel.Text = myPhone.Price.ToString("C2"); // 格式化為貨幣格式
        }

        /// <summary>
        /// 當使用者按下「離開」按鈕時執行。
        /// 會關閉目前的表單。
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e)
        {
            // 關閉表單
            this.Close();
        }
    }
}
