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
    public void OnGet()
    {
        string filePath = "Data/elden_ring_reviews.xlsx"; // Replace with your actual file path

        // This single line reads the sheet and binds it to your model list
        IEnumerable<Review> reviews = MiniExcel.Query<Review>(filePath);

        foreach (var review in reviews)
        {
            Console.WriteLine($"{review.Id}");
        }
    }
}
