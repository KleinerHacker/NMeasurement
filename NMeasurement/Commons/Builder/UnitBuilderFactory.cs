using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using NMeasurement.Commons.Builder.Attributes;

namespace NMeasurement.Commons.Builder
{
    /// <summary>
    /// Factory to create unit builder based on <see cref="IUnitBuilder"/> with attribute <see cref="UnitBuilderAttribute"/>
    /// </summary>
    public sealed class UnitBuilderFactory
    {
        public static T CreateUnitBuilder<T>() where T : IUnitBuilder
        {
            return (T) new UnitBuilderProxy<T>().GetTransparentProxy();
        }

        private sealed class UnitBuilderProxy<T> : RealProxy
        {
            public override IMessage Invoke(IMessage msg)
            {
                var methodCall = (IMethodCallMessage)msg;
                var method = (MethodInfo)methodCall.MethodBase;
                
                
            }
        }
    }
    
    internal sealed class UnitData<TU, TP> where TU : IUnit where TP : IPrefixBase
    {
        public TU Unit { get; }
        public TP Prefix { get; }

        public UnitData(TU unit, TP prefix)
        {
            Unit = unit;
            Prefix = prefix;
        }
    }
}