using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MySelf.NET8.WebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private IConfiguration _iConfiguration;

        public UsersController(ILogger<UsersController> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._iConfiguration = configuration;
        }

        #region DataInit
        private List<User> _UserList = new List<User>()
        {
            new User()
            {
                Id=1,
                Account="Administrator",
                Email="57265177@qq.com",
                Name="Eleven",
                Password="1234567890",
                LoginTime=DateTime.Now,
                Role="Admin"
            },
             new User()
            {
                Id=1,
                Account="Apple",
                Email="57265177@qq.com",
                Name="Apple",
                Password="1234567890",
                LoginTime=DateTime.Now,
                Role="Admin"
            },
              new User()
            {
                Id=1,
                Account="Cole",
                Email="57265177@qq.com",
                Name="Cole",
                Password="1234567890",
                LoginTime=DateTime.Now,
                Role="Admin"
            },
        };
        private User FindUser(int id)
        {
            return this._UserList.Find(u => u.Id == id);
        }

        private IEnumerable<User> UserAll()
        {
            return this._UserList;
        }
        #endregion


        [HttpGet]
        [Route("Get")]
        public User Get(int id)
        {
            Console.WriteLine($"This is UsersController {this._iConfiguration["port"]} Invoke Get");
            var u = this.FindUser(id);
            return new User()
            {
                Id = u.Id,
                Account = u.Account + "MA",
                Name = u.Name,
                Role = $"{this._iConfiguration["ip"] ?? "192.168.3.230"}{this._iConfiguration["port"] ?? "5726"}",
                Email = u.Email,
                LoginTime = u.LoginTime,
                Password = u.Password + "K8S"
            };
        }

        [HttpGet]
        [Route("All")]
        //[Authorize]//需要授权
        //[Authorize(Roles = "Admin")]
        //[Authorize(Policy = "AdminPolicy")]
        public IEnumerable<User> Get()
        {
            Console.WriteLine($"This is UsersController {this._iConfiguration["port"] ?? "5726"} Invoke");

            return this.UserAll().Select(u => new User()
            {
                Id = u.Id,
                Account = u.Account + "MA",
                Name = u.Name,
                Role = $"{this._iConfiguration["ip"] ?? "192.168.3.230"}{this._iConfiguration["port"] ?? "5726"}",
                Email = u.Email,
                LoginTime = u.LoginTime,
                Password = u.Password + "K8S"
            });
        }

    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
