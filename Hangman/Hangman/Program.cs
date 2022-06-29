// See https://aka.ms/new-console-template for more information



// One of the words included in the array will be used for the hangman game.

using System.Text;

string[] listOfWords = { "gubbe", "dator", "program", "mobil", "fotboll", "handboll" };

string secretWord;

char[] underscoreArr;

int attempts;


// Stringbuilders for both

StringBuilder incorrectGuessedLetters = new StringBuilder();

StringBuilder correctGuessedLetters = new StringBuilder();

// a variable that decides if the word is revealed or not

bool wordUnrevealed;

HangmanGame();

void HangmanGame()
{

    wordUnrevealed = true;

    Random random = new Random();

    // Randomize the word that is going to be used

    secretWord = listOfWords[random.Next(0, listOfWords.Length)];


    // Assigning the variable to a char that corresponds to the length of the secret word. The variable below will consist of underscores. 

    underscoreArr = new char[secretWord.Length];

    FillWithUnderscores(underscoreArr);



    attempts = 10;

   

    while (attempts > 0 && wordUnrevealed)
    {

        // Hangman hint
        Console.WriteLine($"The word contains {secretWord.Length} letters");

        // Show progress

        Console.Write("Word: ");

        Console.WriteLine(underscoreArr);

        try { 

        Console.WriteLine("Press 1 to guess a letter. Press 2 to guess a word.");

            // Let the player decide in what way to make an attempt

        char decision = Console.ReadKey().KeyChar;

        Console.WriteLine("\n");

            switch (decision)
            {

                case '1':
                    
                    // Guess with a letter if 1 is pressed

                    LetterGuess(secretWord, underscoreArr);

                    break;

                case '2':

                    // Guess with a word if 2 is pressed

                    WordGuess(secretWord, underscoreArr);

                    break;

                default:

                    // Clear window

                    Console.Clear();

                    Console.WriteLine("Please press 1 or 2 in order to continue.");

                    break;

            }
        }
        catch {

            Console.Clear();

            Console.Error.WriteLine("Please press 1 or 2 in order to continue.");

        }

        
    }

    // The game over message depends on the number of attempts when the game is over. 

    if(attempts > 0) Console.WriteLine("Game over. \n\nPress 1 to start a new game. Press Q to quit.");

    else if (attempts == 0) Console.WriteLine("No attempts left! Game over. \n\nPress 1 to start a new game. Press Q to quit.");

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

                // Reset previous data (letters) stored in the stringbuilders
                ResetStringbuilders(incorrectGuessedLetters, correctGuessedLetters);

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

    // Check if the secret word contains the letter that has been guessed from the user.

    if (secret.Contains(letterGuessed))
    {
        // Check if the correct letter has been inputted more than once.

        if (correctGuessedLetters.ToString().Contains(letterGuessed))
        {
            DuplicateLetterInput(letterGuessed, attempts);
        }
        else {

            for (int i = 0; i < secret.Length; i++)
            {

                if (letterGuessed == secret[i])
                {
                    // Fill the underscore char with the letter guessed at the correct position

                    Console.Clear();

                    underscore_arr[i] = letterGuessed;

                    LetterCorrect(letterGuessed);

                    // If the underscore_arr does not contain any underscores, the player has won the game.
                    if (!underscore_arr.Contains('_'))
                    {
                        WordCorrect(secret);
                    }
                }
            }
        }
        Console.WriteLine();
    }

    // Check if the incorrect letter has been pressed more than once.

    else if (incorrectGuessedLetters.ToString().Contains(letterGuessed))
    {
        DuplicateLetterInput(letterGuessed, attempts);

    }

    // If the letter has not been guessed before and the letter is not included
    // in the secret word, notify the player the number of attempts
    // and the letters that already have been guessed

    else
    {
        LetterIncorrect(letterGuessed);
        
    }

    // Print out the following when the user has 9 attempts left.

    if (attempts < 10)  Console.WriteLine($"Incorrectly guessed letters: {incorrectGuessedLetters} \n");

}

// Use the letter that is incorrect in the parameter of the method below

void LetterIncorrect(char letter)
{
    Console.Clear();

    incorrectGuessedLetters.Append(letter);

    reduceNumOfAttempts();

}

void LetterCorrect(char letter)
{
    Console.Clear();

    correctGuessedLetters.Append(letter);

    // If the player's guess has been wrong at one point before guessing the letter right,
    // the following message that displays the number of attempts left prints out

    if (attempts < 10) Console.WriteLine($"You have guessed the letter correctly. Attempts remaining: {attempts}");

    else Console.WriteLine($"You have guessed the letter correctly.");
}


void WordGuess(string secret, char[] underscore_arr)
{

    Console.WriteLine("Enter a word: ");

    string wordGuessed = Console.ReadLine();

    string lowerTheWord = wordGuessed.ToLower();

    if (secret.Contains(lowerTheWord))
    {
        WordCorrect(secret);
    }
    else
    {
        WordIncorrect();
    }

}

void WordIncorrect()
{
    reduceNumOfAttempts();
}

void WordCorrect(string secret)
{
    Console.Clear();

    Console.WriteLine($"You have guessed the word correctly. The word was {secret} \n");

    wordUnrevealed = false;

}

// fill underscoreArr with underscores at the outset

void FillWithUnderscores(char[] arr)
{
    for (int i = 0; i < arr.Length; i++)
    {
        arr[i] = '_';
    }
}


void reduceNumOfAttempts()
{
    Console.Clear();

    attempts--;

    if(attempts > 0) Console.WriteLine($"Incorrect, try again. Attempts remaining: {attempts}");

}

void DuplicateLetterInput(char input, int tries)
{
    Console.Clear();
    Console.WriteLine($"You have already guessed the letter {input}. Attempts left: {tries}");
}

void ResetStringbuilders(StringBuilder sb1, StringBuilder sb2)
{
    sb1.Replace(sb1.ToString(), "");
    sb2.Replace(sb2.ToString(), "");
}


