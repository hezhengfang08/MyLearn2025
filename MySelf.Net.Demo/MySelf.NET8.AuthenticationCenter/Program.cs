
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using MySelf.AgileFramework.WebCore.JWTExtend;
using MySelf.AgileFramework.WebCore.JWTExtend;
using MySelf.AgileFramework.WebCore.JWTExtend.RSA;
using System.Security.Cryptography;
namespace MySelf.NET8.AuthenticationCenter
{
    public class Program
    {
        public static bool IsHaveTwo(string input1, string input2)
        {
            int length = input1.Length;
            if (length >= 2)
                for (int i = 0; i < length - 2; i++)
                {
                    string substr = input1.Substring(i, 2);
                    if (input2.IndexOf(substr) != -1)
                        return true;
                }

            return false;
         }
        public static void Main(string[] args)
        {
           var test1 = IsHaveTwo("abcd", "dcca");
            var test2 = IsHaveTwo("abcd", "dbca");
            var test3 = IsHaveTwo("abccde", "ccbdae");
            var test4 = IsHaveTwo("abcd", "dba");
            var test5 = IsHaveTwo("dba", "abcd");
            Console.WriteLine(test1);
            Console.WriteLine(test2);
            Console.WriteLine(test3);
            Console.WriteLine(test4);
            Console.WriteLine(test5);
        
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            #region Swagger配置
            builder.Services.AddSwaggerGen(options =>
            {
                #region Swagger配置支持Token参数传递 
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "请输入token,格式为 Bearer jwtToken(注意中间必须有空格)",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });//添加安全定义

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {   //添加安全要求
                    new OpenApiSecurityScheme
                    {
                        Reference =new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id ="Bearer"
                        }
                    },
                    new string[]{ }
                }
                });
                #endregion
            });
            #endregion
            #region RS256 对称可逆加密
            //builder.Services.AddScoped<IJWTService, JWTHSService>();
            //builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));
            #endregion
            #region RS256 非对称可逆加密，需要获取一次公钥
            //程序启动时，即初始化一组秘钥
            string keyDir = Directory.GetCurrentDirectory();
            if (!RSAHelper.TryGetKeyParameters(keyDir, true, out RSAParameters keyParams))
            {
                keyParams = RSAHelper.GenerateAndSaveKey(keyDir);
            }

            builder.Services.AddScoped<IJWTService, JWTRSService>();
            builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));
            #endregion
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
