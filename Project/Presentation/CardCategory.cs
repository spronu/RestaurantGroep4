public class CardCategory : MenuLogic
{
    public string returnedOption = "";

    public CardCategory(List<String> Elements, int pos) : base(Elements, pos) { }

    public override void Logics(string title)
    {
        bool selecting = true;
        PrintOptions(pos, title);
        while (selecting)
        {
            ConsoleKeyInfo input = Console.ReadKey(true);
            Selection(input, title);

            if (input.Key == ConsoleKey.Enter)
            {
                if (pos == 0)
                {
                    returnedOption = "vis";
                    selecting = false;
                }
                else if (pos == 1)
                {
                    returnedOption = "vlees";
                    selecting = false;
                }
                else if (pos == 2)
                {
                    returnedOption = "veganistisch";
                    selecting = false;
                }
                else if (pos == 3)
                {
                    returnedOption = "vegetarisch";
                    selecting = false;
                }
                else if (pos == 4)
                {
                    returnedOption = "return";
                    Menu.Start();
                }
            }
        }
    }
}