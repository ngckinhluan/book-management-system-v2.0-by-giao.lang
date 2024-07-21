using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Entities;

namespace Services
{
    public class BookServices
    {
        //gui -> services -> repos
        //		CRUD RAM	CRUD TBL
        //		|method| >	|method|
        //class này là 1 dạng cabinet của book trong RAM, cần repos giúp đi xuống DB
        //Giao tiếp với GUI để đồng bộ
        private BookRepository _repository = new();
        //Method bên Service đặt tên dễ hiểu
        public List<Book> GetAllBooks() => _repository.GetBooks();
        //Tên hàm đặt gần với người dùng hơn
        public void AddBook(Book book) => _repository.Add(book);

        public void UpdateBook(Book book)
        {
            _repository.Update(book);
        }

        public void DeleteBook(Book book)
        {
            _repository.Delete(book);
        }

        public List<Book> FindBooksByName(string name)
        {
            return _repository.FindBooksByName(name);
        }

        public List<Book> FindBooksByDescription(string description)
        {
            return _repository.FindBooksByDescription(description);
        }

        //public List<Book> SearchBooks(string bookName, string description)
        //{
        //    return _repository.SearchBooks(bookName, description);
        //}


        // Search làm ở service vì nó cần xử lý nhiều thứ
        // hàm Search trả về nhiều Book
        public List<Book> SearchByNameAndDescription(string name, string desc)
        {
            //name = name.ToLower();
            //desc = desc.ToLower();
            return _repository.GetBooks().Where(b =>
                b.BookName.ToLower().Contains(name.ToLower()) &&
                b.Description.ToLower().Contains(desc.ToLower())
            ).ToList();

        }

        // trong trường hợp đề bài đòi search OR!!!

        //public List<Book> SearchByNameOrDescription(string name, string desc)
        //{
        //    //name = name.ToLower();
        //    //desc = desc.ToLower();
        //    return _repository.GetBooks().Where(b =>
        //        b.BookName.ToLower().Contains(name.ToLower()) ||
        //        b.Description.ToLower().Contains(desc.ToLower())
        //    ).ToList();
        //}





    }
}
