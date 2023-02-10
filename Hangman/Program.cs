namespace Hangman
{
    class Program
    {
        public static string logo;
        static string CurrentWord;
        static int CurrentWordLength;
        static Random randomWord;
        static int Chances;
        static char guess;
        static List<char> GuessedLetters = new List<char>();
        static List<char> UsedLetters = new List<char>();
        private static string _answer;
        static List<string> WordList = new List<string>() //declare list of words
        {
            "human",
            "dwarf",
            "nightelf",
            "gnome",
            "draenei",
            "worgen",
            "pandaren",
            "orc",
            "undead",
            "tauren",
            "troll",
            "bloodelf",
            "goblin",
            "vulpera",
            "nightborne",
            "mechagnome",
            "voidelf"
        };


        static void Main(string[] args)
        {
            randomWord = new Random();
            StartGame();
        }

        static void StartGame()
        {
            Chances = 6;
            CurrentWord = WordList[randomWord.Next(WordList.Count() - 1)]; //randomize selection of word
            CurrentWordLength = CurrentWord.Length; //getting and storing the length of selected word
            Console.WriteLine(" _");
            Console.WriteLine("| | ");
            Console.WriteLine("| |__   __ _ _ __   __ _ _ __ ___   __ _ _ __ ");
            Console.WriteLine("| '_ \\ / _` | '_ \\ / _` | '_ ` _ \\ / _` | '_ \\ ");
            Console.WriteLine("| | | | (_| | | | | (_| | | | | | | (_| | | | |");
            Console.WriteLine("|_| |_|\\__,_|_| |_|\\__, |_| |_| |_|\\__,_|_| |_|");
            Console.WriteLine("                    __/ |       ");
            Console.WriteLine("                   |___/ ");
               
            
            Console.WriteLine("\n");
            Console.WriteLine("Welcome to Hangman. You have 6 guesses to reveal the word. Goodluck.");
            Console.WriteLine("\n");
             
            Console.WriteLine("Press any key to Continue.");
            Console.ReadKey();
            Console.Clear();
            GameInterface();//goes to game interface
        }

        static void GameInterface()
        {
            DisplayHangman(); //displays hangman :)
            Console.WriteLine("\n");
            Console.WriteLine("Chances left: " + Chances + "\n"); // displays the chances left
            string winning = "";
            for (int i = 0; i < CurrentWordLength; i++)
            {
                if(GuessedLetters.Any()) //checking any input letter
                {
                    if (GuessedLetters.Contains(CurrentWord[i])) //catches if letter is in the selected random word
                    {
                        Console.Write(CurrentWord[i].ToString() + " "); //prints the letter
                        winning += CurrentWord[i].ToString();
                        if(winning == CurrentWord) // if all is guessed
                        {
                            Success();
                        }
                    }
                    else
                    {
                        Console.Write("_ "); // prints as line when input is not in the word
                    }
                    
                }
                else
                {
                    Console.Write("_ "); // prints as line when letter is not in the word
                    
                }
            }
            
            Console.WriteLine("\n\n");
            Console.Write("Used letter: "+ String.Join(" ",UsedLetters)); //prints out used letter
            Console.WriteLine();
            Console.Write("Guess a letter: "); 
            guess = Char.ToLower(Console.ReadKey().KeyChar); //asks for a letter
            ConfirmInput();// confirms user to use input letter or discard
            
            
        }
        
        
        static void CheckGuess(char guess)
        {
            Console.WriteLine("\n");
            bool guessedOne = false;
            for(int i = 0; i < CurrentWordLength; i++)
            {
                if(CurrentWord[i] == guess) // if letter is found in selected random word
                {
                    guessedOne = true;
                    GuessedLetters.Add(CurrentWord[i]);
                }
                
            }

            if (guessedOne == false) // -1 chance if letter is not found
                --Chances;
            
            if (Chances <= 0) // if chances run out go to fail
                Fail();

            Console.Clear();
            GameInterface(); // returns to interface
        }

        
        
        static void ConfirmInput() // confirming user input
        {
            Console.WriteLine("\n");
            
            while (true)
            {
                Console.WriteLine("Are you sure with your guess?");
                Console.WriteLine("Confirm by pressing \"Enter\"");
                Console.WriteLine("Discard by pressing \"Backspace\"");
            
                if (Console.ReadKey().Key == ConsoleKey.Enter) // use enter to confirm
                {
                    if (!UsedLetters.Contains(guess)) //saves character to usedletter list
                    {
                        UsedLetters.Add(guess);
                        CheckGuess(guess);
                        Console.Clear();
                    }
                    if (UsedLetters.Contains(guess)) //asks user to input a different one since character has already been used.
                    {
                        Console.Clear();
                        Console.WriteLine("Letter has been used. Choose a different one.");
                        GameInterface();
                    }

                    Console.Clear();
                }
                else if (Console.ReadKey().Key == ConsoleKey.Backspace) // use backspace to discard
                {
                    
                    Console.Clear();
                    GameInterface();
                }

                else
                {
                    
                    Console.WriteLine("Invalid input.");
                }
                
                Console.WriteLine();
            }
        }

        static void Fail() // when failed to guess word
        {
            Console.Clear();
            Console.WriteLine(" ____");
            Console.WriteLine("|/   |");
            Console.WriteLine("|   (_)");
            Console.WriteLine("|   /|\\");
            Console.WriteLine("|    |");
            Console.WriteLine("|   | |");
            Console.WriteLine("|");
            Console.WriteLine("|_______ ");
            Console.WriteLine("\n");
            Console.WriteLine("Sorry. You ran out of guess attempts.");
            Console.WriteLine($"The word was: {CurrentWord.ToUpper()}");
            PlayAgain(); // asks if to play again
        }
        
        static void Success() // successfully guessed word
        {
            Console.Clear();
            Console.WriteLine(" ____");
            Console.WriteLine("|/");
            Console.WriteLine("|   YAY!");
            Console.WriteLine("|   (_)");
            Console.WriteLine("|   \\|/");
            Console.WriteLine("|    |");
            Console.WriteLine("|   / \\");
            Console.WriteLine("|_______ ");
            
            Console.WriteLine("\n");
            Console.WriteLine("Congrats you rock!");
            Console.WriteLine($"{CurrentWord.ToUpper()} is the correct word.");
            PlayAgain(); // asks if to play again
        }
        static void PlayAgain() // asks to restart game
        {
            Console.WriteLine("\n");
            UsedLetters = new List<char>(); // resets usedletters
            while (_answer != "y" || _answer != "n") // while _answer is not y or n
            {
                Console.WriteLine("Would you like to play again? Y/N");
                _answer = Console.ReadLine().ToLower();
                if (_answer == "y")
                {
                    Console.Clear();
                    GuessedLetters = new List<char>();
                    StartGame(); //start again
                }
                
                if (_answer == "n")
                {
                    Console.Clear();
                    GuessedLetters = new List<char>();
                    End(); // end game
                }
            }
        }
        static void End() 
        {
            Console.WriteLine("Thanks for playing! Bye!");
            Environment.Exit(0);
        }

        static void DisplayHangman()
        {
            if (Chances == 6)
            {
                Console.WriteLine(" ____");
                Console.WriteLine("|/   |");
                Console.WriteLine("|");
                Console.WriteLine("|");
                Console.WriteLine("|");
                Console.WriteLine("|");
                Console.WriteLine("|");
                Console.WriteLine("|_______ ");
            }
            if (Chances == 5)
            {
                Console.WriteLine(" ____");
                Console.WriteLine("|/   |");
                Console.WriteLine("|   (_)");
                Console.WriteLine("|");
                Console.WriteLine("|");
                Console.WriteLine("|");
                Console.WriteLine("|");
                Console.WriteLine("|_______ ");
            }

            if (Chances == 4)
            {
                Console.WriteLine(" ____");
                Console.WriteLine("|/   |");
                Console.WriteLine("|   (_)");
                Console.WriteLine("|    |");
                Console.WriteLine("|    |");
                Console.WriteLine("|");
                Console.WriteLine("|");
                Console.WriteLine("|_______ ");
            }
            
            if (Chances == 3)
            {
                Console.WriteLine(" ____");
                Console.WriteLine("|/   |");
                Console.WriteLine("|   (_)");
                Console.WriteLine("|   \\|");
                Console.WriteLine("|    |");
                Console.WriteLine("|");
                Console.WriteLine("|");
                Console.WriteLine("|_______ ");
            }
            
            if (Chances == 2)
            {
                Console.WriteLine(" ____");
                Console.WriteLine("|/   |");
                Console.WriteLine("|   (_)");
                Console.WriteLine("|   \\|/");
                Console.WriteLine("|    |");
                Console.WriteLine("|");
                Console.WriteLine("|");
                Console.WriteLine("|_______ ");
            }
            
            if (Chances == 1)
            {
                Console.WriteLine(" ____");
                Console.WriteLine("|/   |");
                Console.WriteLine("|   (_)");
                Console.WriteLine("|   \\|/");
                Console.WriteLine("|    |");
                Console.WriteLine("|   /");
                Console.WriteLine("|");
                Console.WriteLine("|_______ ");
            }
            
        }


        
    }
}
