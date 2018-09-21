using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    public class Ec2Child : EC2
    {
        public Ec2Child(Session session) : base(session) { }

        public string StringProperty2 { get; set; }
        public int IntProperty2 { get; set; }
    }

}