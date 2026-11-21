namespace tournament_scheduler.Models
{
    public class GetTournamentTeamsResponse
    {
        public GetTournamentTeamsResponse()
        {
            tournament_id = 0;
            teams = new List<Team>();
        }

        public int tournament_id { get; set; }
        public List<Team> teams { get; set; }
    }
}
