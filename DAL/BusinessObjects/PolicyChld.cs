using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    // ReSharper disable once InconsistentNaming
    public class PolicyChld : Policy
    {
        public PolicyChld(Session session) : base(session) { }

        public string StringProperty2 { get; set; }
        public int IntProperty2 { get; set; }
    }

}