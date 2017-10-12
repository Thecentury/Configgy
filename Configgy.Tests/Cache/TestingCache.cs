using Configgy.Cache;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Configgy.Tests.Cache
{
    /// <summary>
    ///     A testing cache implementation that doesn't actually cache anything
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TestingCache : IValueCache
    {
        public void Clear()
        {
            // Nothing to do
        }

        public object Get( string settingName, string valueName, Func<GetValueArgs, object> valueCallback )
        {
            return valueCallback( new GetValueArgs( settingName, valueName ) );
        }

        public void Remove( string settingName, string valueName )
        {
            // Nothing to do
        }
    }
}