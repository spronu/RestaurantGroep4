public static class ShowThemeDates{
    public static void Showthemes(){
        Console.Clear();
        Dictionary<string, string>couple = MakeDateList.reciveInfo();
        foreach (KeyValuePair<string, string> numbers in couple)
                {
                    string key = numbers.Key;
                    string value = numbers.Value;
                    Console.WriteLine($"{key}'s thema is {value}");
                }
    }
}