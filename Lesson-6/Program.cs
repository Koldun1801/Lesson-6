
using System.Text;

namespace Lesson6
{
    class Programm
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo ski;
            string pathFile = "DataWorkers.csv"; //вывел путь к файлу в метод Main т.к. в изначальных планах было добавить выбор файла

            Console.WriteLine("Нажмите на кнопку \n\t1 — вывести данные на экран; \n\t2 — заполнить данные и добавить новую запись в конец файла.");
            
            while (true)
            {
                ski = Console.ReadKey();

                if (ski.Key == ConsoleKey.D1 || ski.Key == ConsoleKey.NumPad1)
                {
                    Exercise_1(pathFile);
                    break;
                }

                if (ski.Key == ConsoleKey.D2 || ski.Key == ConsoleKey.NumPad2)
                {
                    Exercise_2(pathFile);
                    break;
                }
            }
            Console.ReadKey();
        }

        /// <summary>
        /// выводит данные о всех работниках в консоль
        /// </summary>
        /// <param name="path">путь к файлу с таблицей</param>
        static void Exercise_1(string path)
        {
            if (File.Exists(path) == true)
            {
                Write(WorkersList(path));
            }
            else
            {
                File.Create(path);
                Write("Создан пустой файл");
            }
        }

        /// <summary>
        /// метод для добавления сотрудника в таблицу
        /// </summary>
        /// <param name="path">путь к файлу с таблицей</param>
        static void Exercise_2(string path)
        {
            char key = 'д';

            do
            {
                AddWorker(FillingForm(), path);

                Console.WriteLine("Добавить ещё сотрудника? д/н");
                key = Console.ReadKey(true).KeyChar;
            }
            while (char.ToLower(key) == 'д');
        }

        /// <summary>
        /// добавление сотрудника в файл (если файла не существует, то он будет создан)
        /// </summary>
        /// <param name="worker">данные о работнике</param>
        /// <param name="path">путь к файлу с таблицей</param>
        static void AddWorker(string worker, string path)
        {
            if (File.Exists(path) == true)
            {
                int nextStringIndex = NextStringIndex(path);
                string addedWorker = nextStringIndex.ToString() + '#' + worker;
                if (nextStringIndex == 1)
                {
                    File.WriteAllText(path, addedWorker, Encoding.Unicode);
                }
                else
                {
                    File.AppendAllText(path, "\n"+addedWorker, Encoding.Unicode);
                }
            }
            else
            {
                string addedWorker = "1#" + worker;
                File.WriteAllText(path, addedWorker, Encoding.Unicode);
            }
        }

        /// <summary>
        /// возвращает номер следующей строки в файле, нумерация начинается с 1
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <returns>номер следующей строки</returns>
        static int NextStringIndex(string path)
        {
            if (File.Exists(path) == false) return 1;
            else
            {
                string[] lines = File.ReadAllLines(path);
                int result = 1 + lines.Length;
                if (lines==null)
                    result = 1;
                return result;
            }
        }

        /// <summary>
        /// метод для получения списка сотрудников из файла
        /// </summary>
        /// <param name="path"> путь к файлу со списком</param>
        /// <returns>массив строк, каждая строка это данные одного сотрудника</returns>
        static string[] WorkersList(string path)
        {
            string[] workers = File.ReadAllLines(path);
            return workers;
        }

        /// <summary>
        /// метод выводящий переменные типа string построчно
        /// </summary>
        /// <param name="words">выводимые переменные</param>
        static void Write(string[] words)
        {
            Console.WriteLine("\n");
            foreach (string word in words)
            {
                string wordOut = StringOutputFormat(word);
                Console.WriteLine(wordOut);
            }
        }
        static void Write(string word)
        {
            Console.WriteLine("\n"+word);
        }

        /// <summary>
        /// приведение строк к единному формату, для аккуратного вывода
        /// </summary>
        /// <param name="inputString">строка формата "из таблицы"</param>
        /// <returns>Строка готовая к выводу в консоль</returns>
        static string StringOutputFormat(string inputString)
        {
            //параметры форматирования строк для вывода
            int valueNumber = 5,
                valueDataAdd = 20,
                valueName = 33,
                valueAge = 6,
                valueHeight = 7,
                valueDataBirth = 14,
                valueTown = 32;

            string[] words = inputString.Split('#');
            string result;

            if (words[0] == "") return "Файл пуст";


            result = NormalizeWord(words[0], valueNumber);
            result += NormalizeWord(words[1], valueDataAdd);
            result += NormalizeWord(words[2], valueName);
            result += NormalizeWord(words[3], valueAge);
            result += NormalizeWord(words[4], valueHeight);
            result += NormalizeWord(words[5], valueDataBirth);
            result += NormalizeWord(words[6], valueTown);

            return result;
        }

        /// <summary>
        /// приводит строку к необходипой длине(обрезает конец или добавляет пробелы)
        /// </summary>
        /// <param name="word">строка приводимая к необходимой длине</param>
        /// <param name="lenghtWord">необходимая длина строки</param>
        /// <returns>строка приведённая к необходимой длине</returns>
        static string NormalizeWord(string word, int lenghtWord)
        {
            string result = word;
            if(word.Length>lenghtWord)
            {
                result= word.Substring(0,lenghtWord);
            }
            if(word.Length<lenghtWord)
            {
                while(result.Length<lenghtWord)
                {
                    result += " ";
                }
            }
            return result;
            
        }

        /// <summary>
        /// метод для сбора данных о сотруднике
        /// </summary>
        /// <returns>строка содержащая информацию о сотруднике и времени добавления его в базу</returns>
        static string FillingForm()
        {
            string result = DateTime.Now.ToString("g");

            Console.WriteLine("введите Ф.И.О. сотрудника");
            result += "#" + Console.ReadLine();

            Console.WriteLine("введите возраст сотрудника");
            result += "#" + Console.ReadLine();

            Console.WriteLine("введите рост сотрудника");
            result += "#" + Console.ReadLine();

            Console.WriteLine("введите дату рождения сотрудника");
            result += "#" + Console.ReadLine();

            Console.WriteLine("введите место рождения сотрудника");
            result += "#" + Console.ReadLine();

            return result;
        }
    }
}