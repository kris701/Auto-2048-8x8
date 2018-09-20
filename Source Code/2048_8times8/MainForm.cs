using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using NDX_Base;

namespace _2048_8times8
{
    public partial class MainForm : Form
    {
        NDX CustomDesign;

        public MainForm()
        {
            InitializeComponent();
            CustomDesign = new NDX(this);
            CustomDesign.DragbarBackColor = Color.FromArgb(64, 64, 64);
            CustomDesign._ButtonBackColor = Color.FromArgb(0, 122, 217);
            CustomDesign.MaxButtonBackColor = Color.FromArgb(0, 122, 217);
            CustomDesign.ParentFormDragStyle = NDXDragStyle.Opacity;
            CustomDesign.Sizable = false;
            CustomDesign.AutoScroll = false;
            CustomDesign.HasMaxButton = false;
            CustomDesign.DoubleClickToMaximize = false;
            CustomDesign.InitializeNDX();
        }

        int SecondsSinceStart = 0;
        int MovesSinceStart = 0;
        int MovesPrSecCounter = 0;
        int MovesPrSec = 0;
        bool Continue = true;
        string[] MoveSet = { "{LEFT}", "{DOWN}" , "{RIGHT}", "{DOWN}" };

        private async void StartButton_Click(object sender, EventArgs e)
        {
            Continue = true;
            await Run();
        }

        async Task Run()
        {
            int MoveDelay = (int)MoveDelayNumericUpDown.Value;
            for (int i = 5; i > 0; i--)
            {
                StatusLabel.Text = "Starting in " + i + " sec";
                await Task.Delay(1000);
            }
            StatusLabel.Text = "Running, press shift to stop";
            SecondTimer.Start();
            while (Continue)
            {
                foreach(string Key in MoveSet)
                {
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        StatisticLabel.Text = "Moves: " + MovesSinceStart + " Time elapsed: " + SecondsSinceStart + " sec Moves/sec: " + MovesPrSec;
                        Continue = false;
                        break;
                    }
                    SendKeys.Send(Key);
                    MovesSinceStart++;
                    MovesPrSecCounter++;
                    if (MoveDelay != 0)
                        await Task.Delay(MoveDelay);
                    else
                        Application.DoEvents();
                }
            }
            SecondTimer.Stop();
            SecondsSinceStart = 0;
            MovesSinceStart = 0;
            StatusLabel.Text = "Stoped";
        }

        private void SecondTimer_Tick(object sender, EventArgs e)
        {
            SecondsSinceStart++;
            MovesPrSec = MovesPrSecCounter;
            MovesPrSecCounter = 0;
            StatisticLabel.Text = "Moves: " + MovesSinceStart + " Time elapsed: " + SecondsSinceStart + " sec Moves/sec: " + MovesPrSec;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Continue = false;
        }
    }
}
