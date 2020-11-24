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
    public partial class Archive : Form
    {
        MedecineEntities db = new MedecineEntities();
        string title, inf;
        int cardnum, pid;
        DateTime date;
        FileStream fs;


        public Archive(object sender, int num)
        {
            InitializeComponent();
            cardnum = num;

            Column();
            Inf();
        }


        public void Column()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Дата");
            listView1.Columns.Add("Вид документа");
            listView1.Columns.Add("Диагноз/Анализ");
        }

        public void Inf()
        {
            try
            { 
            var patinf = db.Patients.FirstOrDefault(i => i.CardNumber == cardnum);
            pid = patinf.id;
            var hosp = db.HospitalsData.Where(i => i.PatientId == pid);
            var test = db.TestResults.Where(i => i.PatientId == pid);

            listView1.Items.Clear();

            foreach (var i in hosp)
            {
                var buff = db.Illness.FirstOrDefault(y => y.id == i.IllID);

                listView1.Items.Add(new ListViewItem(new string[] { i.Date.ToString(), i.Title.ToString(), buff.Sick }));
            }
            foreach (var i in test)
            {
                var buff = db.Analyzes.FirstOrDefault(y => y.id == i.AnalyzeID);

                listView1.Items.Add(new ListViewItem(new string[] { i.Date.ToString(), i.Title.ToString(), buff.Test }));
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.Clear();
            //var card = db.Patients.Where(i => EF.Functions.Like(i.CardNumber, textBox1.Text))/*.Where(i => EF.Functions.Like(i.Registration, textBox1.Text)).Where(i => EF.Functions.Like(i.Surname, textBox1.Text))*/;
            DateTime dateTime;

            var illbuff = db.Illness.FirstOrDefault(i => i.Sick == textBox1.Text);
            var testbuff = db.Analyzes.FirstOrDefault(i => i.Test == textBox1.Text);

            int illnumm = 0;
            int testid = 0;
            if (illbuff != null)
                illnumm = illbuff.id;

            if (testbuff != null)
                testid = testbuff.id;

            if (comboBox1.SelectedItem.ToString() == "Дата")
            {
                dateTime = Convert.ToDateTime(textBox1.Text);
                var hosp = db.HospitalsData.Where(i => i.Date == dateTime);
                var test = db.TestResults.Where(i => i.Date == dateTime);
                foreach (var i in hosp)
                {
                    var buff = db.Illness.FirstOrDefault(y => y.id == i.IllID);
                        if (i.PatientId == pid)
                            listView1.Items.Add(new ListViewItem(new string[] { i.Date.ToString(), i.Title.ToString(), buff.Sick }));
                }

                foreach (var i in test)
                {
                    var buff = db.Analyzes.FirstOrDefault(y => y.id == i.AnalyzeID);
                        if(i.PatientId==pid)
                    listView1.Items.Add(new ListViewItem(new string[] { i.Date.ToString(), i.Title.ToString(), buff.Test }));
                }

            }
            else
            {
                var hosp = db.HospitalsData.Where(i => i.IllID == illnumm);
                foreach (var i in hosp)
                {
                    var buff = db.Illness.FirstOrDefault(y => y.id == i.IllID);
                        if (i.PatientId == pid)
                            listView1.Items.Add(new ListViewItem(new string[] { i.Date.ToString(), i.Title.ToString(), buff.Sick }));
                }

                var test = db.TestResults.Where(i => i.AnalyzeID == testid);
                foreach (var i in test)
                {
                    var buff = db.Analyzes.FirstOrDefault(y => y.id == i.AnalyzeID);
                        if (i.PatientId == pid)
                            listView1.Items.Add(new ListViewItem(new string[] { i.Date.ToString(), i.Title.ToString(), buff.Test }));
                }

            }

            if (listView1.Items.Count > 0)
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Selected)
                {
                    date = Convert.ToDateTime(item.SubItems[0].Text);
                    title = item.SubItems[1].Text;
                    inf = item.SubItems[2].Text;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (title == "Больничный")
                {
                    var buff = db.Illness.FirstOrDefault(y => y.Sick == inf);

                    var hospd = db.HospitalsData.FirstOrDefault(i => i.Date == date && i.PatientId == pid && i.IllID == buff.id);

                    using ( fs = new FileStream(hospd.FileName, FileMode.OpenOrCreate))
                    {
                        fs.Write(hospd.hospitalsData1, 0, hospd.hospitalsData1.Length);
                    }
                }
                else if (title == "Анализ")
                {
                    var buff = db.Analyzes.FirstOrDefault(y => y.Test == inf);
                    var testd = db.TestResults.FirstOrDefault(i => i.Date == date && i.PatientId==pid && i.AnalyzeID== buff.id);

                    using ( fs = new FileStream(testd.FileName, FileMode.OpenOrCreate))
                    {
                        fs.Write(testd.ResultData, 0, testd.ResultData.Length);
                    }
                }
                MessageBox.Show("Файл извлечен");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if(fs!=null)
                {
                    fs.Close();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Inf();
        }
    }
}
