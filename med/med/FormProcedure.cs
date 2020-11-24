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
    public partial class FormProcedure : Form
    {
        MedecineEntities db = new MedecineEntities();
        int doc, cardnum;
        string ill, procedureTitle;
        public FormProcedure(object sender, int num, int docID)
        {
            InitializeComponent();
            cardnum = num;
            doc = docID;

            TextInf();
            ProcedureIll();
        }

        public void TextInf()
        {
            var docinf = db.FamilyDoctors.FirstOrDefault(i => i.id == doc);
            var pacinf = db.Patients.FirstOrDefault(i => i.CardNumber == cardnum);
            textBox1.Text = docinf.Surname + " " + docinf.Name + " " + docinf.Patronymic;
            textBox9.Text = pacinf.Surname + " " + pacinf.Name + " " + pacinf.Patronymic;
            textBox2.Text = pacinf.DateOfBirth.Date.ToShortDateString();
            textBox4.Text = DateTime.Now.ToShortDateString();
        }

        public void ProcedureIll()
        {
            var buff = db.Procedures;
            foreach (var i in buff)
            {
                listBox1.Items.Add(i.HealthCheck);
            }

            var illbuff = db.Illness;
            foreach (var i in illbuff)
            {
                listBox2.Items.Add(i.Sick);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string text = "Дата: " + textBox4.Text + "\r\n\r\nФИО врача: " + textBox1.Text + "\r\nФИО пациента: " + textBox9.Text +"   Дата рождения: "+ textBox2.Text+
                "\r\nДиагноз: " + ill + "\r\nВид процедуры: " + procedureTitle + "\r\nПроцедура: " + textBox6.Text;

                using (StreamWriter sw = new StreamWriter(textBox5.Text, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
                MessageBox.Show("Запись выполнена");
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ill = listBox2.SelectedItem.ToString();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            procedureTitle = listBox1.SelectedItem.ToString();
        }
    }
}
