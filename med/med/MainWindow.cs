using Microsoft.EntityFrameworkCore;
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
    public partial class MainWindow : Form
    {
        MedecineEntities db = new MedecineEntities();
        string login;
        int doc, cardnum;
        string str;

        public MainWindow(Form1 form, string log)
        {
            InitializeComponent();
            login = log;
            Column();
            AllPatients();
        }

        private void button1_Click(object sender, EventArgs e) //переход на форму для добавления пациентов
        {
            AddPatient addPatient = new AddPatient(this, doc);
            addPatient.Show();
        }

        public void Column()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Фамилия");
            listView1.Columns.Add("Имя");
            listView1.Columns.Add("Отчество");
            listView1.Columns.Add("Дата рождения");
            listView1.Columns.Add("Телефон");
            listView1.Columns.Add("Адрес");
            listView1.Columns.Add("№ карты");
        }

        public void AllPatients()    //вывод перечня пациентов вошедшего врача
        {
            listView1.Items.Clear();

            var docID = db.DocInitializations.FirstOrDefault(i => i.Login == login);
            var patients = db.Patients.Where(i => i.FamilyDoctorID == docID.FamilyDoctorID);
            doc = docID.FamilyDoctorID;
            foreach (var i in patients)
            {
                listView1.Items.Add(new ListViewItem(new string[] { i.Surname, i.Name, i.Patronymic, i.DateOfBirth.ToString(), i.Phone, i.Registration, i.CardNumber.ToString() }));
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void button2_Click(object sender, EventArgs e)    //переход на форму для оформления процедур
        {
            FormProcedure formProcedure = new FormProcedure(this, cardnum, doc);
            formProcedure.Show();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Selected) 
                {
                    str = item.SubItems[0].Text+" ";
                    str += item.SubItems[1].Text+" ";
                    str += item.SubItems[2].Text;
                    cardnum = Convert.ToInt32(item.SubItems[6].Text);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hospitals hospitals = new Hospitals(this, cardnum, doc);
            hospitals.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MakeDiagnosis makeDiagnosis = new MakeDiagnosis(this, cardnum, doc);
            makeDiagnosis.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            { 
            listView1.Items.Clear();
            //var card = db.Patients.Where(i => EF.Functions.Like(i.CardNumber, textBox1.Text))/*.Where(i => EF.Functions.Like(i.Registration, textBox1.Text)).Where(i => EF.Functions.Like(i.Surname, textBox1.Text))*/;

            var card = db.Patients.Where(i => (i.CardNumber.ToString().Contains(textBox1.Text)) || ( i.Surname.Contains(textBox1.Text)|| (i.Registration.Contains(textBox1.Text))));

                foreach (var i in card)
                {
                    if(i.FamilyDoctorID == doc)
                    listView1.Items.Add(new ListViewItem(new string[] { i.Surname, i.Name, i.Patronymic, i.DateOfBirth.ToString(), i.Phone, i.Registration, i.CardNumber.ToString() }));
                }

                if(listView1.Items.Count>0)
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AllPatients();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Archive archive = new Archive(this, cardnum);
            archive.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AddTestResults addTestResults = new AddTestResults(this, cardnum);
            addTestResults.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            EditPatients editPatients = new EditPatients(this, cardnum, doc);
            editPatients.Show();
        }

        private void button3_Click(object sender, EventArgs e)     //переход на форму для оформления анализов
        {
            Form2 form2 = new Form2(this, str, doc);
            form2.Show();
        }
    }
}
