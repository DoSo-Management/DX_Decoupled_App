using System;
using System.Collections.Generic;
using System.Linq;
using DAL.ValueObjects;
using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    [NonPersistent]
    [OptimisticLocking(true)]
    // ReSharper disable once InconsistentNaming
    public class DSEntityBaseNoUser<T, TKeyType> : XPLiteObject where T : DSEntityBaseNoUser<T, TKeyType>
    {
        [Key(true)]
        [GetterSetterIgnoreWeaving]
        public TKeyType Oid
        {
            get => _oid;
            set => SetPropertyValue(nameof(Oid), ref _oid, value);
        }

        public static event Action<T> OnSavingEvent;
        public static event Action<T, string, object, object> OnChangedEvent;

        public List<ValueObject> ValueObjects = new List<ValueObject>();
        TKeyType _oid;
        public virtual void AddValueObject(ValueObject vo) => ValueObjects.Add(vo);
        public virtual void ClearAndAddValueObject(ValueObject vo)
        {
            ValueObjects.Clear();
            AddValueObject(vo);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            SetValueObjects();
        }

        public virtual IEnumerable<string> ValueObjectNames { get; }

        public virtual void SetValueObjects() { }

        public bool IsValid => ValueObjects.TrueForAll(o => o.IsValid);
        public string InvalidReason => string.Join("\r\n", ValueObjects.Select(o => o));

        //public Guid CreatedByOid { get; set; }

        //public DoSoUser2 CreatedBy { get; set; }


        public DSEntityBaseNoUser(Session session) : base(session) { }

        protected override void OnSaving()
        {
            base.OnSaving();

            SetValueObjects();

            OnSavingEvent?.Invoke((T)this);

            //CreatedBy = GetCreatedByGuid();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            //SetValueObjects();

            OnChangedEvent?.Invoke((T)this, propertyName, oldValue, newValue);
        }
    }
}