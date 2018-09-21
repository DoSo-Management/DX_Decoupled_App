using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    public class EntityClasses2 : DSEntityBase<EntityClasses2>
    {
        public EntityClasses2(Session session) : base(session) { }

        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
    }

    public class PC3 : DSEntityBase<PC3>
    {
        public PC3(Session session) : base(session) { }

        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
    }
}