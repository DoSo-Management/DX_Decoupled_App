using DAL.BusinessObjects;

namespace BLL
{
    public class Pc3Bl : BllBase<PC3>// where T : EntityClasses2/*, ILogicSpecificTo*/
    {
        readonly IPCRepository _pcRepository;

        public Pc3Bl(IPCRepository pcRepository)
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
}