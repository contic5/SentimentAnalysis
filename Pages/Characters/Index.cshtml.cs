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

public class CharacterModel : PageModel
{
    public IEnumerable<Review>? reviews {get; set;}
    public IEnumerable<Review>? reviewsToDisplay {get; set;}

    public IEnumerable<Character> characters {get; set;}

    public void OnGet(int page=0)
    {
        
        string filePath = "Data/elden_ring_reviews.xlsx"; // Replace with your actual file path
        if(reviews==null||reviews.Count()==0)
        {
            Console.WriteLine("Accessing reviews");
            // This single line reads the sheet and binds it to your model list
            reviews = MiniExcel.Query<Review>(filePath);
        }

        if(characters==null||characters.Count()==0)
        {
            characters=[];
            string[] charactersNames = System.IO.File.ReadAllLines("Data/characters.txt");
            for(int i=0;i<charactersNames.Length;i++)
            {
                string characterName=charactersNames[i];
                Console.WriteLine($"Adding {characterName}");

                Character character=new Character();
                character.Name=characterName;
                character.Id=i+1;
                foreach(var review in reviews)
                {
                    if(review.ReviewText!=null&&review.ReviewText.Contains(character.Name))
                    {
                        character.Count+=1;
                        if(review.Reccomendation=="Recommended")
                        {
                            character.PositiveCount+=1;
                        }
                    }
                }
                characters=characters.Append(character);
            }
        }
        Console.WriteLine($"Total Characters {characters.Count()}");

        reviewsToDisplay=reviews.Skip(10*page).Take(10);
        foreach (var review in reviewsToDisplay)
        {
            //Console.WriteLine($"{review.ReviewText}");
        }
    }
}
