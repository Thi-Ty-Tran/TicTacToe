//    Name: Thi Ty Tran
//    Date Created: Oct 7, 2024
//    Description: Tic Tac Toe App - Assignment 2
//    Last modified: Oct 14, 2024 

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // 2D array representing the game board
        private string[,] boardGame = new string[3, 3];

        // Keeps track of the current player (X or O)
        private string currentPlayer = "X";

        // Scores for both players
        private int xPlayerScore = 0;
        private int oPlayerScore = 0;

        public MainWindow()
        {
            InitializeComponent();       // Initialize UI components
            ResetBoard();               // Set up the game board
            xPlayerNameTxtBox.Focus();  // Focus on the X player's name input
        }

        /// <summary>
        /// Handles the click event for the game buttons
        /// Updates game state, checks for winner or draw
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            // Extract the row and column from the button's Name 
            string buttonName = clickedButton.Name;             // Button identifier (e.g., Btn00)
            int row = int.Parse(buttonName[3].ToString());      // Row index
            int column = int.Parse(buttonName[4].ToString());   // Column index

            // Check if the button has already been clicked
            if (!string.IsNullOrEmpty(boardGame[row, column]))
            {
                return; // Exit if the cell is already occupied
            }

            // Update the board and UI with the current player's move
            clickedButton.Content = currentPlayer;  // Show current player's symbol on the button
            boardGame[row, column] = currentPlayer; // Update the board state with the current move

            // Check if there's a winner
            if (CheckWinner())
            {
                // Highlight the winning line
                HighlightWinningLine();

                // Get the winner's name
                string winnerName;

                // Determine the winner's
                if (currentPlayer == "X")
                {
                    winnerName = xPlayerNameTxtBox.Text;  // Get the X player's name
                }
                else
                {
                    winnerName = oPlayerNameTxtBox.Text;  // Get the O player's name
                }

                // Display winner message
                MessageBox.Show($"🎊 {winnerName} ({currentPlayer}) is the winnner 🎊");
                UpdateScore(); // Update the score for the winner
                ResetBoard(); // Reset the board for a new game
                return;
            }

            // Check if it's a draw
            if (CheckDraw())
            {
                // Display draw message
                MessageBox.Show("It's a draw!");

                // Reset the board for a new game
                ResetBoard();
                return;
            }

            // Switch Player
            if (currentPlayer == "X")
            {
                currentPlayer = "O";
            }
            else
            {
                currentPlayer = "X";
            }

            // Update the current player display in the textbox
            currentPlayerTxtBox.Text = currentPlayer;
        }

        /// <summary>
        /// Checks if there is a winner by analyzing rows, columns, and diagonals
        /// </summary>
        /// <returns>True if there's a winner, false otherwise</returns>
        private bool CheckWinner()
        {
            // Check all rows and columns for a winner
            for (int index = 0; index < 3; index++)
            {
                // Check rows
                if (
                    boardGame[index, 0] == currentPlayer && 
                    boardGame[index, 1] == currentPlayer && 
                    boardGame[index, 2] == currentPlayer)
                    return true;

                // Check columns
                if (
                    boardGame[0, index] == currentPlayer && 
                    boardGame[1, index] == currentPlayer && 
                    boardGame[2, index] == currentPlayer)
                    return true;
            }

            // Check diagonals for a winner
            if (
                boardGame[0, 0] == currentPlayer && 
                boardGame[1, 1] == currentPlayer && 
                boardGame[2, 2] == currentPlayer)
                return true;

            if (boardGame[0, 2] == currentPlayer && 
                boardGame[1, 1] == currentPlayer && 
                boardGame[2, 0] == currentPlayer)
                return true;

            // No winner found
            return false;
        }

        /// <summary>
        /// Highlights the buttons that form the winning line using a bright color
        /// </summary>
        private void HighlightWinningLine()
        {
            // Check rows for a winning line
            for (int index = 0; index < 3; index++)
            {
                if (
                    boardGame[index, 0] == currentPlayer && 
                    boardGame[index, 1] == currentPlayer && 
                    boardGame[index, 2] == currentPlayer)
                {
                    HighlightButtons($"btn{index}0", $"btn{index}1", $"btn{index}2");
                    return;
                }
            }

            // Check columns for a winning line
            for (int index = 0; index < 3; index++)
            {
                if (
                    boardGame[0, index] == currentPlayer && 
                    boardGame[1, index] == currentPlayer && 
                    boardGame[2, index] == currentPlayer)
                {
                    HighlightButtons($"btn0{index}", $"btn1{index}", $"btn2{index}");
                    return;
                }
            }

            // Check diagonals for a winning line
            if (
                boardGame[0, 0] == currentPlayer && 
                boardGame[1, 1] == currentPlayer && 
                boardGame[2, 2] == currentPlayer)
            {
                HighlightButtons("btn00", "btn11", "btn22");
            }
            else if (boardGame[0, 2] == currentPlayer && 
                    boardGame[1, 1] == currentPlayer && 
                    boardGame[2, 0] == currentPlayer)
            {
                HighlightButtons("btn02", "btn11", "btn20");
            }
        }

        /// <summary>
        /// Sets the background color of the buttons in the winning line to yellow
        /// </summary>
        /// <param name="btn1">First button in the winning line</param>
        /// <param name="btn2">Second button in the winning line</param>
        /// <param name="btn3">Third button in the winning line</param>
        private void HighlightButtons(string btn1, string btn2, string btn3)
        {
            // Find and highlight each button in the winning line
            Button button1 = (Button)FindName(btn1);
            Button button2 = (Button)FindName(btn2);
            Button button3 = (Button)FindName(btn3);

            // Set the background color to yellow
            button1.Background = Brushes.Yellow;
            button2.Background = Brushes.Yellow;
            button3.Background = Brushes.Yellow;
        }

        /// <summary>
        /// Checks if the game is a draw 
        /// </summary>
        /// <returns>True if it's a draw, false otherwise</returns>
        private bool CheckDraw()
        {
            // Loop through each cell in the board and check if there's an empty cell
            foreach (var cell in boardGame)
            {
                if (string.IsNullOrEmpty(cell))
                {
                    return false;  // Exit if there's at least one empty cell
                }
            }
            return true;  // No empty cells found, it's a draw
        }

        /// <summary>
        /// Resets the game board and clears the button content for a new game
        /// </summary>
        private void ResetBoard()
        {
            // Clear the board data
            boardGame = new string[3, 3];
            currentPlayer = "X";                        // Set current player to "X" for the new game
            currentPlayerTxtBox.Text = currentPlayer;  // Update the current player display

            // Clear all button contents and reset background color
            btn00.Content = ""; btn00.Background = Brushes.White;
            btn01.Content = ""; btn01.Background = Brushes.White;
            btn02.Content = ""; btn02.Background = Brushes.White;
            btn10.Content = ""; btn10.Background = Brushes.White;
            btn11.Content = ""; btn11.Background = Brushes.White;
            btn12.Content = ""; btn12.Background = Brushes.White;
            btn20.Content = ""; btn20.Background = Brushes.White;
            btn21.Content = ""; btn21.Background = Brushes.White;
            btn22.Content = ""; btn22.Background = Brushes.White;
        }

        /// <summary>
        /// Updates the score for the current player after winning
        /// </summary>
        private void UpdateScore()
        {
            // Increment the score for the current player
            if (currentPlayer == "X")
            {
                xPlayerScore++;                                    // Increment X player's score
                xPlayerScoreTxtBox.Text = xPlayerScore.ToString(); // Update X player's score display
            }
            else
            {
                oPlayerScore++;                                    // Increment O player's score
                oPlayerScoreTxtBox.Text = oPlayerScore.ToString(); // Update O player's score display
            }
        }

        /// <summary>
        /// Resets all game data including player names and scores, ready for a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Reset the scores to 0 for both players
            xPlayerScore = 0;
            oPlayerScore = 0;

            // Update the score display textboxes
            xPlayerScoreTxtBox.Text = xPlayerScore.ToString();
            oPlayerScoreTxtBox.Text = oPlayerScore.ToString();

            // Clear the player names
            xPlayerNameTxtBox.Clear();
            oPlayerNameTxtBox.Clear();

            // Reset the board for a new game
            ResetBoard();
        }

        /// <summary>
        /// Closes the application when the Exit button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();  // Closes the application
        }
    }
}
