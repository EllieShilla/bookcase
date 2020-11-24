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
    public partial class Hospitals : Form
    {
        MedecineEntities db = new MedecineEntities();
        int docid, cardnum, pid;
        string datefrom, dateto, docfio, pacfio, ill, registration;

        public Hospitals(object sender, int num, int docID)
        {
            InitializeComponent();
            cardnum = num;
            docid = docID;

            Column();
            IllPatient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] textdata;
                var buff = db.Illness.FirstOrDefault(y => y.Sick == ill);

                string text = "Больничный\r\n\r\n" + "Дата обращения: " + Convert.ToDateTime(datefrom).Date.ToShortDateString() + "                                                   Дата закрытия: " + Convert.ToDateTime(dateto).Date.ToShortDateString() + "\r\n\r\nФИО врача: " + docfio + "\r\nФИО пациента: " + pacfio +
                  "\r\nАдрес проживания: " + registration + "\r\nМесто работы: " + textBox6.Text + "\r\nДиагноз: " + ill;

                using (StreamWriter sw = new StreamWriter(textBox7.Text, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }


                using (System.IO.FileStream fs = new System.IO.FileStream(textBox7.Text, FileMode.Open))
                {
                    textdata = new byte[fs.Length];
                    fs.Read(textdata, 0, textdata.Length);
                }

                HospitalsData hospitalsData = new HospitalsData { FileName = textBox7.Text, Date = Convert.ToDateTime(datefrom), PatientId = pid, IllID = buff.id, Title = "Больничный", hospitalsData1 = textdata };
                db.HospitalsData.Add(hospitalsData);
                db.SaveChanges();

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
                textBox7.Text = saveFileDialog.FileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Column()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Дата поступления");
            listView1.Columns.Add("Дата выписки");
            listView1.Columns.Add("Диагноз");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Selected)
                {
                    datefrom = item.SubItems[0].Text;
                    dateto = item.SubItems[1].Text;
                    ill = item.SubItems[2].Text;
                }
            }
        }

        public void IllPatient()
        {
            var docinf = db.FamilyDoctors.FirstOrDefault(i => i.id == docid);
            var pacinf = db.Patients.FirstOrDefault(i => i.CardNumber == cardnum);
            docfio = docinf.Surname + " " + docinf.Name + " " + docinf.Patronymic;
            pacfio = pacinf.Surname + " " + pacinf.Name + " " + pacinf.Patronymic;
            registration = pacinf.Registration;
            pid = pacinf.id;
            listView1.Items.Clear();
            var allill = db.Recover.Where(i => i.PatientId == pid);

            foreach (var i in allill)
            {
                var buff = db.Illness.FirstOrDefault(y => y.id == i.IllnessID);

                listView1.Items.Add(new ListViewItem(new string[] { i.DateFrom.ToString(), i.DateTo.ToString(), buff.Sick }));
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }
}
