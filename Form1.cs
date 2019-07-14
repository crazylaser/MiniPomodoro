using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;
using MiniPomodoro.Properties;
using System.Media;

namespace MiniPomodoro
{
    public partial class Form1 : Form
    {
        private readonly decimal WorkMinutes = 25M; //25; // 一个番茄的时间
        private readonly decimal ShortRestMinutes = 5M; //5; // 休息时间
        private DateTime _startTime;
        private PomodoState _currentState;
        SoundPlayer p1 = new SoundPlayer("workmusic.wav");
        SoundPlayer p2 = new SoundPlayer("restdone.wav");
        Boolean p1ExsitFlag = false;
        Boolean p2ExsitFlag = false;
		
        public Form1()
        {
            InitializeComponent();
            try
            {
                p1.Load();
                p1ExsitFlag = true;
            }
            catch {}
            try
            {
                p2.LoadAsync();
                p2ExsitFlag = true;
            }catch{}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 打开程序自动开始一个新的工作番茄
            StartWorkPomodo();
            timer1.Start();
        }

        /// <summary>
        /// 开始一个新的工作番茄
        /// </summary>
        private void StartWorkPomodo()
        {
            if (p1ExsitFlag) p1.PlayLooping();
            _startTime = DateTime.Now;
            _currentState = PomodoState.Working;
        }

        /// <summary>
        /// 开始一个新的休息番茄
        /// </summary>
        private void StartRestPomodo()
        {
            
            _startTime = DateTime.Now;
            _currentState = PomodoState.Resting;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //
            // 检查是否需要切换状态
            //
            decimal elapsedMinutes = GetElapsedMinutes(); // 已经消耗的分钟数
            if (_currentState == PomodoState.Working)
            {
                if (elapsedMinutes >= WorkMinutes)
                {
					timer1.Stop();
                    MessageBox.Show("休息时间到！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (p1ExsitFlag) p1.Stop();
                    StartRestPomodo();
					timer1.Start();
				}
            }
            else if (_currentState == PomodoState.Resting)
            {
                if (elapsedMinutes >= ShortRestMinutes)
                {
                    timer1.Stop();
                    if (p2ExsitFlag) p2.PlayLooping();
                    MessageBox.Show("休息时间结束，即将开始新的番茄！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (p2ExsitFlag) p2.Stop();
                    timer1.Start();
                    StartWorkPomodo();
                }
            }

            //
            // 显示效果
            //
            elapsedMinutes = GetElapsedMinutes(); // 已经消耗的分钟数
            if (_currentState == PomodoState.Working)
            {
                // 显示进度
                int progress = 100 - Convert.ToInt32(elapsedMinutes / WorkMinutes * 100);
                TaskBar.SetProgressValue(progress, TaskbarProgressBarState.Normal);

                // 显示剩余分钟数
                int leftMinitues = Convert.ToInt32(WorkMinutes - Math.Floor(elapsedMinutes));
                switch (leftMinitues)
                {
                    case 1:
                        TaskBar.SetTaskBarIcon(Resources.W1);
                        break;
                    case 2:
                        TaskBar.SetTaskBarIcon(Resources.W2);
                        break;
                    case 3:
                        TaskBar.SetTaskBarIcon(Resources.W3);
                        break;
                    case 4:
                        TaskBar.SetTaskBarIcon(Resources.W4);
                        break;
                    case 5:
                        TaskBar.SetTaskBarIcon(Resources.W5);
                        break;
                    case 6:
                        TaskBar.SetTaskBarIcon(Resources.W6);
                        break;
                    case 7:
                        TaskBar.SetTaskBarIcon(Resources.W7);
                        break;
                    case 8:
                        TaskBar.SetTaskBarIcon(Resources.W8);
                        break;
                    case 9:
                        TaskBar.SetTaskBarIcon(Resources.W9);
                        break;
                    case 10:
                        TaskBar.SetTaskBarIcon(Resources.W10);
                        break;
                    case 11:
                        TaskBar.SetTaskBarIcon(Resources.W11);
                        break;
                    case 12:
                        TaskBar.SetTaskBarIcon(Resources.W12);
                        break;
                    case 13:
                        TaskBar.SetTaskBarIcon(Resources.W13);
                        break;
                    case 14:
                        TaskBar.SetTaskBarIcon(Resources.W14);
                        break;
                    case 15:
                        TaskBar.SetTaskBarIcon(Resources.W15);
                        break;
                    case 16:
                        TaskBar.SetTaskBarIcon(Resources.W16);
                        break;
                    case 17:
                        TaskBar.SetTaskBarIcon(Resources.W17);
                        break;
                    case 18:
                        TaskBar.SetTaskBarIcon(Resources.W18);
                        break;
                    case 19:
                        TaskBar.SetTaskBarIcon(Resources.W19);
                        break;
                    case 20:
                        TaskBar.SetTaskBarIcon(Resources.W20);
                        break;
                    case 21:
                        TaskBar.SetTaskBarIcon(Resources.W21);
                        break;
                    case 22:
                        TaskBar.SetTaskBarIcon(Resources.W22);
                        break;
                    case 23:
                        TaskBar.SetTaskBarIcon(Resources.W23);
                        break;
                    case 24:
                        TaskBar.SetTaskBarIcon(Resources.W24);
                        break;
                    case 25:
                        TaskBar.SetTaskBarIcon(Resources.W25);
                        break;
                    default:
                        TaskBar.SetTaskBarIcon(Resources.W1);
                        break;
                }
            }
            else if (_currentState == PomodoState.Resting)
            {
                int progress = 100 - Convert.ToInt32(elapsedMinutes / ShortRestMinutes * 100);
                int currentSecond = DateTime.Now.Second;
                if (currentSecond%2 == 0)
                {
                    // 显示剩余分钟数
                    TaskBar.SetTaskBarIcon(Resources.R5);
                    // 显示进度
                    TaskBar.SetProgressValue(progress, TaskbarProgressBarState.Paused);
                }
                else
                {
                    // 进度闪烁
                    TaskBar.ClearProgressValue();
                }
            }
        }

        /// <summary>
        /// 返回已经消耗了多少分钟
        /// </summary>
        /// <returns></returns>
        private decimal GetElapsedMinutes()
        {
            return Convert.ToDecimal((DateTime.Now - _startTime).TotalMinutes);
        }
    }

    public enum PomodoState
    {
        Working = 0,
        Resting = 1
    }
}
