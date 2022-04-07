using CBF.Application.Queries.Interfaces;
using CBF.Application.Queries.Player;
using CBF.Application.Queries.Team;
using CBF.Application.Queries.Tournament;
using CBF.Application.Queries.Transfer;
using CBF.Application.Services.Interfaces;
using CBF.Application.Services.Player;
using CBF.Application.Services.Team;
using CBF.Application.Services.Tournament;
using CBF.Application.Services.Transfer;
using CBF.Application.Services.User;
using CBF.Infra.Data.Auditing;
using CBF.Infra.Data.Interfaces;
using CBF.Infra.Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.Common;

namespace CBF.Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITeamService, TeamService>()
                    .AddScoped<ITransferService, TransferService>()
                    .AddScoped<IPlayerService, PlayerService>()
                    .AddScoped<IUserService, UserService>()
                    .AddScoped<ITournamentService, TournamentService>();


            return services;
        }

        public static IServiceCollection AddApplicationQueries(this IServiceCollection services)
        {
            services.AddScoped<ITeamQueries, TeamQueries>()
                    .AddScoped<IPlayerQueries, PlayerQueries>()
                    .AddScoped<ITransferQueries, TransferQueries>()
                    .AddScoped<ITournamentQueries, TournamentQueries>();
            
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITeamRepository, TeamRepository>()
                    .AddScoped<ITransferRepository, TransferRepository>()
                    .AddScoped<IPlayerRepository, PlayerRepository>()
                    .AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<ITournamentRepository, TournamentRepository>();

            return services;
        }

        public static IServiceCollection AddInfraData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnection, DbConnection>(provider =>
            {
                string connectionString = configuration.GetConnectionString("Connection");
                return new SqlConnection(connectionString);
            });

            services.AddScoped<IEntryAuditor, EntryAuditor>();

            return services;
        }

    }
}
