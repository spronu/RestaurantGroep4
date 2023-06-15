using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class AddNewFoodItemLogic
{
    public static void givenames()
    {
        List<MenuTheme> jsonArray = GetThemes.getheme();
        int optionNumber = AddNewFoodItemPresentation.AskThemeForAdd( jsonArray);
        MenuTheme data = jsonArray.FirstOrDefault(j => (int)j.Id == optionNumber);
        string THEME = data?.Name?.ToString();

        var jsonObject = jsonArray.FirstOrDefault(j => (int)j.Id == optionNumber);
        string JsonName = jsonObject?.Json?.ToString();

         Dictionary<string, string> course = new Dictionary<string, string>();
         course.Add("1", "hoofdgerecht");
         course.Add("2", "bijgerecht");
         Dictionary<string, string> catogorie = new Dictionary<string, string>();
         catogorie.Add("1", "vis");
         catogorie.Add("2", "vlees");
         catogorie.Add("3", "veganistisch");
         catogorie.Add("4", "vegetarisch");

        Thread.Sleep(800);

        string NAME = AddNewFoodItemPresentation.GetName();
        string COURSE = AddNewFoodItemPresentation.GetCourse(course);
        string CATEGORY = AddNewFoodItemPresentation.GetCategory(catogorie);

        string prijs = AddNewFoodItemPresentation.GetPrice();
        // Parse the input to double
        if (double.TryParse(prijs, out double PRICE))
        {
        }

        AddNewFoodItemDataAccess.AddItemJson(NAME, COURSE, CATEGORY, PRICE, THEME);
        AddNewFoodItemPresentation.FinischedNewItem();
     
    }
}