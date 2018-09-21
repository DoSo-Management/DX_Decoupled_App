using DAL.BusinessObjects;

namespace BLL
{

    //public static class a
    //{
    //    public static T GetBl<T>() => Activator.CreateInstance<T>();
    //}

    public class PersistentClasses2Bl : BllBase<EC2>, IPersistentClasses2Bl// where T : EC2/*, ILogicSpecificTo*/
    {
        readonly IPCRepository _pcRepository;
        public PersistentClasses2Bl(IPCRepository pcRepository)
        {
            _pcRepository = pcRepository;
        }

        public virtual void CalculatePremium(EC2 bo) => bo.IntProperty = bo.IntProperty / 2;
        //public virtual void CalculatePremium(EC2 bo) => bo.IntProperty = bo.IntProperty / 2;


        //public IEnumerable<EC2> GetAllFromDb()
        //{
        //    return _pcRepository.GetAllFromDb();
        //}

        public void SaveObject(EC2 bo) => _pcRepository.SaveObject(bo);
        //public EC2 CreateNewObject()
        //{
        //    return _pcRepository.CreateNewObject();
        //}

        //public EC2 GetFromDb(int key)
        //{
        //    return _pcRepository.GetFromDb(key);
        //}

        public override void OnSaving(EC2 bo)
        {
            base.OnSaving(bo);
        }
    }
}