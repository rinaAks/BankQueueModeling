using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab14BankModeling
{
    public class Modeling
    {
        private static double T;
        public static double Time { get { return T; } }
        private static List<Agent> agents = new List<Agent>();
        private static Bank bank;
        //...



        public static void Run()
        {
            T = 0;
            Agent activeAgent;
            while (Form1.buttonValue)
            {
                double tmin = double.MaxValue;
                activeAgent = null;

                foreach (Agent a in agents)
                {
                    double ta = a.getNextEventTime();
                    if (ta < tmin)
                    {
                        tmin = ta; // теперь в tmin - ближайшее время события
                        activeAgent = a;
                    }
                }

                T = tmin; // сдвиг модельного времени
                if (activeAgent != null) { activeAgent.processEvent(); }
            }
        }
    }
}
