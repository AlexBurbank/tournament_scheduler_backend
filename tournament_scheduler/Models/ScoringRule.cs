namespace tournament_scheduler.Models
{
    public class ScoringRule
    {
        public ScoringRule(string point_name, int point_value)
        {
            this.point_name = point_name;
            this.point_value = point_value;
        }
        public string point_name { get; set; }
        public int point_value { get; set; }
    }
}