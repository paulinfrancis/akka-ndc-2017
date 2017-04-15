using System;
using Akka.Actor;

namespace AkkaCodeSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }


    public static class BankAccount
    {
        private static int _balance = 1000;

        public static bool WithdrawMoney(int amountToWithdraw)
        {
            if (amountToWithdraw <= _balance)
            {
                _balance -= amountToWithdraw;
                return true;
            }

            return false;
        }
    }

    public static class BankAccountWithLock
    {
        private static int _balance = 1000;

        public static bool WithdrawMoney(int amountToWithdraw)
        {
            lock (new object())
            {
                if (amountToWithdraw <= _balance)
                {
                    _balance -= amountToWithdraw;
                    return true;
                }

                return false;
            }
        }
    }

    public class BankAccountActor : ReceiveActor
    {
        private int _balance = 1000;

        public BankAccountActor()
        {
            Receive<int>(amountToWithdraw =>
            {
                if (amountToWithdraw <= _balance)
                {
                    _balance -= amountToWithdraw;
                    Sender.Tell(true);
                }
                else
                {
                    Sender.Tell(false);
                }
            });
        }
    }

    public class SwitchableBehaviourActor : ReceiveActor
    {
        public SwitchableBehaviourActor()
        {
            Become(ABehaviour);
        }

        private void ABehaviour()
        {
            Receive<string>(_ =>
            {
                Console.WriteLine("From A Behaviour");
                Become(BBehaviour);
            });
        }

        private void BBehaviour()
        {
            Receive<string>(_ =>
            {
                Console.WriteLine("From B Behaviour");
                Become(ABehaviour);
            });
        }
    }
}