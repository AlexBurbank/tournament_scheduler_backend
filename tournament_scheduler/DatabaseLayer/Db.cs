using dotenv.net;
using Npgsql;
using System.ComponentModel;
using tournament_scheduler.Models;

namespace tournament_scheduler.DatabaseLayer
{
    public class Db
    {
        private readonly string connString;

        public Db(IConfiguration configuration)
        {
            DotEnv.Load();

            // Read variables from .env without modifying the system environment
            var envVars = DotEnv.Read();

            Console.WriteLine(envVars["ConnectionString"]);
        }

        public async Task<GetVenuesResponse> GetTournamentVenueInfo()
        {
            var getVenuesResponse = new GetVenuesResponse();
            var tournament_id = 0;
            var venue_id = 0;
            var venue_name = "";
            var availability = new List<VenueAvailability>();
            var start_date = DateTime.MinValue;
            var end_date = DateTime.MinValue;
            var databaseLines = new List<(string, DateTime, DateTime)>();

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            await using var tournament_info_cmd = new NpgsqlCommand("SELECT DISTINCT tournament_id from vw_tournament_info", conn); 
            await using var venues_info_cmd = new NpgsqlCommand("SELECT * from vw_venues SORT BY venue_name", conn);

            await using var tournamentInfoReader = await tournament_info_cmd.ExecuteReaderAsync();
            await using var venueInfoReader = await venues_info_cmd.ExecuteReaderAsync();
            int i = 0;

            while (await tournamentInfoReader.ReadAsync())
            {
                tournament_id = tournamentInfoReader.GetInt32(0);
            }
            getVenuesResponse.tournament_id = tournament_id;

            while (await venueInfoReader.ReadAsync())
            {
                //venue_id = venueInfoReader.GetInt32(0);
                venue_name = venueInfoReader.GetString(0);
                start_date = venueInfoReader.GetDateTime(1);
                end_date = venueInfoReader.GetDateTime(2);

                databaseLines.Add((venue_name, start_date, end_date));
            }

            var groupedDatabaseLines = databaseLines.GroupBy(line => line.Item1).ToList(); // returns a list of groups grouped by venue name

            foreach (var group in groupedDatabaseLines)
            {
                var venueName = group.Key;
                var tournamentVenue = new TournamentVenue(venueName);
                tournamentVenue.availability = group.Select(line => new VenueAvailability(line.Item2, line.Item3)).ToList(); // adds just the star_date and end_date from the group
                getVenuesResponse.venues.Add(tournamentVenue);
            }

            foreach (var (name, startDate, endDate) in databaseLines)
            {
                availability.Add(new VenueAvailability(startDate, endDate));
                var tournamentVenue = new TournamentVenue(name);
                getVenuesResponse.venues.Add(tournamentVenue);

                i++;
            }

            return getVenuesResponse;
        }

        public async Task<TournamentInfo> GetTournamentInfo()
        {
            var tournament_id = 0;
            var tournament_name = "";
            var start_date = DateTime.MinValue;
            var end_date = DateTime.MinValue;
            var scoring_rules = new List<ScoringRule>();

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT * from vw_tournaments_info", conn);

            await using var reader = await cmd.ExecuteReaderAsync();

            int i = 0;
            while (await reader.ReadAsync())
            {
                if (i == 0)
                {
                    tournament_id = reader.GetInt32(0);
                    tournament_name = reader.GetString(1);
                    start_date = reader.GetDateTime(2);
                    end_date = reader.GetDateTime(3);
                }
                scoring_rules.Add(new ScoringRule(point_name: reader.GetString(4), point_value: reader.GetInt32(5)));

                i++;
            }

            return new TournamentInfo(tournament_id,
                tournament_name,
                start_date,
                end_date,
                scoring_rules);

        }
        public async Task<GetTournamentTeamsResponse> GetRegisteredTeams()
        {
            var getTournamentTeamsResponse = new GetTournamentTeamsResponse();
            var teams = new List<Team>();
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT * from vw_teams", conn);

            await using var reader = await cmd.ExecuteReaderAsync();
            var i = 0;
            while (await reader.ReadAsync())
            {
                if (i == 0)
                {
                    getTournamentTeamsResponse.tournament_id = reader.GetInt32(0);
                }

                teams.Add(new Team(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5)));
                i++;
            }

            getTournamentTeamsResponse.teams = teams;

            return getTournamentTeamsResponse;
        }
    }
}
