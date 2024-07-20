using System.Collections.Generic;

namespace CT_MKWII.Common.GameData;

public class GameData
{
    public List<User> Users { get; set; }

    public GameData()
    {
        Users = new List<User>(4);
    }
}

