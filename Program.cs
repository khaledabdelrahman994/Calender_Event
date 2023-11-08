
using Event_Calender.Services;
using Microsoft.Extensions.Options;

namespace Event_Calender
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IGoogleCalendarService, GoogleCalendarService>();
            //builder.Services.AddCors(option => option.AddPolicy("myname",builder => {
            //    builder.AllowAnyOrigin();
            //}));
            builder.Services.Configure<GoogleCalenderSetting>(builder.Configuration.GetSection(nameof(GoogleCalenderSetting)));
            builder.Services.AddSingleton<IGoogleCalenderSetting>(s => s.GetRequiredService<IOptions<GoogleCalenderSetting>>().Value);
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();


            app.MapControllers();

            app.Run();
        }
    }
}