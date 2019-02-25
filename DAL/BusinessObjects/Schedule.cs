using DevExpress.Xpo;
using System;

namespace DAL.BusinessObjects
{
    [DSტრუხა]
    public class Schedule : DSEntityBase<Schedule>
    {
        public Schedule(Session session) : base(session) { }

        public DateTime ScheduleDate { get; set; }
        public decimal ScheduleAmount { get; set; }
        [Association]
        public Policy Policy { get; set; }
    }

}