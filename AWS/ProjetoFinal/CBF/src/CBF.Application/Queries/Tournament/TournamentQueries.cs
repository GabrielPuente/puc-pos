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

namespace CBF.Application.Queries.Tournament
{
    public class TournamentQueries : ITournamentQueries
    {
        private readonly IDbConnection _connection;

        public TournamentQueries(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<TournamentViewModel>> GetAll()
        {
            var query = new Query();

            query.SelectRaw(@"
                            Id,
                            Name,
                            Reference");
            query.FromRaw(@"[Tournament]");

            query.WhereFalse("IsDeleted");

            SqlServerCompiler compiler = new() { UseLegacyPagination = false };
            var sqlResult = compiler.Compile(query);

            return await _connection.QueryAsync<TournamentViewModel>(sqlResult.Sql, param: sqlResult.NamedBindings);
        }

        public async Task<TournamentViewModel> GetAllMatchByTournamentId(Guid id)
        {
            var query = new Query();

            query.SelectRaw(@"
                            t.Id,
		                    t.[Name],
		                    t.Reference,

		                    m.Id,
		                    m.TournamentId,
		                    HomeTeamName = (select name from Team where Id = m.HomeTeamId),
		                    AwayTeamName = (select name from Team where Id = m.AwayTeamId),
		                    m.Reference");

            query.FromRaw(@"[Tournament] t
                        join Match m
                        on m.TournamentId = t.Id");

            query.Where("t.Id", id);
            query.WhereFalse("t.IsDeleted");

            SqlServerCompiler compiler = new() { UseLegacyPagination = false };
            var sqlResult = compiler.Compile(query);

            var types = new[] {
                typeof(TournamentViewModel),
                typeof(MatchViewModel),
            };

            TournamentViewModel response = null;

            var result = await _connection.QueryAsync(sqlResult.Sql, types, mappedTypes =>
            {
                var match = mappedTypes[1] as MatchViewModel;

                if (response is null)
                {
                    response = mappedTypes[0] as TournamentViewModel;
                }

                if (!response.Matchs.Any(x => x.Id == match?.Id))
                {
                    response.Matchs.Add(match);
                }

                return response;
            }, param: sqlResult.NamedBindings);

            return result.FirstOrDefault();
        }

        public async Task<TournamentViewModel> GetAllEventMatchByTournamentId(Guid id, Guid matchId)
        {
            var query = new Query();

            query.SelectRaw(@"
                            t.Id,
		                    t.[Name],
		                    t.Reference,

		                    m.Id,
		                    m.TournamentId,
		                    HomeTeamName = (select name from Team where Id = m.HomeTeamId),
		                    AwayTeamName = (select name from Team where Id = m.AwayTeamId),
		                    m.Reference,

                            e.Id,
                            e.Message,
                            e.Reference");

            query.FromRaw(@"[Tournament] t
                        
                        join Match m
                        on m.TournamentId = t.Id
                        
                        join Event e
	                    on e.MatchId = m.Id");

            query.Where("t.Id", id);
            query.Where("m.Id", matchId);
            query.WhereFalse("t.IsDeleted");

            SqlServerCompiler compiler = new() { UseLegacyPagination = false };
            var sqlResult = compiler.Compile(query);

            var types = new[] {
                typeof(TournamentViewModel),
                typeof(MatchViewModel),
                typeof(EventViewModel),
            };

            TournamentViewModel response = null;

            var result = await _connection.QueryAsync(sqlResult.Sql, types, mappedTypes =>
            {
                var match = mappedTypes[1] as MatchViewModel;
                var evt = mappedTypes[2] as EventViewModel;

                if (response is null)
                {
                    response = mappedTypes[0] as TournamentViewModel;
                }

                if (!response.Matchs.Any(x => x.Id == match?.Id))
                {
                    response.Matchs.Add(match);
                }

                var matchFind = response.Matchs.Find(x => x.Id == match.Id);
                matchFind.Events.Add(evt);

                return response;
            }, param: sqlResult.NamedBindings);

            return result.FirstOrDefault();
        }
    }
}
