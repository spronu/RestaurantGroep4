using System.Text.Json.Serialization;

public class ReservationModel
{

    [JsonPropertyName("reservationId")]
    public Guid ReservationId { get; set; }

    [JsonPropertyName("orderItemIDs")]
    public List<int> OrderItemIDs { get; set; }

    [JsonPropertyName("totalPrice")]
    public double TotalPrice { get; set; }

    [JsonPropertyName("accountId")]
    public int AccountId { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName {get; set;}

    [JsonPropertyName("tableId")]
    public int TableId { get; set; }

    [JsonPropertyName("reservationDateTime")]
    public DateTime ReservationDateTime { get; set; }

    [JsonPropertyName("numberOfPeople")]
    public int NumberOfPeople { get; set; }

    public ReservationModel(int accountId, string fullName, int tableId, int numberOfPeople, DateTime reservationDateTime)
    {
        ReservationId = Guid.NewGuid();
        AccountId = accountId;
        FullName = fullName;
        TableId = tableId;
        NumberOfPeople = numberOfPeople;
        ReservationDateTime = reservationDateTime;
        OrderItemIDs = new List<int>();
    }

    public ReservationModel(){ }
}
