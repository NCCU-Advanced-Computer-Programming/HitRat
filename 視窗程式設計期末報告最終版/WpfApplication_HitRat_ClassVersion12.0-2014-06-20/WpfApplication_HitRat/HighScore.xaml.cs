using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.IO;


namespace WpfApplication_HitRat
{
    /// <summary>
    /// HighScore.xaml 的互動邏輯
    /// </summary>
    public partial class HighScore : Window
    {
        public string _first = "000", _second = "000", _third = "000";

        public HighScore()
        {
            InitializeComponent();
            ReadTextFile("champion.txt");
        }

        private void ReadTextFile(string filename)
        {
            int[] maxInt = new int[1000];
            int i = 0;

            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        maxInt[i++] = Int32.Parse(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: {0}", ex.Message);
            }

            sortArray(maxInt);
            lb1.Content = maxInt[0].ToString("000");
            lb2.Content = maxInt[1].ToString("000");
            lb3.Content = maxInt[2].ToString("000");

            for (int j = 0; j < i; j++)
                if (maxInt[j] != 0)
                    tb.Text += maxInt[j].ToString("000") + '\n';
        }

        public void sortArray(int[] array)
        {
            int tmp = 0;
            int i = 0, j = 0;

            for (i = 0; i < 999; i++)
            {
                for (j = 0; j < 999; j++)
                {
                    if (array[j] < array[j + 1])
                    {
                        tmp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = tmp;
                    }
                }
            }
        }


    }
}
