using Microsoft.EntityFrameworkCore;
namespace Repositories
{
    public class GenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        
        public T Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges(); 
            return entity;
        }


        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

       
        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges(); 
        }

        public void Remove(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            _context.SaveChanges(); 
        }
    }
}
