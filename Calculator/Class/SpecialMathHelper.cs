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

            // Заполнение массива новыми данными снизу вверх

            // Строка 0 (самая нижняя строка из списка: 31 24 20 17 14)
            m_ush[0, 0] = 31; m_ush[0, 1] = 24; m_ush[0, 2] = 20; m_ush[0, 3] = 17; m_ush[0, 4] = 14;

            // Строка 1 (предпоследняя: 35 29 25 22 20)
            m_ush[1, 0] = 35; m_ush[1, 1] = 29; m_ush[1, 2] = 25; m_ush[1, 3] = 22; m_ush[1, 4] = 20;

            // Строка 2 (40 34 30 27 25)
            m_ush[2, 0] = 40; m_ush[2, 1] = 34; m_ush[2, 2] = 30; m_ush[2, 3] = 27; m_ush[2, 4] = 25;

            // Строка 3 (45 39 35 32 30)
            m_ush[3, 0] = 45; m_ush[3, 1] = 39; m_ush[3, 2] = 35; m_ush[3, 3] = 32; m_ush[3, 4] = 30;

            // Строка 4 (49 44 40 37 35)
            m_ush[4, 0] = 49; m_ush[4, 1] = 44; m_ush[4, 2] = 40; m_ush[4, 3] = 37; m_ush[4, 4] = 35;

            // Строка 5 (54 49 45 42 40)
            m_ush[5, 0] = 54; m_ush[5, 1] = 49; m_ush[5, 2] = 45; m_ush[5, 3] = 42; m_ush[5, 4] = 40;

            // Строка 6 (59 54 50 47 45)
            m_ush[6, 0] = 59; m_ush[6, 1] = 54; m_ush[6, 2] = 50; m_ush[6, 3] = 47; m_ush[6, 4] = 45;

            // Строка 7 (63 58 55 52 50)
            m_ush[7, 0] = 63; m_ush[7, 1] = 58; m_ush[7, 2] = 55; m_ush[7, 3] = 52; m_ush[7, 4] = 50;

            // Строка 8 (68 63 60 57 55)
            m_ush[8, 0] = 68; m_ush[8, 1] = 63; m_ush[8, 2] = 60; m_ush[8, 3] = 57; m_ush[8, 4] = 55;

            // Строка 9 (72 68 65 63 61)
            m_ush[9, 0] = 72; m_ush[9, 1] = 68; m_ush[9, 2] = 65; m_ush[9, 3] = 63; m_ush[9, 4] = 61;

            // Строка 10 (77 73 70 68 66)
            m_ush[10, 0] = 77; m_ush[10, 1] = 73; m_ush[10, 2] = 70; m_ush[10, 3] = 68; m_ush[10, 4] = 66;

            // Строка 11 (82 78 75 73 71)
            m_ush[11, 0] = 82; m_ush[11, 1] = 78; m_ush[11, 2] = 75; m_ush[11, 3] = 73; m_ush[11, 4] = 71;

            // Строка 12 (87 83 80 78 76)
            m_ush[12, 0] = 87; m_ush[12, 1] = 83; m_ush[12, 2] = 80; m_ush[12, 3] = 78; m_ush[12, 4] = 76;

            // Строка 13 (92 88 85 83 81)
            m_ush[13, 0] = 92; m_ush[13, 1] = 88; m_ush[13, 2] = 85; m_ush[13, 3] = 83; m_ush[13, 4] = 81;

            // Строка 14 (97 93 90 88 86) - самая верхняя строка из списка
            m_ush[14, 0] = 97; m_ush[14, 1] = 93; m_ush[14, 2] = 90; m_ush[14, 3] = 88; m_ush[14, 4] = 86;

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
