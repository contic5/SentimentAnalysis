using System.ComponentModel.DataAnnotations;

namespace SentimentAnalysis.Models;

public class Character
{
    public int Id { get; set; }

    public string Name {get; set;}

    public int Count {get; set;}
    public int PositiveCount {get; set;}
}