using System;
using DAL.BusinessObjects;

namespace BLL
{
    public class PersistentClasses2ChildBl : PersistentClasses2Bl//<EntityClasses2Child>
    {
        public override Type BoType => typeof(EntityClasses2Child);
        public PersistentClasses2ChildBl(IPCRepository pcRepository) : base(pcRepository) { }

        public override void CalculatePremium(EntityClasses2 bo)
        {
            base.CalculatePremium(bo);
            var a = 1;
        }

        public void CalculatePremium2(EntityClasses2 bo)
        {
            base.CalculatePremium(bo);
        }
    }
}