﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DotNetBejewelledBot
{
    public partial class frmMain : Form
    {
        public BejeweledWindowManager m_BWM;
        private int tick = 0;

        public frmMain()
        {
            InitializeComponent();

            BejeweledColor.Collection = new List<Color>();
            BejeweledColor.Collection.Add(BejeweledColor.Blue);
            BejeweledColor.Collection.Add(BejeweledColor.Green);
            BejeweledColor.Collection.Add(BejeweledColor.Orange);
            BejeweledColor.Collection.Add(BejeweledColor.Purple);
            BejeweledColor.Collection.Add(BejeweledColor.Red);
            BejeweledColor.Collection.Add(BejeweledColor.White);
            BejeweledColor.Collection.Add(BejeweledColor.Yellow);

            m_BWM = new BejeweledWindowManager(screenGrabTimer);
            WinAPI.Startup(m_BWM);
        }

        private void screenGrabTimer_Tick(object sender, EventArgs e)
        {
            tick++;
            m_BWM.GetScreenshot();
            m_BWM.GetColourGrid();
            //m_BWM.CalculateMoves();
            pictureBox1.Image = m_BWM.ColourGrid;
            //if (tick * screenGrabTimer.Interval == 60000)
            //{
            //    screenGrabTimer.Stop();
            //    tick = 0;
            //    btnStart.Enabled = true;
            //}
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            m_BWM.GetScreenshot();
            if (m_BWM.Calibrate())
            {
                screenGrabTimer.Start();
            }
            else
            {
                MessageBox.Show("Couldn't calibrate");
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            m_BWM.GetScreenshot();
            if (m_BWM.Calibrate())
            {
                m_BWM.isStarted = true;
                screenGrabTimer.Start();
            }
            else
            {
                MessageBox.Show("Couldn't calibrate");
            }
        }

        private void debugButton_Click(object sender, EventArgs e)
        {
            m_BWM.isDebuggingPx = !m_BWM.isDebuggingPx;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out m_BWM.debugX))
            {
                m_BWM.GetScreenshot();
                m_BWM.GetColourGrid();
                m_BWM.MouseMove();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out m_BWM.debugY))
            {
                m_BWM.GetScreenshot();
                m_BWM.GetColourGrid();
                m_BWM.MouseMove();
            }
        }
    }
}
