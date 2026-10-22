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

public class MonthModel : PageModel
{
    public IEnumerable<Review>? reviews {get; set;}
    public Character[] characters {get; set;}

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

        if(characters==null||characters.Count()==0)
        {
            string[] charactersNames = System.IO.File.ReadAllLines("Data/characters.txt");
            characters=new Character[charactersNames.Length];

            //Tracks which characters have been added
            OrderedDictionary<string,bool> charactersAdded=new OrderedDictionary<string, bool>();

            //Initialize each character
            for(int i=0;i<charactersNames.Length;i++)
            {
                string characterName=charactersNames[i];

                //Initialize each dictionary key
                charactersAdded[characterName]=true;

                characters[i]=new Character();
                characters[i].Name=characterName;
                characters[i].Id=i+1;
                
            }

            //Loop through each review. Previous method was O(r*c) (Total reviews*total characters). 
            // It is now O(r) (Total reviews)

            int reviewIndex=1;
            foreach(var review in reviews)
            {
                if((reviewIndex-1)%1000==0)
                {
                    Console.WriteLine($"Review {reviewIndex-1}/{reviews.Count()}");
                }
                reviewIndex+=1;

                if(review.ReviewText==null)
                {
                    continue;
                }

                //Gets each word in the review
                string[] words=review.ReviewText.Split(" ");
    
                foreach(var word in words)
                {
                    int index=charactersAdded.IndexOf(word);
                    if(index!=-1&&!charactersAdded[word])
                    {
                        //Mark that character was added
                        charactersAdded[word]=true;
                        characters[index].Count+=1;
                        if(review.Reccomendation==true)
                        {
                            characters[index].PositiveCount+=1;
                        }
                        else
                        {
                            characters[index].NegativeCount+=1;
                        }
                    }
                }

                //Loop using Ordered Dictionary Key
                foreach(string characterName in charactersAdded.Keys)
                {
                    charactersAdded[characterName]=false;
                }
            }
        }
        Console.WriteLine($"Total Characters {characters.Count()}");

        long end=DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        Console.WriteLine($"Completion Time: {end-start} ms");
    }
}
