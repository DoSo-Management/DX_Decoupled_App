using System;
using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    [NonPersistent]
    // ReSharper disable once InconsistentNaming
    public class DSEntityBase<T> : XPLiteObject where T : DSEntityBase<T>
    {
        [Key(true)]
        public int Oid { get; set; }
        public static event Action<T> OnSavingEvent;
        public static event Action<T, string, object, object> OnChangedEvent;

        //public static event Action<DSEntityBase> OnObjectCreated;

        public DSEntityBase(Session session) : base(session) { }//=> OnObjectCreated?.Invoke(this);

        protected override void OnSaving()
        {
            base.OnSaving();
            OnSavingEvent?.Invoke((T)this);
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            OnChangedEvent?.Invoke((T)this, propertyName, oldValue, newValue);
        }
    }
}