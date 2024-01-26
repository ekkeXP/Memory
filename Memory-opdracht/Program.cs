namespace Memory.ConsoleApp;
using Memory;
using Memory.DataAccess.SQLServer;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Formats.Asn1.AsnWriter;

class Program
{
    static MemoryGame game;
    static MemoryScoreRepository MemoryDataAccess;
    //static MemoryService MemoryService;
    static string name = "";
    static int amountOfCards;
    static int score;

    //Main function
    static void Main(string[] args)
    {
        MemoryDataAccess = new MemoryScoreRepository();
        //MemoryService = new MemoryService(MemoryDataAccess);
        game = new MemoryGame();
        Start();
    }

    //Start the Game
    private static void Start()
    {
        Console.Clear();
        Console.WriteLine("With how many cards do you want to play? (10-52)");
        while (!int.TryParse(Console.ReadLine(), out amountOfCards) || (amountOfCards % 2 != 0 || amountOfCards <= 8 || amountOfCards > 52))
        {
            Console.WriteLine("Amount should be an even number between 10 and 52");
            Console.WriteLine("With how many cards do you want to play?");
        }
        Console.WriteLine("Please enter your name:");
        while (!(name.Length > 0))
        {
            name = Console.ReadLine();
        }
        game.FillCardList(amountOfCards);
        game.Start();
        // Zolang het spel niet is afgelopen
        while (!game.IsFinished())
        {
            // Toon het bord
            ShowBoard();

            // Vraag de gebruiker om een kaart te kiezen
            Console.Write("Choose a card:");
            int card;
            if (!(int.TryParse(Console.ReadLine(), out card) && card <= amountOfCards && card > 0))
            {
                Console.WriteLine("Invalid input");
                Thread.Sleep(500);
                continue;
            }

            // Draai de kaart om
            if (game.cards[card - 1].IsFlipped)
            {
                Console.WriteLine("This card is already flipped");
                Thread.Sleep(1000);
            }
            else
            {
                game.FlipCard(card);
                if (game.turns % 2 == 0)
                {
                    if (!game.MatchCards())
                    {
                        ShowBoard();
                        Thread.Sleep(2000);
                        game.reflipChosenCards();
                    }
                }
            }
        }
        ShowScore();
        UploadScore();
        Finish();
    }

    //Show the board
    private static void ShowBoard()
    {
        Console.Clear();
        Console.WriteLine("Cards:");
        DrawCards();
        Console.WriteLine();
    }

    //Draw the cards on the board
    private static void DrawCards()
    {
        int rows = (int)Math.Sqrt(game.cards.Length) - 1;
        int cols = game.cards.Length / rows + 1;
        int CardCounter = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (CardCounter < game.cards.Length)
                {
                    DrawCard(CardCounter, i, j);
                }
                CardCounter++;
            }
            Console.SetCursorPosition(0, Console.CursorTop + 5);
        }
    }

    //Draw a single card with the correct symbol
    public static void DrawCard(int CardCounter, int i, int j)
    {
        ConsoleColor c = ConsoleColor.White;
        string character = game.cards[CardCounter].GetSymbol();
        if (character.Contains("#")) { character = $"{CardCounter + 1}"; }
        else { c = ConsoleColor.Yellow; }
        Console.SetCursorPosition(j * 6, i * 5);
        Console.Write("┌----┐");
        Console.SetCursorPosition(j * 6, i * 5 + 1);
        Console.Write("|    |");
        Console.SetCursorPosition(j * 6, i * 5 + 2);
        Console.Write("| ");
        Console.ForegroundColor = c;
        Console.Write($"{string.Format("{0,2}", character)}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" |");
        Console.SetCursorPosition(j * 6, i * 5 + 3);
        Console.Write("|    |");
        Console.SetCursorPosition(j * 6, i * 5 + 4);
        Console.Write("└----┘");
    }

    //Show the score of the player
    public static void ShowScore()
    {
        float playerCardAmount = amountOfCards;
        float temp = ((playerCardAmount * playerCardAmount) / (game.secondsCounter * game.turns)) * 1000;
        score = (int)temp;
        game.stopTimer();
        Console.Clear();
        Console.WriteLine($"{name} (You) scored: {score} points");
        Console.WriteLine($"It took you {game.turns} turns and {game.secondsCounter} seconds to complete the board.");
        Console.WriteLine();
    }

    //Upload the score to the database
    private static void UploadScore()
    {
        var HighscoreItem = new MemoryHighscore()
        {
            Rank = MemoryDataAccess.GetRank(score),
            Name = name,
            Score = score,
            CardsAmount = amountOfCards
        };

        if(HighscoreItem.Rank < 10)
        {
            MemoryDataAccess.Insert(HighscoreItem);
        }
    }

    //Finish the game
    private static void Finish()
    {
        ShowHighscores();
        Console.WriteLine("\nPress 'Y' to play again or anything else to close:");
        if (Console.ReadKey().Key == ConsoleKey.Y)
        {
            Start();
        }
    }

    //Show the top 10 scores in the database
    private static void ShowHighscores()
    {
        List<MemoryHighscore> result = MemoryDataAccess.GetAll();
        foreach(MemoryHighscore MHC in result)
        {
            Console.WriteLine("Name: " + MHC.Name + ", Rank: " + MHC.Rank + ", Score: " + MHC.Score + ", Cards: " + MHC.CardsAmount);
        }
    }
}