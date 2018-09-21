using System;
using System.Collections.Generic;
using DAL.BusinessObjects;

namespace ClassLibrary3
{
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

    public class PC3Bl : BllBase<PC3>// where T : EntityClasses2/*, ILogicSpecificTo*/
    {
        readonly IPCRepository _pcRepository;

        public PC3Bl(IPCRepository pcRepository)
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

        public override void OnSaving(PC3 bo)
        {
            base.OnSaving(bo);
        }
    }

    public class PersistentClasses2ChildBl : PersistentClasses2Bl//<EntityClasses2Child>
    {
        public PersistentClasses2ChildBl(IPCRepository pcRepository) : base(pcRepository) { }

        public override void CalculatePremium(EntityClasses2 bo)
        {
            base.CalculatePremium(bo);
            var a = 1;
        }
    }

    public class TestViewModel
    {
        public int ID { get; set; }
        public int OID { get; set; }
        public string StringProperty { get; set; }
    }

    public class ApiMapper : IApiMapper
    {
        readonly IPersistentClasses2Bl _persistentClasses2Bl;

        public ApiMapper(IPersistentClasses2Bl persistentClasses2Bl)
        {
            _persistentClasses2Bl = persistentClasses2Bl;
        }

        public void GetObjectFromDatabase(TestViewModel viewModel)
        {
            var obj = _persistentClasses2Bl.GetFromDb(viewModel.ID);
            obj.IntProperty = viewModel.ID;

            _persistentClasses2Bl.CalculatePremium(obj);

            _persistentClasses2Bl.SaveObject(obj);
        }

        public TestViewModel GetObjectFromDatabase(int id)
        {
            var x = _persistentClasses2Bl.GetFromDb(id);
            return new TestViewModel { ID = x.IntProperty, StringProperty = x.StringProperty, OID = x.Oid };
        }

        public IEnumerable<TestViewModel> GetAllObjectFromDatabase()
        {
            return new List<TestViewModel> { GetObjectFromDatabase(4) };
        }

        public TestViewModel AddObject(TestViewModel viewModel)
        {
            var obj = _persistentClasses2Bl.CreateNewObject();
            obj.IntProperty = viewModel.ID;
            obj.StringProperty = viewModel.StringProperty;

            _persistentClasses2Bl.SaveObject(obj);

            viewModel.OID = obj.Oid;
            return viewModel;
        }
    }
}