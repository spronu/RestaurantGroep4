public class AdminMenu : MenuLogic
{
    public string returnedOption = "";

    public AdminMenu(List<String> Elements, int pos) : base(Elements, pos) { }

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

                    // deze is voor nieuw ding toeoegen pas volgende sprint goed aan
                    // AddNewFoodItem.givenames();
                    ChangeTheme.ChangeIt();
                    selecting = false;
                }
                else if (pos == 1)
                {
                    ShowAllUsers.Show();
                    selecting = false;
                }
                else if (pos == 2)
                {
                    Console.WriteLine("Nog niet gemaakt");
                    selecting = false;
                }
                else if (pos == 3)
                {
                    Menu.Start();
                    selecting = false;
                }

            }
        }
    }
}