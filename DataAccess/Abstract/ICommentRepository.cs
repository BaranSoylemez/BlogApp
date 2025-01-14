﻿using BlogApp.Entities;

namespace BlogApp.DataAccess.Abstract
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }
        void CreateComment(Comment comment);    

    }
}
