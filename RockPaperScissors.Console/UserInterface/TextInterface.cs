namespace RockPaperScissors.Console.UserInterface;

public class TextInterface : GameInterfaceBase
{
    public override void GameStart()
    {
        System.Console.WriteLine("Welcome to Rock, Paper, Scissors!");
        System.Console.WriteLine("You will be playing best of 3, good luck!");
    }
    
    public override void DisplayMatchStart()
    {
        
    }
}
