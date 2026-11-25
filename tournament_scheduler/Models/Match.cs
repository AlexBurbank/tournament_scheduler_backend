namespace tournament_scheduler.Models
{
    public class Match
    {
        public Match()
        {
            match_id = 0;
            home_team_id = 0;
            home_team_name = string.Empty;
            away_team_id = 0;
            away_team_name = string.Empty;
            venue_id = 0;
            venue_name = string.Empty;
            start_time = DateTime.MinValue;
            end_time = DateTime.MinValue;
        }

        public int match_id { get; set; }
        public int home_team_id { get; set; }
        public string home_team_name { get; set; }
        public int away_team_id { get; set; }
        public string away_team_name { get; set; }
        public int venue_id { get; set; }
        public string venue_name { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
    }
}
