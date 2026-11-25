namespace tournament_scheduler.Models
{
    public class CreateTournamentRequest
    {
        public CreateTournamentRequest()
        {
            Team_ids = Array.Empty<int>();
            Venue_ids = Array.Empty<int>();
            Schedule_settings = new ScheduleSettings();
        }
        public int[] Team_ids { get; set; }
        public int[] Venue_ids { get; set; }
        public ScheduleSettings Schedule_settings { get; set; }
    }
}
 