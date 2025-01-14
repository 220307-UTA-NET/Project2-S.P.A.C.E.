﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPACE.API.SPACE.API.BusinessLogic;

namespace SPACE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        // DataContext private field
        private readonly DataContext _dataContext;

        // Constructor
        public CommentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Methods
        [HttpGet]
        public async Task<ActionResult<List<Comment>>> GetAllComments()
        {
            return await _dataContext.Comments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Comment>>> GetComment(int id)
        {
            var comment = await _dataContext.Comments.Where(comment => comment.UserId == id).ToListAsync();
            if (comment == null)
                return BadRequest("Not comments found.");
            return Ok(comment);
        }

        [HttpPost]
        public async Task<ActionResult<List<Comment>>> AddComment(Comment comment)
        {
            _dataContext.Comments.Add(comment);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Comments.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Comment>>> UpdateComment(Comment comment)
        {
            var dbComment = await _dataContext.Comments.FindAsync(comment.CommentId);
            if (dbComment == null)
                return BadRequest("Comment not found.");

            dbComment.Content = comment.Content;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Comments.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Comment>>> DeleteComment(int id)
        {
            var dbComment = await _dataContext.Comments.FindAsync(id);
            if (dbComment == null)
                return BadRequest("Comment not found.");

            _dataContext.Comments.Remove(dbComment);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Comments.ToListAsync());
        }

    }
}
