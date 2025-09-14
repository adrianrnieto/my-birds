namespace MyBirds.Server.Requests;

public record AddFavouriteRequest
{
    public required int PhotoId { get; set; }
}
