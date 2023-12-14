using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipCaptainCrew
{
    public class Program
    {
        static void Main()
        {
            do
            {
                Console.WriteLine("Welcome to Ship, Captain, Crew!");
                Console.WriteLine("1. Read the Rules");
                Console.WriteLine("2. Start the Game");
                Console.WriteLine("3. Exit");

                int choice;

                while (true)
                {
                    Console.Write("Enter your choice (1, 2, or 3): ");

                    if (int.TryParse(Console.ReadLine(), out choice))
                    {
                        if (choice == 1)
                        {
                            DisplayRules();
                            break;
                        }
                        else if (choice == 2)
                        {
                            PlayGame(); // Start the game immediately
                            break;
                        }
                        else if (choice == 3)
                        {
                            Environment.Exit(0); // Exit the application
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                }

                Console.WriteLine("\nDo you want to play again?");
                Console.WriteLine("1. Play again");
                Console.WriteLine("2. Exit");

                int playAgainChoice;

                while (true)
                {
                    Console.Write("Enter your choice (1 or 2): ");

                    if (int.TryParse(Console.ReadLine(), out playAgainChoice))
                    {
                        if (playAgainChoice == 1)
                        {
                            PlayGame(); // Start a new game
                            break;
                        }
                        else if (playAgainChoice == 2)
                        {
                            Environment.Exit(0); // Exit the application
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                }

            } while (true);
        }

        static void PlayGame()
        {
            Console.Clear(); // Clear the console before starting the game

            Console.WriteLine("Welcome to Ship, Captain, Crew!");

            // Get the number of players
            Console.Write("Enter the number of players: ");
            int numberOfPlayers;

            // Validate input for the number of players
            while (!int.TryParse(Console.ReadLine(), out numberOfPlayers) || numberOfPlayers < 2)
            {
                Console.Write("Invalid input. Please enter a valid number of players (minimum 2): ");
            }

            int[] playerScores = new int[numberOfPlayers];

            // Play the game for each player
            for (int playerNumber = 1; playerNumber <= numberOfPlayers; playerNumber++)
            {
                Console.Clear(); // Clear the console before each player's turn
                Console.WriteLine($"\nPlayer {playerNumber}, it's your turn!");

                // Play the game and store the score
                int score = PlayGameRound();
                playerScores[playerNumber - 1] = score;

                // Display the final score of the current player
                Console.WriteLine($"\nPlayer {playerNumber}'s final score is: {score}");

                if (playerNumber < numberOfPlayers)
                {
                    Console.WriteLine("\nPress Enter to start the next player's turn...");
                    Console.ReadLine(); // Wait for Enter key press before starting the next player's turn
                }
            }

            // Determine the winner
            int highestScore = playerScores.Max();
            int winnerIndex = Array.IndexOf(playerScores, highestScore) + 1;

            Console.WriteLine($"\nPlayer {winnerIndex} is the winner with a score of {highestScore}!");

            // Ask if the player wants to play again
            Console.WriteLine("\nDo you want to play again?");
            Console.WriteLine("1. Play again");
            Console.WriteLine("2. Exit");

            int playAgainChoice;

            while (true)
            {
                Console.Write("Enter your choice (1 or 2): ");

                if (int.TryParse(Console.ReadLine(), out playAgainChoice))
                {
                    if (playAgainChoice == 1)
                    {
                        PlayGame(); // Start a new game
                        break;
                    }
                    else if (playAgainChoice == 2)
                    {
                        Environment.Exit(0); // Exit the application
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        static int PlayGameRound()
        {
            ShipCaptainCrewGame game = new ShipCaptainCrewGame();
            int score = 0;
            int rollsRemaining = 3;

            while (rollsRemaining > 0)
            {
                Console.WriteLine($"\nPress Enter to roll the dice (Rolls remaining: {rollsRemaining})...");
                Console.ReadLine();

                int[] dice = game.RollDice();

                Console.WriteLine($"You rolled: {string.Join(", ", dice)}");

                int shipCount = game.CountOccurrences(dice, 6);
                int captainCount = game.CountOccurrences(dice, 5);
                int crewCount = game.CountOccurrences(dice, 4);

                Console.WriteLine($"Ship: {shipCount}, Captain: {captainCount}, Crew: {crewCount}");

                if (shipCount >= 1 && captainCount >= 1 && crewCount >= 1)
                {
                    Console.WriteLine("Congratulations! You rolled Ship, Captain, and Crew!");

                    // You won! Calculate the score
                    score = 1000 + game.SumOfRemainingDice(dice);
                    break;
                }
                else
                {
                    Console.WriteLine("No Ship, Captain, and Crew. Your score is the sum of the remaining two dice.");
                    score = game.SumOfRemainingDice(dice);
                }

                Console.WriteLine($"Your current score is: {score}");

                rollsRemaining--;
            }

            Console.WriteLine($"\nYour final score is: {score}");

            return score;
        }

        static void DisplayRules()
        {
            Console.Clear(); // Clear the console before displaying the rules

            Console.WriteLine("\nRules:");
            Console.WriteLine("\nObjective:");
            Console.WriteLine("The objective of the game is to score the highest possible combination of Ship, Captain, and Crew.");
            Console.WriteLine("\nEquipment:");
            Console.WriteLine("You need five standard six-sided dice to play the game.");
            Console.WriteLine("\nScoring:");
            Console.WriteLine("Ship (6): The die with the number 6 represents the Ship. You need to roll at least one 6 to score in this category.");
            Console.WriteLine("Captain (5): The die with the number 5 represents the Captain. You need to roll at least one 5 to score in this category.");
            Console.WriteLine("Crew (4): The die with the number 4 represents the Crew. You need to roll at least one 4 to score in this category.");
            Console.WriteLine("The remaining two dice: These are not scored individually. Instead, the sum of these two dice contributes to your overall score.");
            Console.WriteLine("\nRolling:");
            Console.WriteLine("On a turn, a player rolls all five dice.");
            Console.WriteLine("After the first roll, the player can choose to set aside any combination of 4s, 5s, and 6s and roll the remaining dice again.");
            Console.WriteLine("After the second roll, the player can again set aside any combination of 4s, 5s, and 6s and roll the remaining dice for the final time.");
            Console.WriteLine("After the third roll, the player records their score based on the rules above.");
            Console.WriteLine("\nScoring the Game:");
            Console.WriteLine("If a player rolls all three of Ship, Captain, and Crew (at least one 4, one 5, and one 6), they achieve the highest possible score.");
            Console.WriteLine("If the player doesn't roll all three, they record the sum of the remaining two dice as their score.");
            Console.WriteLine("\nWinning: Players take turns rolling, and the player with the highest score at the end of the game wins.");
            Console.WriteLine("\nPress Enter to start the game...");
            Console.ReadLine(); // Consume the Enter key press
            PlayGame();
        }
    }
}