using UnityEngine;

namespace DebugHelper
{
    public static class DebugColored
    {

        public static void LogWarning(string prefix, Object context, string message, bool suppress = false, Color? color = null)
        {
            if (!suppress)
            {
                Color c = color ?? Color.yellow;
                Debug.LogWarning("<color=#" + ColorUtility.ToHtmlStringRGBA(c) + ">" + prefix + " " + context.ToString() + "</color>: " + message, context);
            }
        }
        
        public static void LogWarning(string prefix, string message, bool suppress = false, Color? color = null)
        {
            if (!suppress)
            {
                Color c = color ?? Color.yellow;
                Debug.LogWarning("<color=#" + ColorUtility.ToHtmlStringRGBA(c) + ">" + prefix + "</color>: " + message);
            }
        }
        
        public static void LogWarning(Object context, string message, bool suppress = false, Color? color = null)
        {
            if (!suppress)
            {
                Color c = color ?? Color.yellow;
                Debug.LogWarning("<color=#" + ColorUtility.ToHtmlStringRGBA(c) + ">" + context.ToString() + "</color>: " + message, context);
            }
        }
        
        public static void LogError(string prefix,string message, Color? color = null)
        {
            Color c = color.GetValueOrDefault(Color.red);
            Debug.LogError("<color=#" + ColorUtility.ToHtmlStringRGBA(c) + ">" + prefix + "</color>: " + message);
        }
        
        public static void LogError(Object context, string message, Color? color = null)
        {
            Color c = color.GetValueOrDefault(Color.red);
            Debug.LogError("<color=#" + ColorUtility.ToHtmlStringRGBA(c) + ">" + context.ToString() + "</color>: " + message, context);
        }

        public static void LogError(string prefix, Object context, string message, Color? color = null)
        {
            Color c = color.GetValueOrDefault(Color.red);
            Debug.LogError("<color=#" + ColorUtility.ToHtmlStringRGBA(c) + ">" + prefix + " " + context.ToString() + "</color>: " + message, context);
        }

        public static void Log(bool show, Color color, string prefix, Object context, string message)
        {
            if (show) Debug.Log("<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + prefix + " " + context + "</color>: " + message, context);
        }

        public static void Log(bool show, Color color, string prefix, string message)
        {
            if (show) Debug.Log("<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + prefix + "</color>: " + message);
        }

        public static void Log(bool show, Color color, string message)
        {
            if (show) Debug.Log("<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + message + "</color>");
        }

        public static void Log(bool show, Color color, Object context, string message)
        {
            if (show) Debug.Log("<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + context + "</color>: " + message, context);
        }
        
    }
}