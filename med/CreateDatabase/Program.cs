using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CreateDatabase
{
    public class Program
    {

        public static void Main(string[] args)
        {
            SqlConnection conn = null;
            SqlDataAdapter da = null;
            DataSet set = null;
            SqlCommandBuilder cmd = null;

            string cs = "";
            conn = new SqlConnection();
            cs = conn.ConnectionString = ConfigurationManager.ConnectionStrings["open"].ConnectionString;
            set = new DataSet();

            string AddDelete = @"create table AddDelete(id int identity primary key not null, Login nvarchar(10) not null, Password nvarchar(10) not null)";
            da = new SqlDataAdapter(AddDelete, conn);
            cmd = new SqlCommandBuilder(da);
            da.Fill(set);

            Thread.Sleep(2000);

            string Illness = @"create table Illness(id int identity primary key not null, Sick nvarchar(30) not null)";
            da = new SqlDataAdapter(Illness, conn);
            cmd = new SqlCommandBuilder(da);
            da.Fill(set);

            Thread.Sleep(2000);

            string FamilyDoctors = @"create table FamilyDoctors (id int identity primary key not null, Name nvarchar(15) not null, Surname nvarchar(15) not null, Patronymic nvarchar(15) not null, Phone nvarchar(10) not null)";
            da = new SqlDataAdapter(FamilyDoctors, conn);
            cmd = new SqlCommandBuilder(da);
            da.Fill(set);

            Thread.Sleep(2000);

            string DocInitializations = @"create table DocInitializations(id int identity primary key not null, FamilyDoctorID int foreign key references FamilyDoctors(id) not null, Login nvarchar(15) not null, Password nvarchar(15) not null)";
            da = new SqlDataAdapter(DocInitializations, conn);
            cmd = new SqlCommandBuilder(da);
            da.Fill(set);

            Thread.Sleep(2000);

            string Patients = @"create table Patients(id int identity primary key not null, Name nvarchar(15) not null, Surname nvarchar(15) not null, Patronymic nvarchar(15) not null, DateOfBirth datetime not null, Phone nvarchar(15) not null, Registration nvarchar(70) not null, Passport_ID nvarchar(10) not null, FamilyDoctorID int foreign key references FamilyDoctors(id) not null, CardNumber int not null)";
            da = new SqlDataAdapter(Patients, conn);
            cmd = new SqlCommandBuilder(da);
            da.Fill(set);

            Thread.Sleep(2000);

            string Recover = @"create table Recover(id int identity primary key not null, PatientId int not null, IllnessID int foreign key references Illness(id) not null, DateFrom datetime not null, DateTo datetime not null)";
            da = new SqlDataAdapter(Recover, conn);
            cmd = new SqlCommandBuilder(da);
            da.Fill(set);

            Thread.Sleep(2000);

            string Analyzes = @"create table Analyzes(id int identity primary key not null, Test nvarchar(40) not null)";
            da = new SqlDataAdapter(Analyzes, conn);
            cmd = new SqlCommandBuilder(da);
            da.Fill(set);

            string Procedures = @"create table Procedures(id int identity primary key not null, HealthCheck nvarchar(40) not null)";
            da = new SqlDataAdapter(Procedures, conn);
            cmd = new SqlCommandBuilder(da);
            da.Fill(set);

            string TestResults = @"create table TestResults(id int identity primary key not null, Date datetime not null, PatientId int not null, AnalyzeID int foreign key references Analyzes(id) not null, FileName nvarchar(50) not null, Title nvarchar(50) not null, ResultData varbinary(max) not null)";
            da = new SqlDataAdapter(TestResults, conn);
            cmd = new SqlCommandBuilder(da);
            da.Fill(set);

            string HospitalsData = @"create table HospitalsData(id int identity primary key not null, Date datetime not null, PatientId int not null, IllID int foreign key references Illness(id) not null, FileName nvarchar(50) not null, Title nvarchar(50) not null, hospitalsData varbinary(max) not null)";
            da = new SqlDataAdapter(HospitalsData, conn);
            cmd = new SqlCommandBuilder(da);
            da.Fill(set);

            string ArchivePatients = @"create table ArchivePatients(id int identity primary key not null, Name nvarchar(15) not null, Surname nvarchar(15) not null, Patronymic nvarchar(15) not null, DateOfBirth datetime not null, Phone nvarchar(15) not null, Registration nvarchar(70) not null, Passport_ID nvarchar(10) not null, FamilyDoctorID int foreign key references FamilyDoctors(id) not null, CardNumber int not null)";
            da = new SqlDataAdapter(ArchivePatients, conn);
            cmd = new SqlCommandBuilder(da);
            da.Fill(set);

            Thread.Sleep(2000);

            string ArchiveTestResults = @"create table ArchiveTestResults(id int identity primary key not null, Date datetime not null, PatientId int foreign key references ArchivePatients(id) not null, AnalyzeID int foreign key references Analyzes(id) not null, FileName nvarchar(50) not null, Title nvarchar(50) not null, ResultData varbinary(max) not null)";
            da = new SqlDataAdapter(ArchiveTestResults, conn);
            cmd = new SqlCommandBuilder(da);
            da.Fill(set);

            string ArchiveRecover = @"create table ArchiveRecover(id int identity primary key not null, PatientId int foreign key references ArchivePatients(id) not null, IllnessID int foreign key references Illness(id) not null, DateFrom datetime not null, DateTo datetime not null)";
            da = new SqlDataAdapter(ArchiveRecover, conn);
            cmd = new SqlCommandBuilder(da);
            da.Fill(set);

            string insertAddDelete = @"insert into AddDelete values('L0G1N','QZTR021jF')";
            da = new SqlDataAdapter(insertAddDelete, conn);
            cmd = new SqlCommandBuilder(da);
            da.TableMappings.Add("Table", "AddDelete");
            da.Update(set);
            set.Clear();
            da.Fill(set);

            Console.WriteLine("Таблицы созданы. \n\nЛогин для входа: L0G1N \nПароль: QZTR021jF");
            Console.ReadLine();
        }

    }
}
