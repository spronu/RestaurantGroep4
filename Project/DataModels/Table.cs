using System.Text.Json.Serialization;

public class Table
{
    [JsonPropertyName("tableId")]
    public int TableId { get; set; }

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }

    [JsonPropertyName("reservationDate")]
    public DateTime? ReservationDate { get; set; }

    [JsonPropertyName("reservationTime")]
    public DateTime? ReservationTime { get; set; }
}
