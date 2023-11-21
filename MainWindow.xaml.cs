using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

namespace Library_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataGrid_Books.ItemsSource = books_dbEntities.GetContext().books_db.ToList();
            Loaded += MainWindow_Loaded;
            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new BooksViewModel();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            AddBookWindow addBookWindow = new AddBookWindow();
            addBookWindow.Owner = this;

            // Откройте окно
            addBookWindow.ShowDialog();
        }
        private void SaveToJson_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is BooksViewModel viewModel)
            {
                var books = viewModel.Items.SourceCollection as List<Book>;

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                    Title = "Сохранить в JSON",
                    AddExtension = true,
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));

                    string json = JsonConvert.SerializeObject(books, Formatting.Indented);
                    File.WriteAllText(filePath, json);
                }
            }
        }

        private void SaveToXml_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is BooksViewModel viewModel)
            {
                var books = viewModel.Items.SourceCollection as List<Book>;
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                    Title = "Сохранить в XML",
                    AddExtension = true,
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));

                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Book>));

                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        serializer.Serialize(stream, books);
                    }
                }
            }
        }
    }
}

