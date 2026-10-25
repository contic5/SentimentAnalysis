using System.ComponentModel.DataAnnotations;

namespace SentimentAnalysis.Models;

public class Review
{
    public int Id { get; set; }
    public string? ReviewText { get; set; }

    [DataType(DataType.Date)]
    public DateTime ReviewDate { get; set; }
    
    public decimal HoursPlayed { get; set; }

    public int HoursPlayedAtReview {get; set;}

    public int Helpful { get; set; }
    public int Funny { get; set; }

    public bool? Reccomendation {get; set;}

    public Review()
    {
        
    }
}