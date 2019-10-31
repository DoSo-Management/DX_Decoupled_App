using System;
using DevExpress.Xpo;
using System.Linq;
using System.Collections.Generic;

namespace DAL.BusinessObjects
{
    [DSDefaultClassOptions]
    public class Client : DSEntityBase<Client>
    {
        public Client(Session session) : base(session) { }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalIdNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age => DateTime.Now.Year - DateOfBirth.Year;
        [Association]
        public XPCollection<Policy> PoliciesCollection => GetCollection<Policy>(nameof(PoliciesCollection));

        [PersistentAlias("[PoliciesCollection].Max([SchedulesCollection].Max([ScheduleDate]))")]
        public DateTime MaxScheduleDate => Convert.ToDateTime(EvaluateAlias(nameof(MaxScheduleDate)));

        //public string GetCriteria() =>  (c => c.PoliciesCollection.Max(p => p.SchedulesCollection.Max(s => s.ScheduleDate))).ToString();

        public string PolicyNumber => PoliciesCollection.Max(z => z.Number);
        public int NumberOfAllPolicies => PoliciesCollection.Count();
        public int NumberOfAllSchedules => PoliciesCollection.Sum(z => z.SchedulesCollection.Count());

        public decimal SumOfAllScheduleAmounts => PoliciesCollection.Sum(z => z.SchedulesCollection.Sum(s => s.ScheduleAmount));
        public int NumberOfAllSchedulesWithPolicyNumber => PoliciesCollection.Sum(z => z.SchedulesCollection.Count(s => s.Policy.Number != null));
        public int NumberOfAllSchedulesWithPolicyNumber2 => PoliciesCollection.Sum(z => z.SchedulesCollection.Where(s => s.Policy.Number != null).Count());
        //public int NumberOfAllSchedulesWithPolicyNumber3 => PoliciesCollection.Where(z => z.Number != null).Sum(z => z.Number != null && z.SchedulesCollection.Where(s => s.Policy.Number != null));

        public int NumberOfAllSchedulesWithoutNumber => 0;
        public IEnumerable<Schedule> AllPolicySchedules => new List<Schedule>();
        public IEnumerable<decimal> AllPolicyScheduleAmounts => new List<decimal>();
        public IEnumerable<IEnumerable<Schedule>> PolicySchedules => new List<List<Schedule>>();





    }
}