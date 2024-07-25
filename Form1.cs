using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Serpent
{
    public partial class Form1 : Form
    {
        List<int> wtext = new List<int>();
        List<int> wkey = new List<int>();
        Encoding utf8 = Encoding.GetEncoding("windows-1251");
        public Form1()
        {
            InitializeComponent();
            c128.Checked = true;
           // text.Text = "4B4841524C4153484B494E41414E4E41";
           // key.Text = "414E4E414B4841524C4153484B494E41";
        }


        private void c192_CheckedChanged(object sender, EventArgs e)
        {
            if (c192.Checked)
            {
                c128.Checked = false;
                c256.Checked = false;
            }
        }

        private void c256_CheckedChanged(object sender, EventArgs e)
        {
            if (c256.Checked)
            {
                c128.Checked = false;
                c192.Checked = false;
            }
        }

        private void c128_CheckedChanged(object sender, EventArgs e)
        {
            if (c128.Checked)
            {
                c256.Checked = false;
                c192.Checked = false;
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
        }

        private void encrypt_Click(object sender, EventArgs e)
        {
            answer.Clear();
            answerbyte.Clear();
            string mytext = text.Text.Replace(" ", "");
            string mykey = key.Text.Replace(" ", "");
            if (mytext == "" || mykey == "" || !Regex.IsMatch(mytext, "^[0-9A-Fa-f]+$") || !Regex.IsMatch(mykey, "^[0-9A-Fa-f]+$") || mytext.Length %2 != 0 || mytext.Length > 32) MessageBox.Show("Ошибка в данных", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                if ((mykey.Length * 4 == 128 && c128.Checked) || (mykey.Length * 4 == 192 && c192.Checked) || (mykey.Length * 4 == 256 && c256.Checked))
                {
                    CreateKeyandText(mytext, mykey);
                    Algorithm cipher = new Algorithm(wtext, wkey);
                    List<int> ciphertext = cipher.Encrypt();
                    ShowAnswer(ciphertext);
                }
                else MessageBox.Show("Длина ключа не совпадает с выбранным значением", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void decrypt_Click(object sender, EventArgs e)
        {
            answer.Clear();
            answerbyte.Clear();
            string mytext = text.Text.Replace(" ", "");
            string mykey = key.Text.Replace(" ", "");
            if (mytext == "" || mykey == "" || !Regex.IsMatch(mytext, "^[0-9A-Fa-f]+$") || !Regex.IsMatch(mykey, "^[0-9A-Fa-f]+$") || mytext.Length % 2 != 0 || mytext.Length > 32) MessageBox.Show("Ошибка в данных", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                if ((mykey.Length * 4 == 128 && c128.Checked) || (mykey.Length * 4 == 192 && c192.Checked) || (mykey.Length * 4 == 256 && c256.Checked))
                {
                    CreateKeyandText(mytext, mykey);
                    Algorithm cipher = new Algorithm(wtext, wkey);
                    List<int> ciphertext = cipher.Decrypt();
                    ShowAnswer(ciphertext);
                }
                else MessageBox.Show("Длина ключа не совпадает с выбранным значением", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void CreateKeyandText(string mytext, string mykey) //Получения ключа нужной длины и текста нужной длины в битах
        {
            wtext.Clear();
            wkey.Clear();
            List<int> numtext = new List<int>();
            List<int> numkey = new List<int>();
            string text = "";
            string key = "";
            int j = 0;
            for (int i = 0; i < mytext.Length; i += 2)
            {
                numtext.Add(Convert.ToInt32(mytext.Substring(i, 2),16)); // Переводим строку в десятичное число
                text += Convert.ToString(numtext[j], 2).PadLeft(8, '0'); // Переводим десятичное число в 2-ую систему счисления
                j++;
            }
            j = 0;
            for (int i = 0; i < mykey.Length; i += 2)
            {
                numkey.Add(Convert.ToInt32(mykey.Substring(i, 2), 16)); // Переводим строку в десятичное число
                key += Convert.ToString(numkey[j], 2).PadLeft(8, '0'); // Переводим десятичное число в 2-ую систему счисления
                j++;
            }
            foreach (char letter in text)
            {
                wtext.Add(Int32.Parse(letter.ToString()));
            }
            foreach (char letter in key)
            {
                wkey.Add(Int32.Parse(letter.ToString()));
            }
            if (wtext.Count() < 128)
            {
                while (wtext.Count() < 128) wtext.Add(0);
            }
            if (wkey.Count() < 256)
            {
                wkey.Add(1);
                while (wkey.Count() < 256) wkey.Add(0);
            }
        }
        private string String(List<int> text)
        {
            string s = "";
            for(int i =0; i<text.Count(); i++)
            {
                s += text[i].ToString();
            }
            return s;
        }

        private void ShowAnswer(List<int> text)
        {
            string binary = String(text);
            for (int j = 0; j < 32; j++)
            {
                string b = binary.Substring(j * 4, 4); //Просмотр 1/2 байта
                int decimalValue = Convert.ToInt32(b, 2); //Конвертация двоичного кода в десятичное число
                string hexValue = decimalValue.ToString("X"); //конвертация десятичного числа в шестнадцатеричное представление
                if (j > 0 && j % 2 == 0) answerbyte.AppendText(" ");
                answerbyte.AppendText(hexValue);
            }
            for (int j = 0; j < 16; j++)
            {
                string b = binary.Substring(j * 8, 8); //Просмотр одного байта
                int decimalValue = Convert.ToInt32(b, 2); //Конвертация двоичного кода в десятичное число
                byte[] bytes = new byte[] { (byte)decimalValue }; // Преобразуем число в массив байтов
                answer.AppendText(utf8.GetString(bytes).ToString()); //Конвертация десятичного числа в символ согласно ASCII
            }
        }
    }
}





