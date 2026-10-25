using System.ComponentModel.DataAnnotations;

namespace SentimentAnalysis.Models;

public class HourData
{
    public int Id { get; set; }

    public int MinHours {get; set;}

    public int MaxHours {get; set;}

    public int Count {get; set;}
    public int PositiveCount {get; set;}

    public int NegativeCount {get; set;}
}