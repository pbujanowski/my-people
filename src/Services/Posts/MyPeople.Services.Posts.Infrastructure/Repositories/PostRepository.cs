using MyPeople.Services.Posts.Application.Repositories;
using MyPeople.Services.Posts.Domain.Entities;
using MyPeople.Services.Posts.Infrastructure.Data;

namespace MyPeople.Services.Posts.Infrastructure.Repositories;

public class PostRepository(ApplicationDbContext dbContext)
    : RepositoryBase<Post>(dbContext),
        IPostRepository { }
