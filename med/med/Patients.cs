//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace med
{
    using System;
    using System.Collections.Generic;
    
    public partial class Patients
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Registration { get; set; }
        public string Passport_ID { get; set; }
        public int FamilyDoctorID { get; set; }
        public int CardNumber { get; set; }
    
        public virtual FamilyDoctors FamilyDoctors { get; set; }
    }
}
