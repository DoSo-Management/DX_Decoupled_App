using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using System.Linq;
using BLL;
using DAL.BusinessObjects;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using RepoServices;

namespace DXApplication1.Module
{
    public static class ExtensionsBl
    {
        public static IEnumerable<Type> ParentTypes(this Type type)
        {
            foreach (var i in type.GetInterfaces())
            {
                yield return i;
                foreach (var t in i.ParentTypes())
                {
                    yield return t;
                }
            }

            if (type.BaseType != null)
            {
                yield return type.BaseType;
                foreach (var b in type.BaseType.ParentTypes())
                {
                    yield return b;
                }
            }
        }

        public static T Bl<T>(this XPLiteObject boType) where T : IBllBase
        {
            var type = boType.GetType();

            //if (!DXApplication1Module._type2BllMap.ContainsKey(type))
            //{
            //    var blInstance = (IBllBase)Activator.CreateInstance(typeof(T), new PCRepository());
            //    DXApplication1Module._type2BllMap.Add(blInstance.BoType, blInstance);
            //}

            var blType = DXApplication1Module._type2BllMap[type];

            if (blType.BoType != boType.GetType())
                throw new InvalidOperationException();

            return (T)DXApplication1Module._type2BllMap[type];
        }
    }

    public sealed partial class DXApplication1Module : ModuleBase
    {
        public static readonly Dictionary<Type, IBllBase> _type2BllMap = new Dictionary<Type, IBllBase>();
        //void DSPersistentBase_OnObjectCreated(DSEntityBase obj)
        //{
        //    if (_type2BllMap.ContainsKey(obj.GetType()))
        //        Activator.CreateInstance(_type2BllMap[obj.GetType()], obj);
        //}
        public DXApplication1Module()
        {
            InitializeComponent();
            BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;

            AdditionalExportedTypes.AddRange(ModuleHelper.CollectExportedTypesFromAssembly(typeof(EC2).Assembly, t => !t.ContainsGenericParameters));

            //var x = new PersistentClasses2Bl(new PCRepository());
            //var y = new PC3Bl(new PCRepository());
            //DSEntityBase.OnObjectCreated += DSPersistentBase_OnObjectCreated;

            //var result = typeof(BllBase<>).Assembly
            //    .GetTypes()
            //    .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
            //                t.BaseType.GetGenericTypeDefinition() == typeof(BllBase<>)
            //    );

            var result = typeof(BllBase<>).Assembly
                .GetTypes()
                //.Select(t => t.IsSubclassOf(typeof(IBllBase)))
                //.Where(t => t != null)

                //.Where(t => t.IsSubclassOf(typeof(IBllBase)))
                .Where(t => t.ParentTypes().Any(p => p.IsGenericType && p.GetGenericTypeDefinition() == typeof(BllBase<>)))
                //.Select(t => new { t, pt = t.ParentTypes(), pt2 = t.ParentTypes().Any(p => p.IsGenericType && p.GetGenericTypeDefinition() == typeof(BllBase<>)) }
                //.Where(t => t.BaseType != null &&// t.BaseType.IsGenericType &&
                //            t.BaseType.IsSubclassOf(typeof(BllBase<>))
                ;

            foreach (var type in result)
            {
                var blInstance = (IBllBase)Activator.CreateInstance(type, new PCRepository());
                _type2BllMap.Add(blInstance.BoType, blInstance);
            }
        }



        //public bool TypeIsDescendantOf(Type type, Type isDescendantOf)
        //{
        //    //var t = type.ParentTypes
        //}

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

            typesInfo.FindTypeInfo(typeof(EC2)).AddAttribute(new DefaultClassOptionsAttribute());
            typesInfo.FindTypeInfo(typeof(Ec2Child)).AddAttribute(new DefaultClassOptionsAttribute());
            typesInfo.FindTypeInfo(typeof(PC3)).AddAttribute(new DefaultClassOptionsAttribute());
        }
    }
}
