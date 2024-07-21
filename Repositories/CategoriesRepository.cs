using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
	public class CategoriesRepository
	{
		//Cabinet chỉ chơi với categories
		//Kỹ thuật mà class lo riêng phần nó gọi là single responsibility
		//Từ đầu trong solid

		//Ko new
		private BookManagementDbContext _context;
		public List<BookCategory> GetAll()
		{
			_context = new BookManagementDbContext();
			return _context.BookCategories.ToList();
		}
	}
}
