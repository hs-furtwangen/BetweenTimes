using System;
using UnityEngine;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class ConditionalHideAttribute : PropertyAttribute
    {
        //The name of the bool field that will be in control
        public string ConditionalSourceField = "";

        public bool Invert = false;
        //TRUE = Hide in inspector / FALSE = Disable in inspector 
        public bool HideInInspector = false;

        public ConditionalHideAttribute(string conditionalSourceField)
        {
            this.ConditionalSourceField = conditionalSourceField;
            Invert = false;
            this.HideInInspector = false;
        }
    
        public ConditionalHideAttribute(string conditionalSourceField, bool invert, bool hideInInspector)
        {
            this.ConditionalSourceField = conditionalSourceField;
            Invert = invert;
            this.HideInInspector = hideInInspector;
        }
    
        public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
        {
            this.ConditionalSourceField = conditionalSourceField;
            Invert = false;
            this.HideInInspector = hideInInspector;
        }
    }
}