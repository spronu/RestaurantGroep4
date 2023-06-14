public static class OptionPrint
{
    public static void FullyPrint(string title, List<string> Elements, int pos)
    {
        Console.Clear();
        Console.WriteLine(title);
        foreach (string str in Elements)
        {
            Console.WriteLine(Mark(str, pos, Elements));
            Console.ResetColor();
        }
    }

    public static string Mark(string str, int pos, List<string> Elements)
    {
        if (Elements[pos] == str)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        return str;
    }
}
