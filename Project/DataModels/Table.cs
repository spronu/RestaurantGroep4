using System.Text.Json.Serialization;

public class Table
{
    [JsonPropertyName("TableId")]
    public int TableId { get; set; }

    [JsonPropertyName("Capacity")]
    public int Capacity { get; set; }

    [JsonPropertyName("ReservationDateTime")]
    public DateTime? ReservationDateTime { get; set; }
}
