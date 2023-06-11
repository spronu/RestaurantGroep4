public static class MenuDataPresentasion{
    public static int ShowCourses(Dictionary<int, string> courselink){
        Console.Clear();
        foreach (KeyValuePair<int, string> entry in courselink)
        {
            int key = entry.Key;
            string value = entry.Value;
            Console.WriteLine($"{key}. {value}");
        }
        Console.WriteLine("welke type maaltijd wilt u?");
        Console.WriteLine("");
        string coursesave = Console.ReadLine() ?? string.Empty;
        int save = Int32.Parse(coursesave);
        return save;
    }
    public static int Showcatogeries(Dictionary<int, string> catogorieslink){
            Console.Clear();
            foreach (KeyValuePair<int, string> entry in catogorieslink)
            {
                int key = entry.Key;
                string value = entry.Value;
                Console.WriteLine($"{key}. {value}");
            }
            Console.WriteLine("welke categorie wilt u?");
            Console.WriteLine("");
            string catogoriesave = Console.ReadLine() ?? string.Empty;
            int save2 = Int32.Parse(catogoriesave);
            Console.Clear();
            return save2;

    }
}