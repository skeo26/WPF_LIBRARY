using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Library_WPF
{
    public class BooksViewModel : DependencyObject
    {

        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string), typeof(BooksViewModel), new PropertyMetadata("", SearchText_Changed));

        private static void SearchText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var currentBook = d as BooksViewModel;
            if (currentBook != null)
            {
                currentBook.Items.Filter = null;
                currentBook.Items.Filter = currentBook.SearchBook;
            }
        }

        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
        public ICommand AddBookCommand { get; set; }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("MyProperty", typeof(ICollectionView), typeof(BooksViewModel), new PropertyMetadata(null));

        public BooksViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(Book.GetBooks());
            Items.Filter = SearchBook;
            AddBookCommand = new RelayCommand(AddNewBook);
            EditBookCommand = new RelayCommand(EditSelectedBook, CanEditSelectedBook);
            DeleteBookCommand = new RelayCommand(DeleteSelectedBook, CanDeleteSelectedBook);
        }

        private bool SearchBook(object obj)
        {
            bool searchingBook = true;
            Book currentBook = obj as Book;
            if (!string.IsNullOrEmpty(SearchText) && currentBook != null && !currentBook.Title.Contains(SearchText))
                searchingBook = false;
            return searchingBook;
        }

        private void AddNewBook()
        {
            AddBookWindow addBookWindow = new AddBookWindow();
            addBookWindow.Owner = Application.Current.MainWindow; 
            addBookWindow.ShowDialog();
        }

        public void AddBook(Book newBook)
        {
            var books = Items.SourceCollection as List<Book>;
            books?.Add(newBook);

            Items.Refresh();
        }

        public ICommand EditBookCommand { get; set; }

        private bool CanEditSelectedBook()
        {
            return SelectedBook != null;
        }

        private void EditSelectedBook()
        {
            // Получаем выбранный элемент через свойство SelectedBook
            Book selectedBook = SelectedBook;

            if (selectedBook != null)
            {
                // Создаем ViewModel для редактирования
                EditBookViewModel editBookViewModel = new EditBookViewModel(selectedBook,this);

                // Создаем окно редактирования и устанавливаем ему DataContext
                EditBookWindow editBookWindow = new EditBookWindow();
                editBookWindow.DataContext = editBookViewModel;
                editBookWindow.Owner = Application.Current.MainWindow;

                // Если пользователь подтвердил изменения, обновляем коллекцию и представление
                if (editBookWindow.ShowDialog() == true)
                {
                    Items.Refresh();
                }
            }
        }


        public Book SelectedBook
        {
            get { return (Book)GetValue(SelectedBookProperty); }
            set { SetValue(SelectedBookProperty, value); }
        }

        public static readonly DependencyProperty SelectedBookProperty =
            DependencyProperty.Register("SelectedBook", typeof(Book), typeof(BooksViewModel), new PropertyMetadata(null));

        public void ApplyBookChanges(Book editedBook)
        {
            if (editedBook != null)
            {
                // Перемещаем текущий элемент книги к редактируемой книге
                Items.MoveCurrentTo(SelectedBook);

                // Обновляем значения свойств текущего элемента
                var currentBook = Items.CurrentItem as Book;
                if (currentBook != null)
                {
                    currentBook.Title = editedBook.Title;
                    currentBook.Author = editedBook.Author;
                    currentBook.Genre = editedBook.Genre;
                    currentBook.Year = editedBook.Year;
                }

                Items.Refresh();
            }
        }
        public ICommand DeleteBookCommand { get; set; }
        public object Books { get; internal set; }

        private void DeleteSelectedBook()
        {
            if (SelectedBook != null)
            {
                // Получаем текущую коллекцию
                var booksCollection = Items.SourceCollection as List<Book>;

                booksCollection?.Remove(SelectedBook);

                Items.Refresh();
            }
        }
        private bool CanDeleteSelectedBook()
        {
            return SelectedBook != null;
        }
    }
}
