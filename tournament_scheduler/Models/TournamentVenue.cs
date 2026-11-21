namespace tournament_scheduler.Models
{
    public class TournamentVenue
    {
        public TournamentVenue(/*int venue_id,*/ string venue_name)
        {
            //this.venue_id = venue_id;
            this.venue_name = venue_name;
            availability = new List<VenueAvailability>();
        }

        //public int venue_id { get; set; }
        public string venue_name { get; set; }
        public List<VenueAvailability> availability { get; set; }
    }
}
