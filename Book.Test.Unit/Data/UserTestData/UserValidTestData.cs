using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace BookTest.Unit.Data.UserTestData;
public class UserValidTestData : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return new object[]
        {
            new UserValidTestObject()
            {
                name = "ali", family = "rezaie", age = 15,nationalCode = "0317144073",email = "javidleo.ef@gmail.com",adminId = 1
            }
        };
    }
}
public class UserValidTestObject
{
    public string name { get; set; }
    public string family { get; set; }
    public int age { get; set; }
    public string nationalCode { get; set; }
    public string email { get; set; }
    public int adminId { get; set; }
}
