using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    public class EntityClasses2Child : EntityClasses2
    {
        public EntityClasses2Child(Session session) : base(session) { }

        public string StringProperty2 { get; set; }
        public int IntProperty2 { get; set; }
    }

}