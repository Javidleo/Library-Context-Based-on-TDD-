using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace BookTest.Unit.Data.OwnerTestData;

public class OwnerInvalidTestData : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return new object[] { "", "family", "0613575024", "09177034678", "javidleo123", "javid123!@" };
        yield return new object[] { "ali", "family", "3124124213", "09177034678", "javidleo123", "javid123!@" };
        yield return new object[] { "reza", "", "0613575024", "09177034678", "javidleo123", "javid123!@" };
        yield return new object[] { "reza", "family", "0613575024", "09177034678", "", "user123@3" };
        yield return new object[] { "reza", "family", "0613575024", "09177034678", "javid123", "12312312" };
    }
}
