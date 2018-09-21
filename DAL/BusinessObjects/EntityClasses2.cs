using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    public class EntityClasses2 : DSEntityBase<EntityClasses2>
    {
        int _intProperty;
        public EntityClasses2(Session session) : base(session) { }

        public string StringProperty { get; set; }

        public int IntProperty
        {
            get => _intProperty;
            set => SetPropertyValue(nameof(IntProperty), ref _intProperty, value);
        }
    }
}