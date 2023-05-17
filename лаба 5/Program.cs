using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace лаба_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ///Вариант 2 (28)
            ///Строковому меню предшествует центрированная надпись: «Выберите интересующий музыкальный жанр:».
            ///Надпись не участвует в пролистывании В качестве пунктов меню предложить: «Классика», «Рок», «Джаз», «Фолк», «Кантри».
            ///Выход из меню предусмотреть по нажатии клавиши «n».
            ///Реализовать «пролистывание» без перехода через границы(стрелка вверх не работает на элементе с нулевым индексом и стрелка вниз не работает на элементе с последним индексом).
            ///Цветовая схема консоли: чёрный фон, белый шрифт.
            ///Цветовая схема курсора: красный фон, белый шрифт.Подтверждение выбранного пункта меню реализовать по клавише «y».

            string[] menuItems = { "Классика", "Рок", "Джаз", "Фолк", "Кантри" }; // создание массива строк с названиями жанров
            int currentIndex = 0; // объявление переменной для хранения индекса текущего выбранного жанра

            Console.BackgroundColor = ConsoleColor.Black; // установка цвета фона консоли на черный
            Console.ForegroundColor = ConsoleColor.White; // установка цвета текста на белый

            Console.WriteLine("Выберите интересующий музыкальный жанр:"); // вывод сообщения на консоль

            while (true) // бесконечный цикл
            {
                Console.Clear(); // очистка консоли

                for (int i = 0; i < menuItems.Length; i++) // цикл для вывода списка жанров на консоль
                {
                    if (i == currentIndex) // если текущий индекс совпадает с индексом выбранного жанра
                    {
                        Console.BackgroundColor = ConsoleColor.Red; // установка цвета фона на красный
                        Console.ForegroundColor = ConsoleColor.White; // установка цвета текста на белый
                    }
                    Console.WriteLine(menuItems[i]); // вывод названия жанра на консоль
                    Console.ResetColor(); // сброс цветов на стандартные
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(); // чтение нажатой клавиши

                switch (keyInfo.Key) // проверка нажатой клавиши
                {
                    case ConsoleKey.UpArrow: // если нажата клавиша вверх
                        if (currentIndex > 0) // если текущий индекс больше 0
                        {
                            currentIndex--; // уменьшаем текущий индекс на 1
                        }
                        break;

                    case ConsoleKey.DownArrow: // если нажата клавиша вниз
                        if (currentIndex < menuItems.Length - 1) // если текущий индекс меньше, чем количество жанров в массиве минус 1
                        {
                            currentIndex++; // увеличиваем текущий индекс на 1
                        }
                        break;

                    case ConsoleKey.Y: // если нажата клавиша Y
                        Console.Clear();
                        Console.WriteLine($"Вы выбрали {menuItems[currentIndex]}"); // вывод сообщения о выбранном жанре на консоль
                        return; // завершение работы метода Main

                    case ConsoleKey.N: // если нажата клавиша N
                        Console.Clear();
                        return; // завершение работы метода Main
                }
            }
        }
    }
}
    

