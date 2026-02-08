namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {
        // int count = 0; // Removed unused field

        // Change 'lastClicked' to nullable
        Button lastClicked;
        bool findingMatch = false;
        int matchesFound;/* it keeps track of the nimber of pair of animals the player found ,so the game can end when they found all 8 pairs*/

        public MainPage()
        {
            InitializeComponent();
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            AnimalButton.IsVisible = true;
            PlayAgainButton.IsVisible = false;
            List<string> aniamalEmoji =
                [ "🐱", "🐱"
                , "🐭","🐭",
                  "🐹","🐹",
                  "🐰","🐰",
                  "🦊","🦊",
                  "🐻","🐻",
                  "🐼","🐼",
                  "🐨","🐨" ];/* create a list of eight pairs of emoji*/
            foreach (var button in AnimalButton.Children.OfType<Button>())
            {
                int index = Random.Shared.Next(aniamalEmoji.Count);/* get a random number between 0 and the number of emoji left in the list and call it "index"*/
                string nextEmoji = aniamalEmoji[index];/* use a random number called "index" to get a random emoji from the list*/
                button.Text = nextEmoji;/* make the button display the selected emoji*/
                aniamalEmoji.RemoveAt(index);/* remove the chosen emoji from the list*/
            }
            /* find every button in the flexlayout and repeat the statement between the {} for each of them*/
            Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTicks);
        }
        int tenthsOfSecondsElapsed = 0;
        private bool TimerTicks()
        {
            if(!this.IsLoaded)return false;
            tenthsOfSecondsElapsed++;
            TimeElapsed.Text = "Time Elapsed :"+(tenthsOfSecondsElapsed/10.0).ToString("0.0s");
            if(PlayAgainButton.IsVisible)
            {
                tenthsOfSecondsElapsed = 0;
                return false;
            }
            return true;
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button buttonClicked)
            {
                if (!string.IsNullOrWhiteSpace(buttonClicked.Text) && (findingMatch == false))
                {
                    lastClicked = buttonClicked;
                    buttonClicked.BackgroundColor = Colors.Red;
                    findingMatch = true;
                }
                else
                {
                    if ((buttonClicked != lastClicked) && (buttonClicked.Text == lastClicked.Text) && (!string.IsNullOrWhiteSpace(buttonClicked.Text)))
                    {
                        matchesFound++;
                        lastClicked.Text = "";
                        buttonClicked.Text = "";
                    }
                    lastClicked.BackgroundColor = Colors.LightBlue;
                    findingMatch = false;
                    buttonClicked.BackgroundColor = Colors.LightBlue;
                }
                if (matchesFound == 8)
                {
                    AnimalButton.IsVisible = false;
                    PlayAgainButton.IsVisible = true;
                    matchesFound = 0;
                }/* if matchfound equals 8,  the player found all 8 pairs of animals. when that happens ,these lines reset the game by setting matchesFound back to zero,hiding the animal buttons,and showing the "Play again?"button so the player can start new game by clicking the "Play again?" button*/
            }
        }
    }
}
