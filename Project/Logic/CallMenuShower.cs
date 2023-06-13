public static class CallMenuShower
{
    public static void ShowMenuCard()
    {
        bool coursecheck = true;
        bool done = true;
        string option = "";

        while (done)
        {
            done = menucardpresentasion.menucard(coursecheck, MenuRecive.getdata());
            if (done)
            {
                option = menucardpresentasion.GetNewoption();
            }
            coursecheck = false;
        }
    }
}
