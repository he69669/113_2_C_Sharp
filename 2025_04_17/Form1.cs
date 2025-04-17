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
        private TextBox columnTextBox; // �ץ��G�N 'object' ��אּ 'TextBox'

        public Form1()
        {
            InitializeComponent();
        }

        private void displayPriceButton_Click(object sender, EventArgs e)
        {
            // �w�q�y�쪺��ƻP�C��
            const int ROWS = 6; // �y�쪺�`���
            const int COLS = 4; // �y�쪺�`�C��

            int row; // �x�s�ϥΪ̿�J���渹
            int col; // �x�s�ϥΪ̿�J���C��

            // �w�q�@�ӤG���}�C���x�s�C�Ӯy�쪺����
            decimal[,] prices = { 
                {450m, 450m, 450m, 450m}, // �Ĥ@�檺�y�����
                {425m, 425m, 425m, 425m}, // �ĤG�檺�y�����
                {400m, 400m, 400m, 400m}, // �ĤT�檺�y�����
                {375m, 375m, 375m, 375m}, // �ĥ|�檺�y�����
                {375m, 375m, 375m, 375m}, // �Ĥ��檺�y�����
                {350m, 350m, 350m, 350m}  // �Ĥ��檺�y�����
            };

            // ���ձN�ϥΪ̿�J���渹�ഫ�����
            if (int.TryParse(rowTextBox.Text, out row))
            {
                // ���ձN�ϥΪ̿�J���C���ഫ�����
                if (int.TryParse(columnTextBox.Text, out col))
                {
                    // ���Ҧ渹�O�_�b���Ľd��
                    if (row >= 0 && row < ROWS)
                    {
                        // ���ҦC���O�_�b���Ľd��
                        if (col >= 0 && col < COLS)
                        {
                            // �b���Ҥ���ܹ����y�쪺����
                            priceLabel.Text = prices[row, col].ToString("C");
                        }
                        else
                        {
                            // �p�G�C���L�ġA��ܿ��~�T���ñN�J�I�]�m��C����J��
                            MessageBox.Show("�п�J���Ī���C.");
                            colTextBox.Focus();
                            return;
                        }
                    }
                    else
                    {
                        // �p�G�渹�L�ġA��ܿ��~�T���ñN�J�I�]�m��渹��J��
                        MessageBox.Show("�п�J���Ī��C��.");
                        rowTextBox.Focus();
                        return;
                    }
                }
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // �������
            this.Close();
        }
    }
}
