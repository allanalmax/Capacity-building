using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Models;
using MyFirstApi.Services;

namespace MyFirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostService _postService;

        public PostsController()
        {
            _postService = new PostService();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Post>>> GetPost(int id)
        {
            var post = await _postService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            await _postService.CreatePost(post);
            
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> UpdatePost(int id, Post post)
        {
            if (post.Id != id)
            {
                return BadRequest();
            }
            
            var updatedPost = await _postService.UpdatePost(id, post);
            if (updatedPost == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeletePost(int id)
        {
            await _postService.DeletePost(id);

            return Ok();
        }
    }
}
