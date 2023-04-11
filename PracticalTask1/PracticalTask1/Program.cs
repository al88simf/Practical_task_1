using System;

namespace PracticalTask1
{
    /// <summary>
    /// Класс Program обеспечивает преобразование в двоичную, восьмеричную и 
    /// шестнадцатеричную системы счисления.
    /// </summary>
    /// <remarks>
    /// Класс принимает число и номер основания системы счисления, проверяет корректность
    /// ввода и повторяется, если ввод не верен. Далее выполняет преобразование 
    /// введенного числа по выбранной системе счисления с помощью операции деления
    /// с остатком. Выводит на консоль результат с использованием массивов.
    /// </remarks>
    class Program
    {
        
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        /// <param name="args">Список аргументов для командной строки.</param>
        static void Main(string[] args)
        {
            int[] numbers = new int[0];             // двоичные и восьмеричные числа
            string[] hexNumbers = new string[0];    // шестнадцатеричные числа

            // Цикл проверяет корректность ввода и повторяется, если он ошибочен.
            while (true)
            {
                Console.Write("Введите число: ");
                string? inputNum = Console.ReadLine();  // введенное число

                if (int.TryParse(inputNum, out int divider))
                {
                    Console.WriteLine("Выберите основание системы счисления");
                    Console.WriteLine("\tДвоичная - введите 2." +
                        "\n\tВосьмеричная - введите 8." +
                        "\n\tШестнадцатеричная - введите 16.");
                    Console.Write("Вы выбрали: ");
                    string? inputRadix = Console.ReadLine();    // ввод система счисления

                    if (int.TryParse(inputRadix, out int radix) && 
                        (radix == 2 || radix == 8 || radix == 16))
                    {                        
                        switch (radix)
                        {
                            case 2:
                                if (radix == 2)
                                {
                                    // Проверка, если переход от 8.
                                    Console.WriteLine("Двоичную систему счисления.");
                                }
                                if (divider > 0)
                                {
                                    // Когда в процессе деления делитель уменьшается
                                    // до нуля, преобразование завершается.
                                    while (divider > 0)
                                    {
                                        divider = Math.DivRem(divider, 
                                            radix, 
                                            out int reminder);
                                        numbers = Add(reminder, numbers);
                                    }
                                    numbers = Reverse(numbers);
                                }
                                // Если введенное значение ноль или меньше - выводит 0.
                                else Console.WriteLine(0);
                                break;
                            case 8:
                                Console.WriteLine("Восьмеричную систему счисления.");
                                
                                // Преобразование в восьмеричное аналогично двоичному.
                                goto case 2;
                            case 16:
                                Console.WriteLine("Шестнадцатеричную систему счисления.");
                                if (divider > 0)
                                {
                                    while (divider > 0)
                                    {
                                        divider = Math.DivRem(divider, 
                                            radix, 
                                            out int reminder);
                                        numbers = Add(reminder, numbers);
                                    }
                                    hexNumbers = AddHex(numbers);
                                    hexNumbers = ReverseHex(hexNumbers);
                                }
                                break;
                        }
                        Console.Write($"\tРезультат: ");
                        if (radix == 2 || radix == 8)
                        {
                            foreach (int i in numbers) Console.Write($"{i}");
                        }
                        else
                        {
                            foreach (string i in hexNumbers) Console.Write($"{i}");
                        }
                        Console.WriteLine();    // пустая строка

                        break;                  // выход из цикла
                    }
                    else Console.WriteLine("Введите значение корректно!");
                }
                else Console.WriteLine("Введите значение корректно!");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Добавляет в массив результаты вычислений в обратном порядке 
        /// (начиная с последнего числа).
        /// </summary>
        /// <param name="item">Принимает результат вычислений.</param>
        /// <param name="numbers">Принимает пустой массив.</param>
        /// <returns>
        /// Возвращает многократно заполненный массив.
        /// </returns>
        static int[] Add(int item, int[] numbers)
        {
            int[] nums = new int[numbers.Length + 1];   // временный массив

            for (int i = 0; i < numbers.Length; i++) nums[i] = numbers[i];

            // Добавляет принятое значение в последний пустой элемент.
            nums[numbers.Length] = item;
            return nums;
        }

        /// <summary>
        /// Переворачивает массив для правильного вывода.
        /// </summary>
        /// <param name="nums">Принимает массив, заполненный результатами в
        /// обратном порядке.</param>
        /// <returns>
        /// Возвращает перевернутый массив.
        /// </returns>
        static int[] Reverse(int[] nums)
        {
            int temp;   // переменная для временного хранения значений
            int len = nums.Length;

            for (int i = 0; i < (nums.Length / 2); i++)
            {
                temp = nums[i];

                // Присваивает элементу по текущему указателю значение элемента с конца.
                nums[i] = nums[len - i - 1];

                // И элементу пропорционально конца значение текущего.
                nums[len - i - 1] = temp;
            }
            return nums;
        }

        /// <summary>
        /// Меняет некоторые результаты на символы шестнадцатеричного кода.
        /// </summary>
        /// <param name="numbers">Принимает пустой массив.</param>
        /// <returns>
        /// Возворащает заполненный массив.
        /// </returns>
        static string[] AddHex(int[] numbers)
        {
            string[] nums = new string[numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
            {
                if ((numbers[i] >= 10) && (numbers[i] <= 15))
                {
                    nums[i] = numbers[i] switch
                    {
                        10 => "A",
                        11 => "B",
                        12 => "C",
                        13 => "D",
                        14 => "E",
                        _ => "F",
                    };
                }
                // Замена значений больше 15 числами с разницей в 5.
                else if (numbers[i] >= 15) nums[i] = Convert.ToString(numbers[i] - 5);
                else nums[i] = Convert.ToString(numbers[i]);
            }
            return nums;
        }

        /// <summary>
        /// Переворачивает шестнадцатеричный массив для правильного отображения.
        /// </summary>
        /// <param name="hexNums">Принимает массив, заполненный результатами в 
        /// обратном порядке.</param>
        /// <returns>
        /// Возвращает переверернутый массив.
        /// </returns>
        static string[] ReverseHex(string[] hexNums)
        {
            string temp;
            int len = hexNums.Length;

            for (int i = 0; i < (hexNums.Length / 2); i++)
            {
                temp = hexNums[i];
                hexNums[i] = hexNums[len - i - 1];
                hexNums[len - i - 1] = temp;
            }
            return hexNums;
        }

    }
}
