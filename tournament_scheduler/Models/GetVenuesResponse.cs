namespace tournament_scheduler.Models
{
    public class GetVenuesResponse
    {
        public GetVenuesResponse()
        {
            tournament_id = 0;
            venues = new List<TournamentVenue>();
        }

        public int tournament_id { get; set; }
        public List<TournamentVenue> venues { get; set; }
    }
}
