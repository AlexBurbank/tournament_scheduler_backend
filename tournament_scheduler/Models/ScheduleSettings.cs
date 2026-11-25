namespace tournament_scheduler.Models
{
    public class ScheduleSettings
    {
        public ScheduleSettings()
        {
            format = "round_robin";
            match_duration_minutes = 90;
            buffer_minutes = 15;
            minimum_rest_minutes = 30;
        }

        public string format { get; set; }
        public int match_duration_minutes { get; set; }
        public int buffer_minutes { get; set; }
        public int minimum_rest_minutes { get; set; }
    }
}