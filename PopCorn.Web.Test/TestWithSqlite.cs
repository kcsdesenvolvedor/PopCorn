using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PopCorn.Web.Infra.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace PopCorn.Web.Test
{
   public class TestWithSqlite
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly PopCornContext DbContext;

        protected TestWithSqlite()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<PopCornContext>()
                    .UseSqlite(_connection)
                    .Options;
            DbContext = new PopCornContext();
            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
