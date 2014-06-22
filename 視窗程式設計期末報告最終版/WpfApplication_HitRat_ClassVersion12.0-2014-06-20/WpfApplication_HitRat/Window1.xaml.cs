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
    /// Window1.xaml 的互動邏輯
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void OkPress_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GameInfo_Click(object sender, RoutedEventArgs e)
        {
            Game_Info GI = new Game_Info();
            GI.Show();
        }
    }
}
