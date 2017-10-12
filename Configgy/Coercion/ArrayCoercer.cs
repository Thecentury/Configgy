using System;
using System.Reflection;

namespace Configgy.Coercion
{
    public sealed class ArrayCoercer : ValueCoercerAttributeBase
    {
        private readonly string _separator;
        private readonly StringSplitOptions _stringSplitOptions;

        public ArrayCoercer( string separator = ";", StringSplitOptions splitOptions = StringSplitOptions.None )
        {
            _separator = separator;
            _stringSplitOptions = splitOptions;
        }

        public override bool Coerce<T>( string value, string valueName, ICustomAttributeProvider property, CoercionContext context,
            out T result )
        {
            result = default( T );
            if ( typeof( T ).IsArray || value == null )
            {
                return false;
            }

            Type elementType = typeof( T ).GetElementType();
            if ( elementType == null )
            {
                return false;
            }

            string[] parts = value.Split( new[] { _separator }, _stringSplitOptions );
            var resultArray = Array.CreateInstance(elementType, parts.Length);

            var genericCoerceMethod = typeof(IValueCoercer).GetMethod( nameof(IValueCoercer.Coerce) );
            var coerceMethod = genericCoerceMethod.MakeGenericMethod( elementType);

            var args = new object[] {null, valueName, property, context, null};

            for ( var i = 0; i < parts.Length; i++ )
            {
                args[0] = parts[i];
                bool coerced = (bool) coerceMethod.Invoke( context.RootCoercer, args );
                if ( !coerced )
                {
                    return false;
                }

                resultArray.SetValue( args[4], i );
            }

            result = (T)(object)resultArray;
            return true;
        }
    }
}