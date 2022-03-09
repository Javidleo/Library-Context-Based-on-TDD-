using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace BookTest.Unit.Data.BookTestData;

public class BookTestData : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return new object[] { 1, "book1", "reza", "11/2/91", "0242230865" };
        yield return new object[] { 1, "book2", "ali", "21/2/91", "0242230865" };
        yield return new object[] { 1, "book3", "rahman", "12/2/91", "0704947579" };
        yield return new object[] { 1, "book4", "rahmat", "14/2/91", "0242230865" };
    }
}
