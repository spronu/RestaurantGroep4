using System.Text.Json.Serialization;

public class Table
{
    [JsonPropertyName("TableId")]
    public int TableId { get; set; }

    [JsonPropertyName("Capacity")]
    public int Capacity { get; set; }

    // [JsonPropertyName("reservationDate")]
    // public DateTime? ReservationDate { get; set; }

    // [JsonPropertyName("reservationTime")]
    // public DateTime? ReservationTime { get; set; }

    [JsonPropertyName("ReservationDateTime")]
    public DateTime? ReservationDateTime { get; set; }
    // public DateTime ReservationDateTime { get; set; }
}
