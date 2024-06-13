using Microsoft.AspNetCore.Mvc;

namespace TodoApi.TodoApi
{
    [Route("[controller]")]
    [ApiController]
    public class TodoApi : ControllerBase
    {
        private readonly TodoService _service;

        public TodoApi(TodoService service)
        {
            _service = service;
        }

        [HttpGet(Name = "p")]
        public async Task<Root[]> Get()
        {
            return await _service.GetUserTodosAsync(2);
        }
        [HttpPost(Name = "p")]
        public async Task<Res> Post()
        {
            var newItem = new Root
            {
                title = "teste",
                userId = 2,
                completed = false,
                id = 0 // O servidor provavelmente gerará um ID diferente
            };

            return await _service.CreateItemAsync(newItem);
        }


    }

}
