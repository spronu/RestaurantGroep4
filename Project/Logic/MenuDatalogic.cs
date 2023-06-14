static class MenuDataLogic
{
    public static string course = "";
    public static bool courseoption = true;

    public static Tuple<string, string> MakeOptionsLists(bool ShowCourse)
    {
        bool choise = true;
        courseoption = ShowCourse;
        string option = "";

        List<string> Allallcatogories = new List<string>();
        List<string> Allallcourses = new List<string>();
        List<MenuItems> ListmenuItems = MenuRecive.getdata();
        foreach (MenuItems item in ListmenuItems)
        {
            Allallcatogories.Add($"{item.category}");
            Allallcourses.Add($"{item.course}");
        }

        List<string> allcatogories = Allallcatogories.Distinct().ToList();
        List<string> allcourses = Allallcourses.Distinct().ToList();
        Dictionary<int, string> catogorieslink = new Dictionary<int, string>();
        Dictionary<int, string> courselink = new Dictionary<int, string>();

        allcatogories.Add("terug");
        allcourses.Add("terug");
        // maken van opties 1 tot eind linken aan keuze
        int i = 1;
        foreach (string item in allcourses)
        {
            courselink.Add(i, item);
            i++;
        }
        int y = 1;
        foreach (string item in allcatogories)
        {
            catogorieslink.Add(y, item);
            y++;
        }

        while (courseoption || choise)
        {
            while (courseoption)
            {
                int save = MenuDataPresentasion.ShowCourses(
                    courselink,
                    "welke type maaltijd wilt u? \n"
                );
                course = courselink[save];
                if (course == "return")
                {
                    course = string.Empty;
                    courseoption = false;
                    choise = false;
                    Tuple<string, string> v = Tuple.Create("quit", "quit");
                    return v;
                }
                if (allcourses.Contains(course))
                {
                    courseoption = false;
                    choise = true;
                }
                else
                {
                    if (course == "return")
                    {
                        course = string.Empty;
                        courseoption = false;
                        Tuple<string, string> v = Tuple.Create("quit", "quit");
                        return v;
                    }
                }
            }

            while (choise)
            {
                int save2 = MenuDataPresentasion.ShowCourses(
                    catogorieslink,
                    "welke categorie wilt u? \n"
                );
                option = catogorieslink[save2];
                if (allcatogories.Contains(option))
                {
                    choise = false;
                    if (option == "return")
                    {
                        courseoption = true;
                    }
                }
            }
        }
        Tuple<string, string> x = Tuple.Create(option, course);
        return x;
    }
}
