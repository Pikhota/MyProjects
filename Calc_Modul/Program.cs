using System;
using System.ServiceProcess;

namespace Calc_Service
{
    public class Program
    {
        private static void Main()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += UnhandledExceptionHandler;

            var svc = new MainService();
            ServiceBase.Run(svc);
        }
        
            
    
    private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
    {
        const string method = "UnhandledExceptionHandler";
        var ex = (Exception)args.ExceptionObject;
        Console.WriteLine(ex == null ? "Error!" : $"{method}\n{ex}");
    }
    #region Start


    internal static void Start()
        {
            var s = new ScanDirectory();//интанцируем обьект класса ScanDirectory
            s.Scan(s.PathDir);//Сканирует директорию/папку, обрабатывает файлы и т.д.:)
            /*do
            {
                var time = 10000;
                var count = time/1000;
                while (count!=0)
                {
                    count--;
                    Thread.Sleep(1000);
                    Console.WriteLine("Loading  " + count);
                }
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);//Для выхода из программы нужно нажать Esc*/
        }
        #endregion
        #region Options 
        internal static void StartMenu()
        {
            string tab2 = new string('\t', 2);
            string tab5 = new string('\t', 5);
            startmenu:
            Console.Write(new string('\t', 2) + "Программа для считывания с файлов данные в виде математических выражений и их просчета.\n" +
            tab5 + "Меню:\n" +
            tab5 + "1. Старт.\n" +
            tab5 + "2. О программе.\n" +
            tab5 + "3. Автор\n" +
            tab5 + "4. Выход\n");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Start();//Метод старт программы        
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Console.Write(tab2 + "Программа сканирует директорию, и обрабатывает все файлы в ней" + "\n" + tab2 +
                        ",находит там математическиевыражения считает их и сохраняет в определенный файл." + "\n" + tab2 +
                        " После чего удаляет файл с которым отработала из данной директории.\n");
                    Console.ReadKey();
                    Console.Clear();
                    goto startmenu;
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    Console.Write(tab5 + "Created by Pikhota Serhii." + "\n" + tab5 + "all right reserved " + '\n');
                    Console.ReadKey();
                    Console.Clear();
                    goto startmenu;
                case ConsoleKey.D4:
                    break;
                default:
                    Console.Clear();
                    goto startmenu;
            }
        }
        #endregion
    }
}