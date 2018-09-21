using System;
using DAL.BusinessObjects;

namespace BLL
{
    public class PersistentClasses2ChildBl : PersistentClasses2Bl//<Ec2Child>
    {
        public override Type BoType => typeof(Ec2Child);
        public PersistentClasses2ChildBl(IPCRepository pcRepository) : base(pcRepository) { }

        public override void CalculatePremium(EC2 bo)
        {
            base.CalculatePremium(bo);
            var a = 1;
        }

        public void CalculatePremium2(EC2 bo)
        {
            base.CalculatePremium(bo);
        }
    }
}