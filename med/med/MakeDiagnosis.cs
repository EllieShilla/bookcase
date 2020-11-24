using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace med
{
    public partial class MakeDiagnosis : Form
    {
        MedecineEntities db = new MedecineEntities();
        string ill;
        int docid, pid, cardnum;
        DateTime dateTime;

        public MakeDiagnosis(object sender, int num, int docID)
        {
            InitializeComponent();
            cardnum = num;
            docid = docID;

            Ill();
            TextInf();
            Column();
            IllPatient();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var illinf = db.Illness.FirstOrDefault(i => i.Sick == ill);
                Recover recover = new Recover { DateFrom = Convert.ToDateTime(textBox2.Text), DateTo = Convert.ToDateTime(textBox3.Text), PatientId = pid, IllnessID = illinf.id };

                db.Recover.Add(recover);
                db.SaveChanges();
                MessageBox.Show("Данные добавлены");
                IllPatient();
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
                listBox2.Items.Add(i.Sick);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var rec = db.Recover.FirstOrDefault(i => i.DateFrom == dateTime);
                rec.DateFrom = Convert.ToDateTime(textBox2.Text);
                rec.DateTo = Convert.ToDateTime(textBox3.Text);

                var illinf = db.Illness.FirstOrDefault(i => i.Sick == ill);

                rec.IllnessID = illinf.id;
                db.SaveChanges();
                MessageBox.Show("Данные изменены");
                IllPatient();
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

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Selected)
                {
                    dateTime = Convert.ToDateTime(item.SubItems[0].Text);
                }
            }
        }

        public void TextInf()
        {
            var docinf = db.FamilyDoctors.FirstOrDefault(i => i.id == docid);
            var pacinf = db.Patients.FirstOrDefault(i => i.CardNumber == cardnum);
            pid = pacinf.id;
            textBox1.Text = docinf.Surname + " " + docinf.Name + " " + docinf.Patronymic;
            textBox9.Text = pacinf.Surname + " " + pacinf.Name + " " + pacinf.Patronymic;
        }

        public void Column()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Дата поступления");
            listView1.Columns.Add("Дата выписки");
            listView1.Columns.Add("Диагноз");
        }

        public void IllPatient()
        {
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
