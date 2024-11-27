using System.Runtime.CompilerServices;
using BookStore.CHBook;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]

public class BookController : ControllerBase
{
    private readonly ICHBookRepository cHBookRepository;

    public BookController(ICHBookRepository cHBookRepository)
    {
        this.cHBookRepository = cHBookRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetBooks()
    {
        try
        {
            return Ok(await cHBookRepository.GetBooks());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        try
        {
            var rs = await cHBookRepository.GetBook(id);
            if (rs == null) return NotFound();
            return rs;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook(Book book) {
        try {
            if (book == null) {
                return BadRequest();
            }

            var createdBook = await cHBookRepository.AddBook(book);
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
        }
        catch (Exception) {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new book record.");
        }
    }

//     [HttpPut]
// public async Task<ActionResult<Book>> UpdateBook([FromBody] Book book)
// {
//     try
//     {
//         if (book == null || book.Id == 0)
//         {
//             return BadRequest("Invalid book data.");
//         }

//         // Kiểm tra sách có tồn tại không
//         var bookToUpdate = await cHBookRepository.GetBook(book.Id);
//         if (bookToUpdate == null)
//         {
//             return NotFound($"Book with ID = {book.Id} not found.");
//         }

//         // Cập nhật sách
//         var updatedBook = await cHBookRepository.UpdateBook(book);

//         return Ok(updatedBook); // Trả về sách đã cập nhật với mã 200 OK
//     }
//     catch (Exception ex)
//     {
//         // Log lỗi nếu cần (ex.Message hoặc ex.ToString)
//         return StatusCode(StatusCodes.Status500InternalServerError, "Error updating book record.");
//     }
// }


    [HttpPut]
    public async Task<ActionResult<Book>> UpdateBook(Book book) {
        try {
            if (book.Id == null)
                return BadRequest();
            
            var bookToUpdate = await cHBookRepository.GetBook(book.Id);

            if (bookToUpdate == null) 
                return NotFound($"Book with ID = {book} not found.");
            

            return await cHBookRepository.UpdateBook(book);
        } catch (Exception) {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error updating book record.");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteBook(int id)
    {
        try
        {
            var bookToDelete = await cHBookRepository.GetBook(id);
            if (bookToDelete == null)
            {
                return NotFound($"Book with ID = {id} not found.");
            }

            await cHBookRepository.DeleteBook(id);
            return null;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting book record.");
        }
    }

}