using Microsoft.EntityFrameworkCore;

namespace gpdSW.repositories
{
    public class Repository<T> where T : class
    {
        private readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        //CRUD
        public void Insert(T entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }
        public IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }
        public T? Get(object id)
        {
            return context.Find<T>(id);
        }
        public void Update(T entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            context.Remove(entity);
            context.SaveChanges();
        }
    }
}
