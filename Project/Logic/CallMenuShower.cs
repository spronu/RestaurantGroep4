public static class  CallMenuShower{
        
        public static void ShowMenuCard(){
        
        bool coursecheck = true;
        bool done = true;
        string option = "";
        // done = menucardpresentasion.menucard();

        while(done){
            done = menucardpresentasion.menucard(coursecheck, MenuRecive.getdata());
            if(done){
            option = menucardpresentasion.GetNewoption();

            }
            coursecheck = false;
            // if (option == "return")
            // {
            //     done = false;
            // }
        }
        }
}