using System;
using DAL.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;

namespace DAL.CriteriaOperator
{
    public class PolicyEqualsCriteriaOperator : ICustomCriteriaOperatorQueryable
    {
        const string FunctionName = "PolicyNumberIsEqualTo";
        static readonly PolicyEqualsCriteriaOperator instance = new PolicyEqualsCriteriaOperator();
        public static void Register()
        {
            CustomCriteriaManager.RegisterCriterion(instance);
        }
        public static bool Unregister()
        {
            return CustomCriteriaManager.UnregisterCriterion(instance);
        }
        public System.Reflection.MethodInfo GetMethodInfo()
        {
            return typeof(ContactExtensions).GetMethod(FunctionName);
        }
        public DevExpress.Data.Filtering.CriteriaOperator GetCriteria(params DevExpress.Data.Filtering.CriteriaOperator[] operands)
        {
            if (operands == null || operands.Length != 2)
            {
                throw new ArgumentException(FunctionName);
            }
            return new BinaryOperator(new OperandProperty("CreatedBy2.UserName"), operands[1], BinaryOperatorType.Equal); ;
        }
    }

    public static class ContactExtensions
    {
        public static bool PolicyNumberIsEqualTo(this Client client, string number)
        {
            return true; // (client.GetMemberValue("mPolicy") as Policy).Number == number;
        }
    }
}