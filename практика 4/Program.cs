using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace практика_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ///Реализовать «выкройку» недиагональных элементов одной матрицы и «вклеивание» их поверх элементов другой матрицы.
            ///«Выкройка» подразумевает выставление на позициях элементов значений, равных «-1»
            ///Вариант 2
        
            // создание двух матриц
            int[,] matrix1 = new int[,]
            {
            { 1, 2, 3 },
            { 4, -1, 6 },
            { 7, 8, 9 }
            };

            int[,] matrix2 = new int[,]
            {
            { 10, 11, 12 },
            { 13, 14, 15 },
            { 16, 17, 18 }
            };

            // выкройка элементов
            for (int i = 0; i < matrix1.GetLength(0); i++) // цикл по строкам матрицы 1
            {
                for (int j = 0; j < matrix1.GetLength(1); j++) // цикл по столбцам матрицы 1
                {
                    if (matrix1[i, j] == -1) // если элемент матрицы 1 равен -1
                    {
                        matrix1[i, j] = matrix2[i, j]; // заменить его на соответствующий элемент матрицы 2
                        matrix2[i, j] = -1; // а элемент матрицы 2 заменить на -1
                    }
                }
            }

            // вывод результатов
            Console.WriteLine("Матрица 1:");
            for (int i = 0; i < matrix1.GetLength(0); i++) // цикл по строкам матрицы 1
            {
                for (int j = 0; j < matrix1.GetLength(1); j++) // цикл по столбцам матрицы 1
                {
                    Console.Write(matrix1[i, j] + "\t"); // вывод элемента матрицы 1
                }
                Console.WriteLine(); // переход на новую строку
            }

            Console.WriteLine("Матрица 2:");
            for (int i = 0; i < matrix2.GetLength(0); i++) // цикл по строкам матрицы 2
            {
                for (int j = 0; j < matrix2.GetLength(1); j++) // цикл по столбцам матрицы 2
                {
                    Console.Write(matrix2[i, j] + "\t"); // вывод элемента матрицы 2
                }
                Console.WriteLine(); // переход на новую строку
            }
        }
    }
}

