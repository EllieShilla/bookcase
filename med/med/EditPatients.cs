using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace med
{
    public partial class EditPatients : Form
    {
        MedecineEntities db = new MedecineEntities();
        int doc, cardnum, pid;
        string  reg;
        MainWindow window;

        public EditPatients(MainWindow main, int num, int docID)
        {
            InitializeComponent();
            cardnum = num;
            doc = docID;
            window = main;

            TextInf();
        }

        public void TextInf()
        {
            var doctor = db.FamilyDoctors.FirstOrDefault(i => i.id == doc);

            var patient = db.Patients.FirstOrDefault(i => i.CardNumber == cardnum);
            pid = patient.id;
            reg = patient.Registration;
            textBox5.Text = patient.Registration;
            textBox1.Text = patient.Surname;
            textBox2.Text = patient.Name;
            textBox3.Text = patient.Patronymic;
            textBox4.Text = patient.DateOfBirth.Date.ToShortDateString();
            textBox6.Text = patient.Passport_ID;
            textBox7.Text = patient.Phone;
            textBox8.Text = patient.CardNumber.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var patient = db.Patients.FirstOrDefault(i => i.CardNumber == cardnum);

                if (textBox5.Text == reg)
                {
                    patient.Surname = textBox1.Text;
                    patient.Name = textBox2.Text;
                    patient.Patronymic = textBox3.Text;
                    patient.DateOfBirth = Convert.ToDateTime(textBox4.Text);
                    patient.Passport_ID = textBox6.Text;
                    patient.Phone = textBox7.Text;
                    db.SaveChanges();
                }
                else if (textBox5.Text != reg)
                {
                    patient.Surname = textBox1.Text;
                    patient.Name = textBox2.Text;
                    patient.Patronymic = textBox3.Text;
                    patient.DateOfBirth = Convert.ToDateTime(textBox4.Text);
                    patient.Passport_ID = textBox6.Text;
                    patient.Phone = textBox7.Text;
                    patient.Registration = textBox5.Text;
                    db.SaveChanges();

                    var pcard = db.Patients.FirstOrDefault(i => i.CardNumber == cardnum);

                    ArchivePatients archivePatients = new ArchivePatients
                    {
                        Surname = pcard.Surname,
                        Name = pcard.Name,
                        Patronymic = pcard.Patronymic,
                        DateOfBirth = pcard.DateOfBirth,
                        Phone = pcard.Phone,
                        CardNumber = pcard.CardNumber,
                        Registration = pcard.Registration,
                        Passport_ID = pcard.Passport_ID,
                        FamilyDoctorID = pcard.FamilyDoctorID
                    };

                    db.ArchivePatients.Add(archivePatients);
                    db.SaveChanges();

                    var test = db.TestResults.Where(i => i.PatientId == pid).ToList();
                    var ar = db.ArchivePatients.FirstOrDefault(y => y.CardNumber == pcard.CardNumber);

                    foreach (var i in test)
                    {
                        ArchiveTestResults archiveTestResults = new ArchiveTestResults
                        {
                            Date = i.Date,
                            PatientId = ar.id,
                            AnalyzeID = i.AnalyzeID,
                            FileName = i.FileName,
                            Title = i.Title,
                            ResultData = i.ResultData
                        };
                        db.ArchiveTestResults.Add(archiveTestResults);
                    }
                    db.SaveChanges();

                    var dio = db.Recover.Where(i => i.PatientId == pid).ToList();

                    foreach (var i in dio)
                    {
                        ArchiveRecover archiveRecover = new ArchiveRecover
                        {
                            PatientId = ar.id,
                            IllnessID = i.IllnessID,
                            DateFrom = i.DateFrom,
                            DateTo = i.DateTo
                        };
                        db.ArchiveRecover.Add(archiveRecover);
                    }
                    db.SaveChanges();




                    var deltest = db.TestResults.Where(i => i.PatientId == pid).ToList();
                    foreach (var i in deltest)
                    {
                        db.TestResults.Remove(i);
                    }
                    db.SaveChanges();

                    var deldio = db.Recover.Where(i => i.PatientId == pid).ToList();
                    foreach (var i in deldio)
                    {
                        db.Recover.Remove(i);

                    }
                    db.SaveChanges();

                    var delpatient = db.Patients.FirstOrDefault(i => i.CardNumber == cardnum);


                    db.Patients.Remove(delpatient);
                    db.SaveChanges();

                }
                MessageBox.Show("Изменения сохранены");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            window.AllPatients();
        }
    }
}
