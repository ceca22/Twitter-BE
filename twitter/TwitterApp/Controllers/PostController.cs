using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TwitterApp.Models.Post;
using TwitterApp.Services.Interfaces;
using TwitterApp.Shared.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwitterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        // GET: api/<PostController>
        [HttpGet]
        public ActionResult<IEnumerable<PostModel>> Get()
        {
            try
            {
                return _postService.GetAllEntities();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getall/{take}/{skip}")]
        public ActionResult<IEnumerable<PostModel>> Get(int take, int skip)
        {
            try
            {
                return _postService.GetAll(take,skip);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //current user posts
        [HttpGet("current-user-posts/{id}")]
        public ActionResult<IEnumerable<PostModel>> GetCurrentUserPosts(string id)
        {
            try
            {
                //Claim nameIdentifier = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                //string id = nameIdentifier.Value;

                return _postService.CurrentUserPosts(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public ActionResult<PostModel> Get(int id)
        {
            try
            {

                return StatusCode(StatusCodes.Status200OK, _postService.GetEntityById(id));
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<PostController>
        [HttpPost("{id}")]
        public ActionResult Post(string id, [FromBody] PostModel post)
        {
            try
            {
                //Claim nameIdentifier = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                //string id = nameIdentifier.Value;

                _postService.AddEntity(post, id);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (PostException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<PostController>
        [HttpPost("post/{id}")]
        public ActionResult PostNormalId(string id, [FromBody] PostModel post)
        {
            try
            {
                //Claim nameIdentifier = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                //string id = nameIdentifier.Value;



                _postService.AddEntityNormalId(post, id);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (PostException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch(UserException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] PostModel post)
        {
            try
            {
                _postService.UpdateEntity(post);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _postService.DeleteEntity(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
