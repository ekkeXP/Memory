using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Memory.Maui_App.Services;

namespace Memory.Maui_App.ViewModels
{
    //Viewmodel for GamePage and ConfirmPage
    public partial class GamePageViewModel : PageViewModel
    {
        public MemoryGame currentGame;
        private int score;
        private bool busy = false;
        public bool FirstRun = true;
        IToastService ts;

        //Constructor
        public GamePageViewModel(IToastService TS)
        {
            ts = TS;
        }

        //Initialize function
        public void init()
        {
            if (currentGame == null) currentGame = new MemoryGame(DB.GetImages());
            currentGame.FillCardList(int.Parse(EnteredCardAmount));
            currentGame.Start();
        }

        //Start command
        [RelayCommand]
        public async Task start()
        {
            await Shell.Current.GoToAsync($"//{nameof(GamePage)}");
        }

        //Go to optionsPage command
        [RelayCommand]
        public async Task GoToOptionsPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(UserOptionsPage)}");
        }

        //Command to flip Clicked card
        [RelayCommand]
        public void ImageClicked(Image sender)
        {
            int styleid = int.Parse(sender.StyleId);
            if (!currentGame.cards[styleid].IsFlipped)
            {
                currentGame.FlipCard(styleid + 1);
            }
            else
            {
                ts.MakeToast("Card is already flipped!");
            }
        }

        //Check if cards are matching
        public void CheckMatched()
        {
            if (!currentGame.MatchCards() && currentGame.TwoCardsSelected())
            {
                //Thread.Sleep(1000);
                currentGame.reflipChosenCards();
            }
        }

        //Check if the Game is Finished
        public async Task CheckFinished()
        {
            if (currentGame.IsFinished())
            {
                ShowScore();
                UploadScore();
                UpdateHighscoresList();
                ResetAllCards();
                await Shell.Current.GoToAsync($"//{nameof(HighscoresPage)}");
            }
            else
            {
                return;
            }
        }

        //Reset all the cards
        private void ResetAllCards()
        {
            foreach (Card C in currentGame.cards)
            {
                C.IsFlipped = false;
                C.IsMatched = false;
            }
            FirstRun = true;
        }

        //Function to show the current score
        public void ShowScore()
        {
            float playerCardAmount = float.Parse(EnteredCardAmount);
            float temp = ((playerCardAmount * playerCardAmount) / (currentGame.secondsCounter * currentGame.turns)) * 1000;
            score = (int)temp;
            currentGame.stopTimer();
            ResultText = $"YOU ({EnteredName}) scored: {score} points.\nIt took you {currentGame.secondsCounter} seconds to complete the board.";
        }

        //Function to upload the current score
        private void UploadScore()
        {
            var HighscoreItem = new MemoryHighscore()
            {
                Rank = DB.GetRank(score),
                Name = EnteredName,
                Score = score,
                CardsAmount = int.Parse(EnteredCardAmount)
            };
            DB.Insert(HighscoreItem);
        }

        //Function to update the highscores
        private void UpdateHighscoresList()
        {
            List<MemoryHighscore> result = DB.GetAll();
            Highscores.Clear();
            foreach (MemoryHighscore MHC in result)
            {
                Highscores.Add("Name: " + MHC.Name + ", Rank: " + MHC.Rank + ", Score: " + MHC.Score + ", Cards: " + MHC.CardsAmount);
            }
        }

    }
}
