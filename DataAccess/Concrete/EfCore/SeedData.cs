using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataAccess.Concrete.EfCore
{
    public static class SeedData
    {
        public static void TestData(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
            if(context != null) 
            {
                if(context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if(!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Entities.Tag {Text="web development", TagUrl="web-development", Color= Entities.TagColor.primary},
                        new Entities.Tag {Text="ASP.Net Core", TagUrl="aspnet-core", Color = Entities.TagColor.danger },
                        new Entities.Tag {Text="MVC", TagUrl = "mvc", Color = Entities.TagColor.warning },
                        new Entities.Tag {Text="Coding", TagUrl="codeing", Color = Entities.TagColor.secondary },
                        new Entities.Tag {Text="Full Stack", TagUrl="fullstack", Color = Entities.TagColor.success}
                        );
                    context.SaveChanges();
                }
                if(!context.Users.Any()) 
                {
                    context.Users.AddRange(
                        new Entities.User { UserName="Cemalettin", UserSurname="Kemal", UserImage="User1.png", Nickname="Cemo06", Email="cemo@mail.com", Password="123456"},
                        new Entities.User { UserName="Hüsniye", UserSurname="Bıyık", UserImage="User2.png", Nickname="Hüsobıy", Email="huso@mail.com", Password = "789101" });
                    context.SaveChanges();
                }
                if(!context.Posts.Any()) 
                {
                    context.Posts.AddRange(
                        new Entities.Post { Title = "Post-1", Content = "ne güzel içerik 1", PostUrl="post1", PostImage="kurs1.jpg", PostDate = DateTime.Now.AddDays(-5), IsActive = true, Tags=context.Tags.Take(3).ToList(), UserId=1, 
                            Comments= new List<Entities.Comment> { new Entities.Comment { CommentContent = "Ne biçim içerik lan bu?!", /*CommentDate= new DateTime().AddDays(-1),*/ UserId=1 } } },

                        new Entities.Post { Title = "Post-2", Content = "ne güzel içerik 2", PostUrl = "post2", PostImage = "kurs2.jpg", PostDate = DateTime.Now.AddDays(-3), IsActive = true, Tags= context.Tags.Take(5).ToList(), UserId=2,
                            Comments = new List<Entities.Comment> { new Entities.Comment { CommentContent = "Çok güzel olmuş", /*CommentDate = new DateTime().AddDays(-5),*/ UserId = 2 } }
                        },
                        new Entities.Post { Title = "Post-3", Content = "ne güzel içerik 3", PostUrl = "post3", PostImage = "kurs3.jpg", PostDate = DateTime.Now.AddDays(-1), IsActive = false, Tags= context.Tags.Take(2).ToList(), UserId=2,
                            Comments = new List<Entities.Comment> { new Entities.Comment { CommentContent = "Gereksiz bir içerik.", /*CommentDate = new DateTime(),*/ UserId = 2 } }
                        });
                    context.SaveChanges();
                }
            }
        }
    }
}
