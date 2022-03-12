using CBF.Application.Queries.Interfaces;
using CBF.Application.ViewModels;
using Dapper;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CBF.Application.Queries.Player
{
    public class PlayerQueries : IPlayerQueries
    {
        private readonly IDbConnection _connection;

        public PlayerQueries(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<TeamPlayerViewModel>> GetAll()
        {
            var query = new Query();

            query.SelectRaw(@"
                            p.Id PlayerId
                            ,p.[Name] PlayerName
                            ,p.MarketValue
                            ,t.Id TeamId
                            ,t.[Name] TeamName");
            query.FromRaw(@"Player p
                            join Team t
                            on p.TeamId = t.Id");

            query.WhereFalse("IsDeleted");

            SqlServerCompiler compiler = new() { UseLegacyPagination = false };
            var sqlResult = compiler.Compile(query);

            return await _connection.QueryAsync<TeamPlayerViewModel>(sqlResult.Sql, param: sqlResult.NamedBindings);

        }

        public async Task<TeamPlayerViewModel> GetById(Guid id)
        {
            var query = new Query();

            query.SelectRaw(@"
                             p.Id PlayerId
                            ,p.[Name] PlayerName
                            ,p.MarketValue
                            ,t.Id TeamId
                            ,t.[Name] TeamName");

            query.FromRaw(@"Player p
                            join Team t
                            on p.TeamId = t.Id");

            query.Where("p.Id", id);
            query.WhereFalse("IsDeleted");

            SqlServerCompiler compiler = new() { UseLegacyPagination = false };
            var sqlResult = compiler.Compile(query);
            
            return await _connection.QueryFirstOrDefaultAsync<TeamPlayerViewModel>(sqlResult.Sql, param: sqlResult.NamedBindings);
        }
    }
}
