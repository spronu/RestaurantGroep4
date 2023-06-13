using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        Console.Write("Geef het nummer van de maand die je wilt aanpassen: ");
        int monthNumber = Convert.ToInt32(Console.ReadLine());

        // Check if the entered month number is valid
        if (monthNumber < 1 || monthNumber > 12)
        {
            Console.WriteLine("Geen geldig nummer.");
        }

        // Get user input for the new theme option
        for (int i = 0; i < jsonArray.Count; i++)
            {
                var menuItem = jsonArray[i];
                Console.WriteLine($"{menuItem.Id}. {menuItem.Name}");
            }
        int themeOption = Convert.ToInt32(Console.ReadLine());

        // Validate the theme option number
        if (themeOption < 0 || themeOption >= (jsonArray.Count + 1))
        {
            Console.WriteLine("Geen geldig Thema nummer.");
        }

        string selectedTheme = "";

        // Map the theme option number to the actual theme name
        for (int i = 0; i < jsonArray.Count; i++)
            {
                var menuItem = jsonArray[i];
                if(menuItem.Id == themeOption){
                    selectedTheme = menuItem.Name;
                }
            }

        // Find the corresponding theme item in the list and update the theme
        ThemeItem selectedMonthTheme = themes.FirstOrDefault(t => t.Month == monthNumber);
        if (selectedMonthTheme != null)
        {
            selectedMonthTheme.Theme = selectedTheme;
            Console.Clear();

            Console.WriteLine("Thema sucsessvol aangepast.");
        }
        else
        {
            Console.WriteLine("Geen thema gevonden voor dit nummer.");
        }

        // Convert the updated list back to JSON
        string updatedJson = JsonConvert.SerializeObject(themes, Formatting.Indented);

        // Display the updated JSON
        ChangeThemeOrderData.WriteToJson(updatedJson);
    }

}

