using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace BookTest.Unit.Data.AdminTestData
{
    public class AdminValidTestDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {

            yield return new object[] { new AdminTestObject() { Name = "javid", Family = "Javadi", NationalCode = "0477786431", DateofBirth = "11/12/1377", UserName = "javid", Email = "javidleo.ef@gmail.com", Password = "javid123##123" } };
            yield return new object[] { new AdminTestObject() { Name = "javid", Family = "ali", NationalCode = "0477786431", DateofBirth = "11/12/1377", UserName = "javid", Email = "javidleo.ef@gmail.com", Password = "javid123##123" } };
        }
    }

    public class AdminTestObject
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string DateofBirth { get; set; }
        public string NationalCode { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}