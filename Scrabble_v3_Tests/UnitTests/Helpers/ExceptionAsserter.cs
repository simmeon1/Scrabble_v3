using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Scrabble_v3_Tests.UnitTests.Helpers
{
    public static class ExceptionAsserter
    {
        public static void AssertExceptionWithMessageIsThrown(Action actionWithException, string message)
        {
            try
            {
                actionWithException.Invoke();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(message), $"Exception message is '{ex.Message}', expected '{message}'.");
            }
        }
    }
}
