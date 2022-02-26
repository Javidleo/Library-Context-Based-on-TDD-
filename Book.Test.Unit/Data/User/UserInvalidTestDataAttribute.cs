using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace BookTest.Unit.Data.User
{
    internal class UserInvalidTestDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { "ali", "rezaie", 1, "0317144073", "javidleo.ef@gmial.com" };
            yield return new object[] { "reza", "rezaie", 16, "12312412", "javidl@gmial.com" };
            yield return new object[] { "reza", "rezaie", 17, "0317144073", "dflsdkfj" };
        }
    }
}