using RockPaperScissors.Console.AI;
using RockPaperScissors.Console.Player;
using RockPaperScissors.Console.UserInterface;

namespace RockPaperScissors.Console;
/// <summary>
/// The game manager handles all of the other scripts, ideally no actual calculations or logic should be in this class.
/// Only calling methods from other classes and passing data around.
/// </summary>
public class GameManager
{
    private GameState gameState;
    
    private UserInterfaceMode uiMode;
    private GameInterfaceBase gameInterface;
    private PlayerInputHandler playerInputHandler;
    private AiBase ai;

    #region Setup

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

    #endregion

    private void StartMatch()
    {
        gameInterface.DisplayRoundStart();
        Round round = new()
        {
            PlayerHand = playerInputHandler.SelectHand(),
            AiHand = ai.GetMove()
        };
        
        gameState.GetWinner(round.GetResult());
        ai.AddRound(round);
        gameInterface.DisplayMatchResult(round, gameState.GameStateData);
        
        if(!gameState.IsMatchOver())
            StartMatch();
        else
            gameInterface.RequestQuit(round, gameState.GameStateData);
        
        QuitOrContinue();
    }

    private void QuitOrContinue()
    {
        playerInputHandler.ContinueGame();
        gameState.StartNewMatch();
        StartMatch();
    }
}
