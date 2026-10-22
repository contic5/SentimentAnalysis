using System.ComponentModel.DataAnnotations;

namespace SentimentAnalysis.Models;

public class MonthData
{
    public int Id { get; set; }

    [DataType(DataType.Date)]
    public DateTime Month {get; set;}

    public int Count {get; set;}
    public int PositiveCount {get; set;}

    public int NegativeCount {get; set;}
}