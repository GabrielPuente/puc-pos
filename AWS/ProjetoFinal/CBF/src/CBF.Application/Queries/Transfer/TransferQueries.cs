using CBF.Application.Queries.Interfaces;
using CBF.Application.ViewModels;
using Dapper;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CBF.Application.Queries.Transfer
{
    public class TransferQueries : ITransferQueries
    {
        private readonly IDbConnection _connection;

        public TransferQueries(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<TransferViewModel>> GetAll()
        {
            var query = new Query();

            query.SelectRaw(@"
                            tr.Id 
                            ,tr.TeamOriginId
	                        ,tr.TeamDestinyId
	                        ,p.Id PlayerId
	                        ,TeamOriginName = (SELECT TOP 1 [Name] from Team where Id = tr.TeamOriginId)
	                        ,TeamDestinyName =  (SELECT TOP 1 [Name] from Team where Id = tr.TeamDestinyId)
	                        ,p.[Name] PlayerName
	                        ,tr.[Value]
	                        ,tr.TransferDate
                            ");

            query.FromRaw(@"Transfer tr
	                        join player p
	                        on tr.PlayerId = p.Id");

            query.WhereFalse("IsDeleted");

            SqlServerCompiler compiler = new() { UseLegacyPagination = false };
            var sqlResult = compiler.Compile(query);

            return await _connection.QueryAsync<TransferViewModel>(sqlResult.Sql, param: sqlResult.NamedBindings);
        }

        public async Task<TransferViewModel> GetById(Guid id)
        {
            var query = new Query();

            query.SelectRaw(@"
                             tr.Id 
                            ,tr.TeamOriginId
	                        ,tr.TeamDestinyId
	                        ,p.Id PlayerId
	                        ,TeamOriginName = (SELECT TOP 1 [Name] from Team where Id = tr.TeamOriginId)
	                        ,TeamDestinyName =  (SELECT TOP 1 [Name] from Team where Id = tr.TeamDestinyId)
	                        ,p.[Name] PlayerName
	                        ,tr.[Value]
	                        ,tr.TransferDate
                            ");

            query.FromRaw(@"Transfer tr
	                        join player p
	                        on tr.PlayerId = p.Id");
            query.Where("tr.Id", id);
            query.WhereFalse("IsDeleted");

            SqlServerCompiler compiler = new() { UseLegacyPagination = false };
            var sqlResult = compiler.Compile(query);
            
            return await _connection.QueryFirstOrDefaultAsync<TransferViewModel>(sqlResult.Sql, param: sqlResult.NamedBindings);
        }
    }
}
