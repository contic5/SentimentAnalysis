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

public class HourModel : PageModel
{
    public List<HourData> HourDataArr {get; set;}
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

        int [] hourRanges=[5,10,20,50,100,250,500,999999999];

        HourDataArr=[];
        for(int i=0;i<hourRanges.Length-1;i++)
        {
            HourDataArr.Add(new HourData{
                Id=i+1,
                MinHours=hourRanges[i],
                MaxHours=hourRanges[i+1],
            });
        }
        foreach(var review in reviews)
        {
            int hoursPlayedAtReview=review.HoursPlayedAtReview;
            int index=HourDataArr.Count-1;
            for(int i=0;i<HourDataArr.Count;i++)
            {
                if(hoursPlayedAtReview<HourDataArr[i].MaxHours)
                {
                    index=i;
                    break;
                }
            }

            HourDataArr[index].Count+=1;
            if(review.Reccomendation==true)
            {
                HourDataArr[index].PositiveCount+=1;
            }
            else
            {
                HourDataArr[index].NegativeCount+=1;
            }
        }

        long end=DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        Console.WriteLine($"Completion Time: {end-start} ms");
    }
}
