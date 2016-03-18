public class GameState
{
    private Player[] playersInRoom;
    private int[] playerScores;
    private int winScore;

    public Player[] PlayersInRoom
    {
        get
        {
            return playersInRoom;
        }

        set
        {
            playersInRoom = value;
        }
    }

    public int[] PlayerScores
    {
        get
        {
            return playerScores;
        }

        set
        {
            playerScores = value;
        }
    }

    public int WinScore
    {
        get
        {
            return winScore;
        }

        set
        {
            winScore = value;
        }
    }
}
