using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NWcf;

using CalculatorCore;

namespace CalculatorServer
{
    public partial class Form1 : Form
    {
        private NNetTcpService<ICalculator, Calculator> service;

        private delegate void AppendTextDelegate(string text);

        public Form1()
        {
            InitializeComponent();

            Font f = textBox1.Font;
            textBox1.Font = new Font(FontFamily.GenericMonospace, f.Size);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                service = new NNetTcpService<ICalculator, Calculator>("localhost", 12345);
                service.Start();

                textBox1.AppendText("started." + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                service.Stop();
                service = null;

                textBox1.AppendText("stopped." + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
