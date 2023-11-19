using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Library_WPF
{
    public class EditBookViewModel : INotifyPropertyChanged
    {
        private Book _editedBook;

        public Book EditedBook
        {
            get { return _editedBook; }
            set
            {
                _editedBook = value;
                OnPropertyChanged(nameof(EditedBook));
            }
        }
        private BooksViewModel _booksViewModel;

        // Добавляем свойство для BooksViewModel
        public BooksViewModel BooksViewModel
        {
            get { return _booksViewModel; }
            set
            {
                _booksViewModel = value;
                OnPropertyChanged(nameof(BooksViewModel));
            }
        }
        public ICommand ApplyChangesCommand { get; set; }

        public EditBookViewModel(Book book, BooksViewModel booksViewModel)
        {
            EditedBook = new Book
            {
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Year = book.Year
            };
            BooksViewModel = booksViewModel;
            ApplyChangesCommand = new RelayCommand(ApplyChanges);
        }

        private void ApplyChanges()
        {
            if (BooksViewModel != null)
            {
                BooksViewModel.ApplyBookChanges(EditedBook);
                Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

                currentWindow?.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
