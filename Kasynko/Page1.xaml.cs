using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kasynko
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {

        private Random random = new Random();
        private string selectedColor = "None";

        public Page1()
        {
            InitializeComponent();
        }

        // Set selected bet color to Red
        private void RedBet_Clicked(object sender, EventArgs e)
        {
            selectedColor = "Red";
            ColorLabel.Text = "Selected Color: Red";
        }

        // Set selected bet color to Black
        private void BlackBet_Clicked(object sender, EventArgs e)
        {
            selectedColor = "Black";
            ColorLabel.Text = "Selected Color: Black";
        }

        // Set selected bet color to Green
        private void GreenBet_Clicked(object sender, EventArgs e)
        {
            selectedColor = "Green";
            ColorLabel.Text = "Selected Color: Green";
        }

        // Get the color of the winning number
        private string GetColor(int number)
        {
            // Define color sets for Red, Black, and Green
            var redNumbers = new HashSet<int> { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
            var blackNumbers = new HashSet<int> { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };

            if (number == 0)
            {
                return "Green"; // Green is 0
            }
            else if (redNumbers.Contains(number))
            {
                return "Red";
            }
            else if (blackNumbers.Contains(number))
            {
                return "Black";
            }

            return "None"; // Should never reach here if input is valid
        }

        private async void SpinButton_Clicked(object sender, EventArgs e)
        {
            // Validate the player's number input
            if (!int.TryParse(BetEntry.Text, out int playerBet) && selectedColor == "None")
            {
                ResultLabel.Text = "Please enter a valid number between 1 and 36 or select a color bet.";
                ResultLabel.TextColor = Color.Yellow;
                return;
            }

            // Simulate spinning the wheel and the ball animation
            await SpinWheelAndBallAnimation();

            // Determine the winning number and color
            int winningNumber = random.Next(0, 37); // 0-36 for roulette numbers
            string winningColor = GetColor(winningNumber);

            // Determine if the player wins
            if (selectedColor == winningColor)
            {
                ResultLabel.Text = $"Congratulations! The ball landed on {winningNumber} ({winningColor}). You win!";
                ResultLabel.TextColor = Color.Gold;
            }
            else if (playerBet == winningNumber)
            {
                ResultLabel.Text = $"Congratulations! The ball landed on {winningNumber}. You win!";
                ResultLabel.TextColor = Color.Gold;
            }
            else
            {
                ResultLabel.Text = $"The ball landed on {winningNumber} ({winningColor}). Better luck next time!";
                ResultLabel.TextColor = Color.White;
            }
        }

        private async System.Threading.Tasks.Task SpinWheelAndBallAnimation()
        {
            ResultLabel.Text = "Spinning...";
            ResultLabel.TextColor = Color.White;

            // Set initial rotation angles
            double wheelRotation = 120;
            double ballRotation = 0;
            double ballRadius = 75; // Adjust the radius based on the wheel size


            // Slow down the wheel
            for (int i = 0; i < 360; i += random.Next(5, 15))
            {
                wheelRotation += random.Next(10, 20);
                Wheel.Rotation = wheelRotation % 360;

                // Animate the ball in the opposite direction of the wheel
                double angle = ballRotation * Math.PI / 180; // Convert to radians
                Ball.TranslationX = ballRadius * Math.Cos(angle);
                Ball.TranslationY = ballRadius * Math.Sin(angle);

                // Continue to rotate the ball in the opposite direction
                ballRotation -= random.Next(10, 20); // Ball moves in the opposite direction
                await System.Threading.Tasks.Task.Delay(50); // Smooth animation
            }

            ResultLabel.Text = "Stopped!";
        }

    }
}