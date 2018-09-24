using System;
using DAL.BusinessObjects;

namespace BLL
{
    public abstract class BlBase<T> : IBllBase where T : DSEntityBase<T>
    {
        public virtual Type BoType => typeof(T);
        protected BlBase()
        {
            DSEntityBase<T>.OnSavingEvent += OnSavingHandler;
            DSEntityBase<T>.OnChangedEvent += OnChangedHandler;
        }

        public virtual void OnChanged(T bo, string propertyName, object oldValue, object newValue) { }
        void OnChangedHandler(T bo, string propertyName, object oldValue, object newValue) => OnChanged(bo, propertyName, oldValue, newValue);

        public virtual void OnSaving(T bo) { }
        public void OnSavingHandler(T bo) => OnSaving(bo);
    }

    public interface IBllBase
    {
        Type BoType { get; }
    }
}
