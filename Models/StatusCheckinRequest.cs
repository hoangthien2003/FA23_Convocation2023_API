namespace FA23_Convocation2023_API.Models
{
    public class StatusCheckinRequest
    {
        public string HallName {  get; set; }
        public int SessionNum {  get; set; }
        public bool Status {  get; set; }
    }
}
