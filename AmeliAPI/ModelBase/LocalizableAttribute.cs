using System;
using System.Collections.Generic;
using System.Text;

namespace ModelBase
{
    //
    // Zusammenfassung:
    //     Denotes one or more properties that are localizable.
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class LocalizableAttribute : Attribute
    {
        public LocalizableAttribute() { }
    }
}
