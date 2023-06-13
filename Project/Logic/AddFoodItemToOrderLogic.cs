static class AddFoodItemToOrderLogic
{

    static ReservationLogic reservationlogics = new ReservationLogic();
    public static void ShowMenu(ReservationModel reservation)
    {
        List<MenuItems> jsonArray = MenuRecive.getdata();
        bool done = true;
        List<string> orderItems = new List<string>();
        List<int> orderItemIDs = new List<int>();
        double totalPrice = 0.0;

        while (done)
        {
            // Call the menu display method at the beginning of the loop
            bool checking = menucardpresentasion.menucard(true, jsonArray);

            string option = "x";
            if (checking)
            {
                option = OrderFoodPresentasion.AskOrder();
            }
            
            bool notFound = true;
            foreach (var item in jsonArray)
            {
                if (option == item.id.ToString())
                {

                    orderItemIDs.Add(Convert.ToInt32(item.id));
                    totalPrice += Convert.ToDouble(item.price);
                    OrderFoodPresentasion.ShowItem(item.name.ToString());

                    Thread.Sleep(1000);
                    notFound = false;

                    // Update the JSON immediately after an order is made.
                    reservationlogics.UpdateReservationJson(orderItemIDs, reservation);
                }
            }
            if (notFound && option != "x")
            {
                OrderFoodPresentasion.DishNotFound();
                Thread.Sleep(1000);
            }

            if (option == "x")
            {
                // Move the warning check here
                if (orderItemIDs.Count < reservation.NumberOfPeople)
                {

                    string response = OrderFoodPresentasion.DoneOrder();
                    if (response.ToLower() == "j")
                    {
                        done = true; // Only continue with the loop if there are more orders to be made and the user confirms they want to order more
                        continue;
                    }
                }

                // Finish the order if no more orders are to be made or the user doesn't want to continue ordering
                done = false;
            }
        }

        OrderFoodPresentasion.Orderfinished();
    }

}