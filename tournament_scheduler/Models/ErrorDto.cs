namespace tournament_scheduler.Models
{
    public class ErrorDto
    {
        public ErrorDto()
        {
            ErrorMessage = string.Empty;
            Valid = true;
        }

        public string ErrorMessage { get; set; }
        public bool Valid { get; set; }
    }
}