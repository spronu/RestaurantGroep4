using System.Text.Json.Serialization;

public class ReservationModel
{
    // [JsonPropertyName("id")]
    // public int Id { get; set; }

    [JsonPropertyName("reservationId")]
    public Guid ReservationId { get; set; }

    [JsonPropertyName("orders")]
    public List<string> Orders { get; set; }

    [JsonPropertyName("accountId")]
    public int AccountId { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName {get; set;}

    [JsonPropertyName("tableId")]
    public int TableId { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("numberOfPeople")]
    public int NumberOfPeople { get; set; }

    public ReservationModel(int accountId, string fullName, int tableId, int numberOfPeople, DateTime date, DateTime time)
    {
        ReservationId = Guid.NewGuid();
        AccountId = accountId;
        FullName = fullName;
        TableId = tableId;
        NumberOfPeople = numberOfPeople;
        Date = date;
        Time = time;
    }

    public ReservationModel()
    {
    }
}