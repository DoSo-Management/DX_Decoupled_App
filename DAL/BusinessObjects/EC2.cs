using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    public class EC2 : DSEntityBase<EC2>
    {
        int _intProperty;
        public EC2(Session session) : base(session) { }

        public string StringProperty { get; set; }

        public int IntProperty
        {
            get => _intProperty;
            set => SetPropertyValue(nameof(IntProperty), ref _intProperty, value);
        }
    }
}