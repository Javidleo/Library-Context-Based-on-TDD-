using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace BookTest.Unit.Data.AdminTestData
{
    public class AdminTestInvalidDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { "", "mohamadi", "11/12/1399", "0317144073", "javidleo", "javidleo.ef@gmial.com", "javidl123#21" };
            yield return new object[] { "reza", "", "11/12/1399", "0317144073", "javidleo", "javidleo.ef@gmial.com", "javidl123#21" };
            yield return new object[] { "reza", "mohamadi", "", "0317144073", "javidleo", "javidleo.ef@gmial.com", "javidl123#21" };
            yield return new object[] { "reza", "mohamadi", "11/12/1399", "", "javidleo", "javidleo.ef@gmial.com", "javidl123#21" };
            yield return new object[] { "reza", "mohamadi", "11/12/1399", "0317144073", "", "javidleo.ef@gmial.com", "javidl123#21" };
            yield return new object[] { "reza", "mohamadi", "11/12/1399", "0317144073", "javidleo", "", "javidl123#21" };
            yield return new object[] { "reza", "mohamadi", "11/12/1399", "0317144073", "javidleo", "javidleo.ef@gmial.com", "" };
            yield return new object[] { "re12za", "mohamadi", "11/12/1399", "0317144073", "javidleo", "javidleo.ef@gmial.com", "javidl123#21" };
            yield return new object[] { "reza", "moh@54amadi", "11/12/1399", "0317144073", "javidleo", "javidleo.ef@gmial.com", "javidl123#21" };
            yield return new object[] { "reza", "mohamadi", "11/112/1399", "0317144073", "javidleo", "javidleo.ef@gmial.com", "javidl123#21" };
            yield return new object[] { "reza", "mohamadi", "11/12/1399", "0317123144073", "javidleo", "javidleo.ef@gmial.com", "javidl123#21" };
            yield return new object[] { "reza", "mohamadi", "11/12/1399", "0317144073", "javidEleo", "javidleo.ef@gmial.com", "javidl123#21" };
            yield return new object[] { "reza", "mohamadi", "11/12/1399", "0317144073", "javidleo", "javidleo.efgmial.com", "javidl123#21" };
            yield return new object[] { "reza", "mohamadi", "11/12/1399", "0317144073", "javidleo", "javidleo.ef@gmial.com", "eqe@3Q" };
        }
    }
}