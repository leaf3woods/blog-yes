﻿using Microsoft.EntityFrameworkCore;

namespace BlogYes.Infrastructure.Utilities
{
    public static class EfExtension
    {
        public static IQueryable<TEntity> WithDeleted<TEntity>(this DbSet<TEntity> entities) where TEntity : class =>
            entities.IgnoreQueryFilters();
    }
}