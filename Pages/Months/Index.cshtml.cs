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

public class MonthModel : PageModel
{
    public List<MonthData> MonthDataArr {get; set;}
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

        reviews=reviews.OrderBy(review=>review.ReviewDate);
        MonthDataArr=[];
    
        Console.WriteLine(reviews.GetType().FullName);
        Review firstReview=reviews.First();
        DateTime previousMonth=new DateTime(firstReview.ReviewDate.Year,firstReview.ReviewDate.Month,1);
        MonthData currentMonthData = new MonthData
        {
            Month = previousMonth,
            Id=1,
        };

        int curId=2;
        foreach (var review in reviews)
        {
            DateTime currentMonth=new DateTime(review.ReviewDate.Year,review.ReviewDate.Month,1);
            if(currentMonth!=previousMonth)
            {
                Console.WriteLine(previousMonth);
                MonthDataArr.Add(currentMonthData);
                currentMonthData = new MonthData
                {
                    Month=previousMonth,
                    Id=curId
                };
                curId+=1;

                previousMonth=currentMonth;
            }

            currentMonthData.Count+=1;
            if(review.Reccomendation==true)
            {
                currentMonthData.PositiveCount+=1;
            }
            else
            {
                currentMonthData.NegativeCount+=1;
            }
        }
        Console.WriteLine(previousMonth);

        MonthDataArr.Append(currentMonthData);
        Console.WriteLine(MonthDataArr.Count);

        long end=DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        Console.WriteLine($"Completion Time: {end-start} ms");
    }
}
