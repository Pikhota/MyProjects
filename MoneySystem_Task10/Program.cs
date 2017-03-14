using System;

namespace MoneySystem
{
    internal class Program
    {
        private static void Main()
        {
            var finish = false;
            while (!finish)
            {
                Console.WriteLine("Entering pounds: ");
                var pound = Console.ReadLine();
                Console.WriteLine("Entering shillings: ");
                var shilling = Console.ReadLine();
                Console.WriteLine("Entering pens: ");
                var pens = Console.ReadLine();
                
                if (CheckMoney(pound)&& CheckMoney(shilling)&&
                    CheckMoney(pens))
                {
                    var moneySystem = new OldMoneySystem(decimal.Parse(pound), decimal.Parse(shilling), decimal.Parse(pens));
                    //ReSharper wants  check  fields on null, but my method CheckMoney already checked this.
                    Console.WriteLine("Old UK money system: " + pound + '.' + shilling + '.' + pens);
                    moneySystem.GetNewSystemMoney();
                    finish = true;
                }
            }
            Console.ReadLine();
        }
        
        internal class OldMoneySystem
        {
            public decimal Pound { get; set; }

            public decimal Shilling { get; set; }

            public decimal Pens { get; set; }

            public OldMoneySystem(decimal pound, decimal shilling, decimal pens)
            {
                Pound = pound;
                Shilling = shilling;
                Pens = pens;
            }
            internal void GetNewSystemMoney()
            {
                Pound *= 240;
                Shilling *= 12;
                const decimal deflation = 2.4M;
                var money = Pens + Shilling + Pound;
                var result = (money / 100) / deflation;
                Console.WriteLine("New UK money system: " + result);
            }   
        }
        public static bool CheckMoney(string money)
        {
            if (!string.IsNullOrEmpty(money))
            {
                var anyString = money.ToCharArray();
                for (var i = 0; i <= anyString.Length - 1; i++)
                {
                    if (char.IsDigit(anyString[i])){}
                    else if (anyString[i] == anyString.Length - 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }   
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
