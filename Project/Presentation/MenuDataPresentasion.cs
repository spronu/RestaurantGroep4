public static class MenuDataPresentasion{
    public static int ShowCourses(Dictionary<int, string> courselink, string message){
        Console.Clear();
        List<string> elements = new List<string>();
        foreach (KeyValuePair<int, string> entry in courselink)
        {
            int key = entry.Key;
            string value = entry.Value;
            elements.Add(value);
            // Console.WriteLine($"{key}. {value}");
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
        // Console.WriteLine("welke type maaltijd wilt u?");
        // Console.WriteLine("");
        // string coursesave = Console.ReadLine() ?? string.Empty;
        // int save = Int32.Parse(coursesave);
        return 0;
    }

    // public static int Showcatogeries(Dictionary<int, string> catogorieslink)
    // {
    //         Console.Clear();
    //         foreach (KeyValuePair<int, string> entry in catogorieslink)
    //         {
    //             int key = entry.Key;
    //             string value = entry.Value;
    //             Console.WriteLine($"{key}. {value}");
    //         }
    //         Console.WriteLine("welke categorie wilt u?");
    //         Console.WriteLine("");
    //         string catogoriesave = Console.ReadLine() ?? string.Empty;
    //         int save2 = Int32.Parse(catogoriesave);
    //         Console.Clear();
    //         return save2;

    // }
}