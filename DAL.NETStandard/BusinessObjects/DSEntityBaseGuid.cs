using System;
using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    [NonPersistent]
    // ReSharper disable once InconsistentNaming
    public class DSEntityBaseGuid<T> : DSEntityBaseWithUser<T, Guid> where T : DSEntityBaseWithUser<T, Guid>
    {
        public DSEntityBaseGuid(Session session) : base(session) { }
    }
}