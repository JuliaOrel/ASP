using ASP_DZ_5_Configuration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ASP_DZ_5_Configuration.Middleware
{
    public class StudentMiddleware
    {
        private readonly RequestDelegate _next;
        public Student Student { get; set; }

        public StudentMiddleware(RequestDelegate next, IOptions<Student> options)
        {
            _next = next;
            Student = options.Value;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"<p>Academy</p>");
            //stringBuilder.Append($"<p>Lastname: {Student?.Lastname}</p>");
            //stringBuilder.Append($"<p>Age: {Student?.Age}</p>");
            stringBuilder.Append("<h3>Disciplines</h3><ul>");
            foreach (string disc in Student.Disciplines)
                stringBuilder.Append($"<li>{disc}</li>");
            stringBuilder.Append("</ul>");

            //or
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(Student, jsonOptions);
            string json2 = JsonSerializer.Serialize(Student.Disciplines, jsonOptions);

            //await context.Response.WriteAsync(stringBuilder.ToString());
            if (path == "/academy")
                await context.Response.WriteAsync(stringBuilder.ToString());
            else if(path=="/home")
            await context.Response.WriteAsync(json);
            else
                await context.Response.WriteAsync("Input home or academy into address");

        }
    }
}
