﻿using Microsoft.EntityFrameworkCore;

public class DbContextFactory<TContext> : IDbContextFactory<TContext> where TContext : DbContext
{
    private readonly IServiceProvider _provider;

    public DbContextFactory(IServiceProvider provider)
    {
        _provider = provider;
    }

    public TContext CreateDbContext()
    {
        if (_provider == null)
        {
            throw new InvalidOperationException("You must configure an instance of IServiceProvider");
        }

        return ActivatorUtilities.CreateInstance<TContext>(_provider);
    }
}
