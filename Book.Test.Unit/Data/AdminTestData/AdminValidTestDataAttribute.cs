using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace BookTest.Unit.Data.AdminTestData
{
    public class AdminValidTestDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {

            yield return new object[] { "Javid", "mohamadi", "0317144073", "11/12/1399", "javidle1o", "javidleo.ef@gmial.com", "javidl123#21" };
            yield return new object[] { "Alireza", "Javadi", "0477786431", "29/12/1350", "re2zand", "reza.sl@gmail.com", "123@35%fdf" };
            yield return new object[] { "Javid", "Navidi", "0988309009", "11/10/1340", "moh4adamdf", "mohamad@gmail.com", "res1@2323:fdsfS" };
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