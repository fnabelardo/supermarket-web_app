using System.Net.Mime;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", (HttpContext context) =>
{
    string html = @"<html>
                        <body>
                            <h1> Hello World!</h1>
                            <br/>
                            Welcome to this new World!
                        </body>
                    </html>";
    WritenHtml(context, html);
});

app.Run();

void WritenHtml(HttpContext context, string html)
{
    context.Response.ContentType = MediaTypeNames.Text.Html;
    context.Response.ContentLength = Encoding.UTF8.GetByteCount(html);
    context.Response.WriteAsync(html);
}