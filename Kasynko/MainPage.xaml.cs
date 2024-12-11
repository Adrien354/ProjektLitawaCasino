using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kasynko
{
    public partial class MainPage : ContentPage
    {
        private readonly string[] symbols = { "🍒", "🍋", "🍇", "⭐", "🍉", "🔔" };
        private Random random = new Random();

        public MainPage()
        {
            InitializeComponent();
        }
        private async void RouletteButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1());
        }

        private void SpinButton_Clicked(object sender, EventArgs e)
        {
            // Randomly assign symbols to slots
            Slot1.Text = symbols[random.Next(symbols.Length)];
            Slot2.Text = symbols[random.Next(symbols.Length)];
            Slot3.Text = symbols[random.Next(symbols.Length)];

            // Check for win condition
            if (Slot1.Text == Slot2.Text && Slot2.Text == Slot3.Text)
            {
                ResultLabel.Text = "Jackpot! 🎉";
                ResultLabel.TextColor = Color.Gold;
            }
            else
            {
                ResultLabel.Text = "Try Again!";
                ResultLabel.TextColor = Color.White;
            }
        }
    }
}
