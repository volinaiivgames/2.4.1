using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Button> ListButtons = new List<Button>();
        List<List<int>> ListWins = new List<List<int>>()
        {
            new List<int>() {0,1,2},
            new List<int>() {3,4,5},
            new List<int>() {6,7,8},

            new List<int>() {0,3,6},
            new List<int>() {1,4,7},
            new List<int>() {2,5,8},

            new List<int>() {0,4,8},
            new List<int>() {2,4,6},
        };

        public MainWindow()
        {
            InitializeComponent();
            ListButtons = MyGrid.Children.OfType<Button>().Where(b => b.Tag.Equals("system")).ToList();
        }

        private void ButtonNewGame(object sender, RoutedEventArgs e)
        {
            LabelResult.Content = "";
            for (int i = 0; i < ListButtons.Count; i++)
            {
                Button button = ListButtons[i];
                button.Content = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!LabelResult.Content.Equals("")) return;
            for (int i = 0; i < ListButtons.Count; i++)
            {
                Button button = ListButtons[i];
                if (button.IsFocused && button.Content.Equals(""))
                {
                    button.Content = "X";
                    if(CheckWinner()) BotClick();
                }
            }
        }

        public void BotClick()
        {
            Random random = new Random();
            List<int> ListContent = new List<int>();
            while (LabelResult.Content.Equals(""))
            {
                int randomNumber = random.Next(0, ListButtons.Count - 1);
                Button button2 = ListButtons[randomNumber];
                if (button2.Content.Equals(""))
                {
                    button2.Content = "O";
                    CheckWinner();
                    break;
                }
                else ListContent.Add(randomNumber);
                if (ListContent.Count == ListButtons.Count)
                {
                    LabelResult.Content = "Ничья";
                    break;
                }
            }
        }

        private bool CheckWinner()
        {
            string winner = GetWinner();
            if (!winner.Equals(""))
            {
                LabelResult.Content = winner;
                return false;
            }
            else return true;
        }

        private string GetWinner()
        {
            for (int i = 0; i < ListWins.Count; i++)
            {
                List<int> items = ListWins[i];
                Button button1 = ListButtons[items[0]];
                Button button2 = ListButtons[items[1]];
                Button button3 = ListButtons[items[2]];
                if (button1.Content.Equals("X") && button2.Content.Equals("X") && button3.Content.Equals("X")) return "Победили крестики";
                if (button1.Content.Equals("O") && button2.Content.Equals("O") && button3.Content.Equals("O")) return "Победили нолики";
            }
            return "";
        }
    }
}
