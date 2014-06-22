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

namespace WpfApplication_HitRat
{
    /// <summary>
    /// Winner2.xaml 的互動邏輯
    /// </summary>
    public partial class Winner2 : Window
    {
        public Winner2()
        {
            InitializeComponent();
            System.Media.SoundPlayer timeclap = new System.Media.SoundPlayer();
            timeclap.Stream = Properties.Resources.Clap;
            timeclap.Load();
            timeclap.Play();

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
