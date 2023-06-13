public class AdminMenu : MenuLogic
{
    public AdminMenu(List<String> Elements, int pos) : base(Elements, pos) { }

    public override void Logics(string title)
    {
        bool selecting = true;
        PrintOptions(pos, title);
        while (selecting)
        {
            PrintOptions(pos, title);
            ConsoleKeyInfo input = Console.ReadKey(true);
            Selection(input, title);

            if (input.Key == ConsoleKey.Enter)
            {
                if (pos == 0)
                {
                    // deze is voor nieuw ding toeoegen pas volgende sprint goed aan
                    // AddNewFoodItem.givenames();
                    ShowThemeDates.Showthemes();
                    selecting = false;
                }
                else if (pos == 1)
                {
                    AddNewFoodItemLogic.givenames();
                    selecting = false;
                }
                else if (pos == 2)
                {
                    ChangeThemeOrder.ChangeOrder();
                    selecting = false;
                }
                else if (pos == 3)
                {
                    RemoveFoodItemJsonDataLogic.RemoveChosenItems();
                    selecting = false;
                }
                else if (pos == 4)
                {
                    ShowAllUsers.Show();
                    selecting = false;
                }
                else if (pos == 5)
                {
                    AdminsInfo adminsInfo = new AdminsInfo();

                    while (!adminsInfo.currentStatus)
                    {
                    adminsInfo.ShowReservationInfo();
                    }
                    // selecting = false;
                }
                else if (pos == 6)
                {
                    Menu.Start();
                    selecting = false;
                }

            }
        }
    }
}