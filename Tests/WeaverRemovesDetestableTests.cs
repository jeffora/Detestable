using System.IO;
using System.Linq;
using System.Reflection;
using Detestable.Fody;
using Mono.Cecil;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class WeaverRemovesDetestableTests
    {
        Assembly _assembly;

        public WeaverRemovesDetestableTests()
        {
#if DEBUG
            var configuration = "Debug";
#else
            var configuration = "Release";
#endif
            var beforeAssemblyPath =
                Path.GetFullPath(string.Format(@"..\..\..\AssemblyToProcess\bin\{0}\AssemblyToProcess.dll",
                                               configuration));
            var afterAssemblyPath = beforeAssemblyPath.Replace(".dll", "NUnitTests.dll");

            File.Copy(beforeAssemblyPath, afterAssemblyPath, true);
            var moduleDefinition = ModuleDefinition.ReadModule(afterAssemblyPath);

            var weavingTask = new ModuleWeaver
            {
                ModuleDefinition = moduleDefinition,
            };

            weavingTask.Execute();
            moduleDefinition.Write(afterAssemblyPath);

            _assembly = Assembly.LoadFile(afterAssemblyPath); 
        }

#if DEBUG
        [Test]
        public void NUnitDetestedDebugFixtureIsRemoved()
        {
            Assert.That(_assembly.GetTypes().All(t => t.Name != "NUnitDebugDetestableClass"));
        }

        [Test]
        public void NUnitDetestedReleaseFixtureIsNotRemoved()
        {
            Assert.That(_assembly.GetTypes().Any(t => t.Name == "NUnitReleaseDetestableClass"));
        }
#endif

        [Test]
        public void NonTestDebugTypeIsNotRemoved()
        {
            Assert.That(_assembly.GetTypes().Any(t => t.Name == "NonTestDebugClassIsNotRemoved"));
        }

        [Test]
        public void NonTestReleaseTypeIsNotRemoved()
        {
            Assert.That(_assembly.GetTypes().Any(t => t.Name == "NonTestReleaseClassIsNotRemoved"));
        }

#if !DEBUG
        [Test]
        public void NUnitDetestedReleaseFixtureIsRemoved()
        {
            Assert.That(_assembly.GetTypes().All(t => t.Name != "NUnitReleaseDetestableClass"));
        }

        [Test]
        public void NUnitDetestedDebugFixtureIsNotRemoved()
        {
            Assert.That(_assembly.GetTypes().Any(t => t.Name == "NUnitDebugDetestableClass"));
        }
#endif
    }
}