using System;

namespace Detestable
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class DetestAttribute : Attribute
    {
        public string Conditional { get; private set; }

        public DetestAttribute(string conditional)
        {
            Conditional = conditional;
        }
    }
}