using System;

namespace SmartPlugins.Common.Abstractions.TeklaStructures
{
    public interface ISmartPicker
    {
        T1 PickObject<T1, T2>(string parameter) where T1 : class where T2 : Enum;
    }
}
