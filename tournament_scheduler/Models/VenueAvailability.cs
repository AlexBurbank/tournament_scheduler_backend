namespace tournament_scheduler.Models
{
    public class VenueAvailability
    {
        public VenueAvailability(DateTime available_from, DateTime available_until)
        {
            //availability_id = 0;
            this.available_from = available_from;
            this.available_until = available_until;
        }
        
        //public int availability_id { get; set; }
        public DateTime available_from { get; set; }
        public DateTime available_until { get; set; }
    }
}
