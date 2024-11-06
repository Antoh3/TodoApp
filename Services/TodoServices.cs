using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoApp.AppDataContext;
using TodoApp.Interface;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TodoServices : ITodoServices
    {
        private readonly TodoDbContext _context;
        private readonly ILogger<TodoServices> _logger;
        private readonly IMapper _mapper;

        public TodoServices(TodoDbContext context,ILogger<TodoServices> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task CreateTodoAsync(CreateTodoRequest request)
        {
            try
            {
                var todo = _mapper.Map<Todo>(request);
                todo.CreatedAt = DateTime.UtcNow;
                _context.Todos.Add(todo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"An error occured while creationg the todo");
                throw new Exception("An error occured while creating the todo");
            }
        }

        public Task DeleteTodoAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            var todo = await _context.Todos.ToListAsync();
            if (todo == null)
            {
                throw new Exception("No todo items found");
            }

            return todo;
        }

        public Task<Todo> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}