using BlogApp.DataAccess.Abstract;
using BlogApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataAccess.Concrete.EfCore
{
    public class EfPostRepository : IPostRepository
    {
        private readonly BlogContext _context;
        public EfPostRepository(BlogContext context)
        {
                _context = context;
        }
        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void EditPost(Post post)
        {
            var entity= _context.Posts.FirstOrDefault(x=>x.PostId==post.PostId);
            if(entity!=null) {
                entity.Title= post.Title;
                entity.Description= post.Description;
                entity.Content= post.Content;
                entity.PostUrl= post.PostUrl;
                entity.IsActive= post.IsActive;

                _context.SaveChanges() ;
            }

        }

        public void EditPost(Post post, int[] tagIds)
        {
            var entity = _context.Posts.Include(i=>i.Tags).FirstOrDefault(x => x.PostId == post.PostId);
            if (entity != null)
            {
                entity.Title = post.Title;
                entity.Description = post.Description;
                entity.Content = post.Content;
                entity.PostUrl = post.PostUrl;
                entity.IsActive = post.IsActive;

                entity.Tags = _context.Tags.Where(i=> tagIds.Contains(i.TagId)).ToList();

                _context.SaveChanges();
            }
        }
    }
}
