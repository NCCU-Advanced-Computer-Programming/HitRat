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
    class Player2
    {
        public DispatcherTimer timerPenalty;
        public Label lb2;
        public Button effectBtn2;
        public Image player2;
        public static int score2 = 0;
        public bool tool_multiple2 = false;
        public bool tool_freeze2 = false;

        public Player2(Label lb, Button btn)
        {
            timerPenalty = new DispatcherTimer();//Very Important!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            timerPenalty.Interval = TimeSpan.FromSeconds(5);//消失時間的難易度(改這)
            timerPenalty.Tick += timerPenalty_Tick; //call timer2_Tick function every second!

            lb2 = lb;
            effectBtn2 = btn;
        }

        private void timerPenalty_Tick(object sender, EventArgs e)
        {
            tool_multiple2 = false;
            tool_freeze2 = false;

            player2 = new Image();
            player2.Source = new BitmapImage(new Uri("Images/player2.jpg", UriKind.Relative));
            effectBtn2.Content = player2;
            timerPenalty.Stop();
        }

        public void resetPlayer2Class()
        {
            timerPenalty.Stop();
            score2 = 0;
            tool_multiple2 = false;
            tool_freeze2 = false;
        }

        public void getAndShowScore2()
        {
            if (tool_multiple2 == true)
                score2 = score2 + 3;
            else if (tool_freeze2 == true) ;
            else
                score2++;

            lb2.Content = score2.ToString("000");
        }

        public void getKingAndShowScore2()
        {
            score2 = score2 + 10;
            lb2.Content = score2.ToString("000");
        }

        public void loseAndShowScore2()
        {
            score2 = score2 - 10;
            lb2.Content = score2.ToString("000");
        }
    }
}
