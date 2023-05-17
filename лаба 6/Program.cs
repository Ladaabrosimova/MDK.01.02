using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ///Вариант №2
            ///Создать файл nmbrs.txt и записать в него(вручную, не программно) натуральные числа от 1 до 500 через запятую.
            ///Считать содержимое файла и вывести в консоль каждое третье значение, записанное через точку с запятой.
            

            string filePath = "nmbrs.txt"; //Устаноовка значения переменной filePath равным nmbrs.txt
            string numbers = File.ReadAllText(filePath); //Считывание всего содержимого файла в строчку
            string[] numbersArray = numbers.Split(','); //Разбиение строк на массивы строк, используя запятые в качестве разделителей
            for (int i = 2; i < numbersArray.Length; i += 3) //Цикл, начинающийся со 2го элемента и увеличивающийся на 3 на каждой итерации
            {
                Console.Write(numbersArray[i] + "; "); // Вывод  на экран каждого третьего числа, через ';' и ' '
            }
        }
    }
}
