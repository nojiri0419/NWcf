using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using NWcf;

using CalculatorCore;

namespace CalculatorClient
{
    public partial class Form1 : Form
    {
        private NNetTcpClient<ICalculator> client;

        public Form1()
        {
            InitializeComponent();

            Font f1 = textBox1.Font;
            textBox1.Font = new Font(FontFamily.GenericMonospace, f1.Size);

            Font f2 = textBox1.Font;
            textBox2.Font = new Font(FontFamily.GenericMonospace, f2.Size);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                client = new NNetTcpClient<ICalculator>("127.0.0.1", 12345);
                client.Open();
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
                client.Close();
                client = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ICalculator calculator = client.GetClient();

                MatchCollection matches = Regex.Matches(textBox1.Text, "^(\\d+)( *)([+-])( *)(\\d+)$");
                if (matches.Count == 1)
                {
                    GroupCollection groups = matches[0].Groups;
                    int x = Int32.Parse(groups[1].Value);
                    int y = Int32.Parse(groups[5].Value);
                    if (groups[3].Value == "+")
                    {
                        int z = calculator.Add(x, y);
                        textBox2.AppendText(x + " + " + y + " = " + z + Environment.NewLine);
                    }
                    else if (groups[3].Value == "-")
                    {
                        int z = calculator.Subtract(x, y);
                        textBox2.AppendText(x + " - " + y + " = " + z + Environment.NewLine);
                    }
                    else
                    {
                        textBox2.AppendText("error.: " + textBox1.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
