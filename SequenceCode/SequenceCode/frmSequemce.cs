﻿using Microsoft.VisualBasic;
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
        int n = 10;
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

        private void controls(bool bo, bool boo, bool bb)
        {
            ImageButtons.ForEach(b => b.Enabled = boo);
            btnRoundstartbutton.Enabled = bb;
            txtPerfectscores.Enabled = bo;
            txtRoundscorenumber.Enabled = bo;
            txtLevelnumber.Enabled = bo;
            lblLevel.Enabled = bo;
            lblPerfectscores.Enabled = bo;
            lblRoundScore.Enabled = bo;
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

        private void getlabelBackColor(Color c)
        {
            ImageLabels.ForEach(l => l.BackColor = c);
        }

        private void SetBackcolor()
        {
            switch (GameStatus)
            {
                case GameStatusEnum.Memorize:
                    getlabelBackColor(Color.Turquoise);
                    break;
                default:
                    getlabelBackColor(Color.Black);
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

        private void RefreshGame()
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
            lblMessagebox.Font = new Font("Times new roman", 14, FontStyle.Regular);
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
            while ((DateTime.Now - starttime).TotalSeconds <= n && GameStatus == GameStatusEnum.Memorize)
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
            ; StringBuilder sb = new();
            Random rnd = new();
            sb.Append((char)rnd.Next(65, 85));
            string s = sb.ToString();
            return s;
        }

        private void BtnRoundstartbutton_Click(object? sender, EventArgs e)
        {
            RoundStartClick();
        }

        private int DetermineCurrentImageBox()
        {
            int i = 4;
            if (lblImageBox1.ForeColor != Color.White && lblImageBox1.ForeColor != Color.Red)
            {
                i = 1;
            }
            else if (lblImagebox2.ForeColor != Color.White && lblImagebox2.ForeColor != Color.Red)
            {
                i = 2;
            }
            else if (lblImagebox3.ForeColor != Color.White && lblImagebox3.ForeColor != Color.Red)
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

        private void B_Click(object? sender, EventArgs e)
        {
            int i = DetermineCurrentImageBox();
            if (sender is Button)
            {
                Button btn = (Button)sender;
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
                            lblMessagebox.Font = new Font("Times new roman", 20, FontStyle.Bold);
                            switch (score)
                            {
                                case 6:
                                    n = 5;
                                    lblMessagebox.Text = "You are up to level " + (level).ToString() + "! You get only " + n.ToString() + " seconds to memorize the 4 images.";
                                    txtLevelnumber.Text = (level++).ToString();
                                    ImageButtons.ForEach(b => b.BackColor = Color.Yellow);
                                    break;
                                case 11:
                                    n = 3;
                                    lblMessagebox.Text = "You are up to level " + (level).ToString() + "! You get only " + n.ToString() + " seconds to memorize the 4 images.";
                                    txtLevelnumber.Text = (level).ToString();
                                    ImageButtons.ForEach(b => b.BackColor = Color.OrangeRed);
                                    break;
                                case 16:
                                    DateTime starttime = DateTime.Now;
                                    while ((DateTime.Now - starttime).TotalSeconds <= 5)
                                    {
                                        lblMessagebox.Text = "YOU WON!!!!!!!!!!!";
                                        ImageButtons.ForEach(b => b.BackColor = Color.HotPink);
                                        Application.DoEvents();
                                    }
                                    RefreshGame();
                                    break;
                            }
                        }
                        ImageButtons.ForEach(b => b.Enabled = false);
                        break;
                }
            }
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            RefreshGame();
        }

    }
}
