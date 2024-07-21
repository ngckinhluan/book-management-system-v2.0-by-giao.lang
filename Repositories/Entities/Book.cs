using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Book
{
    public int BookId { get; set; }

    public string BookName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime PublicationDate { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public string Author { get; set; } = null!;

    public int BookCategoryId { get; set; }

    //BookCatId là FK -> Category
    //Muốn lấy cateName thì phải join 2 tbl

    //DB: Book có catId = ???

    //OOP: Cuốn sách thuộc về category. Muốn lấy catId, genre... thì . ở biến Category. Trong book có đủ info của obj category 3 field
    //Nếu có join thì join ngầm trong ram. Dev chỉ thấy obj để lấy data
    public virtual BookCategory BookCategory { get; set; } = null!;
    //cuốn sách thuộc về category
    //                          biến nói về 1 cat cụ thể trong Category. Biến này gọi là navigation path. Con đường giúp mình sang bên kia
    //Mặc định là null, ko join - lazy loading để tăng performance. Cần dùng mới báo DBContext
    //Để join, dùng hàm include để móc sang tbl bên kia
}
