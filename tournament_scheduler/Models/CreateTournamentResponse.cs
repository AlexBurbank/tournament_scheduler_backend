using System.Text.RegularExpressions;

namespace tournament_scheduler.Models
{
    public class CreateTournamentResponse
    {
        public CreateTournamentResponse()
        {
            tournament_id = 0;
            message = string.Empty;
            selected_team_count = 0;
            match_count = 0;
            matches = new List<Match>();
        }

        public int tournament_id { get; set; }
        public string message { get; set; }
        public int selected_team_count { get; set; }
        public int match_count { get; set; }
        public List<Match> matches { get; set; }
    }
}
