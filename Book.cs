using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_WPF
{
    public class Book
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }

        public static List<Book> GetBooks()
        {
            var books = new List<Book>();
            {
                books.Add(new Book { Title = "Заводной апельсин", Author = "Энтони Бёрджесс", Genre = "Роман", Year = 1972 });
                books.Add(new Book { Title = "Евгений Онегин", Author = "Александр Пушкин", Genre = "Роман в стихах", Year = 1832 });
                books.Add(new Book { Title = "Герой нашего времени", Author = "Михаил Ломоносов", Genre = "Роман", Year = 1839 });
                books.Add(new Book { Title = "1984", Author = "Джордж Оруэлл", Genre = "Дистопия", Year = 1949 });
                books.Add(new Book { Title = "Война и мир", Author = "Лев Толстой", Genre = "Роман", Year = 1869 });
                books.Add(new Book { Title = "Мастер и Маргарита", Author = "Михаил Булгаков", Genre = "Роман", Year = 1967 });
                books.Add(new Book { Title = "Преступление и наказание", Author = "Фёдор Достоевский", Genre = "Роман", Year = 1866 });
                books.Add(new Book { Title = "Три товарища", Author = "Эрих Мария Ремарк", Genre = "Роман", Year = 1936 });
                books.Add(new Book { Title = "Гарри Поттер и философский камень", Author = "Дж. К. Роулинг", Genre = "Фэнтези", Year = 1997 });
                books.Add(new Book { Title = "Тень ветра", Author = "Карлос Руис Сафон", Genre = "Роман", Year = 2001 });
                books.Add(new Book { Title = "Анна Каренина", Author = "Лев Толстой", Genre = "Роман", Year = 1877 });
                books.Add(new Book { Title = "Маленький принц", Author = "Антуан де Сент-Экзюпери", Genre = "Философская сказка", Year = 1943 });
                books.Add(new Book { Title = "Гарри Поттер и Принц-полукровка", Author = "Дж. К. Роулинг", Genre = "Фэнтези", Year = 2005 });
            }
            return books;
        }

    }
}

