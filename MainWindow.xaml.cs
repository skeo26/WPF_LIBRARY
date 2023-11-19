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
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new BooksViewModel();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            // Создайте окно AddBookWindow и установите его владельцем текущее окно
            AddBookWindow addBookWindow = new AddBookWindow();
            addBookWindow.Owner = this;

            // Откройте окно
            addBookWindow.ShowDialog();
        }
        private void SaveToJson_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is BooksViewModel viewModel)
            {
                // Получите фактическую коллекцию данных (List<Book>)
                var books = viewModel.Items.SourceCollection as List<Book>;

                // Создайте диалоговое окно сохранения файла JSON
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                    Title = "Сохранить в JSON",
                    AddExtension = true,
                };

                // Если пользователь указал путь, сохраните файл
                if (saveFileDialog.ShowDialog() == true)
                {
                    // Получите полный путь к файлу
                    string filePath = saveFileDialog.FileName;

                    // Создайте директорию, если её нет
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));

                    // Сериализуйте список книг в формат JSON
                    string json = JsonConvert.SerializeObject(books, Formatting.Indented);

                    // Сохраните JSON в выбранный файл
                    File.WriteAllText(filePath, json);
                }
            }
        }

        private void SaveToXml_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is BooksViewModel viewModel)
            {
                // Получите фактическую коллекцию данных (List<Book>)
                var books = viewModel.Items.SourceCollection as List<Book>;

                // Создайте диалоговое окно сохранения файла XML
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                    Title = "Сохранить в XML",
                    AddExtension = true,
                };

                // Если пользователь указал путь, сохраните файл
                if (saveFileDialog.ShowDialog() == true)
                {
                    // Получите полный путь к файлу
                    string filePath = saveFileDialog.FileName;

                    // Создайте директорию, если её нет
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));

                    // Создайте объект XmlSerializer для типа List<Book>
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Book>));

                    // Сохраните список книг в формат XML
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        serializer.Serialize(stream, books);
                    }
                }
            }
        }
    }
}

