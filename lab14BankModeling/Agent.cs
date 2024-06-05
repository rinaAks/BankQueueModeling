using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab14BankModeling
{
    public abstract class Agent
    {
        public virtual double getNextEventTime() 
        {
            return double.MaxValue;
        }

        public virtual void processEvent()
        {
            
        }
    }

    public class ArrivalProcess : Agent
    {
        private Random rnd = new Random();
        private double nextArrivalTime = 0;
        public double lambda = 2;
        private Bank bank;

        public ArrivalProcess(Bank bank)
        {
            this.bank = bank;
            nextArrivalTime = simulateInterArrivalTime();
        }
        private double simulateInterArrivalTime()
        {
            return Math.Log(rnd.NextDouble()) / lambda;
        }

        public override double getNextEventTime()
        {
            return nextArrivalTime;
        }
        public override void processEvent()
        {
            base.processEvent();
            Customer customer = new Customer(); //здесь можно в комментариях передавать например время прихода клиента. Это объект типа для переноса данных
            bank.customerIsArrived(customer); 
            nextArrivalTime += simulateInterArrivalTime();
        }
    }

    public class Bank : Agent
    {
        private Service service = new Service();
        private MyQueue queue = new MyQueue();

        internal void customerIsArrived(Customer customer)
        {
            if (service.hasFreeOperator(customer)) service.takeCustomer(customer); // если есть свободный оператор, сразу нового клиента в очередь
            else queue.takeCustomer(customer); // иначе клиент 
        }
    }

    public class Customer : Agent
    {

    }
    public class MyQueue : Agent
    {
        private Queue<Customer> queue = new Queue<Customer>();
        internal void takeCustomer(Customer customer)
        {
            queue.Enqueue(customer);
        }
    }
    public class Operator : Agent
    {
        private Customer customerInService = null;
        private double endOfServiceTime = double.MaxValue;
        private Random rnd = new Random();
        private double lambda = 3;
        internal bool isFree()
        {
            return customerInService == null;
        }

        internal void takeCustomer(Customer customer)
        {
            if (isFree())
            {
                customerInService = customer;
                endOfServiceTime = Modeling.Time + simulateServiceTime();
            }
        }

        private double simulateServiceTime()
        {
            return Math.Log(rnd.NextDouble()) / lambda;
        }
    }
    public class Service : Agent
    {
        private List<Operator> operators = new List<Operator>();
        internal bool hasFreeOperator(Customer customer)
        {
            Operator op = findFreeOperator();
            return(op != null);
        }

        private Operator findFreeOperator()
        {
            foreach (Operator op in operators)
                if (op.isFree()) return op;
            return null;
        }

        internal void takeCustomer(Customer customer)
        {
            Operator op = findFreeOperator();
            if (op != null) op.takeCustomer(customer);
        }
    }
}
