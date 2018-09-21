﻿using System;
using DAL.BusinessObjects;

namespace BLL
{
    public abstract class BllBase<T> : IBllBase where T : DSEntityBase<T>
    {
        //readonly List<BllBase<T>> _blls = new List<BllBase<T>>();
        public virtual Type BoType => typeof(T);
        protected BllBase()
        {
            DSEntityBase<T>.OnSavingEvent += OnSavingHandler;
            DSEntityBase<T>.OnChangedEvent += OnChangedHandler;
        }

        //public T GetById(int id)
        //{
        //    using (var uow = new UnitOfWork())
        //    {
        //        uow.GetObjectByKey<T>(id);
        //        uow.CommitChanges();

        //        uow.Query<EC2>().Where(t => t.IntProperty == 5);
        //    }
        //}

        public virtual void OnChanged(T bo, string propertyName, object oldValue, object newValue) { }
        private void OnChangedHandler(T bo, string propertyName, object oldValue, object newValue)
        {
            //if (bo.GetType() == typeof(T))
            OnChanged(bo, propertyName, oldValue, newValue);
        }

        public virtual void OnSaving(T bo)
        {
            //foreach (var bll in _blls)
            //{
            //    bll.OnSaving(bo);
            //}
        }
        public void OnSavingHandler(T bo) => OnSaving((T)bo);
    }

    public interface IBllBase
    {
        Type BoType { get; }// => typeof(T);
    }
}
