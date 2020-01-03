using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    [NonPersistent]
    // ReSharper disable once InconsistentNaming
    public class DSEntityBaseWithUser<T, TKey> : DSEntityBaseNoUser<T, TKey> where T : DSEntityBaseNoUser<T, TKey>
    {
        public DSEntityBaseWithUser(Session session) : base(session) { }
        public DoSoUser2 CreatedBy3 { get; set; }
    }
}