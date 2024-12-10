using static System.Formats.Asn1.AsnWriter;
using System.Drawing;
using System.Reflection.Emit;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static TheSequenceSystem.Sequence;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TheSequenceSystem
{
    public class Sequence : INotifyPropertyChanged
    {

        string _messagebox = "";
        public enum GameStatusEnum{ start, Playing, Memorize, end }
        public GameStatusEnum GameStatus { get; set; } = GameStatusEnum.end;
        private int Time { get; set; } = 10;
        public int Score { get; set; } = 1;
        public int Level { get; set; } = 1;
        public int Round { get; set; } = 2;

        public string MessageBox { get => _messagebox; set { _messagebox = value; this.InvokePropertyChanged(); } }
        public string LevelBox { get; set; }
        public string RoundMessage { get; set; }
        public string ScoreBox { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void StartGame()
        {
            GameStatus = GameStatusEnum.start;
            SetMessageBox();
        }

        public void RoundStart()
        {
            //ImageLabels.ForEach(l => l.ForeColor = Color.Black);
            DateTime starttime = DateTime.Now;
            GameStatus = GameStatusEnum.Memorize;
            
            //ImageLabels.ForEach(l => l.Text = GetRandomLetter());
            while ((DateTime.Now - starttime).TotalSeconds <= Time && GameStatus == GameStatusEnum.Memorize)
            {

                //    ImageButtons.ForEach(b => b.Enabled = false);
                //    ImageLabels.ForEach(l => l.BackColor = Color.Turquoise);
                //    EnableDisable();
                //Application.DoEvents();
                SetMessageBox();
            }
            if (GameStatus == GameStatusEnum.start)
            {
                GameStatus = GameStatusEnum.start;
            }
            else
            {
                GameStatus = GameStatusEnum.Playing;
                SetMessageBox();
            }
            //EnableDisable();
        }

        private void SetMessageBox()
        {
                switch (GameStatus)
               {
                   case GameStatusEnum.Memorize:
                      MessageBox = "Memorize the four images in the correct row. ";
                       break;
                            case GameStatusEnum.start:
                     MessageBox  = " => You will be getting ten seconds to memorize the 4 images that will come up in the empty spots.";
                                break;
                           case GameStatusEnum.Playing:
                     MessageBox = "=> Click the right images in the correct order.";
                               break;
            }
        }

        private int DetermineCurrentImageBox()
        {
            int i = 4;
            //if (lblImageBox1.ForeColor == Color.Black)
            //{
              //  i = 1;
            //}
            //else if (lblImagebox2.ForeColor == Color.Black)
            //{
              // i = 2;
            //}
            //else if (lblImagebox3.ForeColor == Color.Black)
            //{
            //    i = 3;
            //}
            return i;
        }

        //private void ImageButton(Button btn)
        //{
        //    int i = DetermineCurrentImageBox();
        //    switch (i)
        //    {
        //        case 1:
        //            //CorrectorIncorrect(lblImageBox1, btn);
        //            break;
        //        case 2:
        //          //  CorrectorIncorrect(lblImagebox2, btn);
        //            break;
        //        case 3:
        //           // CorrectorIncorrect(lblImagebox3, btn);
        //            break;
        //        case 4:
        //            //CorrectorIncorrect(lblImagebox4, btn);
        //            //int t = ImageLabels.Count(l => l.ForeColor == Color.White);
        //            //txtRoundscorenumber.Text = t + "/4";
        //            //btnRoundstartbutton.Enabled = true;
        //           // btnRoundstartbutton.Text = "Click me to start round #" + (round++).ToString();

        //            //if (t == 4)
        //            //{
        //            //    ImageLabels.ForEach(l => l.ForeColor = Color.SpringGreen);
        //            //    MessageBox = "Great Job!";
        //            //    txtPerfectscores.Text = (score++).ToString();
        //            //    switch (Score)
        //            //    {
        //            //        case 6:
        //            //            LevelUp(5, Color.Yellow);
        //            //            break;
        //            //        case 11:
        //            //            LevelUp(3, Color.OrangeRed);
        //            //            Time = 3;
        //            //            break;
        //            //        case 16:
        //            //            DateTime starttime = DateTime.Now;
        //            //            while ((DateTime.Now - starttime).TotalSeconds <= 5)
        //            //            {
        //            //                MessageBox = "YOU WON!!!!!!!!!!!";
        //            //                ImageButtons.ForEach(b => b.BackColor = Color.HotPink);
        //            //                Application.DoEvents();
        //            //            }
        //            //            StartGame();
        //            //            break;
        //            //    }
        //            //}
        //            //ImageButtons.ForEach(b => b.Enabled = false);
        //            break;
        //    }

        //}
        //private void CorrectorIncorrect(Label imagebox, Button btn)
        //{
        //    if (btn.Text == imagebox.Text)
        //    {
        //        imagebox.ForeColor = Color.White;
        //    }
        //    else
        //    {
        //        imagebox.Text = "x";
        //        imagebox.ForeColor = Color.Red;
        //    }
        //}

        private void LevelUp(int seconds, Color c)
        {
            Time = seconds;
            MessageBox = "You are up to level " + (Level).ToString() + "! You get only " + Time.ToString() + " seconds to memorize the 4 images.";
            LevelBox = (Level++).ToString();
            //ImageButtons.ForEach(b => b.BackColor = c);
        }

        private void InvokePropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
