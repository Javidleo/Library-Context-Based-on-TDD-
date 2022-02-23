using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace BookTest.Unit.Data.Admin
{
    public class AdminTestInvalidDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { "reza", "mohamadi", "123d1123", "11/12/1399", "javidleo", "javidleo.ef@gmial.com", "javidl123#21" };
            yield return new object[] { "Alireza", "Javadi", "0477786431", "29/12/1350", "rezand", "reza.sl@yahoo.com", "123123123" };
            yield return new object[] { "mohamad", "Navidi", "0988309009", "11/10/1340", "MohammadReza", "mohamad@cloud.com", "res1@2323:fdsfS" };
            yield return new object[] { "mohamad", "reza23", "0988309009", "", "MohammadReza", "mohamad@cloud.com", "res1@2323:fdsfS" };
        }
    }
}
