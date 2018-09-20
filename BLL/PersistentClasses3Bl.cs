using ClassLibrary2.BusinessObjects;

namespace ClassLibrary3
{
    public class PersistentClasses3Bl : BllBase<PersistentClasses4>
    {
        private readonly PersistentClasses2Bl _persistentClasses2Bl;
        public PersistentClasses3Bl(PersistentClasses4 bo) : base(bo)
        {
            _persistentClasses2Bl = new PersistentClasses2Bl(bo);
        }
        public override void OnSaving(PersistentClasses4 bo)
        {
            _persistentClasses2Bl.OnSaving(bo);

            bo.IntProperty = bo.IntProperty / 2;
        }

        public static void Test()
        {

        }
    }
}