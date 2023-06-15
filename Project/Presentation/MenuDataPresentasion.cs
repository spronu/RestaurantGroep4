public static class MenuDataPresentasion
{
    public static int ShowCourses(Dictionary<int, string> courselink, string message)
    {
        Console.Clear();
        List<string> elements = new List<string>();
        foreach (KeyValuePair<int, string> entry in courselink)
        {
            int key = entry.Key;
            string value = entry.Value;
            elements.Add(value);
        }

        MenuLogic choosing = new MenuLogic(elements);

        choosing.PrintOptions(0, message);
        bool currently = true;
        while (currently)
        {
            ConsoleKeyInfo input = Console.ReadKey(true);
            choosing.Selection(input, message);

            if (input.Key == ConsoleKey.Enter)
            {
                return choosing.pos + 1;
            }
        }
        return 0;
    }
}
