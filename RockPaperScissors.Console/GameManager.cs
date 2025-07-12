using RockPaperScissors.Console.AI;
using RockPaperScissors.Console.Player;
using RockPaperScissors.Console.UserInterface;

namespace RockPaperScissors.Console;

public class GameManager
{
    private GameState gameState;
    
    private UserInterfaceMode uiMode;
    private GameInterfaceBase gameInterface;
    private PlayerInputHandler playerInputHandler;
    private AiBase ai;

    public void StartGame(UserInterfaceMode mode, int bestOfRounds)
    {
        gameState = new GameState();
        gameState.SetBestOfRounds(bestOfRounds);
        
        uiMode = mode;
        switch (mode)
        {
            case UserInterfaceMode.Console:
                gameInterface = gameInterface = new TextInterface();
                playerInputHandler = new PlayerInputHandlerCommands();
                break;
            case UserInterfaceMode.GraphicalUserInterface:
                throw new NotImplementedException("Graphical User Interface is not implemented yet.");
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }

        gameInterface.GameStart(bestOfRounds);
        SetDifficulty();
        StartMatch();
    }

    private void SetDifficulty()
    {
        gameInterface.DisplayDifficultySelection();
        Difficulty difficulty = playerInputHandler.SelectDifficulty();
        gameInterface.DifficultySelected(difficulty);
        ai = AiDifficultySelector.SelectDifficulty(difficulty);
    }

    private void StartMatch()
    {
        gameInterface.DisplayMatchStart();
        Round round = new()
        {
            PlayerHand = playerInputHandler.SelectHand(),
            AiHand = ai.GetMove()
        };
        
        gameState.GetWinner(round.GetResult());
        gameInterface.DisplayMatchResult(round, gameState.GameStateData);
        
        if(!gameState.IsMatchOver())
            StartMatch();
        else
            gameInterface.RequestQuit(round, gameState.GameStateData);
        
        //For now it will exit at the end of the round, later it should ask the player if they want to play again
    }
}
