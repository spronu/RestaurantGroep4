public static class AddNewFoodItemPresentation{
    public static int AskThemeForAdd(List<MenuTheme> jsonArray){
        // Display the options to the user
        Console.WriteLine("Kies een optie:");
        for (int i = 0; i < jsonArray.Count; i++)
        {
            var menuItem = jsonArray[i];
            Console.WriteLine($"{menuItem.Id}. {menuItem.Name}");
        }

        // Ask the user for the desired option
        Console.Write("voer het nummer in van het thema waar je een gerecht aan wilt toevoegen: ");
        int optionNumber = Convert.ToInt32(Console.ReadLine());
        return optionNumber;
    }
    public static string GetName(){
        Console.Clear();
        Console.WriteLine("geef de naam van het nieuwe gerecht:  ");
        string NAME = Console.ReadLine() ?? string.Empty;
        return NAME;
    }
    public static string GetCourse(Dictionary<string, string> course){
        Thread.Sleep(800);
        Console.Clear();
        Console.WriteLine("geef aan of de type maaltijd 'hoofdgerecht' of 'bijgerecht' is: ");
        foreach(var c in course){
            Console.WriteLine($"{c.Key}. {c.Value}");
        }
        string _course = Console.ReadLine() ?? string.Empty;
        string COURSE = course[_course];
        return COURSE;
    }
    public static string GetCategory(Dictionary<string, string> catogorie){
        Thread.Sleep(800);
        Console.Clear();
        Console.WriteLine("geef aan of de categorie 'vis', 'vlees', 'veganistisch', 'vegetarisch' is ");
         foreach(var c in catogorie){
            Console.WriteLine($"{c.Key}. {c.Value}");
        }
        string _category = Console.ReadLine() ?? string.Empty;
        string CATEGORY = catogorie[_category];
        return CATEGORY;
    }
    public static string GetPrice(){
        Thread.Sleep(800);
        Console.Clear();
        Console.WriteLine("geef de prijs van de maaltijd ");
        string prijs = Console.ReadLine() ?? string.Empty;
        return prijs;
    }

    public static void FinischedNewItem(){
        Console.WriteLine("het gerecht is succesvol toegevoegd aan de menukaart!");

    }
}