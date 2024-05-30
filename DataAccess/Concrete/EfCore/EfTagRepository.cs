using BlogApp.DataAccess.Abstract;
using BlogApp.Entities;

namespace BlogApp.DataAccess.Concrete.EfCore
{
    public class EfTagRepository : ITagRepository
    {
        private BlogContext _context;
        public EfTagRepository(BlogContext context)
        {
            _context = context;  
        }
        public IQueryable<Tag> Tags => _context.Tags;

        public void Create(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }
    }
}
