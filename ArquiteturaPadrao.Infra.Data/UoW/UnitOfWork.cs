﻿using ArquiteturaPadrao.Infra.Data.Context;
using Equinox.Domain.Interfaces;

namespace ArquiteturaPadrao.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArquiteturaPadraoContext _context;

        public UnitOfWork(ArquiteturaPadraoContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
