using System;
using Microsoft.EntityFrameworkCore;

namespace Shared
{
    public class UnitOfWork
    {
        private readonly DbContext context;

        public UnitOfWork(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}