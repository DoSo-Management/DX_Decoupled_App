using System;
using DevExpress.Xpo;
using System.Collections.Generic;
using AutoDI;

namespace DAL.BusinessObjects
{
    public interface IService
    {

    }


    [DSDefaultClassOptions]
    public class Client32 : XPLiteObject
    {
        [Dependency] IService Service { get; }

        public Client32(Session session) : base(session)
        {
        }

        [Key(true)]
        public int ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalIdNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age => DateTime.Now.Year - DateOfBirth.Year;

        public int NumberOfAllSchedulesWithoutNumber => 0;
        public IEnumerable<Schedule> AllPolicySchedules => new List<Schedule>();
        public IEnumerable<decimal> AllPolicyScheduleAmounts => new List<decimal>();
        public IEnumerable<IEnumerable<Schedule>> PolicySchedules => new List<List<Schedule>>();





    }
}