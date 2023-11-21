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
using System.Windows.Shapes;

namespace Library_WPF
{
    /// <summary>
    /// Логика взаимодействия для AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public AddBookWindow()
        {
            InitializeComponent();
        }
        private bool IsCorrectYear()
        {
            string year = YearTextBox.Text;
            if (int.Parse(year) > 0 && int.Parse(year) <= 2023)
                return true;
            else
            {
                MessageBox.Show("Вы ввели некорректное значение!");
                return false;
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsCorrectYear())
                return;
            Book newBook = new Book
            {
                title = TitleTextBox.Text,
                author = AuthorTextBox.Text,
                genre = GenreTextBox.Text,
                year = int.Parse(YearTextBox.Text)
            };

            var viewModel = (BooksViewModel)Owner.DataContext;
            viewModel?.AddBook(newBook);

            this.Close();
        }
    }
}
