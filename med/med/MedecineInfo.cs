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
    public partial class MedecineInfo : Form
    {
        MedecineEntities db = new MedecineEntities();
        string test, ill, procedure;

        public MedecineInfo()
        {
            InitializeComponent();
            InfOutput();
        }

        public void InfOutput()
        {
            listBox1.Items.Clear(); listBox2.Items.Clear(); listBox3.Items.Clear();

            var buff1 = db.Analyzes;
            foreach (var i in buff1)
            {
                listBox1.Items.Add(i.Test);
            }
            var buff2 = db.Illness;
            foreach (var i in buff2)
            {
                listBox2.Items.Add(i.Sick);
            }
            var buff3 = db.Procedures;
            foreach (var i in buff3)
            {
                listBox3.Items.Add(i.HealthCheck);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var check = db.Analyzes.FirstOrDefault(i => i.Test == textBox1.Text);

                if (textBox1.Text.Length > 1)
                    if (check == null)
                    {
                        Analyzes analyzes = new Analyzes { Test = textBox1.Text };
                        db.Analyzes.Add(analyzes);
                        db.SaveChanges();
                        MessageBox.Show("Данные сохранены");
                        InfOutput();
                    }
                    else
                    {
                        MessageBox.Show("Введенный анализ уже есть в базе");
                    }
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
                var check = db.Illness.FirstOrDefault(i => i.Sick == textBox2.Text);

                if (textBox2.Text.Length > 1)
                    if (check == null)
                    {
                        Illness illness = new Illness { Sick = textBox2.Text };
                        db.Illness.Add(illness);
                        db.SaveChanges();
                        MessageBox.Show("Данные сохранены");
                        InfOutput();
                    }
                    else
                    {
                        MessageBox.Show("Введенный диагноз уже есть в базе");
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var check = db.Procedures.FirstOrDefault(i => i.HealthCheck == textBox3.Text);

                if (textBox3.Text.Length > 1)
                    if (check == null)
                    {
                        Procedures procedures = new Procedures { HealthCheck = textBox3.Text };
                        db.Procedures.Add(procedures);
                        db.SaveChanges();
                        MessageBox.Show("Данные сохранены");
                        InfOutput();
                    }
                    else
                    {
                        MessageBox.Show("Введенная процедура уже есть в базе");
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (test.Length != 0)
                {
                    var buff = db.Analyzes.FirstOrDefault(i => i.Test == test);
                    if (buff != null)
                    {
                        db.Analyzes.Remove(buff);
                        db.SaveChanges();
                        InfOutput();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (ill.Length != 0)
                {
                    var buff = db.Illness.FirstOrDefault(i => i.Sick == ill);
                    if (buff != null)
                    {
                        db.Illness.Remove(buff);
                        db.SaveChanges();
                        InfOutput();
                    }
                }
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

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            procedure = listBox3.SelectedItem.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (procedure.Length != 0)
                {
                    var buff = db.Procedures.FirstOrDefault(i => i.HealthCheck == procedure);
                    if (buff != null)
                    {
                        db.Procedures.Remove(buff);
                        db.SaveChanges();
                        InfOutput();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            test = listBox1.SelectedItem.ToString();
        }
    }
}
