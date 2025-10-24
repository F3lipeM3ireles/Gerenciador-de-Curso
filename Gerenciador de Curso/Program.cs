using Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories;
using Gerenciador_de_Curso.Data.Context;
using Gerenciador_de_Curso.Data.Repositories;
using Microsoft.EntityFrameworkCore;     

namespace Gerenciador_de_Curso
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
            builder.Services.AddScoped<IInstrutorRepository, InstrutorRepository>();
            builder.Services.AddScoped<ICursoRepository, CursoRepository>();
            builder.Services.AddScoped<IAlunoCursoRepository, AlunoCursoRepository>();
            builder.Services.AddScoped<IAlunoAtividadesRepository, AlunoAtividadesRepository>();
            builder.Services.AddScoped<IAtividadesCursoRepository, AtividadesCursoRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}