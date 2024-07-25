using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace Serpent
{
    internal class Algorithm
    {
        List<int> _text; //текст в битах
        List<int> _key; //ключ в битах
        List<List<int>> _RoundKey = new List<List<int>>(); //ключ раунда
        List<int> _f = new List<int>() { 1, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 1, 1, 1, 0, 0, 1 }; //округленная дробная часть золотого сечения 9E3779B9 в битах
        public Algorithm(List<int> text, List<int> key) //конструктор
        {
            _text = text;
            _key = key;
        }
        //свойства
        public List<int> Text { get { return _text; } set { _text = value; } }
        public List<int> Key { get { return _key; } set { _key = value; } }
        public List<int> Encrypt() //шифрование
        {
            KeyGen();
            List<int> perm = new List<int>();
            for(int i = 0; i < 128; i++) //начальная перестановка IP
            {
                perm.Add(_text[IP[i]]); 
            }

            for(int i = 0; i<31; i++) //31 раунд
            {
                for (int j = 0; j < 128; j++) //накладываем 128 ключ раунда XOR
                {
                    perm[j] = perm[j] ^ _RoundKey[i][j]; //XOR
                }
                perm = ChangeViaS(perm, i%8); //табличная замена

                List<int> yround = new List<int>();
                for (int j = 0; j < 128; j++) //Линейное преобразование блока данных таблицей замен
                {
                    int y = perm[LT[j][0]];
                    for (int k = 1; k < LT[j].Length; k++)
                    {
                        y = y ^ perm[LT[j][k]]; //XOR
                    }
                    yround.Add(y);
                }
                for (int j = 0; j < 128; j++)
                {
                    perm[j] = yround[j]; //значение в конце раунда
                }
            }

            //32 раунд
            for (int j = 0; j < 128; j++) //накладываем 128 ключ раунда XOR
            {
                perm[j] = perm[j]^_RoundKey[31][j]; //XOR
            }
            perm = ChangeViaS(perm, 31 % 8); //табличная замена

            for (int i = 0; i < 128; i++) //накладываем 32-ой ключ раунда
            {
                perm[i] = perm[i] ^ _RoundKey[32][i]; //XOR
            }

            List<int> cipher = new List<int>();
            for (int i = 0; i < 128; i++) //финальная перестановка FP
            {
                cipher.Add(perm[FP[i]]);
            }
            return cipher;
        }

        public List<int> Decrypt()
        {
            KeyGen();
            List<int> perm = new List<int>();
            List<int> c = new List<int>();
            for(int i =0; i<128; i++) //Получение введенного текста
            {
                perm.Add(new int());
                c.Add(_text[i]);
            }
            for (int i = 0; i < 128; i++) //Обратная конечная перестановка FP
            {
                perm[FP[i]] = c[i];
            }
            for (int i = 0; i < 128; i++) //накладываем 32-ой ключ раунда
            {
                perm[i] = perm[i] ^ _RoundKey[32][i]; //XOR
            }
            perm = ChangeViainvS(perm, 31 % 8); //Обратная табличная замена

            for (int j = 0; j < 128; j++) //накладываем 128 ключ раунда 31 XOR
            {
                perm[j] = perm[j] ^ _RoundKey[31][j]; //XOR
            }


            for (int i = 30; i >= 0; i--) //31 раунд в обратном порядке
            {
                List<int> yround = new List<int>();
                for (int j = 0; j < 128; j++) //Обратное линейное преобразование блока данных
                {
                    int y = perm[ILT[j][0]];
                    for (int k = 1; k < ILT[j].Length; k++)
                    {
                        y = y ^ perm[ILT[j][k]]; //XOR
                    }
                    yround.Add(y);
                }
                for (int j = 0; j < 128; j++)
                {
                    perm[j] = yround[j]; //значение после ИЛП
                }
                perm = ChangeViainvS(perm, i % 8); //табличная замена

                for (int j = 0; j < 128; j++) //накладываем 128 ключ раунда XOR
                {
                    perm[j] = perm[j] ^ _RoundKey[i][j]; //XOR
                }
            }

            List<int> ip = new List<int>();
            for (int i = 0; i < 128; i++) //Обратная начальная перестановка IP
            {
                ip.Add(new int());
            }
            for (int i = 0; i < 128; i++) //Обратная начальная перестановка IP
            {
                ip[IP[i]] = perm[i];
            }
            return ip;
        }
        private void KeyGen()//Генерация ключей
        {
            List<List<int>> w = new List<List<int>>();
            for(int i= 0; i < 8; i++) //Исходный ключ представляется в виде 8 32-битных слов для вычисления промежуточного ключа
            {
                w.Add(new List<int>());
                for (int j = 0; j < 32; j++)
                {
                    w[i].Add(_key[j + 32 * i]);
                }
            }
            for(int i = 8; i<140;i++) //вычисление промежуточного ключа w от 0 до 131
            {
                w.Add(new List<int>());
                for (int j = 0; j < 32; j++)
                {
                    w[i].Add(w[i - 8][j] ^ w[i - 5][j] ^ w[i - 3][j] ^ w[i - 1][j] ^ _f[j] ^ ConvertTo2(i - 8, 32)[j]); //XOR
                }
                List<int> c = new List<int>();
                for (int j = 0; j < 32; j++) //сдвиг на 11
                {
                    c.Add(w[i][((j + 11) + 32) % 32]);
                }
                for (int j = 0; j < 32; j++) //присвоение сдвинутого значения для w
                {
                    w[i][j] = c[j];
                }
            }

            //Вычисление ключей раундов с использованием S
            List<List<int>> roundkey = new List<List<int>>();
            for (int i = 0; i < 33; i++)
            {
                roundkey.Add(new List<int>());
                int[] order = new int[] {3, 2, 1, 0, 7, 6, 5, 4};
                for(int j = 0; j<4; j++)
                {
                    roundkey[i].AddRange(ChangeViaS(w[4*i+j+8], order[i%8]));
                }
                _RoundKey.Add(new List<int>());
                //Применение начальной перестановки IP к раундовому ключу, чтобы расположить биты ключа в надлежащем порядке
                for (int k = 0; k < 128; k++) //начальная перестановка IP
                {
                    _RoundKey[i].Add(roundkey[i][IP[k]]);
                }
            }

        }
        private List<int> ChangeViaS(List<int> w, int s) //применение операции замены
        {
            int len = w.Count() / 4;
            List<int> futureVal = new List<int>(); //будущее значение
            for(int i = 0; i<len;i++)
            {
                int[] block = new int[4];
                for (int j = 0; j < 4; j++)
                {
                    block[j] = w[j+4*i];
                }
                int tens = ConvertTo10(block);
                List<int> fourbytes = ConvertTo2(S[s, tens], 4);
                futureVal.AddRange(fourbytes);
            }
            return futureVal;
        }
        private List<int> ChangeViainvS(List<int> w, int s) //применение операции обратной замены
        {
            int len = w.Count() / 4;
            List<int> futureVal = new List<int>(); //будущее значение
            for (int i = 0; i < len; i++)
            {
                int[] block = new int[4];
                for (int j = 0; j < 4; j++)
                {
                    block[j] = w[j + 4 * i];
                }
                int tens = ConvertTo10(block);
                List<int> fourbytes = ConvertTo2(invS[s, tens], 4);
                futureVal.AddRange(fourbytes);
            }
            return futureVal;
        }

        private List<int> ConvertTo2(int n, int l) //перевод в двоичную систему
        {
            List<int> res = new List<int>();
            while (n > 0)
            {
                res.Add(n % 2);
                n = n / 2;
            }
            int len = res.Count();
            if(l==4)
            {
                while (len < 4)
                {
                    len++;
                    res.Add(0);
                }
            }
            else if(l == 32)
            {
                while (len < 32)
                {
                    len++;
                    res.Add(0);
                }
            }
            res.Reverse();
            return res;
        }
        private int ConvertTo10(int[] n) //перевод в десятичную систему 
        {
            int res = 0;
            for(int i = 0; i<n.Length; i++)
            {
                res += (int)Math.Pow(2, n.Length-i-1) *n[i];
            }
            return res;
        }

        //начальная перестановка
        int[] IP = new int[128] {0,  32,  64,  96,  1,   33,  65,  97,  2,   34,  66,  98,  3,   35,  67,  99,
                                 4,  36,  68,  100, 5,   37,  69,  101, 6,   38,  70,  102, 7,   39,  71,  103,
                                 8,  40,  72,  104, 9,   41,  73,  105, 10,  42,  74,  106, 11,  43,  75,  107,
                                12,  44,  76,  108, 13,  45,  77,  109, 14,  46,  78,  110, 15,  47,  79,  111,
                                16,  48,  80,  112, 17,  49,  81,  113, 18,  50,  82,  114, 19,  51,  83,  115,
                                20,  52,  84,  116, 21,  53,  85,  117, 22,  54,  86,  118, 23,  55,  87,  119,
                                24,  56,  88,  120, 25,  57,  89,  121, 26,  58,  90,  122, 27,  59,  91,  123,
                                28,  60,  92,  124, 29,  61,  93,  125, 30,  62,  94,  126, 31,  63,  95,  127};
        //конечная перестановка
        int[] FP = new int[128] {0,  4,   8,   12,  16,  20,  24,  28,  32,  36,  40,  44,  48,  52,  56,  60,
                                64,  68,  72,  76,  80,  84,  88,  92,  96,  100, 104, 108, 112, 116, 120, 124,
                                 1,   5,   9,  13,  17,  21,  25,  29,  33,  37,  41,  45,  49,  53,  57,  61,
                                65,  69,  73,  77,  81,  85,  89,  93,  97,  101, 105, 109, 113, 117, 121, 125,
                                 2,   6,  10,  14,  18,  22,  26,  30,  34,  38,  42,  46,  50,  54,  58,  62,
                                66,  70,  74,  78,  82,  86,  90,  94,  98,  102, 106, 110, 114, 118, 122, 126,
                                 3,   7,  11,  15,  19,  23,  27,  31,  35,  39,  43,  47,  51,  55,  59,  63,
                                67,  71,  75,  79,  83,  87,  91,  95,  99,  103, 107, 111, 115, 119, 123, 127};
        //таблицы подстановок Si 
        int[,] S = new int[8, 16] {{3,   8,   15,  1,  10,   6,   5,   11,  14,  13,  4,   2,   7,   0,   9,   12},
                                   {15,  12,  2,   7,   9,   0,   5,   10,  1,   11,  14,  8,   6,   13,  3,   4},
                                   {8,   6,   7,   9,   3,   12,  10,  15,  13,  1,   14,  4,   0,   11,  5,   2},
                                   {0,   15,  11,  8,   12,  9,   6,   3,   13,  1,   2,   4,  10,   7,   5,   14},
                                   {1,   15,  8,   3,   12,  0,   11,  6,   2,   5,   4,   10,  9,   14,  7,   13},
                                   {15,  5,   2,   11,  4,   10,  9,   12,  0,   3,   14,  8,   13,  6,   7,   1},
                                   {7,   2,   12,  5,   8,   4,   6,   11,  14,  9,   1,   15,  13,  3,   10,  0},
                                   {1,  13,  15,  0,   14,  8,   2,   11,  7,   4,   12,  10,   9,   3,   5,   6}};
        //таблицы инверсных подстановок Si 
        int[,] invS = new int[8, 16] {{13,   3,   11,  0,   10,  6,   5,   12,  1,   14,  4,   7,   15,  9,  8,   2},
                                      {5,    8,   2,   14,  15,  6,   12,  3,   11,  4,   7,   9,   1,   13, 10,  0},
                                      {12,   9,   15,  4,   11,  14,  1,   2,   0,   3,   6,   13,  5,   8,  10,  7},
                                      {0,    9,   10,  7,   11,  14,  6,   13,  3,   5,   12,  2,   4,   8,  15,  1},
                                      {5,    0,   8,   3,   10,  9,   7,   14,  2,   12,  11,  6,   4,   15, 13,  1},
                                      {8,    15,  2,   9,   4,   1,   13,  14,  11,  6,   5,   3,   7,   12, 10,  0},
                                      {15,   10,  1,   13,  5,   3,   6,   0,   4,   9,   14,  7,   2,   12, 8,   11},
                                      {3,    0,   6,   13,  9,   14,  15,  8,   5,   12,  11,  7,   10,  1,  4,   2}};
        //Линейное преобразование LT
        List<int[]> LT = new List<int[]>() {new int[]{16, 52, 56, 70, 83, 94, 105}, new int[]{72, 114, 125}, new int[]{2, 9, 15, 30, 76, 84, 126},  new int[]{36, 90, 103},
                                            new int[]{20, 56, 60, 74, 87, 98, 109}, new int[]{1, 76, 118}, new int[]{2, 6, 13, 19, 34, 80, 88},  new int[]{40, 94, 107},
                                            new int[]{24, 60, 64, 78, 91, 102, 113},    new int[]{5, 80, 122}, new int[]{6, 10, 17, 23, 38, 84, 92},  new int[]{44, 98, 111},
                                            new int[]{28, 64, 68, 82, 95, 106, 117},    new int[]{9, 84, 126}, new int[]{10, 14, 21, 27, 42, 88, 96},  new int[]{48, 102, 115},
                                            new int[]{32, 68, 72, 86, 99, 110, 121},    new int[]{2, 13, 88},  new int[]{14, 18, 25, 31, 46, 92, 100}, new int[]{52, 106, 119},
                                            new int[]{36, 72, 76, 90, 103, 114, 125},   new int[]{6, 17, 92},  new int[]{18, 22, 29, 35, 50, 96, 104}, new int[]{56, 110, 123},
                                            new int[]{1, 40, 76, 80, 94, 107, 118},    new int[]{10, 21, 96},  new int[]{22, 26, 33, 39, 54, 100, 108},   new int[] {60, 114, 127},
                                            new int[]{5, 44, 80, 84, 98, 111, 122},    new int[]{14, 25, 100}, new int[]{26, 30, 37, 43, 58, 104, 112},   new int[]{3, 118},
                                            new int[]{9, 48, 84, 88, 102, 115, 126},   new int[]{18, 29, 104}, new int[]{30, 34, 41, 47, 62, 108, 116},    new int[]{7, 122},
                                            new int[]{2, 13, 52, 88, 92, 106, 119},   new int[]{22, 33, 108}, new int[]{34, 38, 45, 51, 66, 112, 120},    new int[]{11, 126},
                                            new int[]{6, 17, 56, 92, 96, 110, 123},    new int[]{26, 37, 112}, new int[]{38, 42, 49, 55, 70, 116, 124},    new int[]{2, 15, 76},
                                            new int[]{10, 21, 60, 96, 100, 114, 127},  new int[]{30, 41, 116}, new int[]{0, 42, 46 ,53, 59, 74, 120}, new int[]{6, 19, 80},
                                            new int[]{3, 14, 25, 100, 104, 118}, new int[]{34, 45, 120}, new int[]{4, 46, 50, 57, 63, 78, 124}, new int[]{10, 23, 84},
                                            new int[]{7, 18, 29, 104, 108, 122}, new int[]{38, 49, 124}, new int[]{0, 8, 50, 54, 61, 67, 82},   new int[]{14, 27, 88},
                                            new int[]{11, 22, 33, 108, 112, 126}, new int[]{0, 42, 53},  new int[]{4, 12, 54, 58, 65, 71, 86}, new int[]{18, 31, 92},
                                            new int[]{2, 15, 26, 37, 76, 112, 116},    new int[]{4, 46, 57},  new int[]{8, 16, 58, 62, 69, 75, 90},  new int[]{22, 35, 96},
                                            new int[]{6, 19, 30, 41, 80, 116, 120},    new int[]{8, 50, 61} , new int[]{12, 20, 62, 66, 73, 79, 94},  new int[]{26, 39, 100},
                                            new int[]{10, 23, 34, 45, 84, 120, 124},    new int[]{12, 54, 65},  new int[]{16, 24, 66, 70, 77, 83, 98},  new int[]{30, 43, 104},
                                            new int[]{0, 14, 27, 38, 49, 88, 124}, new int[]{16, 58, 69},  new int[]{20, 28, 70, 74, 81, 87, 102}, new int[]{34, 47, 108},
                                            new int[]{0, 4, 18, 31, 42, 53, 92},   new int[]{20, 62, 73},  new int[]{24, 32, 74, 78, 85, 91, 106}, new int[]{38, 51, 112},
                                            new int[]{4, 8, 22, 35, 46, 57, 96},   new int[]{24, 66, 77},  new int[]{28, 36, 78, 82, 89, 95, 110}, new int[]{42, 55, 116},
                                            new int[]{8, 12, 26, 39, 50, 61, 100}, new int[]{28, 70, 81},  new int[]{32, 40, 82, 86, 93, 99, 114}, new int[]{46, 59, 120},
                                            new int[]{12, 16, 30, 43, 54, 65, 104}, new int[]{32, 74, 85},  new int[]{36, 90, 103, 118},    new int[]{50, 63, 124},
                                            new int[]{16, 20, 34, 47, 58, 69, 108}, new int[]{36, 78, 89},  new int[]{40, 94, 107, 122},    new int[]{0, 54, 67},
                                            new int[]{20, 24, 38, 51, 62, 73, 112}, new int[]{40, 82, 93},  new int[]{44, 98, 111, 126},    new int[]{4, 58, 71},
                                            new int[]{24, 28, 42, 55, 66, 77, 116}, new int[]{44, 86, 97},  new int[]{2, 48, 102, 115},   new int[]{8, 62, 75},
                                            new int[]{28, 32, 46, 59, 70, 81, 120}, new int[]{48, 90, 101}, new int[]{6, 52, 106,119},    new int[]{12, 66, 79},
                                            new int[]{32, 36, 50, 63, 74, 85, 124}, new int[]{52, 94, 105}, new int[]{10, 56, 110, 123},   new int[]{16, 70, 83},
                                            new int[]{0, 36, 40, 54, 67, 78, 89},  new int[]{56, 98, 109}, new int[]{14, 60, 114, 127},  new int[]{20, 74, 87},
                                            new int[]{4, 40, 44, 58, 71, 82, 93},  new int[]{60, 102, 113},    new int[]{3, 18, 72, 114, 118, 125}, new int[]{24, 78, 91},
                                            new int[]{8, 44, 48, 62, 75, 86, 97},  new int[]{64, 106, 117},    new int[]{1, 7, 22, 76, 118, 122},   new int[]{28, 82, 95},
                                            new int[]{12, 48, 52, 66, 79, 90, 101}, new int[]{68, 110, 121},    new int[]{5, 11, 26, 80, 122, 126},  new int[]{32, 86, 99}};
        //Обратное линейное преобразование ILT
        List<int[]> ILT = new List<int[]>() {new int[]{53, 55, 72}, new int[]{1, 5, 20, 90}, new int[]{15, 102},  new int[]{3, 31, 90},
                                            new int[]{57, 59, 76}, new int[]{5, 9, 24, 94}, new int[]{19, 106},  new int[]{7, 35, 94}, 
                                            new int[]{61, 63, 80}, new int[]{9, 13, 28, 98}, new int[]{23, 110}, new int[]{11, 39, 98},
                                            new int[]{65, 67, 84}, new int[]{13, 17, 32, 102}, new int[]{27, 114}, new int[]{1, 3, 15, 20, 43, 102},
                                            new int[]{69, 71, 88}, new int[]{17, 21, 36, 106}, new int[]{1, 31, 118}, new int[]{5, 7, 19, 24, 47, 106},
                                            new int[]{73, 75, 92}, new int[]{21, 25, 40, 110}, new int[]{5, 35, 122}, new int[]{9, 11, 23, 28, 51, 110},
                                            new int[]{77, 79, 96}, new int[]{25, 29, 44, 114}, new int[]{9, 39, 126}, new int[]{13, 15, 27, 32, 55, 114},
                                            new int[]{81, 83, 100}, new int[]{1, 29, 33, 48, 118}, new int[]{2, 13, 43}, new int[]{1, 17, 19, 31, 36, 59, 118},
                                            new int[]{85, 87, 104}, new int[]{5, 33, 37, 52, 122}, new int[]{6, 17, 47}, new int[]{5, 21 ,23 ,35 ,40 ,63 ,122},
                                            new int[]{89 ,91 ,108}, new int[]{9 ,37 ,41 ,56 ,126}, new int[]{10 ,21 ,51}, new int[]{9 ,25 ,27 ,39 ,44 ,67 ,126},
                                            new int[]{93 ,95 ,112}, new int[]{2 ,13 ,41 ,45 ,60}, new int[]{14 ,25 ,55}, new int[]{2 ,13 ,29 ,31 ,43 ,48 ,71},
                                            new int[]{97 ,99 ,116}, new int[]{6 ,17 ,45 ,49 ,64}, new int[]{18 ,29 ,59}, new int[]{6 ,17 ,33 ,35 ,47 ,52 ,75},
                                            new int[]{101 ,103 ,120}, new int[]{10 ,21 ,49 ,53 ,68}, new int[]{22 ,33 ,63}, new int[]{10 ,21 ,37 ,39 ,51 ,56 ,79},
                                            new int[]{105 ,107 ,124}, new int[]{14 ,25 ,53 ,57 ,72}, new int[]{26 ,37 ,67}, new int[]{14 ,25 ,41 ,43 ,55 ,60 ,83},
                                            new int[]{0 ,109 ,111}, new int[]{18 ,29 ,57 ,61 ,76}, new int[]{30 ,41 ,71}, new int[]{18 ,29 ,45 ,47 ,59 ,64 ,87},
                                            new int[]{4 ,113 ,115}, new int[]{22, 33, 61, 65, 80},    new int[]{34, 45, 75},  new int[]{22, 33, 49, 51, 63, 68, 91},
                                            new int[]{8, 117, 119}, new int[]{26, 37, 65, 69, 84}, new int[]{38, 49, 79}, new int[]{26, 37, 53, 55, 67, 72, 95},
                                            new int[]{12, 121, 123}, new int[]{30, 41, 69, 73, 88}, new int[]{42, 53, 83}, new int[]{30, 41, 57, 59, 71, 76, 99},
                                            new int[]{16, 125, 127}, new int[]{34, 45, 73, 77, 92}, new int[]{46, 57, 87}, new int[]{34, 45, 61, 63, 75, 80, 103},
                                            new int[]{1, 3, 20}, new int[]{38, 49, 77, 81, 96}, new int[]{50, 61, 91}, new int[]{38, 49, 65, 67, 79, 84, 107},
                                            new int[]{5, 7, 24}, new int[]{42, 53, 81, 85, 100}, new int[]{54, 65, 95}, new int[]{42, 53, 69, 71, 83, 88, 111},
                                            new int[]{9,11 ,28}, new int[]{46 ,57 ,85 ,89 ,104} , new int[]{58 ,69 ,99}, new int[]{46 ,57 ,73 ,75 ,87 ,92 ,115},
                                            new int[]{13 ,15 ,32}, new int[]{50 ,61 ,89 ,93 ,108}, new int[]{62 ,73 ,103}, new int[]{50 ,61 ,77 ,79 ,91 ,96 ,119},
                                            new int[]{17 ,19 ,36}, new int[]{54 ,65 ,93 ,97 ,112}, new int[]{66 ,77 ,107}, new int[]{54 ,65 ,81 ,83 ,95 ,100 ,123},
                                            new int[]{21 ,23 ,40}, new int[]{58 ,69 ,97 ,101 ,116}, new int[]{70 ,81 ,111} , new int[]{58 ,69 ,85 ,87 ,99 ,104 ,127},
                                            new int[]{25 ,27 ,44}, new int[]{62 ,73 ,101 ,105 ,120}, new int[]{74 ,85 ,115}, new int[]{3 ,62 ,73 ,89 ,91 ,103 ,108},
                                            new int[]{29 ,31 ,48}, new int[]{66 ,77 ,105 ,109 ,124}, new int[]{78 ,89 ,119}, new int[]{7 ,66 ,77 ,93 ,95 ,107 ,112},
                                            new int[]{33 ,35 ,52}, new int[]{0 ,70 ,81 ,109 ,113}, new int[]{82 ,93 ,123}, new int[]{11 ,70 ,81 ,97 ,99 ,111 ,116},
                                            new int[]{37 ,39 ,56}, new int[]{4 ,74 ,85 ,113 ,117}, new int[]{86, 97, 127}, new int[]{15, 74, 85, 101, 103, 115, 120},
                                            new int[]{41, 43, 60}, new int[]{8, 78, 89, 117, 121}, new int[]{3, 90}, new int[]{19, 78, 89, 105, 107, 119, 124},
                                            new int[]{45, 47, 64}, new int[]{12, 82, 93, 121, 125}, new int[]{7, 94}, new int[]{0, 23, 82, 93, 109, 111, 123},
                                            new int[]{49, 51, 68}, new int[]{1, 16, 86, 97, 125}, new int[]{11, 98}, new int[]{4, 27, 86, 97, 113, 115, 127}};
    }
}
