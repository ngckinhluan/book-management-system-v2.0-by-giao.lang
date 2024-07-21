using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
	public class BookRepository 
	{
		//class này sẽ gọi DBContext để crud trên đúng Book
		//Chính là Cabinet<Book> như đã học
		//Insert trực tiếp xuống DB
		//Mô hình thực thi
		//GUI -> service -> repositories -> dbcontext -> db
		//1			2			3
		//3-layer architecture
		private BookManagementDbContext _context;
		//Mỗi lần dùng cho crud mới new
		public List<Book> GetBooks()
		{
			_context = new BookManagementDbContext();
			return _context.Books.Include(t => t.BookCategory).ToList();
			//Gửi vào tên biến obj móc sang category
		}

		//Tên hàm ở repos nên đặt gần với tác vụ trong DB
		public void Add(Book book)
		{
			_context = new();
			_context.Books.Add(book);
			_context.SaveChanges();
		}

		public void Update(Book book)
		{
			_context = new();
			_context.Books.Update(book);
			_context.SaveChanges();
		}

		public void Delete(Book book) {
			_context = new();
			_context.Books.Remove(book);
			_context.SaveChanges();
		}

	    public List<Book> FindBooksByName(string name)
		{
			_context = new();
            var books = _context.Books
                        .Where(b => b.BookName.Contains(name)) 
                        .ToList();
            return books;
		}

        public List<Book> FindBooksByDescription(string name)
        {
            _context = new();
            var books = _context.Books
                        .Where(b => b.Description.Contains(name))
                        .ToList();
            return books;
        }

       



    }
}
