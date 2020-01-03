using DevExpress.Xpo;
using Domain.NETStandard;

namespace DAL.BusinessObjects
{
    public class Currency : DSEntityBase<Currency>
    {
        public Currency(Session session) : base(session) { }

        public string CurrencyName { get; set; }
        //public DoWrapperContainer
        public DoWrapper<CurrencyV> CurrencyV => DoWrapper.Create(() => new CurrencyV(CurrencyName));
    }
}