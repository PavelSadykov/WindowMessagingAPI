using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowMessagingAPI
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        const uint WM_CLOSE = 0x0010;
        const uint WM_SETTEXT = 0x000C;
        string nameWindow = "";
        string newTitle = "";

        public Form1()
        {
            InitializeComponent();
        }
       
        //обработка события для  поиска окна
        private void button1_Click(object sender, EventArgs e)
        {
            nameWindow = textBoxInput.Text;//Вводим название
            IntPtr windowHandle = FindWindow(null, nameWindow);

            if (windowHandle != IntPtr.Zero)// проверяем наличие
            {

                MessageBox.Show("Окно : " + nameWindow, "Успешно найдено");
            }
            else
            {
                MessageBox.Show("Окно не найдено", "Ошибка");
            }


        }

        //обработка события для  изменения названия  окна
        private void btnChangeTitle_Click_1(object sender, EventArgs e)
        {
            newTitle = txtNewTitle.Text;// вводим новое название
          

            IntPtr windowHandle = FindWindow(null, nameWindow);

            if (windowHandle != IntPtr.Zero)// проверяем наличие окна с существующим названием
            {
                SendMessage(windowHandle, WM_SETTEXT, IntPtr.Zero, Marshal.StringToBSTR(newTitle));// меняем название
                MessageBox.Show("Заголовок окна изменен на: " + newTitle, "Успешно");
            }
            else
            {
                MessageBox.Show("Окно не найдено", "Ошибка");
            }
        }
        //обработка события закрытия окна
        private void btnCloseWindow_Click_1(object sender, EventArgs e)
        {
            IntPtr windowHandle = FindWindow(null, newTitle);

            if (windowHandle != IntPtr.Zero)// если окно с новым названием существует
            {
                SendMessage(windowHandle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);// закрываем его
                MessageBox.Show("Окно закрыто", "Успешно");
            }
            else
            {
                MessageBox.Show("Окно не найдено", "Ошибка");
            }
        }
       
    }
}
