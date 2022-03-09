using Microsoft.EntityFrameworkCore;
using System;
using System.Transactions;

namespace BookTest.Integration;

public abstract class PersistTest<T> : IDisposable where T : DbContext, new()
{
    protected T DBContext;
    private TransactionScope scope;
    protected PersistTest()
    {
        scope = new TransactionScope();
        DBContext = new T();
    }

    public void Dispose()
    {
        scope.Dispose();
        DBContext.Dispose();
    }
}
