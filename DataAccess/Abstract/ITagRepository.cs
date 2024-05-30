using BlogApp.Entities;

namespace BlogApp.DataAccess.Abstract
{
    public interface ITagRepository
    {
        IQueryable<Tag> Tags { get; }
        void Create(Tag tag);
    }
}
