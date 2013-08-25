using System;

namespace Detestable
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class DetestAttribute : Attribute
    {
    }
}