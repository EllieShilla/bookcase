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
    public partial class Form2 : Form
    {
        MedecineEntities db = new MedecineEntities();
        string ill, analize, fiop;
        int doc;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ill = listBox1.SelectedItem.ToString();
        }

        public Form2(MainWindow form, string fio, int docID)
        {
            InitializeComponent();
            fiop = fio;
            doc = docID;

            AllAnalyz();
            Ill();
            TextInf();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(textBox5.Text, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine("Дата: " + textBox4.Text);
                    sw.WriteLine();
                    sw.WriteLine("ФИО врача: " + textBox1.Text);
                    sw.WriteLine("ФИО пациента: " + textBox9.Text);
                    sw.WriteLine("Диагноз: " + ill);
                    sw.WriteLine("Анализ: " + analize);
                }
                MessageBox.Show("Запись выполнена");
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            analize += checkedListBox1.GetItemText(checkedListBox1.Items[e.Index]) + "\r\n     ";
        }

        public void AllAnalyz()
        {
            var buff = db.Analyzes;
            foreach (var i in buff)
            {
                checkedListBox1.Items.Add(i.Test);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files(*.txt)|*.txt";

                if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                    return;
                textBox5.Text = saveFileDialog.FileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Ill()
        {
            var buff = db.Illness;
            foreach (var i in buff)
            {
                listBox1.Items.Add(i.Sick);
            }
        }

        public void TextInf()
        {
            var docinf = db.FamilyDoctors.FirstOrDefault(i => i.id == doc);

            textBox1.Text = docinf.Surname + " " + docinf.Name + " " + docinf.Patronymic;
            textBox9.Text = fiop;
            textBox4.Text = DateTime.Now.ToShortDateString();
        }
    }
}
