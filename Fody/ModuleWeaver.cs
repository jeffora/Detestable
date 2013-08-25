using System;
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
 
        } 
    }
}