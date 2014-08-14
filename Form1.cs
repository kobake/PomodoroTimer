﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PomodoroTimer
{
	public partial class Form1 : Form
	{
		// Mode
		private String m_mode = "";
		private TimeSpan m_modeTime;

		// State
		private int m_state = 0; // 0:Stop 1:Start
		private TimeSpan m_rest;
		private DateTime m_start;

		// -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- //
		// Initialize
		// -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- //
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			btnPomodoro_Click(null, null);
			btnReset_Click(null, null);
		}

		// -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- //
		// Mode
		// -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- //
		private void btnPomodoro_Click(object sender, EventArgs e)
		{
			m_mode = "Pomodoro";
			btnReset_Click(null, null);
			btnStart_Click(null, null);
		}

		private void btnShortBreak_Click(object sender, EventArgs e)
		{
			m_mode = "Short break";
			btnReset_Click(null, null);
			btnStart_Click(null, null);
		}

		private void btnLongBreak_Click(object sender, EventArgs e)
		{
			m_mode = "Long break";
			btnReset_Click(null, null);
			btnStart_Click(null, null);
		}

		// -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- //
		// Control
		// -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- //
		private void btnStart_Click(object sender, EventArgs e)
		{
			// Mode
			label1.Text = m_mode;

			// State
			m_state = 1;
			m_start = DateTime.Now;
			timer1_Tick(null, null);
			label1.Focus();
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			// Mode
			label1.Text = m_mode; // +" (Pausing)";

			// State
			m_state = 0;
			m_modeTime = m_rest;
			timer1_Tick(null, null);
			label1.Focus();
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			// Mode
			label1.Text = m_mode; // +" (Pausing)";
			if (m_mode == "Pomodoro")
			{
				m_modeTime = new TimeSpan(0, 25, 0);
			}
			else if (m_mode == "Short break")
			{
				m_modeTime = new TimeSpan(0, 5, 0);
			}
			else if (m_mode == "Long break")
			{
				m_modeTime = new TimeSpan(0, 20, 0);
			}
			m_rest = m_modeTime;

			// State
			m_state = 0;
			timer1_Tick(null, null);
			label1.Focus();
		}

		// -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- //
		// Timer
		// -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- //
		private void timer1_Tick(object sender, EventArgs e)
		{
			if (m_state == 1)
			{
				TimeSpan elapsed = DateTime.Now - m_start;
				m_rest = m_modeTime - elapsed;
			}
			label2.Text = (m_rest + new TimeSpan(0, 0, 0, 0, 950)).ToString(@"hh\:mm\:ss"); // round up
			this.Text = label2.Text + " - PomodoroTimer";
			btnStart.Enabled = (m_state == 0);
			btnStop.Enabled = (m_state == 1);
		}
	}
}