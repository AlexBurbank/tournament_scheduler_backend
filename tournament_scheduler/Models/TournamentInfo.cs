namespace tournament_scheduler.Models
{
    public class TournamentInfo
    {
        public TournamentInfo(int tournament_id, string tournament_name, DateTime start_date, DateTime end_date, List<ScoringRule> scoring_rules)
        {
            this.tournament_id = tournament_id;
            this.tournament_name = tournament_name;
            this.start_date = start_date;
            this.end_date = end_date;
            this.scoring_rules = scoring_rules ?? new List<ScoringRule>();
        }

        public int tournament_id { get; set; }
        public string tournament_name { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public List<ScoringRule> scoring_rules { get; set; }
    }
}
