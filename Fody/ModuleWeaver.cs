using System;
using System.Linq;
using Mono.Cecil;

namespace Detestable.Fody
{
    public class ModuleWeaver
    {
        public Action<string> LogInfo { get; set; }
        public Action<string> LogWarning { get; set; }
        public ModuleDefinition ModuleDefinition { get; set; }

        public ModuleWeaver()
        {
            LogInfo = _ => { };
            LogWarning = _ => { };
        }

        public void Execute()
        {
            var types = ModuleDefinition.GetTypes().ToList();
            foreach (var type in types)
            {
                RemoveDetestableNUnit(type);
            }
        }

        void RemoveDetestableNUnit(TypeDefinition type)
        {
            var detestAttribute =
                type.CustomAttributes.FirstOrDefault(a => a.AttributeType.FullName == "Detestable.DetestAttribute");
            if (detestAttribute != null)
            {
                var conditional = detestAttribute.ConstructorArguments.Single().Value.ToString();
#if DEBUG
                if (conditional == "DEBUG")
#else
                if (conditional == "RELEASE")
#endif
                {
                    ModuleDefinition.Types.Remove(type); 
                }
            }
        }
    }
}