using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadsAndDelegates
{
    public partial class UsingThreads : Form
    {
        int _Max;
        delegate void StartProcessHandler();

        public UsingThreads()
        {
            InitializeComponent();
        }

        [STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new UsingThreads());
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _Max = 100;
            //Start thread
            var t = new Thread(new ThreadStart(StartProcess));
            t.Start();
        }

        private void StartProcess()
        {
            if (pbStatus.InvokeRequired)
            {
                var sph = new StartProcessHandler(StartProcess);
                this.Invoke(sph);

            }
            else
            {
                this.Refresh();
                this.pbStatus.Maximum = _Max;
                for (int i = 0; i <= _Max; i++)
                {
                    Thread.Sleep(10);
                    this.lblOutput.Text = i.ToString();
                    this.pbStatus.Value = i;
                }
            }
        }
    }
}
