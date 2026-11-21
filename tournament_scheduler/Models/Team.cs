namespace tournament_scheduler.Models
{
    public class Team
    {
        public Team(int team_id, string team_name, int coach_id, string coach_name, int age_group_id, string age_group)
        {
            this.team_id = team_id;
            this.team_name = team_name;
            this.coach_id = coach_id;
            this.coach_name = coach_name;
            this.age_group_id = age_group_id;
            this.age_group = age_group;
        }

        public int team_id { get; set; }
        public string team_name { get; set; }
        public int coach_id { get; set; }
        public string coach_name { get; set; }
        public string coach_email { get; set; }
        public int age_group_id { get; set; }
        public string age_group { get; set; }
        public int? division_id { get; set; }
        public string? division_name { get; set; }
    }
}
