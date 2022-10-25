﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QueReal.BLL.Interfaces;

namespace QueReal.BLL.Services
{
    internal class DatabaseService : IDatabaseService
    {
        private readonly DbContext dbContext;

        public DatabaseService(DbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public Task InitDatabaseAsync()
        {
            return dbContext.Database.MigrateAsync();
        }

        public Task SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
