﻿using DAL.BusinessObjects;

namespace BLL
{

    //public static class a
    //{
    //    public static T GetBl<T>() => Activator.CreateInstance<T>();
    //}

    public class PersistentClasses2Bl : BllBase<EntityClasses2>, IPersistentClasses2Bl// where T : EntityClasses2/*, ILogicSpecificTo*/
    {
        readonly IPCRepository _pcRepository;
        public PersistentClasses2Bl(IPCRepository pcRepository)
        {
            _pcRepository = pcRepository;
        }

        public virtual void CalculatePremium(EntityClasses2 bo) => bo.IntProperty = bo.IntProperty / 2;
        //public virtual void CalculatePremium(EntityClasses2 bo) => bo.IntProperty = bo.IntProperty / 2;

        public void SaveObject(EntityClasses2 bo) => _pcRepository.SaveObject(bo);
        public EntityClasses2 CreateNewObject()
        {
            return _pcRepository.CreateNewObject();
        }

        public EntityClasses2 GetFromDb(int key)
        {
            return _pcRepository.GetFromDb(key);
        }

        public override void OnSaving(EntityClasses2 bo)
        {
            base.OnSaving(bo);
        }
    }
}