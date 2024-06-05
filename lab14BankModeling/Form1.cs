using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab14BankModeling
{
    // Агенты: Клиенты (Clients), Очередь (Queue), Операторы (Employees), Банк - управляющий агент
    public partial class Form1 : Form
    {
        bool buttonValue;
        private void Form1_Load(object sender, EventArgs e)
        {
            buttonValue = true;
        }

        public Form1()
        {
            InitializeComponent();
        }

        int operatorsN, queueClientsN, servingClientsN;
        double nextClientTime, servingEndTime;
        double time;

        Random rnd = new Random(); //от 0 до 1
        private void btStart_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            queueClientsN = 0;
            servingClientsN = 0;
            time = 0;

            buttonValue = true;

            operatorsN = (int)numOperators.Value;
            chart1.ChartAreas[0].Axes[1].Maximum = operatorsN + 1;


            


            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            nextClientTime = ExpRV(3);
            if (servingClientsN > 0)
            {
                servingEndTime = ExpRV(3 * servingClientsN);
            }
            else
            {
                servingEndTime = 100;
            }

            if (nextClientTime < servingEndTime)
            {
                if (servingClientsN < operatorsN)
                {
                    servingClientsN++;
                }
                else
                {
                    queueClientsN++;
                }
                time += nextClientTime;
            }
            else
            {
                if (queueClientsN == 0)
                {
                    servingClientsN--;
                }
                else
                {
                    queueClientsN--;
                }
                time += servingEndTime;
            }

            lbQueueClients.Text = queueClientsN.ToString();
            lbServingClients.Text = servingClientsN.ToString();

            chart1.Series[0].Points.AddXY(time, servingClientsN);
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            buttonValue = false;
            timer1.Stop();
        }


        double ExpRV(double lambda)
        {
            return -Math.Log(rnd.NextDouble()) /lambda;
        }
    }
}
