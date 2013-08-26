using Detestable;
using NUnit.Framework;

namespace AssemblyToProcess.NUnit
{
    [Detest("DEBUG")]
    [TestFixture]
    public class NUnitDebugDetestableClass
    {
        [Test]
        public void MethodShouldNotExistInDebug()
        {
            Assert.Fail();
        }
    }
}