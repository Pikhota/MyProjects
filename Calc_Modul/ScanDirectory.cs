using System.IO;
using System.Text.RegularExpressions;
using Calc_Service.Interfaces;

namespace Calc_Service
{
    internal class ScanDirectory : IScanDirectory
    {
        public string PathDir { get; set; }
        public string PathResult { get; set; }
        #region Constructor
        //конструктор по умолчанию
        internal ScanDirectory()
        {
            PathDir = @"../../Test"; // путь к директории по умолчанию
            PathResult = @"../../result.txt"; // путь к файлу куда, будут записываться результаты
        }
        //конструктор по желанию №1:)
        internal ScanDirectory(string _pathDir)
        {
            PathDir = _pathDir; // путь к директории который указывается при инстанцировании обьекта класса, которую будет сканировать программа
            PathResult = @"../../result.txt"; // путь к файлу по умолчанию, куда будут записываться результаты вычеслений
        }
        //конструктор по желанию №2
        internal ScanDirectory(string _pathDir, string _pathResult)
        {
            PathDir = _pathDir; // путь к директории который указывается при инстанцировании обьекта класса, которую будет сканировать программа
            PathResult = _pathResult; // путь к файлу который указывается при инстанцировании обьекта класса, куда будут записываться результаты вычеслений
        }
        #endregion
        
        #region Сканирования директории
        //Сканирует директорию
        public void Scan(string _pathDirectory)
        {
            var files = Directory.GetFiles(_pathDirectory); // считываем все файлы с указанной директории
            foreach (var fileName in files) //проходим по каждому файлу отдельно
            {
                
                var _str = File.ReadAllText(fileName); // считываем данные с файла
                File.AppendAllText(PathResult, '\n' + "математические выражения на входе" + _str +'\n');
                var _result = DataProcessing(_str); //обрабатаваем данные в методе DataProcessing(str)
                var _strResult = _result.Replace("=", string.Empty);
                //File.AppendAllText(PathInput,'\n'+"сверху промежуточные результаты" + '\n'+"Снизу общие результаты каждого посчитаного выражения"+'\n'+_strResult);
                File.Delete(fileName);
            }
            
        }
        #endregion
        #region Парсинг данных с файла.
        //Обработка полученных данных и возвращение результата обработки
        public string DataProcessing(string _str)
        {
            //var result = 0;
            var _pattern = @"(\d+)([*/+-])(\d+)";//pattern - переменная которая хранит модель регулярного выражения математических операций.
            var _reStr = _str.Replace(" ", string.Empty);//удаляем все пробелы в строке что бы не мешали:)
            var _expression = GetExpression(_reStr);//находим в строке приоритетное простое выражение совпадающее с паттерном выражений и записываем его в переменную _expression
            var Calculator = new Calculator();//
            if (Regex.IsMatch(_expression, _pattern))//проверяем корректность выражения
            {
                //если матиматическое выражение прошло проверку, начинаем считать его с помощью метода Calculation из класса Calculator,
                //после чего сохраняем результат в переменную _expressionResult
                var _expressionResult = Calculator.Calculation(_pattern, _expression).ToString();
                //заменяем уже посчитаное выражение в строке на его результат
                var _newStr = _reStr.Replace(_expression, _expressionResult.ToString());
                return DataProcessing(_newStr); //делаем рекурсию 
            }
            return _str;//когда все выражения в строке посчитаны,выводим ее из метода.
        }
        #endregion
        #region Получаем из строки выражение
        //Метод получения выражения
        internal string GetExpression(string _str)
        {
            
            var _exp = string.Empty;//создаем переменную в которую будем записывать свое выражение
           //Блок кода связка ветвлений, которая определяет приоритетность операторов 
            if (_str.Contains("*"))//есть строка содержит знак умножения
            {
                var _indexM = _str.IndexOf("*");//определяем индекс
                var _valueLeft = GetLeftValue(_str, _indexM);//получаем значение/операнд слева от оператора
                var _valueRight = GetRightValue(_str, _indexM);//получаем значение/операнд справа от оператора
                _exp = _valueLeft + "*" + _valueRight;//сохраняем выражение
            }
            //если предыдущий не нашли, то ищем остальные по аналогии
            else if (_str.Contains("/"))
            {
                var _indexPM = _str.IndexOf("/");
                var _valueLeft = GetLeftValue(_str, _indexPM);
                var _valueRight = GetRightValue(_str, _indexPM);
                _exp = _valueLeft + "/" + _valueRight;
            }
            else if (_str.Contains("+"))
            {
                var _indexPM = _str.IndexOf("+");
                var _valueLeft = GetLeftValue(_str, _indexPM);
                var _valueRight = GetRightValue(_str, _indexPM);
                _exp = _valueLeft + "+" + _valueRight;
            }
            else if (_str.Contains("-"))
            {
                var _indexPM = _str.IndexOf("-");
                var _valueLeft = GetLeftValue(_str, _indexPM);
                var _valueRight = GetRightValue(_str, _indexPM);
                _exp = _valueLeft + "-" + _valueRight;
            }
            return _exp;//возвращаем  выражение
        }
        #endregion
        #region Получение левого значения от найденого оператора
        //метод получения левого значения/операнда от оператора
        internal string GetLeftValue(string _str, int _index)
        {

            var _valueLeft = "";//пустая строка в которую запишем значение/операнд
            var _indexLeft = _index - 1;//индекс с которого будем начинать идти по циклу
            //for(начинаем идти по циклу с указоного индекса; идем до тех пор пока индекс больше/равен нулю, после каждой итерации индекс уменьшается на единицу)
            for (var i = _indexLeft; i >= 0; i--)
            {
                //делаем проверку (являеться ли елемент цифрой)
                if (char.IsDigit(_str[i]))
                {
                    _indexLeft = i;//если правда присваеваем елементу этого значения/операнда индекс равный итерации 
                }
                else
                {
                    _indexLeft = i + 1; break;//если ложь (это не цифра), то присваеваем индексу значение данная итерация плюс один 
                }
            }
            //получаем значение со строки методом подстроки с стартовым индексом _indexLeft(до которого мы дошли) и длинной равняющейся _indexLeft минус индекс оператора от которого считали.
            _valueLeft = _str.Substring(_indexLeft, _index - _indexLeft);
            return _valueLeft;
        }
        #endregion
        #region Получение правого значения от найденого оператора
        //метод получения правого значения от оператора
        internal string GetRightValue(string _str, int _index)
        {
            var _valueRight = "";//сострока в которую запишем 
            //for(начинаем идти по циклу с индекса знака + 1; идем до тех пор пока индекс меньше длины строки, после каждой итерации индекс увеличивается на единицу)
            for (int i = _index + 1; i < _str.Length; i++)
            {
                //делаем проверку (являеться ли елемент цифрой)
                if (char.IsDigit(_str[i]))
                {
                    _valueRight += _str[i];//если является, довляем елемент в строку, таким образом формируем правое значение/операнд от оператора.
                }
                else
                    break;//если елемент не является цифрой, выходим из цикла
            }
            return _valueRight;//возвращаем правое значение/операнд
        }
        #endregion
    }
}