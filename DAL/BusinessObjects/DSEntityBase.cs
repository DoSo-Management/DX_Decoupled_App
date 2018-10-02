using System;
using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    [NonPersistent]
    // ReSharper disable once InconsistentNaming
    public class DSEntityBase<T> : DSEntityBaseWithUser<T, int> where T : DSEntityBaseWithUser<T, int>
    {
        public DSEntityBase(Session session) : base(session) { }
    }

    [NonPersistent]
    // ReSharper disable once InconsistentNaming
    public class DSEntityBaseGuid<T> : DSEntityBaseWithUser<T, Guid> where T : DSEntityBaseWithUser<T, Guid>
    {
        public DSEntityBaseGuid(Session session) : base(session) { }
    }

    [NonPersistent]
    // ReSharper disable once InconsistentNaming
    public class DSEntityBaseWithUser<T, TKey> : DSEntityBaseNoUser<T, TKey> where T : DSEntityBaseNoUser<T, TKey>
    {
        public DSEntityBaseWithUser(Session session) : base(session) { }
        public DoSoUser2 CreatedBy3 { get; set; }
    }

    [NonPersistent]
    // ReSharper disable once InconsistentNaming
    public class DSEntityBaseNoUser<T, TKeyType> : XPLiteObject where T : DSEntityBaseNoUser<T, TKeyType>
    {
        [Key(true)]
        public TKeyType Oid { get; set; }
        public static event Action<T> OnSavingEvent;
        public static event Action<T, string, object, object> OnChangedEvent;

        //public Guid CreatedByOid { get; set; }

        //public DoSoUser2 CreatedBy { get; set; }
        

        public DSEntityBaseNoUser(Session session) : base(session) { }

        protected override void OnSaving()
        {
            base.OnSaving();
            OnSavingEvent?.Invoke((T)this);

            //CreatedBy = GetCreatedByGuid();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            OnChangedEvent?.Invoke((T)this, propertyName, oldValue, newValue);
        }
    }
    
    [Persistent("public.SecuritySystemUser")]
    public class DoSoUser2 : DSEntityBaseNoUser<DoSoUser2, Guid>
    {
        public DoSoUser2(Session session) : base(session) { }

        public string UserName { get; set; }
    }
}