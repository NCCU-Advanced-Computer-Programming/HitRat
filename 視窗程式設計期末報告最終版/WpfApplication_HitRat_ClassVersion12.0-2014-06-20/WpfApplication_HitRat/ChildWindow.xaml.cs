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
    /// Interaction logic for ChildWindow.xaml
    /// </summary>
    public partial class ChildWindow : Window
    {
        public Image imageBG;

        public ChildWindow(Image imageBG)
        {
            InitializeComponent();

            this.imageBG = imageBG;

        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key kPressed;
            if (e.Key == Key.ImeProcessed)
                kPressed = e.ImeProcessedKey;
            else
                kPressed = e.Key;
            Key key = kPressed;

            //  makeImage();
            //player 1
            if ((key == Key.Enter))
            {

                MainWindow.userTime = Int32.Parse(tb1.Text);
                MainWindow.pop = Double.Parse(tb2.Text);
                MainWindow.stay = Double.Parse(tb3.Text);

                if (grassland.IsChecked == true)
                    imageBG.Source = new BitmapImage(new Uri("Images/Grass.jpg", UriKind.Relative));
                if (city.IsChecked == true)
                    imageBG.Source = new BitmapImage(new Uri("Images/City.jpg", UriKind.Relative));
                if (iceland.IsChecked == true)
                    imageBG.Source = new BitmapImage(new Uri("Images/Iceland.jpg", UriKind.Relative));
                if (trickyrat.IsChecked == true)
                    imageBG.Source = new BitmapImage(new Uri("Images/stingHit.png", UriKind.Relative));

                this.Close();

            }
        }
        private void RadioBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Beg.IsChecked == true)
            {
                tb2.Text = "1.5";
                tb3.Text = "1.5";
            }
            else if (Inte.IsChecked == true)
            {
                tb2.Text = "0.5";
                tb3.Text = "0.5";
            }
            else if (Adv.IsChecked == true)
            {
                tb2.Text = "0.1";
                tb3.Text = "0.1";
            }
            else if (Exp.IsChecked == true)
            {
                tb2.Text = "0.05";
                tb3.Text = "0.05";
            }
        }

        private void Button_Click_save(object sender, RoutedEventArgs e)
        {
            MainWindow.userTime = Int32.Parse(tb1.Text);
            MainWindow.pop = Double.Parse(tb2.Text);
            MainWindow.stay = Double.Parse(tb3.Text);

            if (grassland.IsChecked == true)
                imageBG.Source = new BitmapImage(new Uri("Images/Grass.jpg", UriKind.Relative));
            if (city.IsChecked == true)
                imageBG.Source = new BitmapImage(new Uri("Images/City.jpg", UriKind.Relative));
            if (iceland.IsChecked == true)
                imageBG.Source = new BitmapImage(new Uri("Images/Iceland.jpg", UriKind.Relative));
            if (trickyrat.IsChecked == true)
            {
                Random kindPhote = new Random();
                int kind_i = kindPhote.Next(1000000);
                kind_i = kind_i % 4;

                if (kind_i == 0)
                    imageBG.Source = new BitmapImage(new Uri("Images/stingHit.png", UriKind.Relative));
                else if (kind_i == 1)
                    imageBG.Source = new BitmapImage(new Uri("Images/Plain.jpg", UriKind.Relative));
                else if (kind_i == 2)
                    imageBG.Source = new BitmapImage(new Uri("Images/Ground.jpg", UriKind.Relative));
                else if (kind_i == 3)
                    imageBG.Source = new BitmapImage(new Uri("Images/Developer.jpg", UriKind.Relative));
            }


            this.Close();
        }

        private void Button_Click_cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



    }
}
