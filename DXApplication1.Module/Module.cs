using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary3;
using DAL.BusinessObjects;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using RepoServices;

namespace DXApplication1.Module
{
    public sealed partial class DXApplication1Module : ModuleBase
    {
        readonly Dictionary<Type, Type> _type2BllMap = new Dictionary<Type, Type>();
        //void DSPersistentBase_OnObjectCreated(DSEntityBase obj)
        //{
        //    if (_type2BllMap.ContainsKey(obj.GetType()))
        //        Activator.CreateInstance(_type2BllMap[obj.GetType()], obj);
        //}
        public DXApplication1Module()
        {
            InitializeComponent();
            BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;

            AdditionalExportedTypes.AddRange(ModuleHelper.CollectExportedTypesFromAssembly(typeof(EntityClasses2).Assembly, t => !t.ContainsGenericParameters));

            var x = new PersistentClasses2Bl(new PCRepository());
            var y = new PC3Bl(new PCRepository());
            //DSEntityBase.OnObjectCreated += DSPersistentBase_OnObjectCreated;

            var result = typeof(BllBase<>).Assembly
                .GetTypes()
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
                            t.BaseType.GetGenericTypeDefinition() == typeof(BllBase<>)
                );

            foreach (var type in result)
                _type2BllMap.Add(type.BaseType.GenericTypeArguments[0], type);
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
        public override void Setup(XafApplication application)
        {
            base.Setup(application);
            // Manage various aspects of the application UI and behavior at the module level.
        }
        public override void CustomizeTypesInfo(ITypesInfo typesInfo)
        {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);

            typesInfo.FindTypeInfo(typeof(EntityClasses2)).AddAttribute(new DefaultClassOptionsAttribute());
            typesInfo.FindTypeInfo(typeof(EntityClasses2Child)).AddAttribute(new DefaultClassOptionsAttribute());
            typesInfo.FindTypeInfo(typeof(PC3)).AddAttribute(new DefaultClassOptionsAttribute());
        }
    }
}
