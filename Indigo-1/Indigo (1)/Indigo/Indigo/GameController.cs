using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Threading;
using Indigo;

namespace Indigo
{
    public abstract class GameController
    {
        private string iconSet;
        protected GameState state = GameState.Running;
        protected List<Card> gameCards;
        private Stack<KeyValuePair<Card, Rectangle>> candidateStack = new Stack<KeyValuePair<Card, Rectangle>>();
        protected Random random = new Random(DateTime.Now.Millisecond);
        protected Grid gameGrid;
        public GameController(Grid gameGrid, string iconSet)
        {
            if (gameGrid == null) throw new ArgumentNullException("gameGrid can not be null.");
            if (String.IsNullOrEmpty(iconSet)) throw new ArgumentException("iconSet can not be null or empty.");

            this.gameGrid = gameGrid;
            this.iconSet = iconSet;
        }


        protected void PushCardOnCandidateStack(Rectangle cardRectangle)
        {
            candidateStack.Push(new KeyValuePair<Card, Rectangle>((Card)cardRectangle.DataContext, cardRectangle));
        }

        protected int CardsOnStack
        {
            get
            {
                return candidateStack.Count;
            }
        }

        private List<Card> AssignCardsToGameGrid(Grid gameGrid, List<Card> initialCardCollection)
        {
            List<Card> gameCardCollection = new List<Card>();

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Rectangle rectangle = gameGrid.Children.OfType<Rectangle>().Single(
                        x =>
                            (int)x.GetValue(Grid.RowProperty) == row &&
                            (int)x.GetValue(Grid.ColumnProperty) == col);

                    int randomCardNumber = random.Next(0, initialCardCollection.Count);

                    Card card = initialCardCollection[randomCardNumber];

                    gameCardCollection.Add(card);
                    rectangle.DataContext = card;

                    initialCardCollection.RemoveAt(randomCardNumber);
                    Wait(200);
                }
            }

            return gameCardCollection;
        }

        public void PickCard(Rectangle cardRectangle)
        {

            if (state == GameState.GameOver) return;

            Card card = cardRectangle.DataContext as Card;

            if (card.Status != CardState.Covered)
            {
                return;
            }

            OnCardPicked(card);
            FlipCard(cardRectangle);

            PushCardOnCandidateStack(cardRectangle);


            if (CardsOnStack == 2)
            {
                string cardName = candidateStack.Peek().Key.Name;
                bool isMatch = CheckIfCardsOnCandiateStackMatch();

                if (isMatch)
                {
                    OnMatch(cardName);
                }
                else
                {
                    OnMiss();
                }
            }


            if (!gameCards.Exists(c => c.Status == CardState.Covered))
            {
                state = GameState.GameOver;
            }
        }

        protected virtual void OnGameStart() { }
        protected virtual void OnCardPicked(Card card) { }
        protected virtual void OnMatch(string cardName) { }
        protected virtual void OnMiss() { }

        protected bool CheckIfCardsOnCandiateStackMatch()//Match eller inte.
        {
            var evalResult = EvalCards();

            if (evalResult.Key) 
            {
                MatchCard(evalResult.Value[0]);
                MatchCard(evalResult.Value[1]);
                return true;
            }
            else 
            {
                FlipCard(evalResult.Value[0]);
                FlipCard(evalResult.Value[1]);
                return false;
            }
        }

        protected void Wait(long milliseconds)
        {

            long dtEnd = DateTime.Now.AddTicks(milliseconds * 1000).Ticks;

            while (DateTime.Now.Ticks < dtEnd)
            {
                Grid g = new Grid();
                g.Dispatcher.Invoke(DispatcherPriority.Background, (DispatcherOperationCallback)delegate(object unused) { return null; }, null);
            }

        }

        protected KeyValuePair<bool, List<Rectangle>> EvalCards()  //Match eller inte-
        {
            var a = candidateStack.Pop();
            var b = candidateStack.Pop();

            if (a.Key.Name == b.Key.Name) 
            {
                Wait(3500);

                return new KeyValuePair<bool, List<Rectangle>>(true, new List<Rectangle>() { a.Value, b.Value });
            }
            else 
            {
                Wait(6000);

                return new KeyValuePair<bool, List<Rectangle>>(false, new List<Rectangle>() { a.Value, b.Value });
            }

        }


        protected void FlipCard(Rectangle cardRectangle)
        {
            Card card = cardRectangle.DataContext as Card;

            FlipCardRectangle(cardRectangle, 1, 0);

            if (card.Status == CardState.Covered)
            {
                card.Uncover();
            }
            else
            {
                card.Cover();
            }

            FlipCardRectangle(cardRectangle, 0, 1);
        }

        protected List<Card> CreateCards()
        {
            List<Card> cards = new List<Card>();

            BitmapImage backgroundImage = GetImage("images/background.jpg");

            for (int x = 1; x < 9; x++)
            {
                BitmapImage frontImage = GetImage(String.Format("images/{1}/R{0}.png", x, iconSet));
                Card a = new Card(x.ToString(), frontImage, backgroundImage);

                frontImage = GetImage(String.Format("images/{1}/R{0}.png", x, iconSet));
                Card b = new Card(x.ToString(), frontImage, backgroundImage);

                cards.Add(a);
                cards.Add(b);
            }

            return cards;
        }


        protected void MatchCard(Rectangle rectangle)
        {
            Card card = rectangle.DataContext as Card;
            card.Match();
        }

        public void StartGame()
        {
            gameGrid.Children.OfType<Rectangle>().ToList().ForEach(rec => rec.DataContext = null);   
            List<Card> initialCards = CreateCards();
            gameCards = AssignCardsToGameGrid(gameGrid, initialCards);
            state = GameState.Running;
            OnGameStart();
        }

        private BitmapImage GetImage(string path)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = Application.GetResourceStream(new Uri(path, UriKind.Relative)).Stream;
            image.EndInit();
            return image;
        }

        private void FlipCardRectangle(Rectangle cardRectangle, int from, int to)
        {
            cardRectangle.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform flipTrans = new ScaleTransform();
            flipTrans.ScaleY = 1;
            cardRectangle.RenderTransform = flipTrans;

            DoubleAnimation da = new DoubleAnimation();
            da.From = from;
            da.To = to;
            da.Duration = TimeSpan.FromMilliseconds(200);

            flipTrans.BeginAnimation(ScaleTransform.ScaleYProperty, da);
        }
    }
}
