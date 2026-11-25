using dotenv.net;
using Microsoft.AspNetCore.Mvc;
using tournament_scheduler.DatabaseLayer;
using tournament_scheduler.Models;

namespace tournament_scheduler.Controllers
{
    [ApiController]
    [Route("tournaments")]
    public class TournamentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Db _db;

        TournamentController(IConfiguration configuration)
        {
            _configuration = configuration;

            _db = new Db(_configuration);
        }
        
        [HttpGet]
        [Route("{tournament_id}/venues")]
        public ObjectResult GetVenues([FromRoute] int tournament_id)
        {
            var response = new GetVenuesResponse();

            // Load .env from the application directory into the system environment
            DotEnv.Load();

            // Read variables from .env without modifying the system environment
            var envVars = DotEnv.Read();

            Console.WriteLine(envVars["DefaultConnection"]);

            return StatusCode(200, response);
        }

        [HttpGet]
        [Route("{tournament_id}/info")]
        public async Task<ObjectResult> GetTournamentInfo([FromRoute] int tournament_id)
        {
            //var tournamentInfo = await _db.GetTournamentInfo();

            var tournamentInfo = new TournamentInfo(0, "Filler data", new DateTime(), new DateTime(), new List<ScoringRule>());

            return StatusCode(200, tournamentInfo);
        }

        [HttpGet]
        [Route("{tournament_id}/teams")]
        public ObjectResult GetTournamentTeams([FromRoute] int tournament_id)
        {
            var response = new GetTournamentTeamsResponse();

            return StatusCode(200, response);
        }

        [HttpPost]
        [Route("{tournament_id}/schedule")]
        public ObjectResult CreateSchedule([FromRoute] int tournament_id, [FromBody] CreateTournamentRequest tournamentDetails)
        {
            IList<(int, int)> teamCombinations = new List<(int, int)>(); // tuple of team IDs for each match
            for (int i = 0; i < tournamentDetails.Team_ids.Count(); i++)
            {
                for (int j = i + 1; j < tournamentDetails.Team_ids.Count(); j++) {
                    //Add combination
                    teamCombinations.Add((tournamentDetails.Team_ids.ElementAt(i), tournamentDetails.Team_ids.ElementAt(j)));
                }
            }

            var validationError = Validation(tournament_id, tournamentDetails);
            if (!validationError.Valid)
            {
                return StatusCode(400, validationError);
            }

            var validCombination = new List<int>();

            var tournament = new CreateTournamentResponse();

            return StatusCode(201, tournament);
        }

        private ErrorDto Validation(int tournament_id, CreateTournamentRequest tournamentDetails)
        {
            ErrorDto errorDto = new ErrorDto();
            if (tournamentDetails == null)
            {
                errorDto.ErrorMessage = "Invalid request body";
                errorDto.Valid = false;
                return errorDto;
            }

            if (tournamentDetails.Team_ids == null || tournamentDetails.Team_ids.Count() == 0)
            {
                errorDto.ErrorMessage = "No team IDs provided";
                errorDto.Valid = false;
                return errorDto;
            }

            if (tournamentDetails.Team_ids.Count() < 2)
            {
                errorDto.ErrorMessage = "At least two team IDs are required";
                errorDto.Valid = false;
                return errorDto;
            }

            if (tournamentDetails.Venue_ids == null || tournamentDetails.Venue_ids.Count() == 0)
            {
                errorDto.ErrorMessage = "No venue IDs provided";
                errorDto.Valid = false;
                return errorDto;
            }

            var i = 0;
            foreach (var team_id in tournamentDetails.Team_ids)
            {
                if (team_id <= 0)
                {
                    errorDto.ErrorMessage = $"Error reading team id {i}: Invalid team ID";
                    errorDto.Valid = false;
                    return errorDto;
                }
                i++;
            }

            if (tournamentDetails.Schedule_settings.format != "round_robin")
            {
                errorDto.ErrorMessage = "Invalid schedule format. Only round_robin is supported.";
                errorDto.Valid = false;
                return errorDto;
            }

            //EnoughVenueTime(tournamentDetails);

            errorDto.Valid = true;
            return errorDto;
        }
        /*
        private bool EnoughVenueTime(CreateTournamentRequest tournamentDetails)
        {
            int tournament_id = 1; // Default

            List<int> numEligibleSpots = new List<int>();

            var venueIndex = 0;
            foreach (var venue in venues)
            {

                numEligibleSpots.Add(0);
                venueIndex++;
            }

            foreach (var venue in venues)
            {
                var eligibleVenueAvailability = numEligibleSpots.ElementAt(venueIndex);
                foreach (var slot in availability)
                {
                    if (tournamentDetails.Schedule_settings.match_duration_minutes > slot.available_until - slot.available_from)
                    {
                        return false;
                    }
                    else
                    {
                        eligibleVenueAvailability++;
                    }
                }
                venueIndex++;
            }

            // Placeholder for venue time validation logic
            return true;
        }
        */
    }
}
