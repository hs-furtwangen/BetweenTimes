using System;
using UnityEngine;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class ConditionalHideRangeAttribute : PropertyAttribute
    {
        //The name of the bool field that will be in control
        public string ConditionalSourceField = "";
        public float Min;
        public float Max;
        public bool Invert;
        //TRUE = Hide in inspector / FALSE = Disable in inspector 
        public bool HideInInspector = false;

        public ConditionalHideRangeAttribute(string conditionalSourceField, float min, float max)
        {
            this.ConditionalSourceField = conditionalSourceField;
            Min = min;
            Max = max;
            Invert = false;
            this.HideInInspector = false;
        }
        
        public ConditionalHideRangeAttribute(string conditionalSourceField, bool invert, float min, float max)
        {
            this.ConditionalSourceField = conditionalSourceField;
            Min = min;
            Max = max;
            Invert = invert;
            this.HideInInspector = false;
        }
        
        public ConditionalHideRangeAttribute(string conditionalSourceField, float min, float max, bool hideInInspector)
        {
            this.ConditionalSourceField = conditionalSourceField;
            Min = min;
            Max = max;
            Invert = false;
            this.HideInInspector = hideInInspector;
        }
        
        public ConditionalHideRangeAttribute(string conditionalSourceField,bool invert, float min, float max,  bool hideInInspector)
        {
            this.ConditionalSourceField = conditionalSourceField;
            Min = min;
            Max = max;
            Invert = invert;
            this.HideInInspector = hideInInspector;
        }
    }
}