using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    // ReSharper disable once InconsistentNaming
    public class Client : DSEntityBase<Client>
    {
        public Client(Session session) : base(session) { }

        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
    }
}