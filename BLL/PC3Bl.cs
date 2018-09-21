using DAL.BusinessObjects;

namespace BLL
{
    public class Pc3Bl : BllBase<PC3>// where T : EC2/*, ILogicSpecificTo*/
    {
        readonly IPCRepository _pcRepository;

        public Pc3Bl(IPCRepository pcRepository)
        {
            _pcRepository = pcRepository;
        }

        public virtual void CalculatePremium(EC2 bo) => bo.IntProperty = bo.IntProperty / 2;
        //public virtual void CalculatePremium(EC2 bo) => bo.IntProperty = bo.IntProperty / 2;

        public void SaveObject(EC2 bo) => _pcRepository.SaveObject(bo);
        public EC2 CreateNewObject()
        {
            return _pcRepository.CreateNewObject();
        }

        public EC2 GetFromDb(int key)
        {
            return _pcRepository.GetFromDb(key);
        }

        public override void OnSaving(PC3 bo)
        {
            base.OnSaving(bo);
        }
    }
}