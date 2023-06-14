public class MenuLogic
{
    protected List<String> Elements = new List<string>();

    public int pos;

    public MenuLogic(List<String> Elements, int pos)
    {
        this.Elements = Elements;
        pos = pos;
    }

    public MenuLogic(List<String> Elements)
    {
        this.Elements = Elements;
        pos = 0;
    }

    public virtual void Logics(string title) { }

    public void Selection(ConsoleKeyInfo input, string title)
    {
        if (input.Key == ConsoleKey.UpArrow)
        {
            pos--;
            if (pos < 0)
                pos = Elements.Count - 1;
            PrintOptions(pos, title);
        }
        else if (input.Key == ConsoleKey.DownArrow)
        {
            pos++;
            if (pos > Elements.Count - 1)
                pos = 0;
            PrintOptions(pos, title);
        }
    }

    public void PrintOptions(int pos, string title)
    {
        OptionPrint.FullyPrint(title, Elements, pos);
    }

    public static List<string> ShowingMenuOptions()
    {
        List<String> items = new List<String>();
        if (AccountsLogic.CurrentAccount != null && AccountsLogic.CurrentAccount.Admin == false)
        {
            items.Add("Welkom " + AccountsLogic.CurrentAccount.FullName);
            items.Add("Start reservering");
            items.Add("Bekijk menukaart");
            items.Add("Bekijk reservering info");
            items.Add("Bekijk account info");
            items.Add("Informatie over Restaurant");
            items.Add("Uitloggen");
            items.Add("Afsluiten");
        }
        else if (AccountsLogic.CurrentAccount != null && AccountsLogic.CurrentAccount.Admin == true)
        {
            items.Add("Welkom " + AccountsLogic.CurrentAccount.FullName);
            items.Add("Bekijk menukaart");
            items.Add("Beheercentrum");
            items.Add("Uitloggen");
            items.Add("Afsluiten");
        }
        else
        {
            items.Add("Niet ingelogd");
            items.Add("Start reservering");
            items.Add("Log in");
            items.Add("Registreer");
            items.Add("Bekijk menukaart");
            items.Add("Informatie over Restaurant");
            items.Add("Afsluiten");
        }
        return items;
    }
}
