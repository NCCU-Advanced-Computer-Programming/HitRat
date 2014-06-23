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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

using System.IO;

//一
//音效(MediaElement的複雜路徑)
//視窗切換、 統計資料
//隱藏版forloop  前導的地鼠探頭影片、AboutBox、背景音重複播放

namespace WpfApplication_HitRat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string nameMusic;

        private bool Computer = false;

        internal DispatcherTimer timerRandom;  //計時器
        public static Image image1, image2, image31, image32, tool_image1, tool_image2, tool_image3, tool_image4;
        public static Image tool_1hit1, tool_1hit2, tool_2hit1, tool_2hit2, tool_3hit1, tool_3hit2, tool_4hit;
        private Button[] btnArray = new Button[9];
        private ButtonwithImage[] btn = new ButtonwithImage[9];

        internal DispatcherTimer timerGame;

        public int timeCountUp = 30;
        public static int userTime = 30;
        public static double pop = 0.5;
        public static double stay = 0.5;

        private Player1 me;
        private Player2 you;

        private Key[] keyPlayer1 = new Key[] { Key.Q, Key.W, Key.E, Key.A, Key.S, Key.D, Key.Z, Key.X, Key.C };
        private Key[] keyPlayer2_1 = new Key[] { Key.U, Key.I, Key.O, Key.J, Key.K, Key.L, Key.M, Key.OemComma, Key.OemPeriod };
        private Key[] keyPlayer2_2 = new Key[] { Key.NumPad7, Key.NumPad8, Key.NumPad9, Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad1, Key.NumPad2, Key.NumPad3 };

        private bool _sound = false;

        private int tool_poptime = 0;

        private void WriteTextFile(string filename)
        {
            int[] maxInt = new int[1000];
            int i = 0;

            try
            {
                using (StreamReader sr = new StreamReader("champion.txt"))
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

            
            try
            {
               StringBuilder sb = new StringBuilder();

               foreach (int score in maxInt)
               {
                   if(score != 0)
                   sb.AppendLine(score.ToString());
               }
                sb.AppendLine(lb1.Content.ToString());
                sb.AppendLine(lb2.Content.ToString());
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.Write(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: {0}", ex.Message);
            }
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            this.lb1.Content = "000";
            this.lb2.Content = "000";
            this.timelb.Content = "Time:" + "030";

            timerGame = new DispatcherTimer();
            timerGame.Interval = TimeSpan.FromSeconds(1);
            timerGame.Tick += timerGame_Tick; //call timerGame_Tick function every second!

            timerRandom = new DispatcherTimer();
            timerRandom.Interval = TimeSpan.FromSeconds(pop);//調整出現速度
            timerRandom.Tick += timerRandom_Tick; //call timerRandom_Tick function every second!

            //foreach (Type btnGet in g1.Children)
            //{
            //    MessageBox.Show(btnGet.ToString());
            //}

            btn[0] = new ButtonwithImage(btnHole1);
            btn[1] = new ButtonwithImage(btnHole2);
            btn[2] = new ButtonwithImage(btnHole3);
            btn[3] = new ButtonwithImage(btnHole4);
            btn[4] = new ButtonwithImage(btnHole5);
            btn[5] = new ButtonwithImage(btnHole6);
            btn[6] = new ButtonwithImage(btnHole7);
            btn[7] = new ButtonwithImage(btnHole8);
            btn[8] = new ButtonwithImage(btnHole9);

            me = new Player1(lb1, btn1);
            you = new Player2(lb2, btn2);

            btnArray[0] = btnHole1;
            btnArray[1] = btnHole2;
            btnArray[2] = btnHole3;
            btnArray[3] = btnHole4;
            btnArray[4] = btnHole5;
            btnArray[5] = btnHole6;
            btnArray[6] = btnHole7;
            btnArray[7] = btnHole8;
            btnArray[8] = btnHole9;
        }

        private void reset()
        {
            resetTimeUp();

            me.resetPlayer1Class();
            you.resetPlayer2Class();
            this.lb1.Content = "000";
            this.lb2.Content = "000";
        }

        private void resetTimeUp()
        {
            for (int j = 0; j < 9; j++)
            {
                makeImage();

                timerRandom.Stop();
                btn[j].resetBtnClass();
                btnArray[j].Content = image2;
            }
            timeCountUp = userTime;
            timerGame.Stop();
            this.timelb.Content = "Time:" + userTime.ToString("000");
        }

        public void makeImage()
        {
            image1 = new Image();
            image1.Source = new BitmapImage(new Uri("Images/Rat.png", UriKind.Relative));
            image2 = new Image();
            image2.Source = new BitmapImage(new Uri("Images/hole.png", UriKind.Relative));
            image31 = new Image();
            image31.Source = new BitmapImage(new Uri("Images/hit1.png", UriKind.Relative));
            image32 = new Image();
            image32.Source = new BitmapImage(new Uri("Images/hit2.png", UriKind.Relative));


            tool_image1 = new Image();
            tool_image1.Source = new BitmapImage(new Uri("Images/bonus.png", UriKind.Relative));
            tool_1hit1 = new Image();
            tool_1hit1.Source = new BitmapImage(new Uri("Images/bonusHit1.png", UriKind.Relative));
            tool_1hit2 = new Image();
            tool_1hit2.Source = new BitmapImage(new Uri("Images/bonusHit2.png", UriKind.Relative));

            tool_image2 = new Image();
            tool_image2.Source = new BitmapImage(new Uri("Images/frozen.png", UriKind.Relative));
            tool_2hit1 = new Image();
            tool_2hit1.Source = new BitmapImage(new Uri("Images/frozenHit1.png", UriKind.Relative));
            tool_2hit2 = new Image();
            tool_2hit2.Source = new BitmapImage(new Uri("Images/frozenHit2.png", UriKind.Relative));

            tool_image3 = new Image();
            tool_image3.Source = new BitmapImage(new Uri("Images/king.png", UriKind.Relative));
            tool_3hit1 = new Image();
            tool_3hit1.Source = new BitmapImage(new Uri("Images/kingHit1.png", UriKind.Relative));
            tool_3hit2 = new Image();
            tool_3hit2.Source = new BitmapImage(new Uri("Images/kingHit2.png", UriKind.Relative));

            tool_image4 = new Image();
            tool_image4.Source = new BitmapImage(new Uri("Images/sting.png", UriKind.Relative));
            tool_4hit = new Image();
            tool_4hit.Source = new BitmapImage(new Uri("Images/stingHit.png", UriKind.Relative));
        }
        private void timerGame_Tick(object sender, EventArgs e)
        {
            timeCountUp--;
            this.timelb.Content = timeCountUp.ToString("Time:" + "000");

            if (timeCountUp < -1)
            {
                resetTimeUp();

                System.Media.SoundPlayer timeUpSiren = new System.Media.SoundPlayer();
                System.Media.SoundPlayer timeclap = new System.Media.SoundPlayer();
                //timeUpSiren.SoundLocation = @"C:\Users\user\Desktop\ClassVersion3.0\WpfApplication_HitRat\WpfApplication_HitRat\sound effect\timeup.wav"; //請自行修正檔案路徑
                timeUpSiren.Stream = Properties.Resources.timeup;
                //  timeUpSiren.Stream = Properties.Resources.Clap;

                if (_sound == true)
                {
                    timeUpSiren.Load();
                    timeUpSiren.Play();

                    if (Player1.score1 > Player2.score2)
                    {

                        MessageBox.Show("Winner is Player 1!");

                        Winner player1 = new Winner();
                        player1.Show();
               //         timeclap.Stream = Properties.Resources.Clap;
             //           timeclap.Load();
             //           timeclap.Play();
                    }
                    else if (Player1.score1 < Player2.score2)
                    {

                        MessageBox.Show("Winner is Player 2 !");
                        Winner2 player2 = new Winner2();
                        player2.Show();

                        //timeclap.Stream = Properties.Resources.Clap;
                        //timeclap.Load();
                        //timeclap.Play();
                    }
                    else
                    {
                        MessageBox.Show("Game Tie !");  
                       TIE tie = new TIE();
                        tie.Show();
                        //timeclap.Stream = Properties.Resources.Clap;
                        //timeclap.Load();
                        //timeclap.Play();
                    }

                }

                WriteTextFile("champion.txt");
            }

            if (Computer == true)
                computerUser();
        }

        private void timerRandom_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            int i = 0;
            do
            {
                i = r.Next(10000);
                i = i % 9;
            } while (btn[i]._opened == true);

            tool_poptime++;
            tool_poptime = tool_poptime % 10;

            Random kind_r = new Random();
            int kind_i = kind_r.Next(1000000);
            kind_i = kind_i % 7;

            if (tool_poptime == 0)
            {

                switch (kind_i)
                {
                    case 0:
                        btn[i]._opened = true;
                        makeImage();
                        btnArray[i].Content = tool_image1;
                        btn[i].tool_start_multiple = true;
                        break;
                    case 1:
                        btn[i]._opened = true;
                        makeImage();
                        btnArray[i].Content = tool_image2;
                        btn[i].tool_start_freeze = true;
                        break;
                    case 2:
                        btn[i]._opened = true;
                        makeImage();
                        btnArray[i].Content = tool_image3;
                        btn[i].tool_start_king = true;
                        break;
                    default://3,4,5,6
                        btn[i]._opened = true;
                        makeImage();
                        btnArray[i].Content = tool_image4;
                        btn[i].tool_start_sting = true;
                        break;
                }

            }
            else
            {
                btn[i]._opened = true;
                makeImage();
                btnArray[i].Content = image1;
            }

            searchOpened();
            //if (Computer == true)
            //    computerUser();
        }

        private void searchOpened()
        {
            for (int j = 0; j < 9; j++)
            {
                if (btn[j]._opened == true)
                {
                    btn[j].timerCheck = 1;
                    btn[j].timerTrigger.Start();
                }
            }
        }



        //start player 1
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key kPressed;

            if (e.Key == Key.ImeProcessed)
                kPressed = e.ImeProcessedKey;
            else
                kPressed = e.Key;

            Key key = kPressed;
            //     MessageBox.Show(ToString() + key); // show Key
            for (int j = 0; j < 9; j++)
            {
                if ((key == keyPlayer1[j]) && (btn[j]._opened == true))
                {
                    makeImage();
                    btn[j]._opened = false;
                    //btn[j].timerTrigger.Stop();//Don't do this!!!!!!!!!!

                    btnArray[j].Content = image31;

                    //sound effect
                    System.Media.SoundPlayer player1 = new System.Media.SoundPlayer();
                    //player1.SoundLocation = @"C:\Users\user\Desktop\ClassVersion3.0\WpfApplication_HitRat\WpfApplication_HitRat\sound effect\player1sound.wav"; //請自行修正檔案路徑
                    player1.Stream = Properties.Resources.hitsound1;

                    try
                    {
                        if (_sound == true)
                        {
                            player1.Load();
                            player1.Play();
                        }
                    }
                    catch (System.IO.FileNotFoundException err)
                    {
                        MessageBox.Show("找不到音效檔 " + err.FileName);
                    }
                    catch (InvalidOperationException err)
                    {
                        MessageBox.Show("播放發生錯誤：" + err.Message);
                    }

                    if (btn[j].tool_start_multiple == true)
                    {
                        me.tool_multiple1 = true;
                        me.timerPenalty.Start();
                        me.effectBtn1.Content = tool_1hit1;

                        makeImage();
                        btn[j].tool_start_multiple = false;
                        btnArray[j].Content = tool_1hit1;
                    }
                    else if (btn[j].tool_start_freeze == true)
                    {
                        me.tool_freeze1 = true;
                        me.timerPenalty.Start();
                        me.effectBtn1.Content = tool_2hit1;

                        makeImage();
                        btn[j].tool_start_freeze = false;
                        btnArray[j].Content = tool_2hit1;
                    }
                    else if (btn[j].tool_start_king == true)
                    {
                        me.getKingAndShowScore1();

                        btn[j].tool_start_king = false;
                        btnArray[j].Content = tool_3hit1;
                    }
                    else if (btn[j].tool_start_sting == true)
                    {
                        me.loseAndShowScore1();

                        btn[j].tool_start_sting = false;
                        btnArray[j].Content = tool_4hit;

                        System.Media.SoundPlayer hahaha = new System.Media.SoundPlayer();
                        hahaha.Stream = Properties.Resources.hahaha;
                        if (_sound == true)
                        {
                            hahaha.Load();
                            hahaha.Play();
                        }
                    }
                    else
                    {
                        me.getAndShowScore1();
                    }


                }


                if ((key == keyPlayer2_1[j]) && (btn[j]._opened == true) || (key == keyPlayer2_2[j]) && (btn[j]._opened == true))
                {
                    makeImage();
                    btn[j]._opened = false;
                    btnArray[j].Content = image32;

                    //sound effect
                    System.Media.SoundPlayer player2 = new System.Media.SoundPlayer();
                    //player2.SoundLocation = @"C:\Users\user\Desktop\ClassVersion3.0\WpfApplication_HitRat\WpfApplication_HitRat\sound effect\player2sound.wav"; //請自行修正檔案路徑
                    player2.Stream = Properties.Resources.hotsound2;
                    try
                    {
                        if (_sound == true)
                        {
                            player2.Load();
                            player2.Play();
                        }
                    }
                    catch (System.IO.FileNotFoundException err)
                    {
                        MessageBox.Show("找不到音效檔 " + err.FileName);
                    }
                    catch (InvalidOperationException err)
                    {
                        MessageBox.Show("播放發生錯誤：" + err.Message);
                    }

                    if (btn[j].tool_start_multiple == true)
                    {
                        you.tool_multiple2 = true;
                        you.timerPenalty.Start();
                        you.effectBtn2.Content = tool_1hit2;

                        makeImage();
                        btn[j].tool_start_multiple = false;
                        btnArray[j].Content = tool_1hit2;
                    }
                    else if (btn[j].tool_start_freeze == true)
                    {
                        you.tool_freeze2 = true;
                        you.timerPenalty.Start();
                        you.effectBtn2.Content = tool_2hit2;

                        makeImage();
                        btn[j].tool_start_freeze = false;
                        btnArray[j].Content = tool_2hit2;
                    }
                    else if (btn[j].tool_start_king == true)
                    {
                        you.getKingAndShowScore2();

                        btn[j].tool_start_king = false;
                        btnArray[j].Content = tool_3hit2;
                    }
                    else if (btn[j].tool_start_sting == true)
                    {
                        you.loseAndShowScore2();

                        btn[j].tool_start_sting = false;
                        btnArray[j].Content = tool_4hit;

                        System.Media.SoundPlayer hahaha = new System.Media.SoundPlayer();
                        hahaha.Stream = Properties.Resources.hahaha;
                        if (_sound == true)
                        {
                            hahaha.Load();
                            hahaha.Play();
                        }
                    }
                    else
                    {
                        you.getAndShowScore2();
                    }

                }

            }

        }

        private void MenuItem_Click_Option(object sender, RoutedEventArgs e)
        {
            reset();

            ChildWindow option = new ChildWindow(imageBG);
            option.ShowDialog();
        }

        private void MenuItem_Click_Howtoplay(object sender, RoutedEventArgs e)
        {
            Window1 howtoplay = new Window1();
            howtoplay.Show();
        }

        private void MenuItem_Click_Aboutbox(object sender, RoutedEventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Show();
        }

        private void MenuItem_Click_Start(object sender, RoutedEventArgs e)
        {
            reset();

            timerRandom.Interval = TimeSpan.FromSeconds(pop);

            for (int i = 0; i < 9; i++)
                btn[i].timerTrigger.Interval = TimeSpan.FromSeconds(stay);

            timerRandom.Start(); //Important!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            timerGame.Start();
        }

        private void MenuItem_Click_Reset(object sender, RoutedEventArgs e)
        {
            reset();
        }

        private void MenuItem_Click_onePlayer(object sender, RoutedEventArgs e)
        {
            reset();
            Computer = true;
        }

        private void MenuItem_Click_twoPlayer(object sender, RoutedEventArgs e)
        {
            reset();
            Computer = false;
        }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            //關閉視窗
            this.Close();
        }

        private void ckbMusic_Click(object sender, RoutedEventArgs e)
        {
            //DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());

            //foreach (FileInfo f in di.GetFiles())
            //{
            //    if (f.Name == "GameMusic.wav")              
            //        nameMusic = f.FullName;              
            //}

            //di = di.Parent.Parent;
            //foreach (DirectoryInfo d in di.GetDirectories())
            //    MessageBox.Show(d.Name);

            //   di = di.MoveTo(di.Name+"\\Resources");

            MediaElement bkgMedia = new MediaElement(); //Avoid using xmal MediaElement
            if (this.ckbMusic.IsChecked == true)
            {
                try
                {
                    //請自行修正檔案路徑
                    mda.Source = new Uri("BackgroundNaruto.wav", UriKind.RelativeOrAbsolute);
                    mda.LoadedBehavior = MediaState.Play;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("媒體檔載入發生錯誤: " + ex.Message);
                }
            }
            else
            {
                mda.LoadedBehavior = MediaState.Close;
            }
        }

        private void ckbSound_Click(object sender, RoutedEventArgs e)
        {
            if (this.ckbSound.IsChecked == true)
                _sound = true;
            else
                _sound = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //關閉視窗
                   if (MessageBox.Show("是否要離開?", "詢問", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                   e.Cancel = true; 
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            HighScore w2 = new HighScore();
            w2.ShowDialog();
        }

        private void computerUser()
        {

            Random r = new Random();
            int i = r.Next(1000000);
            i = i % 3;

            if (i == 1 || i == 2)
            {
                for (int j = 0; j < 9; j++)
                    if (btn[j]._opened == true)
                    {
                        makeImage();
                        btn[j]._opened = false;
                        btnArray[j].Content = image32;

                        //sound effect
                        System.Media.SoundPlayer player2 = new System.Media.SoundPlayer();
                        //player2.SoundLocation = @"C:\Users\user\Desktop\ClassVersion3.0\WpfApplication_HitRat\WpfApplication_HitRat\sound effect\player2sound.wav"; //請自行修正檔案路徑
                        player2.Stream = Properties.Resources.hotsound2;
                        try
                        {
                            if (_sound == true)
                            {
                                player2.Load();
                                player2.Play();
                            }
                        }
                        catch (System.IO.FileNotFoundException err)
                        {
                            MessageBox.Show("找不到音效檔 " + err.FileName);
                        }
                        catch (InvalidOperationException err)
                        {
                            MessageBox.Show("播放發生錯誤：" + err.Message);
                        }

                        if (btn[j].tool_start_multiple == true)
                        {
                            you.tool_multiple2 = true;
                            you.timerPenalty.Start();
                            you.effectBtn2.Content = tool_1hit2;

                            makeImage();
                            btn[j].tool_start_multiple = false;
                            btnArray[j].Content = tool_1hit2;
                        }
                        else if (btn[j].tool_start_freeze == true)
                        {
                            you.tool_freeze2 = true;
                            you.timerPenalty.Start();
                            you.effectBtn2.Content = tool_2hit2;

                            makeImage();
                            btn[j].tool_start_freeze = false;
                            btnArray[j].Content = tool_2hit2;
                        }
                        else if (btn[j].tool_start_king == true)
                        {
                            you.getKingAndShowScore2();

                            btn[j].tool_start_king = false;
                            btnArray[j].Content = tool_3hit2;
                        }
                        else if (btn[j].tool_start_sting == true)
                        {
                        }
                        else
                        {
                            you.getAndShowScore2();
                        }

                    }
            }
        }

        private void MenuItem_Click_AboutRat(object sender, RoutedEventArgs e)
        {
            videoWin video1 = new videoWin();
            video1.ShowDialog();
        }



    }
}
