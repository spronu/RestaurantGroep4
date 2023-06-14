class ChangeThemeOrder
{
    public static void ChangeOrder()
    {
        Console.Clear();

        // GiveThemeLogic.Givename(GiveThemeLogic.NumbersLogic());
        //  CallMenuPresentation.hoofd();
        List<MenuTheme> jsonArray = GetThemes.getheme();

        // Deserialize the JSON array into a list of objects
        List<ThemeItem> themes = GetThemes.gethemeNumber();

        // Get user input for the month number
        int monthNumber = ChangeThemeOrderPresentation.GetMonthForChange();

        // Check if the entered month number is valid
        if (monthNumber < 1 || monthNumber > 12)
        {
            ChangeThemeOrderPresentation.NoCorrectMonthMessage();
        }

        // Get user input for the new theme option

        int themeOption = ChangeThemeOrderPresentation.ShowThemeOptions(jsonArray);

        // Validate the theme option number
        if (themeOption < 0 || themeOption >= (jsonArray.Count + 1))
        {
            ChangeThemeOrderPresentation.NoCorrectThemeNumber();
        }

        string selectedTheme = "";

        // Map the theme option number to the actual theme name
        for (int i = 0; i < jsonArray.Count; i++)
        {
            var menuItem = jsonArray[i];
            if (menuItem.Id == themeOption)
            {
                selectedTheme = menuItem.Name;
            }
        }

        // Find the corresponding theme item in the list and update the theme
        ThemeItem selectedMonthTheme = themes.FirstOrDefault(t => t.Month == monthNumber);
        if (selectedMonthTheme != null)
        {
            selectedMonthTheme.Theme = selectedTheme;
            Console.Clear();
        }

        // Convert the updated list back to JSON

        // Display the updated JSON
        ChangeThemeOrderData.WriteToJson(themes);
        ChangeThemeOrderPresentation.UpdateSucsesvol();
    }
}
