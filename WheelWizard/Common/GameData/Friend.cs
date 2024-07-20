namespace CT_MKWII.Common.GameData;

public class Friend
{
    public string Name { get; set; }
    public string FriendCode { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public string MiiData { get; set; } // Base64 encoded
}
