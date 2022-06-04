namespace Revix.Rate.Domain.Models;

public class Status {
    public DateTime Timestamp { get; set; }
    public int Error_code { get; set; }
    public string Error_message { get; set; }
    public int Elapsed { get; set; }
    public int Credit_count { get; set; }
}