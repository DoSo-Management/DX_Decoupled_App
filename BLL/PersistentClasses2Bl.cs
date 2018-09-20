using ClassLibrary2.BusinessObjects;

namespace ClassLibrary3
{
    public class PersistentClasses2Bl : BllBase<PersistentClasses2>
    {
        public PersistentClasses2Bl(PersistentClasses2 bo) : base(bo) { }
        public override void OnSaving(PersistentClasses2 bo)
        {
            bo.IntProperty = bo.IntProperty / 2;
        }
    }
}