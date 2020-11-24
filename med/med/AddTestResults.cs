using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace med
{
    public partial class AddTestResults : Form
    {
        MedecineEntities db = new MedecineEntities();
        string test;
        int pcard, pid;
        public AddTestResults(object sender, int num)
        {
            InitializeComponent();
            pcard = num;

            Test();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files(*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            textBox5.Text = saveFileDialog.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var buff = db.Analyzes.FirstOrDefault(y => y.Test == test);

            try
            {
            string text = "Результаты анализов\r\n\r\n" + "Дата получения результатов: " + Convert.ToDateTime(textBox4.Text).Date.ToShortDateString() + "\r\n\r\nФИО пациента: " + textBox9.Text +
              "\r\nАнализ: " + test + "\r\nРезультаты: " + textBox1.Text;

                byte[] textdata = System.Text.Encoding.Default.GetBytes(text);

                TestResults testResults = new TestResults { FileName = textBox5.Text, Date = Convert.ToDateTime(textBox4.Text), Title = "Анализ", PatientId = pid, AnalyzeID = buff.id, ResultData = textdata };
                db.TestResults.Add(testResults);
                db.SaveChanges();

                MessageBox.Show("Запись выполнена");
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Test()
        {
            var patinf = db.Patients.FirstOrDefault(i => i.CardNumber == pcard);
            textBox9.Text = patinf.Surname + " " + patinf.Name + " " + patinf.Patronymic;
            pid = patinf.id;
            var buff = db.Analyzes;
            foreach (var i in buff)
            {
                listBox2.Items.Add(i.Test);
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            test = listBox2.SelectedItem.ToString();
        }
    }
}
