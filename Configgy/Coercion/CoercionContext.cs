namespace Configgy.Coercion
{
    public sealed class CoercionContext
    {
        public CoercionContext( IValueCoercer rootCoercer )
        {
            RootCoercer = rootCoercer;
        }

        public IValueCoercer RootCoercer { get; }
    }
}