using BookStore.CHBook;

public interface ICHBookRepository {
    Task<IEnumerable<Book>> GetBooks();
    Task<Book> GetBook(int bookID);
    Task<Book> AddBook(Book book);
    Task<Book> UpdateBook(Book book);
    Task<Book> DeleteBook(int bookID);
}