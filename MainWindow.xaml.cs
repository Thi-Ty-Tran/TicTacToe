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
            xPlayerNameTxtBox.Focus(); // Focus on the X player's name input
        }

        /// <summary>
        /// Handles the click event for the game buttons
        /// Managing game state updates such as checking for a winner or a draw
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            // Extarct the row and colum from button's Name 
            string buttonName = clickedButton.Name;             // Btn00, Button identifier
            int row = int.Parse(buttonName[3].ToString());     // Row index
            int column = int.Parse(buttonName[4].ToString()); // Column index

            // Check if button is already clicked
            if (!string.IsNullOrEmpty(boardGame[row, column]))
            {
                return; // Exit if the cell is already occupied
            }

            //Update the board and UI
            clickedButton.Content = currentPlayer;  // Show current player's symbol
            boardGame[row, column] = currentPlayer; // Update the board state

            // Check for winner
            if (checkWinner(boardGame))
            {
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

            // Check for draw
            if (CheckDraw())
            {
                MessageBox.Show("It's a draw!"); // Notify players of a draw
                ResetBoard();                   // Reset the board for a new game
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
        /// Checks if there is a winner on the board
        /// </summary>
        /// <param name="boadrGame"></param>
        /// <returns>Return true if any row, column diagonal are the same, otherwise it will return false</returns>
        private bool checkWinner(string[,] boardGame)
        {
            // Size of tic-tac toe board (3x3)
            int size = 3;

            for (int index = 0; index < size; index++)
            {
                // Checking for a win for rows

                if (
                        boardGame[index, 0] == boardGame[index, 1] &&
                        boardGame[index, 1] == boardGame[index, 2] &&
                        !string.IsNullOrEmpty(boardGame[index, 0])
                    )
                {
                    return true; // Row win detected
                }

                // Checking a win for columns

                if (
                        boardGame[0, index] == boardGame[1, index] &&
                        boardGame[1, index] == boardGame[2, index] &&
                        !string.IsNullOrEmpty(boardGame[0, index])
                    )
                { 
                    return true; // Column win detected
                }
            }

            // check for diagonal from top left - to bottom right
            if (
                    boardGame[0, 0] == boardGame[1, 1] &&
                    boardGame[1, 1] == boardGame[2, 2] && !
                    string.IsNullOrEmpty(boardGame[0, 0])
                )
            {
                return true;
            }

            // Check for diagonal from top right - to bottom left
            if (
                    boardGame[0, 2] == boardGame[1, 1] &&
                    boardGame[1, 1] == boardGame[2, 0] && !
                    string.IsNullOrEmpty(boardGame[0, 2])
                )
            {
                return true; // Diagonal win detected
            }

            // If nothing matches return false → No winner found
            return false;
        }

        /// <summary>
        /// Check for Draw
        /// </summary>
        /// <returns></returns>
        private bool CheckDraw()
        {
            foreach (var cell in boardGame)
            {
                if (string.IsNullOrEmpty(cell))
                {
                    return false;  // There's still an empty cell, so no draw.
                }
            }
            return true;  // No empty cells, so it's a draw.
        }

        /// <summary>
        /// Reset board
        /// </summary>
        private void ResetBoard()
        {
            // CLear board data
            boardGame = new string[3, 3];
            currentPlayer = "X";
            currentPlayerTxtBox.Text = currentPlayer;

            // Clear buttons
            btn00.Content = "";
            btn01.Content = "";
            btn02.Content = "";
            btn10.Content = "";
            btn11.Content = "";
            btn12.Content = "";
            btn20.Content = "";
            btn21.Content = "";
            btn22.Content = "";

        }

        /// <summary>
        /// Update scores
        /// </summary>
        private void UpdateScore()
        {
            // Update scores based on the current player
            if (currentPlayer == "X")
            {
                xPlayerScore++;                                    // Increment X player's score
                xPlayerScoreTxtBox.Text = xPlayerScore.ToString(); // Update UI
            }
            else
            {
                oPlayerScore++;                                     // Increment O player's score
                oPlayerScoreTxtBox.Text = oPlayerScore.ToString(); // Update UI
            }
        }

        /// <summary>
        /// Clear all data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Reset the scores to 0
            xPlayerScore = 0;
            oPlayerScore = 0;

            // Update the score textboxes in the UI
            xPlayerScoreTxtBox.Text = xPlayerScore.ToString();
            oPlayerScoreTxtBox.Text = oPlayerScore.ToString();

            // Clear player names
            xPlayerNameTxtBox.Clear();
            oPlayerNameTxtBox.Clear();

            // Reset game board
            ResetBoard();
        }

        // Close the app
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}