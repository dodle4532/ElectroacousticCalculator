using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Class
{
    public class SpecialMathHelper
    {
        public static int[] Proc_USH(int ush)
        {
            // Инициализация массива m_ush (15 строк, 6 столбцов)
            int[,] m_ush = new int[15, 6];

            // Заполнение массива контрольными значениями
            // Строка 0
            m_ush[0, 0] = 25; m_ush[0, 1] = 31; m_ush[0, 2] = 24; m_ush[0, 3] = 20; m_ush[0, 4] = 17; m_ush[0, 5] = 14;
            // Строка 1
            m_ush[1, 0] = 30; m_ush[1, 1] = 35; m_ush[1, 2] = 29; m_ush[1, 3] = 25; m_ush[1, 4] = 22; m_ush[1, 5] = 20;
            // Строка 2
            m_ush[2, 0] = 35; m_ush[2, 1] = 40; m_ush[2, 2] = 34; m_ush[2, 3] = 30; m_ush[2, 4] = 27; m_ush[2, 5] = 25;
            // Строка 3
            m_ush[3, 0] = 40; m_ush[3, 1] = 45; m_ush[3, 2] = 39; m_ush[3, 3] = 35; m_ush[3, 4] = 32; m_ush[3, 5] = 30;
            // Строка 4
            m_ush[4, 0] = 45; m_ush[4, 1] = 49; m_ush[4, 2] = 44; m_ush[4, 3] = 40; m_ush[4, 4] = 37; m_ush[4, 5] = 35;
            // Строка 5
            m_ush[5, 0] = 50; m_ush[5, 1] = 54; m_ush[5, 2] = 49; m_ush[5, 3] = 45; m_ush[5, 4] = 42; m_ush[5, 5] = 40;
            // Строка 6
            m_ush[6, 0] = 55; m_ush[6, 1] = 59; m_ush[6, 2] = 54; m_ush[6, 3] = 50; m_ush[6, 4] = 47; m_ush[6, 5] = 45;
            // Строка 7
            m_ush[7, 0] = 60; m_ush[7, 1] = 63; m_ush[7, 2] = 58; m_ush[7, 3] = 55; m_ush[7, 4] = 52; m_ush[7, 5] = 50;
            // Строка 8
            m_ush[8, 0] = 65; m_ush[8, 1] = 67; m_ush[8, 2] = 63; m_ush[8, 3] = 60; m_ush[8, 4] = 57; m_ush[8, 5] = 55;
            // Строка 9
            m_ush[9, 0] = 70; m_ush[9, 1] = 72; m_ush[9, 2] = 68; m_ush[9, 3] = 65; m_ush[9, 4] = 62; m_ush[9, 5] = 60;
            // Строка 10
            m_ush[10, 0] = 75; m_ush[10, 1] = 77; m_ush[10, 2] = 73; m_ush[10, 3] = 70; m_ush[10, 4] = 67; m_ush[10, 5] = 65;
            // Строка 11
            m_ush[11, 0] = 80; m_ush[11, 1] = 82; m_ush[11, 2] = 78; m_ush[11, 3] = 75; m_ush[11, 4] = 72; m_ush[11, 5] = 70;
            // Строка 12
            m_ush[12, 0] = 85; m_ush[12, 1] = 87; m_ush[12, 2] = 83; m_ush[12, 3] = 80; m_ush[12, 4] = 78; m_ush[12, 5] = 76;
            // Строка 13
            m_ush[13, 0] = 90; m_ush[13, 1] = 92; m_ush[13, 2] = 88; m_ush[13, 3] = 85; m_ush[13, 4] = 83; m_ush[13, 5] = 81;
            // Строка 14
            m_ush[14, 0] = 95; m_ush[14, 1] = 97; m_ush[14, 2] = 93; m_ush[14, 3] = 90; m_ush[14, 4] = 88; m_ush[14, 5] = 86;

            int[] U_shf = new int[5]; // Результирующий массив

            // Вычисление октавных уровней
            if (ush <= 25)
            {
                int p_dop1 = 25 - ush;
                U_shf[0] = m_ush[0, 1] - p_dop1;
                U_shf[1] = m_ush[0, 2] - p_dop1;
                U_shf[2] = m_ush[0, 3] - p_dop1;
                U_shf[3] = m_ush[0, 4] - p_dop1;
                U_shf[4] = m_ush[0, 5] - p_dop1;
            }
            else if (ush > 25 && ush <= 30)
            {
                int p_dop1 = 30 - ush;
                int p_dop2 = ush - 25;
                U_shf[0] = Convert.ToInt32((p_dop1 * m_ush[0, 1] + p_dop2 * m_ush[1, 1]) / 5);
                U_shf[1] = Convert.ToInt32((p_dop1 * m_ush[0, 2] + p_dop2 * m_ush[1, 2]) / 5);
                U_shf[2] = Convert.ToInt32((p_dop1 * m_ush[0, 3] + p_dop2 * m_ush[1, 3]) / 5);
                U_shf[3] = Convert.ToInt32((p_dop1 * m_ush[0, 4] + p_dop2 * m_ush[1, 4]) / 5);
                U_shf[4] = Convert.ToInt32((p_dop1 * m_ush[0, 5] + p_dop2 * m_ush[1, 5]) / 5);
            }
            else if (ush > 30 && ush <= 35)
            {
                int p_dop1 = 35 - ush;
                int p_dop2 = ush - 30;
                U_shf[0] = Convert.ToInt32((p_dop1 * m_ush[1, 1] + p_dop2 * m_ush[2, 1]) / 5);
                U_shf[1] = Convert.ToInt32((p_dop1 * m_ush[1, 2] + p_dop2 * m_ush[2, 2]) / 5);
                U_shf[2] = Convert.ToInt32((p_dop1 * m_ush[1, 3] + p_dop2 * m_ush[2, 3]) / 5);
                U_shf[3] = Convert.ToInt32((p_dop1 * m_ush[1, 4] + p_dop2 * m_ush[2, 4]) / 5);
                U_shf[4] = Convert.ToInt32((p_dop1 * m_ush[1, 5] + p_dop2 * m_ush[2, 5]) / 5);
            }
            else if (ush > 35 && ush <= 40)
            {
                int p_dop1 = 40 - ush;
                int p_dop2 = ush - 35;
                U_shf[0] = Convert.ToInt32((p_dop1 * m_ush[2, 1] + p_dop2 * m_ush[3, 1]) / 5);
                U_shf[1] = Convert.ToInt32((p_dop1 * m_ush[2, 2] + p_dop2 * m_ush[3, 2]) / 5);
                U_shf[2] = Convert.ToInt32((p_dop1 * m_ush[2, 3] + p_dop2 * m_ush[3, 3]) / 5);
                U_shf[3] = Convert.ToInt32((p_dop1 * m_ush[2, 4] + p_dop2 * m_ush[3, 4]) / 5);
                U_shf[4] = Convert.ToInt32((p_dop1 * m_ush[2, 5] + p_dop2 * m_ush[3, 5]) / 5);
            }
            else if (ush > 40 && ush <= 45)
            {
                int p_dop1 = 45 - ush;
                int p_dop2 = ush - 40;
                U_shf[0] = Convert.ToInt32((p_dop1 * m_ush[3, 1] + p_dop2 * m_ush[4, 1]) / 5);
                U_shf[1] = Convert.ToInt32((p_dop1 * m_ush[3, 2] + p_dop2 * m_ush[4, 2]) / 5);
                U_shf[2] = Convert.ToInt32((p_dop1 * m_ush[3, 3] + p_dop2 * m_ush[4, 3]) / 5);
                U_shf[3] = Convert.ToInt32((p_dop1 * m_ush[3, 4] + p_dop2 * m_ush[4, 4]) / 5);
                U_shf[4] = Convert.ToInt32((p_dop1 * m_ush[3, 5] + p_dop2 * m_ush[4, 5]) / 5);
            }
            else if (ush > 45 && ush <= 50)
            {
                int p_dop1 = 50 - ush;
                int p_dop2 = ush - 45;
                U_shf[0] = Convert.ToInt32((p_dop1 * m_ush[4, 1] + p_dop2 * m_ush[5, 1]) / 5);
                U_shf[1] = Convert.ToInt32((p_dop1 * m_ush[4, 2] + p_dop2 * m_ush[5, 2]) / 5);
                U_shf[2] = Convert.ToInt32((p_dop1 * m_ush[4, 3] + p_dop2 * m_ush[5, 3]) / 5);
                U_shf[3] = Convert.ToInt32((p_dop1 * m_ush[4, 4] + p_dop2 * m_ush[5, 4]) / 5);
                U_shf[4] = Convert.ToInt32((p_dop1 * m_ush[4, 5] + p_dop2 * m_ush[5, 5]) / 5);
            }
            else if (ush > 50 && ush <= 55)
            {
                int p_dop1 = 55 - ush;
                int p_dop2 = ush - 50;
                U_shf[0] = Convert.ToInt32((p_dop1 * m_ush[5, 1] + p_dop2 * m_ush[6, 1]) / 5);
                U_shf[1] = Convert.ToInt32((p_dop1 * m_ush[5, 2] + p_dop2 * m_ush[6, 2]) / 5);
                U_shf[2] = Convert.ToInt32((p_dop1 * m_ush[5, 3] + p_dop2 * m_ush[6, 3]) / 5);
                U_shf[3] = Convert.ToInt32((p_dop1 * m_ush[5, 4] + p_dop2 * m_ush[6, 4]) / 5);
                U_shf[4] = Convert.ToInt32((p_dop1 * m_ush[5, 5] + p_dop2 * m_ush[6, 5]) / 5);
            }
            else if (ush > 55 && ush <= 60)
            {
                int p_dop1 = 60 - ush;
                int p_dop2 = ush - 55;
                U_shf[0] = Convert.ToInt32((p_dop1 * m_ush[6, 1] + p_dop2 * m_ush[7, 1]) / 5);
                U_shf[1] = Convert.ToInt32((p_dop1 * m_ush[6, 2] + p_dop2 * m_ush[7, 2]) / 5);
                U_shf[2] = Convert.ToInt32((p_dop1 * m_ush[6, 3] + p_dop2 * m_ush[7, 3]) / 5);
                U_shf[3] = Convert.ToInt32((p_dop1 * m_ush[6, 4] + p_dop2 * m_ush[7, 4]) / 5);
                U_shf[4] = Convert.ToInt32((p_dop1 * m_ush[6, 5] + p_dop2 * m_ush[7, 5]) / 5);
            }
            else if (ush > 60 && ush <= 65)
            {
                int p_dop1 = 65 - ush;
                int p_dop2 = ush - 60;
                U_shf[0] = Convert.ToInt32((p_dop1 * m_ush[7, 1] + p_dop2 * m_ush[8, 1]) / 5);
                U_shf[1] = Convert.ToInt32((p_dop1 * m_ush[7, 2] + p_dop2 * m_ush[8, 2]) / 5);
                U_shf[2] = Convert.ToInt32((p_dop1 * m_ush[7, 3] + p_dop2 * m_ush[8, 3]) / 5);
                U_shf[3] = Convert.ToInt32((p_dop1 * m_ush[7, 4] + p_dop2 * m_ush[8, 4]) / 5);
                U_shf[4] = Convert.ToInt32((p_dop1 * m_ush[7, 5] + p_dop2 * m_ush[8, 5]) / 5);
            }
            else if (ush > 65 && ush <= 70)
            {
                int p_dop1 = 70 - ush;
                int p_dop2 = ush - 65;
                U_shf[0] = Convert.ToInt32((p_dop1 * m_ush[8, 1] + p_dop2 * m_ush[9, 1]) / 5);
                U_shf[1] = Convert.ToInt32((p_dop1 * m_ush[8, 2] + p_dop2 * m_ush[9, 2]) / 5);
                U_shf[2] = Convert.ToInt32((p_dop1 * m_ush[8, 3] + p_dop2 * m_ush[9, 3]) / 5);
                U_shf[3] = Convert.ToInt32((p_dop1 * m_ush[8, 4] + p_dop2 * m_ush[9, 4]) / 5);
                U_shf[4] = Convert.ToInt32((p_dop1 * m_ush[8, 5] + p_dop2 * m_ush[9, 5]) / 5);
            }
            else if (ush > 70 && ush <= 75)
            {
                int p_dop1 = 75 - ush;
                int p_dop2 = ush - 70;
                U_shf[0] = Convert.ToInt32((p_dop1 * m_ush[9, 1] + p_dop2 * m_ush[10, 1]) / 5);
                U_shf[1] = Convert.ToInt32((p_dop1 * m_ush[9, 2] + p_dop2 * m_ush[10, 2]) / 5);
                U_shf[2] = Convert.ToInt32((p_dop1 * m_ush[9, 3] + p_dop2 * m_ush[10, 3]) / 5);
                U_shf[3] = Convert.ToInt32((p_dop1 * m_ush[9, 4] + p_dop2 * m_ush[10, 4]) / 5);
                U_shf[4] = Convert.ToInt32((p_dop1 * m_ush[9, 5] + p_dop2 * m_ush[10, 5]) / 5);
            }
            else if (ush > 75 && ush <= 80)
            {
                int p_dop1 = 80 - ush;
                int p_dop2 = ush - 75;
                U_shf[0] = Convert.ToInt32((p_dop1 * m_ush[10, 1] + p_dop2 * m_ush[11, 1]) / 5);
                U_shf[1] = Convert.ToInt32((p_dop1 * m_ush[10, 2] + p_dop2 * m_ush[11, 2]) / 5);
                U_shf[2] = Convert.ToInt32((p_dop1 * m_ush[10, 3] + p_dop2 * m_ush[11, 3]) / 5);
                U_shf[3] = Convert.ToInt32((p_dop1 * m_ush[10, 4] + p_dop2 * m_ush[11, 4]) / 5);
                U_shf[4] = Convert.ToInt32((p_dop1 * m_ush[10, 5] + p_dop2 * m_ush[11, 5]) / 5);
            }
            else if (ush > 80 && ush <= 85)
            {
                int p_dop1 = 85 - ush;
                int p_dop2 = ush - 80;
                U_shf[0] = Convert.ToInt32((p_dop1 * m_ush[11, 1] + p_dop2 * m_ush[12, 1]) / 5);
                U_shf[1] = Convert.ToInt32((p_dop1 * m_ush[11, 2] + p_dop2 * m_ush[12, 2]) / 5);
                U_shf[2] = Convert.ToInt32((p_dop1 * m_ush[11, 3] + p_dop2 * m_ush[12, 3]) / 5);
                U_shf[3] = Convert.ToInt32((p_dop1 * m_ush[11, 4] + p_dop2 * m_ush[12, 4]) / 5);
                U_shf[4] = Convert.ToInt32((p_dop1 * m_ush[11, 5] + p_dop2 * m_ush[12, 5]) / 5);
            }
            else if (ush > 85 && ush <= 90)
            {
                int p_dop1 = 90 - ush;
                int p_dop2 = ush - 85;
                U_shf[0] = Convert.ToInt32((p_dop1 * m_ush[12, 1] + p_dop2 * m_ush[13, 1]) / 5);
                U_shf[1] = Convert.ToInt32((p_dop1 * m_ush[12, 2] + p_dop2 * m_ush[13, 2]) / 5);
                U_shf[2] = Convert.ToInt32((p_dop1 * m_ush[12, 3] + p_dop2 * m_ush[13, 3]) / 5);
                U_shf[3] = Convert.ToInt32((p_dop1 * m_ush[12, 4] + p_dop2 * m_ush[13, 4]) / 5);
                U_shf[4] = Convert.ToInt32((p_dop1 * m_ush[12, 5] + p_dop2 * m_ush[13, 5]) / 5);
            }
            else if (ush > 90 && ush <= 95)
            {
                // Внимание: в оригинале была опечатка (использовалась Ush_ecv). Исправлено на ush.
                int p_dop1 = 95 - ush;
                int p_dop2 = ush - 90;
                U_shf[0] = Convert.ToInt32((p_dop1 * m_ush[13, 1] + p_dop2 * m_ush[14, 1]) / 5);
                U_shf[1] = Convert.ToInt32((p_dop1 * m_ush[13, 2] + p_dop2 * m_ush[14, 2]) / 5);
                U_shf[2] = Convert.ToInt32((p_dop1 * m_ush[13, 3] + p_dop2 * m_ush[14, 3]) / 5);
                U_shf[3] = Convert.ToInt32((p_dop1 * m_ush[13, 4] + p_dop2 * m_ush[14, 4]) / 5);
                U_shf[4] = Convert.ToInt32((p_dop1 * m_ush[13, 5] + p_dop2 * m_ush[14, 5]) / 5);
            }
            else // ush > 95
            {
                int p_dop1 = ush - 95;
                U_shf[0] = m_ush[14, 1] + p_dop1;
                U_shf[1] = m_ush[14, 2] + p_dop1;
                U_shf[2] = m_ush[14, 3] + p_dop1;
                U_shf[3] = m_ush[14, 4] + p_dop1;
                U_shf[4] = m_ush[14, 5] + p_dop1;
            }

            return U_shf;
        }
    }
}
