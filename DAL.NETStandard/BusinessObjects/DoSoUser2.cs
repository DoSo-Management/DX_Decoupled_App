using System;
using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    [Persistent("public.SecuritySystemUser")]
    public class DoSoUser2 : DSEntityBaseNoUser<DoSoUser2, Guid>
    {
        public DoSoUser2(Session session) : base(session) { }

        public string UserName { get; set; }
    }
}