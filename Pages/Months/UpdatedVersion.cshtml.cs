//Install-Package CsvHelper
using MiniExcelLibs;
using CsvHelper;

using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace SentimentAnalysis.Pages;
using SentimentAnalysis.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;

/*
public class MonthModel : PageModel
{
    public List<MonthData> MonthDataArr {get; set;} = [];
    public IEnumerable<Review>? reviews {get; set;}

    public void OnGet(int page=0)
    {
        long start=DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        string filePath = "Data/df_eldenRing_very_short.xlsx"; // Replace with your actual file path
        if(reviews==null||reviews.Count()==0)
        {
            Console.WriteLine("Accessing reviews");
            // This single line reads the sheet and binds it to your model list
            reviews = MiniExcel.Query<Review>(filePath);
        }

        reviews = reviews.OrderBy(review => review.ReviewDate).ToList();
        MonthDataArr = reviews
            .GroupBy(review => new DateTime(review.ReviewDate.Year, review.ReviewDate.Month, 1))
            .Select((monthReviews, index) => new MonthData
            {
                Id = index + 1,
                Month = monthReviews.Key,
                Count = monthReviews.Count(),
                PositiveCount = monthReviews.Count(review => review.Reccomendation == true),
                NegativeCount = monthReviews.Count(review => review.Reccomendation != true)
            })
            .ToList();

        long end=DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        Console.WriteLine($"Completion Time: {end-start} ms");
    }
}
*/