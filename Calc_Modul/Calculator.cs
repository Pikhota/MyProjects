using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Calc_Service
{
    internal class Calculator
    {
        //Метод калькулятора расчет или  Calculation
        protected internal int Calculation(string _pattern, string _expression)//принимает паттерн регулярного выражение и выражение которое будем считать
        {
            var result = 0;//переменная result будет хранить в себе полученный результат
            //проходим по коллекции регулярного выражения которое получаем с помощью метода Matches из класса Regex 
            foreach (Match c in Regex.Matches(_expression, _pattern))
            {
                //получаем первое значение/операнд, вытаскивая его по индексу из колекции регулярного выражения _pattern
                //созданное с помощью метода Matches класса Regex
                //которым является первым значением математического выражения _expression 
                var value1 = int.Parse(c.Groups[1].Value);
                //получаем первое значение/операнд, вытаскивая его по индексу из колекции регулярного выражения _pattern
                //созданное с помощью метода Matches класса Regex
                //которым является вторым значением математического выражения _expression 
                var value2 = int.Parse(c.Groups[3].Value);
                //получаем первое значение/операнд, вытаскивая его по индексу из колекции регулярного выражения _pattern
                //созданное с помощью метода Matches класса Regex
                //которым является третим значением математического выражения _expression
                var action = c.Groups[2].Value;
                //проверяем какое действие нужно выполнить 
                switch (action)
                {
                    case "*": //если попадаем на знак/операнд умножение
                        {
                            var f = new ScanDirectory();//создаем и инстанцируем объект класса ScanDirectory() в данном случае с конструктором по умолчанию
                            Console.WriteLine("{0}" + "=" + "{1}", c.Value, value1 * value2);//выводим для наглядности выражение с результатом в консоль
                            result = value1 * value2;//считаем выражение
                            File.AppendAllText(f.PathResult,"\n"+value1 + "*" + value2 + "=" + result);//Проводим запись выражения с результатом в указанный файл
                            break;//выходим из ветвления
                        }
                        //если находим другие знаки/операнды делаем по аналогии как и со знаком умножение.
                    case "/":
                        {
                            var f = new ScanDirectory();
                            Console.WriteLine("{0}" + "=" + " {1:N2}", c.Value, value1 / value2);
                            result = value1 / value2;
                            File.AppendAllText(f.PathResult, "\n" + value1 + "/" + value2 + "=" + result);
                            break;
                        }
                    case "+":
                        {
                            var f = new ScanDirectory();
                            Console.WriteLine("{0} " + "=" + " {1}", c.Value, value1 + value2);
                            result = value1 + value2;
                            File.AppendAllText(f.PathResult, "\n" + value1 + "+" + value2 + "=" + result);
                            break;
                        }
                    case "-":
                        {
                            var f = new ScanDirectory();
                            Console.WriteLine("{0} " + "=" + " {1}", c.Value, value1 - value2);
                            result = value1 - value2;
                            File.AppendAllText(f.PathResult, "\n" + value1 + "-" + value2 + "=" + result);
                            break;
                        }
                }
            }
            return result;//возвращаем посчитанный результат
        }   
    }
}



