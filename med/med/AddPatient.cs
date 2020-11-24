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
    public partial class AddPatient : Form
    {
        MedecineEntities db = new MedecineEntities();
        int docid;
        MainWindow window;

        public AddPatient(MainWindow main, int docID)
        {
            InitializeComponent();
            window = main;
            docid = docID;
            FIO();
        }

        public void FIO()
        {
            var docID = db.FamilyDoctors.FirstOrDefault(i => i.id == docid);

            textBox9.Text = docID.Surname;
            textBox12.Text = docID.Name;
            textBox13.Text = docID.Patronymic;
        }

        private void button1_Click(object sender, EventArgs e) //очистка полей
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Text = null;
            textBox8.Text = null;
        }

        private void button2_Click(object sender, EventArgs e) //добавление новых пациентов
        {
            try
            {
                var docID = db.FamilyDoctors.FirstOrDefault(i => i.id == docid);

                Patients patients = new Patients
                {
                    Surname = textBox1.Text,
                    Name = textBox2.Text,
                    Patronymic = textBox3.Text,
                    DateOfBirth = Convert.ToDateTime(textBox4.Text),
                    Passport_ID = textBox6.Text,
                    Phone = textBox7.Text,
                    Registration = textBox5.Text,
                    FamilyDoctorID = docID.id,
                    CardNumber = Convert.ToInt32(textBox8.Text)
                };

                db.Patients.Add(patients);
                db.SaveChanges();
                MessageBox.Show("Пациент добавлен");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            window.AllPatients();
        }
    }
}
