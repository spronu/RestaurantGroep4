using Newtonsoft.Json;

public static class AddNewFoodItemDataAccess
{
    public static void AddItemJson(string NAME, string COURSE, string CATEGORY, double PRICE)
    {
        // Read existing JSON data from the file
        string filePath = $"DataSources/MenuItems.json";
        string existingJson = File.ReadAllText(filePath);

        // Deserialize existing JSON data into a list of MenuItems objects
        List<MenuItems> existingMenuItems = JsonConvert.DeserializeObject<List<MenuItems>>(
            existingJson
        );

        int ID = existingMenuItems.Count();
        ID++;

        MenuItems newMenuItem = new MenuItems
        {
            id = ID,
            name = NAME,
            course = COURSE,
            category = CATEGORY,
            price = PRICE
        };

        // Add the new MenuItem to the existing list
        existingMenuItems.Add(newMenuItem);

        // Serialize the updated list of MenuItems back to JSON
        string updatedJson = JsonConvert.SerializeObject(existingMenuItems);

        // Write the updated JSON back to the file
        File.WriteAllText(filePath, updatedJson);
    }
}
