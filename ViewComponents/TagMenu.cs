﻿using BlogApp.DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents
{
    public class TagMenu: ViewComponent
    {
        private ITagRepository _tagRepository;
        public TagMenu(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
                
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _tagRepository.Tags.ToListAsync());
        }
    }
}
