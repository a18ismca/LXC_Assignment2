// See https://aka.ms/new-console-template for more information



// One of the words included in the array will be used for the hangman game.

using System.Text;

string[] listOfWords = { "gubbe", "dator", "program", "mobil", "fotboll", "handboll" };

string secretWord;

char[] underscoreArr;

int remainingGuesses;

StringBuilder incorrectGuessedLetters = new StringBuilder();

// a variable that decides if the word is revealed or not

bool wordUnrevealed;

HangmanGame();

void HangmanGame()
{
    wordUnrevealed = true;

    Random random = new Random();

    // Randomize the word that is going to be used

    secretWord = listOfWords[random.Next(0, listOfWords.Length)];


    // Assigning the variable to a char that corresponds to the length of the secret word. The var below will consist of underscores. 

    underscoreArr = new char[secretWord.Length];

    FillWithUnderscores(underscoreArr);


    // For testing purposes

    Console.WriteLine(secretWord);

    // For testing purposes

    Console.WriteLine(underscoreArr);

    remainingGuesses = 10;


    while(remainingGuesses > 0 && wordUnrevealed)
    {
        try { 

        Console.WriteLine("Press 1 to guess a letter. Press 2 to guess a word.");

        char decision = Console.ReadKey().KeyChar;

        Console.WriteLine("\n");

            switch (decision)
            {

                case '1':

                    Console.WriteLine("Redirecting to letter guess...");

                    LetterGuess(secretWord, underscoreArr);

                    break;

                case '2':

                    Console.WriteLine("Redirecting to word guess...");

                    WordGuess(secretWord, underscoreArr);

                    break;

                default:

                    Console.WriteLine("Please press 1 or 2 in order to continue.");

                    break;

            }
        }
        catch {

            Console.Error.WriteLine("Please press 1 or 2 in order to continue.");

        }

        
       

        
    }

    Console.WriteLine("Game over. \n Press 1 to start a new game. Press Q to quit.");

    char input = Console.ReadKey().KeyChar;

    try
    {
        switch (input)
        {
            case 'q':

                Console.WriteLine("Exiting game...");

                Environment.Exit(0);

                break;

            case '1':

                Console.WriteLine("Starting a new round...");

                HangmanGame();

                break;

            default:
                Console.WriteLine("Please press 1 or Q in order to continue");

                break;
        }
    }
    catch
    {

        Console.Error.WriteLine("Please press 1 or Q in order to continue");

    }

}

void LetterGuess(string secret, char[] underscore_arr)
{
    Console.WriteLine("Enter a letter: ");

    char letterGuessed = Console.ReadKey().KeyChar;

    Console.WriteLine("\n");

    if (secret.Contains(letterGuessed) && !(secret.Equals(underscore_arr)))
    {
        for(int i = 0; i < secret.Length; i++)
        {
            if(letterGuessed == secret[i])
            {
                underscore_arr[i] = letterGuessed;
                if (secret.Equals(underscore_arr[i])){
                    wordUnrevealed = false;
                }
            } 
            Console.Write(underscore_arr[i]);
            
        }
    } 
    
    else if (incorrectGuessedLetters.ToString().Contains(letterGuessed))
    {
        Console.WriteLine($"You have already guessed the letter {letterGuessed}");
    }
    else
    {
        IncorrectGuessUsingLetters(letterGuessed);
    }

    Console.WriteLine("\n");

    
}

void FillWithUnderscores(char[] arr)
{
    for (int i = 0; i < arr.Length; i++)
    {
        arr[i] = '_';
    }
}

// Klart

void WordGuess(string secret, char[] underscore_arr)
{

    Console.WriteLine("Enter a word: ");

    string wordGuessed = Console.ReadLine();

    string lowerTheWord = wordGuessed.ToLower();

    if (secret.Contains(lowerTheWord))
    {

        Console.WriteLine($"You have guessed the word correctly. The word was {secret}");

        wordUnrevealed = false;

    }
    else
    {

        IncorrectGuessUsingWords();

    }
}

void IncorrectGuessUsingWords()
{

    reduceNumOfGuesses();

}

void IncorrectGuessUsingLetters(char letter)
{
    

    incorrectGuessedLetters.Append(letter);

    reduceNumOfGuesses();

}

void reduceNumOfGuesses()
{
    remainingGuesses--;

    Console.WriteLine($"Incorrect, try again. Guesses remaining: {remainingGuesses}");

}