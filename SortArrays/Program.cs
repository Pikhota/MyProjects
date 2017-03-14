using System;

namespace SortArrays
{
    internal class Program
    {
        private static void Main()
        {
            var rand = new Random();
            Title("Sorting of two-dimentional array.");
            //Creating variable type bool, which will be a determinant criteria of finish a program.
            var finished = false;
            while (!finished)
            {
                try
                {
                    // Entering values columns and lines 
                    Console.Write("Enter count of columns : ");
                    var columns = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter count of lines: ");
                    var lines = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    //Creating and initializing two-dimensional array
                    var array = new int[columns, lines];
                    //Filling an array randomly and output before sorting.
                    Console.WriteLine("Before sorting.");
                    for (var i = 0; i < columns; i++)
                    {
                        for (var j = 0; j < lines; j++)
                        {
                            array[i, j] = rand.Next(100);
                            Console.Write("{0}\t", array[i, j]);
                            Console.ForegroundColor = (ConsoleColor) rand.Next(1, 15);

                        }
                        Console.WriteLine();
                    }
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(
                        "------------------------------------------------------------------------------------------------------------");
                    //Sorting an array by columns and lines
                    for (var i = 0; i < columns; i++)
                    {
                        for (var j = 0; j < lines; j++)
                        {
                            for (var x = 0; x < columns - 1; x++)
                            {
                                if (array[x, j] < array[x + 1, j]) continue;
                                var temp = array[x + 1, j];
                                array[x + 1, j] = array[x, j];
                                array[x, j] = temp;
                            }
                            for (var x = 0; x < lines - 1; x++)
                            {
                                if (array[i, x] < array[i, x + 1]) continue;
                                var temp = array[i, x + 1];
                                array[i, x + 1] = array[i, x];
                                array[i, x] = temp;
                            }

                        }
                    }
                    //The output of array in console after sorting.
                    Console.WriteLine("After sorting.");
                    for (var i = 0; i < columns; i++)
                    {
                        for (var j = 0; j < lines; j++)
                        {
                            Console.Write("{0}\t", array[i, j]);
                            Console.ForegroundColor = (ConsoleColor)rand.Next(1, 15);
                        }
                        Console.WriteLine();
                    }
                    Console.ForegroundColor = ConsoleColor.Gray;
                    finished = true;
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Something wrong!");
                    Console.WriteLine("Enter only numeric values, please!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            Console.ReadKey();
        }

        public static void Title(string title)
        {
            Console.Write("                                       " + title);
            Console.WriteLine();
        }
    }
}


