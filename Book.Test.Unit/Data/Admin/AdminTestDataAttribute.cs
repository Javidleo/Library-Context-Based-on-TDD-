using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace BookTest.Unit.Data.Admin
{
    public class AdminTestDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { 1, "ali", "rezaie", "11/2/91", "0584418035", "ali123", "123123" };
        }
    }
}