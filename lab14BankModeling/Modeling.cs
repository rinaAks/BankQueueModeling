using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab14BankModeling
{
    public class Modeling
    {
        private static double T = 0;
        public static double Time { get { return T; } }
        private static List<Agent> agents = new List<Agent>();
        //...
        private static Bank bank;
        static ArrivalProcess arrivalProcess;
        static Agent currentAgent;
        public static int people = 0;
        static Agent activeAgent;

        public static int operators;
        //___

        public static void Run()
        {
            do
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
            } while (!stopCondition(T, activeAgent)) ;
        }
        private static bool stopCondition(double t, Agent activeAgent)
        {
            return (t < 100) || (activeAgent == null);
        }

        public static void ReRun()
        {
            bank = new Bank();
            arrivalProcess = new ArrivalProcess(bank);
            agents.Add(bank);
            agents.Add(arrivalProcess);
        }

        public static int queueCount()
        {
            return bank.getQueueSize();
        }
        public static int getBusyOperatorsCount()
        {
            return bank.getBusyOperatorsCount();
        }
    }
}
