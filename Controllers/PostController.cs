using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Concrete.EfCore;
using BlogApp.Entities;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace BlogApp.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository _repository;
        private ICommentRepository _crepository;
        private ITagRepository _tagRepository;
        
        public PostController(IPostRepository repository, ICommentRepository crepository, ITagRepository tagRepository)
        {
            _repository = repository;
            _crepository = crepository;
            _tagRepository = tagRepository;
            
        }
        public async Task<IActionResult> Index(string tag)
        {
            var claims = User.Claims;
            var posts = _repository.Posts.Where(x=>x.IsActive==true);

            if (!string.IsNullOrEmpty(tag))
            {
                posts=posts.Where(x=>x.Tags.Any(t=>t.TagUrl == tag));
            }

            return View(
                new PostViewModel
                {
                    Posts= await posts.ToListAsync()
                    
                });
        }

        public async Task<IActionResult> Details(string PostUrl)
        {
            return View(await _repository.Posts.Include(p=>p.Tags).Include(a => a.User).Include(c=>c.Comments).ThenInclude(u=>u.User).FirstOrDefaultAsync(x=>x.PostUrl == PostUrl));
        }

        [HttpPost]
        public IActionResult AddComment(int PostId, string CommentContent, string PostUrl)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           

            var entity = new Comment
            {
                CommentContent = CommentContent,
                CommentDate = DateTime.Now,
                PostId = PostId,
                UserId = int.Parse(userId ?? ""),
                              
            };
            _crepository.CreateComment(entity);
            return Redirect("/posts/details/" + PostUrl);
        }

        [Authorize]
        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost(CreatePostViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _repository.CreatePost(new Post
                {
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    PostUrl = model.PostUrl,
                    PostDate = DateTime.Now,
                    UserId= int.Parse(userid ?? ""),
                    PostImage = "kurs2.jpg",
                    IsActive=false

                });
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "İçerik bilgisine ulaşılamıyor!");
                return View(model);
            }

            
            
            
        }
        public async Task<IActionResult> ListPost()
        {
            var userid =int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var role = User.FindFirstValue(ClaimTypes.Role);

            var postlist= _repository.Posts;
            if (string.IsNullOrEmpty(role))
            {
                postlist = postlist.Where(x => x.UserId == userid);
            }
            return View(await postlist.ToListAsync());

        }

        [Authorize]
        public async Task<IActionResult> EditPost(int id)
        {
            ViewBag.Tags= _tagRepository.Tags.ToList();

            return View(await _repository.Posts.Include(t=>t.Tags).FirstOrDefaultAsync(x=>x.PostId==id));

        }

        [Authorize]
        [HttpPost]
        public  IActionResult EditPost(Post model, int[] tagIds)
        {
            var tobeupdated = new Post
            {
                PostId = model.PostId,
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                PostUrl = model.PostUrl,
                IsActive = model.IsActive
            };
            _repository.EditPost(tobeupdated, tagIds);
            return RedirectToAction("ListPost");

        }
    }
}
