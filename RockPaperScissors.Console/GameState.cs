namespace RockPaperScissors.Console;

public class GameState
{
    private int bestOfRounds;

    public GameStateData GameStateData = new();
    
    private bool isMatchOver;

    public void SetBestOfRounds(int rounds) => bestOfRounds = rounds % 2 == 0 ? rounds + 1 : rounds;

    public void GetWinner(RoundResult result)
    {
        switch (result)
        {
            case RoundResult.Draw:
                return; //no points awarded, go to next game
            case RoundResult.PlayerWin:
            {
                GameStateData.PlayerWonRound();

                if (GameStateData.PlayerRoundPoints >= bestOfRounds / 2 + 1)
                {
                    //TODO: we dont currently reset the score when the match is over, need to link that up to when the player choose to continue to play
                    GameStateData.PlayerWonMatch();
                    isMatchOver = true;
                }

                break;
            }
            case RoundResult.AiWin:
            {
                GameStateData.PlayerLostRound();

                if (GameStateData.AiRoundPoints >= bestOfRounds / 2 + 1)
                {
                    //TODO: we dont currently reset the score when the match is over, need to link that up to when the player choose to continue to play
                    GameStateData.PlayerLostMatch();
                    isMatchOver = true;
                }

                break;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(result), result, null);
        }
    }

    public bool IsMatchOver()
    {
        if (!isMatchOver) 
            return false;
        
        isMatchOver = false;
        return true;
    }

    public void StartNewMatch()
    {
        GameStateData.ResetMatch();
    }
}
