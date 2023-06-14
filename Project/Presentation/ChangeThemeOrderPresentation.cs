public static class ChangeThemeOrderPresentation
{
    public static int GetMonthForChange()
    {
        Console.Write("Geef het nummer van de maand die je wilt aanpassen: ");
        int monthNumber = Convert.ToInt32(Console.ReadLine());
        return monthNumber;
    }

    public static void NoCorrectMonthMessage()
    {
        Console.WriteLine("Geen geldig nummer.");
    }

    public static int ShowThemeOptions(List<MenuTheme> jsonArray)
    {
        for (int i = 0; i < jsonArray.Count; i++)
        {
            var menuItem = jsonArray[i];
            Console.WriteLine($"{menuItem.Id}. {menuItem.Name}");
        }
        int themeOption = Convert.ToInt32(Console.ReadLine());
        return themeOption;
    }

    public static void NoCorrectThemeNumber()
    {
        Console.WriteLine("Geen geldig Thema nummer.");
    }

    public static void UpdateSucsesvol()
    {
        Console.WriteLine("Thema sucsessvol aangepast.");
    }
}
