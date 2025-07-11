using RockPaperScissors.Console.UserInterface;

namespace RockPaperScissors.Console;

public class GameManager
{
    private UserInterfaceMode uiMode;
    private GameInterfaceBase gameInterface;
    
    public void StartGame(UserInterfaceMode mode = UserInterfaceMode.Console)
    {
        uiMode = mode;
        gameInterface = mode switch
        {
            UserInterfaceMode.Console => gameInterface = new TextInterface(),
            UserInterfaceMode.GraphicalUserInterface => throw new NotImplementedException("Graphical User Interface is not implemented yet."),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
        
        gameInterface.GameStart();
    }

    private void SetDifficulty()
    {
        
    }

    private void StartMatch()
    {
        
    }

    private void GetWinner()
    {
        
    }

    private void AttributePoint()
    {
        
    }

    private void QuitGame()
    {
        
    }
}
