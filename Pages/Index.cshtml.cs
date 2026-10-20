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

public class IndexModel : PageModel
{
    public IEnumerable<Review>? reviews {get; set;}
    public IEnumerable<Review>? reviewsToDisplay {get; set;}

    public void OnGet(int page=0)
    {
        string filePath = "Data/elden_ring_reviews.xlsx"; // Replace with your actual file path

        // This single line reads the sheet and binds it to your model list
        reviews = MiniExcel.Query<Review>(filePath);
        reviewsToDisplay=reviews.Skip(10*page).Take(10);
        foreach (var review in reviewsToDisplay)
        {
            Console.WriteLine($"{review.ReviewText}");
        }
    }
}
