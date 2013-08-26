using Detestable;
using NUnit.Framework;

namespace AssemblyToProcess.NUnit
{
    [Detest("RELEASE")]
    [TestFixture]
    public class NUnitReleaseDetestableClass
    {
        [Test]
        public void ShouldNotExistInRelease()
        {
            Assert.Fail();
        }
    }
}