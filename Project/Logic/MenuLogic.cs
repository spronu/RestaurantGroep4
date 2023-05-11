public class MenuLogic
{

    protected List<String> Elements = new List<string>();

    public int pos;

    public MenuLogic(List<String> Elements, int pos)
    {
        this.Elements = Elements;
        pos = 0;
    }

    public virtual void Logics(string title)
    {

    }

    public void Selection(ConsoleKeyInfo input, string title)
    {
        if (input.Key == ConsoleKey.UpArrow)
        {
            pos--;
            if (pos < 0) pos = Elements.Count - 1;
            PrintOptions(pos, title);

        }
        else if (input.Key == ConsoleKey.DownArrow)
        {
            pos++;
            if (pos > Elements.Count - 1) pos = 0;
            PrintOptions(pos, title);
        }
    }

    public void PrintOptions(int pos, string title)
    {
        Console.Clear();
        Console.WriteLine(title);
        Console.WriteLine("kies een optie");
        foreach (string str in Elements)
        {
            Console.WriteLine(Mark(str, pos));
            Console.ResetColor();
        }
    }

    public string Mark(string str, int pos)
    {
        if (Elements[pos] == str)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        return str;
    }

}
