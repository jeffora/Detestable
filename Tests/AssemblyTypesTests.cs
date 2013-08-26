using System.IO;
using System.Linq;
using System.Reflection;
using Detestable.Fody;
using Mono.Cecil;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AssemblyTypesTests
    {
        Assembly _assembly;

        public AssemblyTypesTests()
        {
#if DEBUG
            var configuration = "Debug";
#else
            var configuration = "Release";
#endif
            var beforeAssemblyPath =
                Path.GetFullPath(string.Format(@"..\..\..\AssemblyToProcess\bin\{0}\AssemblyToProcess.dll",
                                               configuration));
            var afterAssemblyPath = beforeAssemblyPath.Replace(".dll", "AssemblyTypesTests.dll");

            File.Copy(beforeAssemblyPath, afterAssemblyPath, true);
            var moduleDefinition = ModuleDefinition.ReadModule(afterAssemblyPath);

            var weavingTask = new ModuleWeaver();

            weavingTask.Execute();
            moduleDefinition.Write(afterAssemblyPath);

            _assembly = Assembly.LoadFile(afterAssemblyPath);
        }

        [Test]
        public void WillBeRemovedTypeExists()
        {
            Assert.That(_assembly.GetTypes().Any(t => t.Name == "WillBeRemoved" && t.IsClass));
        }
    }
}