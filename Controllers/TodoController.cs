using Microsoft.AspNetCore.Mvc;
using TodoApp.Interface;


namespace TodoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoServices _todoServices;

        public TodoController(ITodoServices todoServices)
        {
            _todoServices = todoServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await  _todoServices.CreateTodoAsync(request);
                return Ok(new {message = "Todo created"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {message = "An error occured while creating the todo", error = ex.Message});
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var todo = await _todoServices.GetAllAsync();
                if (todo == null || !todo.Any())
                {
                    return Ok(new {message = "No todos found"});
                }

                return Ok(new {message = "Successfully retrieved all todos"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {message = "An error occured while getting todos", error = ex.Message});
            }
        }
    }
}