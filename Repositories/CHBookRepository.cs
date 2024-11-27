using BookStore.CHBook;
using Microsoft.EntityFrameworkCore;

public class CHBookRepository : ICHBookRepository{
    private readonly BookStoreContext bookStoreContext;

    public CHBookRepository(BookStoreContext bookStoreContext){
        this.bookStoreContext = bookStoreContext;
    }

    public async Task<IEnumerable<Book>> GetBooks(){
        return await bookStoreContext.Books.ToListAsync();
    }

    public async Task<Book> GetBook(int bookID){
        return await bookStoreContext.Books.FirstOrDefaultAsync(e=>e.Id==bookID);
    }

    public async Task<Book> AddBook(Book book){
        var rs = await bookStoreContext.Books.AddAsync(book);
        await bookStoreContext.SaveChangesAsync();
        return rs.Entity;
    }

    public async Task<Book> UpdateBook(Book book){
        var rs = await bookStoreContext.Books.FirstOrDefaultAsync(e=>e.Id==book.Id);
        
        if(rs != null){
            rs.BookName = book.BookName;
            rs.Quantity = book.Quantity;
            rs.Publisher = book.Publisher;
            rs.UnitPrice = book.UnitPrice;
            rs.Price = book.Price;
        }
        await bookStoreContext.SaveChangesAsync();

        return rs;
    }

    public async Task<Book> DeleteBook(int bookID){
        var rs = await bookStoreContext.Books.FirstOrDefaultAsync(e=>e.Id==bookID);

        if(rs != null){
            bookStoreContext.Books.Remove(rs);
            await bookStoreContext.SaveChangesAsync();
            return rs;
        }

        return null;
    }
}