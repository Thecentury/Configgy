using System;

namespace Configgy.Cache
{
    public sealed class GetValueArgs : IEquatable<GetValueArgs>
    {
        public string PropertyName { get; }

        public string SettingName { get; }

        public string GetFinalSettingName( string prefix )
        {
            string actualSettingName = SettingName ?? PropertyName;
            if ( !string.IsNullOrWhiteSpace( prefix ) )
            {
                return prefix + "." + actualSettingName;
            }

            return actualSettingName;
        }

        public GetValueArgs( string settingName, string propertyName )
        {
            PropertyName = propertyName;
            SettingName = settingName;
        }

        public bool Equals( GetValueArgs other )
        {
            if ( ReferenceEquals( null, other ) )
                return false;
            if ( ReferenceEquals( this, other ) )
                return true;

            return string.Equals( PropertyName, other.PropertyName ) && string.Equals( SettingName, other.SettingName );
        }

        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) )
                return false;
            if ( ReferenceEquals( this, obj ) )
                return true;

            return obj is GetValueArgs args && Equals( args );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ( ( PropertyName != null ? PropertyName.GetHashCode() : 0 ) * 397 ) ^
                    ( SettingName != null ? SettingName.GetHashCode() : 0 );
            }
        }

        public static bool operator ==( GetValueArgs left, GetValueArgs right )
        {
            return Equals( left, right );
        }

        public static bool operator !=( GetValueArgs left, GetValueArgs right )
        {
            return !Equals( left, right );
        }
    }
}