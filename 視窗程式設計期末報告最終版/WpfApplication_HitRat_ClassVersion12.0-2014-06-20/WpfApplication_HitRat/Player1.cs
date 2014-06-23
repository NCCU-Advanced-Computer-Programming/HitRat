using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;

using System.Windows.Threading;

using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApplication_HitRat
{
    class Player1
    {

        public DispatcherTimer timerPenalty;
        public Label lb1;
        public Button effectBtn1;
        public Image player1;
        public static int score1 = 0;
        public bool tool_multiple1 = false;
        public bool tool_freeze1 = false;


        public Player1(Label lb, Button btn)
        {
            timerPenalty = new DispatcherTimer();//Very Important!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            timerPenalty.Interval = TimeSpan.FromSeconds(5);//消失時間的難易度(改這)
            timerPenalty.Tick += timerPenalty_Tick; //call timer2_Tick function every second!

            lb1 = lb;
            effectBtn1 = btn;
        }

        private void timerPenalty_Tick(object sender, EventArgs e)
        {
            tool_multiple1 = false;
            tool_freeze1 = false;

            player1 = new Image();
            player1.Source = new BitmapImage(new Uri("Images/player1.jpg", UriKind.Relative));
            effectBtn1.Content = player1;
            timerPenalty.Stop();
        }

        public void resetPlayer1Class()
        {
            timerPenalty.Stop();
            score1 = 0;
            tool_multiple1 = false;
            tool_freeze1 = false;
        }

        public void getAndShowScore1()
        {
            if (tool_multiple1 == true)
                score1 = score1 + 3;
            else if (tool_freeze1 == true) ;
            else
                score1++;

            lb1.Content = score1.ToString("000");
        }

        public void getKingAndShowScore1()
        {
            score1 = score1 + 10;
            lb1.Content = score1.ToString("000");
        }

        public void loseAndShowScore1()
        {
            score1 = score1 - 10;
            lb1.Content = score1.ToString("000");
        }

    }
}
