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
    class ButtonwithImage : Button
    {
        public DispatcherTimer timerTrigger;  //計時器
        public int timerCheck;
        public int timerRecord;
        public bool _opened;
        public Button btnHole;

        public bool tool_start_multiple = false;
        public bool tool_start_freeze = false;
        public bool tool_start_king = false;
        public bool tool_start_sting = false;


        public ButtonwithImage(Button btn)
        {
            timerTrigger = new DispatcherTimer();//Very Important!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            timerTrigger.Interval = TimeSpan.FromSeconds(MainWindow.stay);//消失時間的難易度(改這)
            timerTrigger.Tick += timerTrigger_Tick; //call timer2_Tick function every second!

            btnHole = btn;
        }

        private void timerTrigger_Tick(object sender, EventArgs e)
        {
            if (timerCheck == 1)
            {
                if (timerRecord < 2) //消失時間的難易度
                    timerRecord++;
                else
                {
                    _opened = false;
                    timerRecord = 0;

                    timerCheck = 0;
                    timerTrigger.Stop();

                    makeImageinClass();
                    btnHole.Content = MainWindow.image2;
                }
            }
        }

        internal void resetBtnClass()
        {
            timerTrigger.Stop();
            timerCheck = 0;
            timerRecord = 0;
            _opened = false;
        }


        internal void makeImageinClass()
        {
            MainWindow.image1 = new Image();
            MainWindow.image1.Source = new BitmapImage(new Uri("Images/Rat.png", UriKind.Relative));
            MainWindow.image2 = new Image();
            MainWindow.image2.Source = new BitmapImage(new Uri("Images/hole.png", UriKind.Relative));
        }
    }
}
