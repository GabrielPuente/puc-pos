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

namespace CBF.Application.Queries.Team
{
    public class TeamQueries : ITeamQueries
    {
        private readonly IDbConnection _connection;

        public TeamQueries(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<TeamViewModel>> GetAll()
        {
            var query = new Query();

            query.SelectRaw(@"
                            t.Id
                            ,t.Name
                            ,t.Locality

                            ,p.Id
                            ,p.Name
                            ,p.Country
                            ,p.BirthDate
                            ,p.MarketValue
                            ,p.TeamId
                            ");

            query.FromRaw(@"Team t
                            left join Player p
                            on t.Id = p.TeamId");
            query.WhereFalse("IsDeleted");

            SqlServerCompiler compiler = new() { UseLegacyPagination = false };
            var sqlResult = compiler.Compile(query);

            var types = new[] {
                typeof(TeamViewModel),
                typeof(PlayerViewModel),
            };

            List<TeamViewModel> teamResponse = new();

            var result = await _connection.QueryAsync(sqlResult.Sql, types, mappedTypes =>
            {
                var team = mappedTypes[0] as TeamViewModel;
                var player = mappedTypes[1] as PlayerViewModel;

                if (!teamResponse.Any(x => x.Id == team.Id))
                {
                    if (team.Id == player?.TeamId && !team.Players.Any(x => x.TeamId == team.Id))
                    {
                        team.Players.Add(player);
                    }

                    teamResponse.Add(team);
                }
                else
                {
                    var teamFind = teamResponse.Find(x => x.Id == team.Id);
                    teamFind.Players.Add(player);
                }

                return teamResponse;
            });

            return result.FirstOrDefault();
        }

        public async Task<TeamViewModel> GetById(Guid id)
        {
            var query = new Query();

            query.SelectRaw(@"
                            t.Id
                            ,t.Name
                            ,t.Locality

                            ,p.Id
                            ,p.Name
                            ,p.Country
                            ,p.BirthDate
                            ,p.TeamId
                            ,p.MarketValue
                            ");

            query.FromRaw(@"Team t
                            left join Player p
                            on t.Id = p.TeamId");
            query.Where("t.Id", id);
            query.WhereFalse("IsDeleted");

            SqlServerCompiler compiler = new() { UseLegacyPagination = false };
            var sqlResult = compiler.Compile(query);

            var types = new[] {
                typeof(TeamViewModel),
                typeof(PlayerViewModel),
            };

            TeamViewModel team = null;

            var result = await _connection.QueryAsync(sqlResult.Sql, types, mappedTypes =>
            {
                var player = mappedTypes[1] as PlayerViewModel;

                if (team is null)
                {
                    team = mappedTypes[0] as TeamViewModel;
                }

                if (!team.Players.Any(x => x.Id == player?.Id))
                {
                    team.Players.Add(player);
                }

                return team;
            }, param: sqlResult.NamedBindings);

            return result.FirstOrDefault();
        }
    }
}
