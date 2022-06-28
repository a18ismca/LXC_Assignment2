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


    // Assigning the variable to a char that corresponds to the length of the secret word. The var below will consist of underscores. 

    underscoreArr = new char[secretWord.Length];

    FillWithUnderscores(underscoreArr);



    attempts = 10;

   

    while (attempts > 0 && wordUnrevealed)
    {

        // Hangman hint
        Console.WriteLine($"The word contains {secretWord.Length} letters");

        // For testing purposes

        Console.Write("Word: ");

        Console.WriteLine(underscoreArr);

        try { 

        Console.WriteLine("Press 1 to guess a letter. Press 2 to guess a word.");

        char decision = Console.ReadKey().KeyChar;

        Console.WriteLine("\n");

            switch (decision)
            {

                case '1':
                    
                    LetterGuess(secretWord, underscoreArr);

                    break;

                case '2':

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


    if (secret.Contains(letterGuessed))
    {
        if (correctGuessedLetters.ToString().Contains(letterGuessed))
        {
            Console.Clear();

            Console.WriteLine($"You have already guessed the letter {letterGuessed}. Attempts left: {attempts}");
        }
        else {

            for (int i = 0; i < secret.Length; i++)
            {

                if (letterGuessed == secret[i])
                {

                    Console.Clear();

                    underscore_arr[i] = letterGuessed;

                    LetterCorrect(letterGuessed);

                    if (!underscore_arr.Contains('_'))
                    {
                        WordCorrect(secret);
                    }
                }
            }
        }
        Console.WriteLine();
    } 
    
    else if (incorrectGuessedLetters.ToString().Contains(letterGuessed))
    {
        DuplicateLetterInput(letterGuessed, attempts);


    }
    else
    {
        LetterIncorrect(letterGuessed);
        
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.


    Console.WriteLine($"Incorrectly guessed letters: {incorrectGuessedLetters}");

}

void LetterIncorrect(char letter)
{
    Console.Clear();

    incorrectGuessedLetters.Append(letter);

    reduceNumOfGuesses();

}

void LetterCorrect(char letter)
{
    Console.Clear();

    correctGuessedLetters.Append(letter);

    Console.WriteLine($"You have guessed the letter correctly. Attempts remaining: {attempts}");
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
    reduceNumOfGuesses();
}

void WordCorrect(string secret)
{
    Console.Clear();

    Console.WriteLine($"You have guessed the word correctly. The word was {secret}");

    wordUnrevealed = false;

}

void FillWithUnderscores(char[] arr)
{
    for (int i = 0; i < arr.Length; i++)
    {
        arr[i] = '_';
    }
}


void reduceNumOfGuesses()
{
    Console.Clear();

    attempts--;

    Console.WriteLine($"Incorrect, try again. Guesses remaining: {attempts}");

}

void DuplicateLetterInput(char input, int tries)
{
    Console.WriteLine($"You have already guessed the letter {input}. Attempts left: {tries}");
}

void ResetStringbuilders(StringBuilder sb1, StringBuilder sb2)
{
    sb1.Replace(sb1.ToString(), "");
    sb2.Replace(sb2.ToString(), "");
}


