using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    // ReSharper disable once InconsistentNaming
    public class PC3 : DSEntityBase<PC3>
    {
        public PC3(Session session) : base(session) { }

        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
    }
}