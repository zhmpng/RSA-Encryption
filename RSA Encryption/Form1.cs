using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Windows.Forms;

namespace RSA_Encryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox_p.Text.Length > 0) && (textBox_q.Text.Length > 0))
            {
                long p = Convert.ToInt64(textBox_p.Text);
                long q = Convert.ToInt64(textBox_q.Text);
                textBox2.Clear();
                label7.Text = "Открытый ключ:";
                label8.Text = "Закрытый ключ:";
                if (Helper.IsTheNumberSimple(p) && Helper.IsTheNumberSimple(q))
                {
                    string origtext = textBox1.Text;
                    string encryptText = Helper.ConvertTo1251(origtext);

                    encryptText = encryptText.ToUpper();

                    long n = p * q;
                    long m = (p - 1) * (q - 1);
                    BigInteger e_ = Helper.Calculate_e(m);
                    BigInteger d = Helper.modInverse(e_, m);

                    List<string> results = Helper.RSA_Endoce(encryptText, e_, n);

                    foreach (var word in results) {
                        textBox2.Text += word + Environment.NewLine;
                    }

                    textBox1.Clear();

                    textBox_d.Text = d.ToString();
                    textBox_n.Text = n.ToString();

                    label7.Text = "Открытый ключ: e = " + e_ + " n = " + n;
                    label8.Text = "Закрытый ключ: d = " + d + " n = " + n;
                }
                else
                    MessageBox.Show("p или q - не простые числа!");
            }
            else
                MessageBox.Show("Введите p и q!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            if ((textBox_d.Text.Length > 0) && (textBox_n.Text.Length > 0))
            {
                long d = Convert.ToInt64(textBox_d.Text);
                long n = Convert.ToInt64(textBox_n.Text);

                List<string> input = new List<string>();

                foreach(var line in textBox2.Lines)
                    input.Add(line);
                input.Remove("");
                string result = Helper.RSA_Dedoce(input, d, n);

                textBox2.Clear();
                textBox1.Text = result;
            }
            else
                MessageBox.Show("Введите секретный ключ!");
        }
    }
}
