using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите двухзначное число: "); // выводим на экран сообщение с просьбой ввести число
            int number = int.Parse(Console.ReadLine()); // считываем введенное число и сохраняем его в переменную number

            int sum = number / 10 + number % 10; // вычисляем сумму цифр числа и сохраняем ее в переменную sum

            switch (sum) // начинаем конструкцию switch, которая позволяет выбирать действие в зависимости от значения переменной sum
            {
                case 2: // если значение sum равно 2
                    Console.WriteLine("Два"); // выводим на экран слово "Два"
                    break; // завершаем конструкцию case
                case 3: // если значение sum равно 3
                    Console.WriteLine("Три"); // выводим на экран слово "Три"
                    break; // завершаем конструкцию case
                case 4: // если значение sum равно 4
                    Console.WriteLine("Четыре"); // выводим на экран слово "Четыре"
                    break; // завершаем конструкцию case
                case 5: // если значение sum равно 5
                    Console.WriteLine("Пять"); // выводим на экран слово "Пять"
                    break; // завершаем конструкцию case
                case 6: // если значение sum равно 6
                    Console.WriteLine("Шесть"); // выводим на экран слово "Шесть"
                    break; // завершаем конструкцию case
                case 7: // если значение sum равно 7
                    Console.WriteLine("Семь"); // выводим на экран слово "Семь"
                    break; // завершаем конструкцию case
                case 8: // если значение sum равно 8
                    Console.WriteLine("Восемь"); // выводим на экран слово "Восемь"
                    break; // завершаем конструкцию case
                case 9: // если значение sum равно 9
                    Console.WriteLine("Девять"); // выводим на экран слово "Девять"
                    break; // завершаем конструкцию case
                case 10: // если значение sum равно 10
                    Console.WriteLine("Десять"); // выводим на экран слово "Десять"
                    break; // завершаем конструкцию case
                case 11: // если значение sum равно 11
                    Console.WriteLine("Одиннадцать"); // выводим на экран слово "Одиннадцать"
                    break; // завершаем конструкцию case
                case 12: // если значение sum равно 12
                    Console.WriteLine("Двенадцать"); // выводим на экран слово "Двенадцать"
                    break; // завершаем конструкцию case
                default: // если значение sum не соответствует ни одному из предыдущих case
                    Console.WriteLine("Ошибка: введено неверное число."); // выводим на экран сообщение об ошибке
                    break; // завершаем конструкцию case
            }

            Console.ReadKey(); // ожидаем нажатия клавиши пользователем
        }
    }
}
