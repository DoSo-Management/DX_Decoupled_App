using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    [NonPersistent]
    // ReSharper disable once InconsistentNaming
    public class DSEntityBase<T> : DSEntityBaseWithUser<T, int> where T : DSEntityBaseWithUser<T, int>
    {
        public DSEntityBase(Session session) : base(session) { }

    }
}