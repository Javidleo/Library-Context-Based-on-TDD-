using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace BookTest.Unit.Data.Admin
{
    public class AdminValidTestDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { "ali", "rezaie", "11/2/91", "0584418035", "ali123", "123123" };

            yield return new object[] { "reza", "mohamadi", "0317144073", "11/12/1399", "javidleo", "javidleo.ef@gmial.com", "javidl123#21" };
            yield return new object[] { "Alireza", "Javadi", "0477786431", "29/12/1350", "rezand", "reza.sl@yahoo.com", "123@35%fdf" };
            yield return new object[] { "mohamad", "Navidi", "0988309009", "11/10/1340", "mohadamdf", "mohamad@cloud.com", "res1@2323:fdsfS" };
        }
    }

    public class AdminTestDataObject
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