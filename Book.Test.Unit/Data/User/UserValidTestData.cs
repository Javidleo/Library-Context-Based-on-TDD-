using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace BookTest.Unit.Data.User
{
    public class UserValidTestData : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { "ali", "rezaie", 17, "0317144073", "javidle@gmial.com" };
            yield return new object[] { "reza", "mohamadi", 19, "0317144073", "javidleo.@cloud.com" };
        }
    }
}
