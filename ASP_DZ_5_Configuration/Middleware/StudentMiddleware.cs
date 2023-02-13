using ASP_DZ_5_Configuration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"<p>Name: {Student?.Name}</p>");
            stringBuilder.Append($"<p>Lastname: {Student?.Lastname}</p>");
            stringBuilder.Append($"<p>Age: {Student?.Age}</p>");
            stringBuilder.Append("<h3>Disciplines</h3><ul>");
            foreach (string disc in Student.Disciplines)
                stringBuilder.Append($"<li>{disc}</li>");
            stringBuilder.Append("</ul>");

            await context.Response.WriteAsync(stringBuilder.ToString());
        }
    }
}
