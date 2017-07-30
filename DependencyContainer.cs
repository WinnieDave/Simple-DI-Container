using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace IOC
{
    static public class DependencyContainer
    {
        private static List<BindingInfo> _bindingStorage;
        private static Dictionary<Type, object> _singleInstances;
        public static void BindToFactory<T1, T2>()
            where T2 : T1
        {
            _bindingStorage.Add(new BindingInfo(typeof(T1),
                typeof(T2), BindingType.BindToFactory));
        }
        public static void BindToSingle<T1, T2>()
    where T2 : T1
        {
            _bindingStorage.Add(new BindingInfo(typeof(T1),
                typeof(T2), BindingType.BindToSingle));
        }
        public static object Resolve<T>(params object[] ctorParams)
        {
            BindingInfo binding = _bindingStorage.Find(b => b.Target == typeof(T));
            Type source = binding.Source;
            if (binding.BindingType == BindingType.BindToSingle)
            {
                
                object toReturn;
                if (!_singleInstances.TryGetValue(source, out toReturn))
                    _singleInstances[source] = Construct(source, ctorParams);
                return _singleInstances[source];
            }
            else if (binding.BindingType == BindingType.BindToFactory)
                return Construct(source, ctorParams);
            else
                return null;
        }
        private static object Construct(Type source,params object[] ctorParams)
        {
            ConstructorInfo[] ctorInfos = source.GetConstructors();
            MemberInfo[] members = source.GetMembers(BindingFlags.Instance | BindingFlags.Public);
            ConstructorInfo ctorInfo =ctorInfos.
                    OrderBy(cinfo => cinfo.GetParameters().Length).ToArray()[ctorInfos.Length-1];
            if (ctorParams.Length != ctorInfo.GetParameters().Length)
                throw new ArgumentNullException();
            return ctorInfo.Invoke(ctorParams);
        }
        static DependencyContainer()
        {
            _bindingStorage = new List<BindingInfo>();
            _singleInstances = new Dictionary<Type, object>();
        }
    }
   

}
