using Repositories;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class CategoriesServices
	{
		private CategoriesRepository _repository = new();

		//Chuẩn bị đổ lên drop down
		public List<BookCategory> GetAllCategories() => _repository.GetAll();
	}
}
