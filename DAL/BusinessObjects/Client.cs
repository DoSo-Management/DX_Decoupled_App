using System;
using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    // ReSharper disable once InconsistentNaming
    public class Client : DSEntityBase<Client>
    {
        public Client(Session session) : base(session) { }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalIdNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age => DateTime.Now.Year - DateOfBirth.Year;
    }
}