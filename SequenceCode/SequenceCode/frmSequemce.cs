///AS It's a little confusing that when the game pops up there is a start btn and then a click me to start round #1 btn. 
///AS When the game starts, the round btn should be blank, only once start is clicked, set it's text to say 'click me...'.
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SequenceCode
{
    public partial class frmSequemce : Form
    {
        enum GameStatusEnum { Playing, start, Memorize, end }
        GameStatusEnum GameStatus;
        List<Label> ImageLabels;
        List<Button> ImageButtons;
/// <summary>
/// AS Name this variable more clearly.
/// </summary>
        int time = 10;
        int score = 1;
        int level = 1;                  //
        int round = 2;

        public frmSequemce()
        {
            InitializeComponent();
            btnStart.Click += BtnStart_Click;
            btnRoundstartbutton.Click += BtnRoundstartbutton_Click;
            ImageLabels = new() { lblImageBox1, lblImagebox2, lblImagebox3, lblImagebox4 };
            ImageButtons = new() { btnA, btnB, btnC, btnD, btnE, btnF, btnG, btnH, btnI, btnJ, btnK, btnL, btnM, btnN, btnO, btnP, btnQ, btnR, btnS, btnT };
            ImageButtons.ForEach(b => b.Click += B_Click);
            txtLevelnumber.Text = (level++).ToString();
        }
///AS Name these paramaters more clearly ie: Enable1, Enable2, Enable3
        private void controls(bool Enable1, bool Enable2, bool Enable3)
        {
            ImageButtons.ForEach(b => b.Enabled = Enable2);
            btnRoundstartbutton.Enabled = Enable3;
            txtPerfectscores.Enabled = Enable1;
            txtRoundscorenumber.Enabled = Enable1;
            txtLevelnumber.Enabled = Enable1;
            lblLevel.Enabled = Enable1;
            lblPerfectscores.Enabled = Enable1;
            lblRoundScore.Enabled = Enable1;
        }

        private void EnableDisable()
        {
            switch (GameStatus)
            {
                case GameStatusEnum.Memorize:
                    controls(false, false, false);
                    break;
                case GameStatusEnum.start:
                    controls(true, false, true);
                    break;
                case GameStatusEnum.Playing:
                    controls(true, true, false);
                    break;
            }
        }
///AS I don't think it makes the code more concise but making a new procedure for this, put the code directly into SetBackcolor

        private void SetBackcolor()
        {
            switch (GameStatus)
            {
                case GameStatusEnum.Memorize:
                    ImageLabels.ForEach(l => l.BackColor = Color.Turquoise);
                    break;
                default:
                    ImageLabels.ForEach(l => l.BackColor = Color.Black);
                    break;
            }
        }

        private void SetMessageBox()
        {
            switch (GameStatus)
            {
                case GameStatusEnum.Memorize:
                    lblMessagebox.Text = "";
                    break;
                case GameStatusEnum.start:
                    lblMessagebox.Text = " => You will be getting ten seconds to memorize the 4 images that will come up in the empty spots.";
                    break;
                case GameStatusEnum.Playing:
                    lblMessagebox.Text = "=> Click the right images in the correct order.";
                    break;
            }
        }

        private void StartGame()
        {
            GameStatus = GameStatusEnum.start;
            score = 1;
            level = 2;
            round = 2;
            ImageLabels.ForEach(l => l.ForeColor = Color.Black);
            ImageLabels.ForEach(l => l.Text = "");
            txtPerfectscores.Text = "";
            txtRoundscorenumber.Text = " / 4";
            txtLevelnumber.Text = "1";
            btnRoundstartbutton.Text = "Click me to start round #1";
            ///AS This should be set in the designer.
            ImageButtons.ForEach(b => b.BackColor = Color.White);
            EnableDisable();
            SetMessageBox();
        }

        private void RoundStartClick()
        {
            ImageLabels.ForEach(l => l.ForeColor = Color.Black);
            DateTime starttime = DateTime.Now;
            GameStatus = GameStatusEnum.Memorize;
            ImageLabels.ForEach(l => l.Text = GetRandomLetter());
            while ((DateTime.Now - starttime).TotalSeconds <= time && GameStatus == GameStatusEnum.Memorize)
            {
                ImageButtons.ForEach(b => b.Enabled = false);
                ImageLabels.ForEach(l => l.BackColor = Color.Turquoise);
                EnableDisable();
                SetMessageBox();
                SetBackcolor();
                Application.DoEvents();
            }
            if (GameStatus == GameStatusEnum.start)
            {
                GameStatus = GameStatusEnum.start;
            }
            else
            {
                GameStatus = GameStatusEnum.Playing;
            }
            SetMessageBox();
            EnableDisable();
            SetBackcolor();
        }

        private string GetRandomLetter()
        {
            StringBuilder sb = new();
            Random rnd = new();
            sb.Append((char)rnd.Next(65, 85));
            string s = sb.ToString();
            return s;
        }

        private int DetermineCurrentImageBox()
        {
            int i = 4;
            if (lblImageBox1.ForeColor == Color.Black )
            {
                i = 1;
            }
            else if (lblImagebox2.ForeColor == Color.Black )
            {
                i = 2;
            }
            else if (lblImagebox3.ForeColor == Color.Black )
            {
                i = 3;
            }
            return i;
        }

        private void CorrectorIncorrect(Label imagebox, Button btn)
        {
            if (btn.Text == imagebox.Text)
            {
                imagebox.ForeColor = Color.White;
            }
            else
            {
                imagebox.Text = "x";
                imagebox.ForeColor = Color.Red;
            }
        }
        private void ImageButtonClick(Button btn)
        {
            int i = DetermineCurrentImageBox();
            switch (i)
            {
                case 1:
                    CorrectorIncorrect(lblImageBox1, btn);
                    break;
                case 2:
                    CorrectorIncorrect(lblImagebox2, btn);
                    break;
                case 3:
                    CorrectorIncorrect(lblImagebox3, btn);
                    break;
                case 4:
                    CorrectorIncorrect(lblImagebox4, btn);
                    int t = ImageLabels.Count(l => l.ForeColor == Color.White);
                    txtRoundscorenumber.Text = t + "/4";
                    btnRoundstartbutton.Enabled = true;
                    btnRoundstartbutton.Text = "Click me to start round #" + (round++).ToString();
                    lblMessagebox.Text = "";
                    if (t == 4)
                    {
                        ImageLabels.ForEach(l => l.ForeColor = Color.SpringGreen);
                        lblMessagebox.Text = "Great Job!";
                        txtPerfectscores.Text = (score++).ToString();
                        switch (score)
                        {
                            case 6:
                                LevelUp(5, Color.Yellow);
                                break;
                            case 11:
                                LevelUp(3, Color.OrangeRed);
                                time = 3;
                                break;
                            case 16:
                                DateTime starttime = DateTime.Now;
                                while ((DateTime.Now - starttime).TotalSeconds <= 5)
                                {
                                    lblMessagebox.Text = "YOU WON!!!!!!!!!!!";
                                    ImageButtons.ForEach(b => b.BackColor = Color.HotPink);
                                    Application.DoEvents();
                                }
                                StartGame();
                                break;
                        }
                    }
                    ImageButtons.ForEach(b => b.Enabled = false);
                    break;
            }

        }
        private void LevelUp(int seconds, Color c)
        {
            time = seconds;
            lblMessagebox.Text = "You are up to level " + (level).ToString() + "! You get only " + time.ToString() + " seconds to memorize the 4 images.";
            txtLevelnumber.Text = (level++).ToString();
            ImageButtons.ForEach(b => b.BackColor = c);
        }

        private void B_Click(object? sender, EventArgs e)
        {
            if (sender is Button)
            {
                ImageButtonClick((Button)sender);
            }
        }

        private void BtnRoundstartbutton_Click(object? sender, EventArgs e)
        {
            RoundStartClick();
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            StartGame();
        }

    }
}
