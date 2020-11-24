using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace med
{
    public partial class Form1 : Form
    {
        MedecineEntities db = new MedecineEntities();

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)                  //инициализация руководства для добавления новых терапевтов
        {
            //Log login
            //pas 12345678

            try
            {
                var adddelte = db.AddDelete.FirstOrDefault(i => i.Login == textBox1.Text);

                if (adddelte != null)
                {
                    if (adddelte.Password == textBox2.Text)
                    {
                        ADD aDD = new ADD();
                        aDD.Show();
                    }
                    else
                        MessageBox.Show("Пароль введен неверно");
                }
                else
                    MessageBox.Show("Указанного логина не существует");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)            //инициализация терапевтов для работы
        {
            //Tamara01
            //    tamara1234

            try
            {
                var adddelte = db.DocInitializations.FirstOrDefault(i => i.Login == textBox1.Text);

                if (adddelte != null)
                {
                    if (adddelte.Password == textBox2.Text)
                    {
                        MainWindow patient = new MainWindow(this, textBox1.Text);
                        patient.Show();
                    }
                    else
                        MessageBox.Show("Пароль введен неверно");
                }
                else
                    MessageBox.Show("Указанного логина не существует");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
