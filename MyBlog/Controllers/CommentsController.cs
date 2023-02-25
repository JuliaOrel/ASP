using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.Authorization;
using MyBlog.Data;
using MyBlog.Data.Entitties;
using MyBlog.Models;
using MyBlog.Models.ViewModels.SharedViewModells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<CommentsController> _logger;
        private readonly UserManager<User> _userManager;
        public CommentsController(
            ApplicationContext context,
            ILogger<CommentsController> logger,
            UserManager<User> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }


        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] CommentModel commentModel)
        {
            User currentUser = await _userManager.GetUserAsync(User); // it is HttpContext.User
            if (currentUser == null)
            {
                return Unauthorized();
            }

            string userId = currentUser.Id;

            Comment comment = new Comment
            {
                Message = commentModel.Message,
                ParentCommentId = commentModel.ParentCommentId,
                PostId = commentModel.PostId,
                Created = DateTime.Now,
                UserId = userId,
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            CommentVM commentVM = new CommentVM
            {
                Comment = comment,
                CurrentNested = commentModel.CurrentNested,
                IsReply = commentModel.IsReply,
            };

            // подгружаем сущность Post
            await _context.Entry(commentVM.Comment).Reference(c => c.Post).LoadAsync();

            return PartialView("_WriteParentCommentAndChildrenPartial", commentVM);
        }
        [Authorize(MyPolicies.AdminAndAboveAccess)]
        public async Task<IActionResult> DeleteComment([FromBody] int? commentId)
        {
            Comment? comment = await _context.Comments.FindAsync(commentId);
            if (comment is null)
            {
                return BadRequest();
            }

            await _context.Entry(comment).Collection(c => c.ChildComments).LoadAsync();
            await _context.Entry(comment).Reference(c => c.ParentComment).LoadAsync();

            _context.Comments.Remove(comment);

            int deletedCount = 1;
            await RemoveChildComments(comment); // recursion

            async Task RemoveChildComments(Comment comment)
            {
                foreach (var childComment in comment.ChildComments)
                {
                    _context.Comments.Remove(childComment);

                    deletedCount++;

                    await _context.Entry(childComment).Collection(c => c.ChildComments).LoadAsync();
                    // await _context.Entry(childComment).Reference(c => c.ParentComment).LoadAsync();

                    if (childComment.ChildComments.Count > 0)
                    {
                        await RemoveChildComments(childComment);

                    }
                }
            }
            await _context.SaveChangesAsync();

            return Ok(deletedCount);
        }

       
    }

}
