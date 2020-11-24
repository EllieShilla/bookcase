using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace med
{
    public partial class AddDoctor : Form
    {
        MedecineEntities db = new MedecineEntities();

        public AddDoctor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //очистка полей
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
        }

        private void button2_Click(object sender, EventArgs e) //добавление врачей
        {
            try
            {
                var log = db.DocInitializations.FirstOrDefault(i => i.Login == textBox5.Text);
                var controlLogin = db.AddDelete.FirstOrDefault(i => i.Login == textBox5.Text);
                if (log == null && controlLogin == null)
                {
                    FamilyDoctors doctor = new FamilyDoctors { Surname = textBox1.Text, Name = textBox2.Text, Patronymic = textBox3.Text, Phone = textBox4.Text };

                    db.FamilyDoctors.Add(doctor);
                    db.SaveChanges();

                    Thread.Sleep(2000);
                    var docID = db.FamilyDoctors.FirstOrDefault(i => i.Surname == textBox1.Text);

                    if (docID != null)
                    {
                        if (docID.Name == textBox2.Text)
                        {

                            DocInitializations initializations = new DocInitializations { FamilyDoctorID = docID.id, Login = textBox5.Text, Password = textBox6.Text };
                            db.DocInitializations.Add(initializations);
                            db.SaveChanges();
                            MessageBox.Show("Данные сохранены");
                        }
                    }
                    else
                        MessageBox.Show("Данные не сохранились.\nПопробуйте снова");
                }
                else
                    MessageBox.Show("Указанный логин уже используется.\nПридумайте другой");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
