using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CodingActivity_TicTacToe_ConsoleGame
{
    public class ConsoleView
    {
        #region ENUMS

        public enum ViewState
        {
            Active,
            PlayerTimedOut, // TODO Track player time on task
            PlayerUsedMaxAttempts
        }

        #endregion

        #region FIELDS

        private const int GAMEBOARD_VERTICAL_LOCATION = 4;

        //private const int POSITIONPROMPT_VERTICAL_LOCATION = 12;
        private const int POSITIONPROMPT_VERTICAL_LOCATION = 33;
        private const int POSITIONPROMPT_HORIZONTAL_LOCATION = 3;

        //private const int MESSAGEBOX_VERTICAL_LOCATION = 15;
        private const int MESSAGEBOX_VERTICAL_LOCATION = 36;

        private const int TOP_LEFT_ROW = 3;
        private const int TOP_LEFT_COLUMN = 6;

        private Gameboard _gameboard;
        private ViewState _currentViewStat;

        #endregion

        #region PROPERTIES
        public ViewState CurrentViewState
        {
            get { return _currentViewStat; }
            set { _currentViewStat = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public ConsoleView(Gameboard gameboard)
        {
            _gameboard = gameboard;

            InitializeView();

        }

        #endregion

        #region METHODS

        /// <summary>
        /// Initialize the console view
        /// </summary>
        public void InitializeView()
        {
            _currentViewStat = ViewState.Active;

            InitializeConsole();
        }

        /// <summary>
        /// configure the console window
        /// </summary>
        public void InitializeConsole()
        {
            ConsoleUtil.WindowWidth = ConsoleConfig.windowWidth;
            ConsoleUtil.WindowHeight = ConsoleConfig.windowHeight;

            Console.BackgroundColor = ConsoleConfig.bodyBackgroundColor;
            Console.ForegroundColor = ConsoleConfig.bodyBackgroundColor;

            ConsoleUtil.WindowTitle = "The Tic-tac-toe Game";
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            ConsoleUtil.DisplayMessage("Press any key to continue.");
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the Exit prompt on a clean screen
        /// </summary>
        public void DisplayExitPrompt()
        {
            ConsoleUtil.DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            ConsoleUtil.DisplayMessage("Thank you for playing the game. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// display the session timed out screen
        /// </summary>
        public void DisplayTimedOutScreen()
        {
            ConsoleUtil.HeaderText = "Session Timed Out!";
            ConsoleUtil.DisplayReset();

            DisplayMessageBox("It appears your session has timed out.");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the maximum attempts reached screen
        /// </summary>
        public void DisplayMaxAttemptsReachedScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "Maximum Attempts Reached!";
            ConsoleUtil.DisplayReset();

            sb.Append(" It appears that you are having difficulty entering your");
            sb.Append(" choice. Please refer to the instructions and play again.");

            DisplayMessageBox(sb.ToString());

            DisplayContinuePrompt();
        }



        /// <summary>
        /// Inform the player that their position choice is not available
        /// </summary>
        public void DisplayGamePositionChoiceNotAvailableScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "Position Choice Unavailable";
            ConsoleUtil.DisplayReset();

            sb.Append(" It appears that you have chosen a position that is all ready");
            sb.Append(" taken. Please try again.");

            DisplayMessageBox(sb.ToString());

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the welcome screen
        /// </summary>
        public void DisplayWelcomeScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "3D Tic-tac-toe Game!";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Written by Cati Kujawski, Lianna Bowman & Thresa Schultz");
            ConsoleUtil.DisplayMessage("Northwestern Michigan College");
            Console.WriteLine();

            sb.Clear();
            sb.AppendFormat("This is a two player game playing 3D Tic Tac Toe. ");
            sb.AppendFormat("You may select from levels 1, 2 or 3 similar to different floors ");
            sb.AppendFormat("of a building. You have the ability to win utilizing all of the different levels.");
            sb.AppendFormat("After choosing a level, you choose a row, then a column.");
            ConsoleUtil.DisplayMessage(sb.ToString());
            Console.WriteLine();

            sb.Clear();
            sb.AppendFormat("Example of a win: player x gets top left corner on all");
            sb.AppendFormat("three levels");
            sb.AppendFormat("Example of a win: player O gets traditional 3 horizontally, diagonally or vertical.");
            ConsoleUtil.DisplayMessage(sb.ToString());

            DisplayContinuePrompt();
        }
        /// <summary>
        /// get the menu choice from the user
        /// </summary>
        public MenuOption DisplayGetUserMenuChoice()
        {
            MenuOption userMenuChoice = MenuOption.None;
            bool usingMenu = true;

            //
            // TODO enable each application function separately and test
            //
            while (usingMenu)
            {
                //
                // set up display area
                //
                ConsoleUtil.DisplayReset();
                Console.CursorVisible = false;

                //
                // display the menu
                //
                ConsoleUtil.DisplayMessage("Please type the number of your menu choice.");
                Console.WriteLine();
                Console.WriteLine(
                    "\t" + "1. Play New Round" + Environment.NewLine +
                    "\t" + "2. View the rules" + Environment.NewLine +
                    "\t" + "3. View current game stats" + Environment.NewLine +
                    "\t" + "E. Exit" + Environment.NewLine);
                
                ConsoleKeyInfo userResponse = Console.ReadKey(true);
                switch (userResponse.KeyChar)
                {
                    case '1':
                        userMenuChoice = MenuOption.PlayNewRound;
                        usingMenu = false;
                        break;
                    case '2':
                        userMenuChoice = MenuOption.ViewRules;

                        usingMenu = false;
                        break;
                    case '3':
                        userMenuChoice = MenuOption.ViewCurrentGameResults;
                        usingMenu = false;
                        break;
                    case 'E':
                    case 'e':
                        userMenuChoice = MenuOption.Quit;
                        usingMenu = false;
                        break;
                    default:
                        Console.WriteLine(
                            "It appears you have selected an incorrect choice." + Environment.NewLine +
                            "Press any key to continue or the ESC key to quit the application.");

                        userResponse = Console.ReadKey(true);
                        if (userResponse.Key == ConsoleKey.Escape)
                        {
                            usingMenu = false;
                        }
                        break;
                }
            }
            Console.CursorVisible = true;

            return userMenuChoice;
        }

        /// <summary>
        /// display a closing screen when the user quits the application
        /// </summary>
        public void DisplayClosingScreen()
        {
            ConsoleUtil.HeaderText = "The Tic-tac-toe Game";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Thank you for using The Tic-tac-toe Game.");

            DisplayContinuePrompt();
        }

        public void DisplayGameArea()
        {
            ConsoleUtil.HeaderText = "Current Game Board";
            ConsoleUtil.DisplayReset();

            DisplayGameboard();
            DisplayGameStatus();
        }

        public void DisplayCurrentGameStatus(int roundsPlayed, int playerXWins, int playerOWins, int catsGames)
        {

            ConsoleUtil.HeaderText = "Current Game Status";
            ConsoleUtil.DisplayReset();

            double playerXPercentageWins = (double)playerXWins / roundsPlayed;
            double playerOPercentageWins = (double)playerOWins / roundsPlayed;
            double percentageOfCatsGames = (double)catsGames / roundsPlayed;

            ConsoleUtil.DisplayMessage("Rounds Played: " + roundsPlayed);
            ConsoleUtil.DisplayMessage("Rounds for Player X: " + playerXWins + " - " + String.Format("{0:P2}", playerXPercentageWins));
            ConsoleUtil.DisplayMessage("Rounds for Player O: " + playerOWins + " - " + String.Format("{0:P2}", playerOPercentageWins));
            ConsoleUtil.DisplayMessage("Cat's Games: " + catsGames + " - " + String.Format("{0:P2}", percentageOfCatsGames));

             
            DisplayContinuePrompt();
        }

        public bool DisplayNewRoundPrompt()
        {
            ConsoleUtil.HeaderText = "Continue or Quit";
            ConsoleUtil.DisplayReset();

            return DisplayGetYesNoPrompt("Would you like to play another round?");
        }

        public void DisplayGameStatus()
        {
            StringBuilder sb = new StringBuilder();

            switch (_gameboard.CurrentRoundState)
            {
                case Gameboard.GameboardState.NewRound:
                    //
                    // The new game status should not be an necessary option here
                    //
                    break;
                case Gameboard.GameboardState.PlayerXTurn:
                    DisplayMessageBox("It is currently Player X's turn.");
                    break;
                case Gameboard.GameboardState.PlayerOTurn:
                    DisplayMessageBox("It is currently Player O's turn.");
                    break;
                case Gameboard.GameboardState.PlayerXWin:
                    DisplayMessageBox("Player X Wins! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                case Gameboard.GameboardState.PlayerOWin:
                    DisplayMessageBox("Player O Wins! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                case Gameboard.GameboardState.CatsGame:
                    DisplayMessageBox("Cat's Game! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                default:
                    break;
            }
        }

        public void DisplayMessageBox(string message)
        {
            string leftMargin = new String(' ', ConsoleConfig.displayHorizontalMargin);
            string topBottom = new String('*', ConsoleConfig.windowWidth - 2 * ConsoleConfig.displayHorizontalMargin);

            StringBuilder sb = new StringBuilder();

            Console.SetCursorPosition(0, MESSAGEBOX_VERTICAL_LOCATION);
            Console.WriteLine(leftMargin + topBottom);

            Console.WriteLine(ConsoleUtil.Center("Game Status"));

            ConsoleUtil.DisplayMessage(message);

            Console.WriteLine(Environment.NewLine + leftMargin + topBottom);
        }

        /// <summary>
        /// display the current game board
        /// </summary>
        private void DisplayGameboard()
        {
            //
            // move cursor below header
            //
            Console.SetCursorPosition(0, GAMEBOARD_VERTICAL_LOCATION);

            for (int level = 0; level < 3; level++)
            {
                Console.Write("\t\t\t        |---+---+---|\n");

                for (int i = 0; i < 3; i++)
                {
                    Console.Write("\t\t\t        | ");

                    for (int j = 0; j < 3; j++)
                    {
                        if (_gameboard.PositionState[level, i, j] == Gameboard.PlayerPiece.None)
                        {
                            Console.Write(" " + " | ");
                        }
                        else
                        {
                            Console.Write(_gameboard.PositionState[level, i, j] + " | ");
                        }

                    }

                    Console.Write("\n\t\t\t        |---+---+---|\n");
                }

                Console.Write($"\t\t\t        Level {level + 1}\n\n\n");
            }
            

        }

        /// <summary>
        /// display prompt for a player's next move
        /// </summary>
        /// <param name="coordinateType"></param>
        private void DisplayPositionPrompt(string coordinateType)
        {
            //
            // Clear line by overwriting with spaces
            //
            Console.SetCursorPosition(POSITIONPROMPT_HORIZONTAL_LOCATION, POSITIONPROMPT_VERTICAL_LOCATION);
            Console.Write(new String(' ', ConsoleConfig.windowWidth));
            //
            // Write new prompt
            //
            Console.SetCursorPosition(POSITIONPROMPT_HORIZONTAL_LOCATION, POSITIONPROMPT_VERTICAL_LOCATION);
            Console.Write("Enter " + coordinateType + " number: ");
        }

        /// <summary>
        /// Display a Yes or No prompt with a message
        /// </summary>
        /// <param name="promptMessage">prompt message</param>
        /// <returns>bool where true = yes</returns>
        private bool DisplayGetYesNoPrompt(string promptMessage)
        {
            bool yesNoChoice = false;
            bool validResponse = false;
            string userResponse;

            while (!validResponse)
            {
                ConsoleUtil.DisplayReset();

                ConsoleUtil.DisplayPromptMessage(promptMessage + "(yes/no)");
                userResponse = Console.ReadLine();

                if (userResponse.ToUpper() == "YES")
                {
                    validResponse = true;
                    yesNoChoice = true;
                }
                else if (userResponse.ToUpper() == "NO")
                {
                    validResponse = true;
                    yesNoChoice = false;
                }
                else
                {
                    ConsoleUtil.DisplayMessage(
                        "It appears that you have entered an incorrect response." +
                        " Please enter either \"yes\" or \"no\"."
                        );
                    DisplayContinuePrompt();
                }
            }

            return yesNoChoice;
        }

        /// <summary>
        /// Get a player's position choice within the correct range of the array
        /// Note: The ConsoleView is allowed access to the GameboardPosition struct.
        /// </summary>
        /// <returns>GameboardPosition</returns>
        public GameboardPosition GetPlayerPositionChoice()
        {
            //
            // Initialize gameboardPosition with -1 values
            //
            GameboardPosition gameboardPosition = new GameboardPosition(-1, -1, -1);

            //
            // Get level number from player.
            //
            gameboardPosition.Level = PlayerCoordinateChoice("Level");

            //
            // Get row number from player.
            //
            if (CurrentViewState != ViewState.PlayerUsedMaxAttempts)
            {
                gameboardPosition.Row = PlayerCoordinateChoice("Row");
            }
            
            //
            // Get column number.
            //
            if (CurrentViewState != ViewState.PlayerUsedMaxAttempts)
            {
                gameboardPosition.Column = PlayerCoordinateChoice("Column");
            }            

            return gameboardPosition;

        }

        /// <summary>
        /// Validate the player's coordinate response for integer and range
        /// </summary>
        /// <param name="coordinateType">an integer value within proper range or -1</param>
        /// <returns></returns>
        private int PlayerCoordinateChoice(string coordinateType)
        {
            int tempCoordinate = -1;
            int numOfPlayerAttempts = 1;
            int maxNumOfPlayerAttempts = 4;

            while ((numOfPlayerAttempts <= maxNumOfPlayerAttempts))
            {
                DisplayPositionPrompt(coordinateType);

                if (int.TryParse(Console.ReadLine(), out tempCoordinate))
                {
                    //
                    // Player response within range
                    //
                    if (tempCoordinate >= 1 && tempCoordinate <= _gameboard.MaxNumOfRowsColumns)
                    {
                        return tempCoordinate;
                    }
                    //
                    // Player response out of range
                    //
                    else
                    {
                        DisplayMessageBox(coordinateType + " numbers are limited to (1,2,3)");
                    }
                }
                //
                // Player response cannot be parsed as integer
                //
                else
                {
                    DisplayMessageBox(coordinateType + " numbers are limited to (1,2,3)");
                }

                //
                // Increment the number of player attempts
                //
                numOfPlayerAttempts++;
            }

            //
            // Player used maximum number of attempts, set view state and return
            //
            CurrentViewState = ViewState.PlayerUsedMaxAttempts;
            return tempCoordinate;
        }

        #endregion
    }
}
