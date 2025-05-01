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

namespace Test_Average
{
    public partial class Form1 : Form
    {
        private List<int> testScores = new List<int>(); // �x�s���դ��ƪ��M��

        public Form1()
        {
            InitializeComponent();
        }

        // Average ��k�����@�� List<int> �Ѽ�
        // �ê�^�ӲM�椤�Ҧ��Ȫ������ȡC
        private double Average(List<int> scores)
        {
            int total = 0;
            // �M���M�椤���C�Ӥ��ơA�ñN��[���`�M��
            foreach (int score in scores)
            {
                total += score;
            }
            // ��^�`�M���H���Ƽƶq��������
            return (double)total / scores.Count;
        }

        // Highest ��k�����@�� List<int> �Ѽ�
        // �ê�^�ӲM�椤���̰��ȡC
        private int Highest(List<int> scores)
        {
            int highest = scores[0];
            // �M���M�椤���C�Ӥ��ơA�ç�X�̰�������
            for (int i = 1; i < scores.Count; i++)
            {
                if (scores[i] > highest)
                {
                    highest = scores[i];
                }
            }
            // ��^�̰�������
            return highest;
        }

        // Lowest ��k�����@�� List<int> �Ѽ�
        // �ê�^�ӲM�椤���̧C�ȡC
        private int Lowest(List<int> scores)
        {
            int lowest = scores[0];
            // �M���M�椤���C�Ӥ��ơA�ç�X�̧C������
            foreach (int score in scores)
            {
                if (score < lowest)
                {
                    lowest = score;
                }
            }
            // ��^�̧C������
            return lowest;
        }

        private void getScoresButton_Click(object sender, EventArgs e)
        {
            int highestScore = 0;
            int lowestScore = 0;
            double averageScore = 0.0;
            StreamReader inputFile;
            try
            {
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    // ���}���C
                    inputFile = File.OpenText(openFile.FileName);
                    // �M�� ListBox �M�M��C
                    testScoresListBox.Items.Clear();
                    testScores.Clear();
                    // �q���Ū�����դ��ơC
                    while (!inputFile.EndOfStream)
                    {
                        int score = int.Parse(inputFile.ReadLine());
                        testScores.Add(score);
                        // �N���ƲK�[�� ListBox ���C
                        testScoresListBox.Items.Add(score);
                    }
                    inputFile.Close();  // �������C
                                        // �p�⥭�����ơB�̰����ƩM�̧C���ơC
                    averageScore = Average(testScores);
                    highestScore = Highest(testScores);
                    lowestScore = Lowest(testScores);
                    // ��ܵ��G�C
                    averageScoreLabel.Text = averageScore.ToString("n1");
                    highScoreLabel.Text = highestScore.ToString();
                    lowScoreLabel.Text = lowestScore.ToString();
                }
            }
            catch (Exception ex)
            {
                // ��ܿ��~�T���C
                MessageBox.Show(ex.Message, "���~");
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // �������C
            this.Close();
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            sortedScoresListBox.Items.Clear(); // �M�űƧǫ᪺ ListBox

            // �N���ƱƧ�
            List<int> sortedScores = new List<int>(testScores);
            sortedScores.Sort();

            // �N�Ƨǫ᪺������ܦb ListBox ��
            foreach (int score in sortedScores)
            {
                sortedScoresListBox.Items.Add(score);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            // �|����{�R���޿�
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            // �|����{���J�޿�
        }
    }
}
