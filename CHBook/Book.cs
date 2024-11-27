using System;
using System.Collections.Generic;

namespace BookStore.CHBook;

public partial class Book
{
    public int Id { get; set; }

    public string? BookName { get; set; }

    public int? Quantity { get; set; }

    public string? Publisher { get; set; }

    public string? UnitPrice { get; set; }

    public int? Price { get; set; }
}
