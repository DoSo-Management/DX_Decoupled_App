using System.Collections.Generic;
using ClassLibrary2.BusinessObjects;

namespace ClassLibrary3
{
    public abstract class BllBase<T> where T : DSPersistentBase
    {
        readonly List<BllBase<T>> _blls = new List<BllBase<T>>();
        protected BllBase(T bo)
        {
            bo.OnSavingEvent += OnSavingHandler;
            bo.OnChangedEvent += OnChangedHandler;
        }

        public virtual void OnChanged(T bo, string propertyName, object oldValue, object newValue) { }
        private void OnChangedHandler(DSPersistentBase bo, string propertyName, object oldValue, object newValue) => OnChanged((T)bo, propertyName, oldValue, newValue);

        public virtual void OnSaving(T bo)
        {
            foreach (var bll in _blls)
            {
                bll.OnSaving(bo);
            }
        }
        public void OnSavingHandler(DSPersistentBase bo) => OnSaving((T)bo);
    }
}
