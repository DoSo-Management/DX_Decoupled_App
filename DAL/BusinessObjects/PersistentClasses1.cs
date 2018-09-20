using System;
using DevExpress.Xpo;

namespace ClassLibrary2.BusinessObjects
{
    [NonPersistent]
    // ReSharper disable once InconsistentNaming
    public class DSPersistentBase : XPObject
    {
        public event Action<DSPersistentBase> OnSavingEvent;
        public event Action<DSPersistentBase, string, object, object> OnChangedEvent;

        public static event Action<DSPersistentBase> OnObjectCreated;

        public DSPersistentBase(Session session) : base(session) => OnObjectCreated?.Invoke(this);

        protected override void OnSaving()
        {
            base.OnSaving();
            OnSavingEvent?.Invoke(this);
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            OnChangedEvent?.Invoke(this, propertyName, oldValue, newValue);
        }
    }

    public class PersistentClasses2 : DSPersistentBase
    {
        public PersistentClasses2(Session session) : base(session) { }

        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
    }

    public class PersistentClasses4 : PersistentClasses2
    {
        public PersistentClasses4(Session session) : base(session) { }

        public string StringProperty2 { get; set; }
        public int IntProperty2 { get; set; }
    }
}