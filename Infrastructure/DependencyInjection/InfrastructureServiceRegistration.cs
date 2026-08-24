using Application.Interfaces;
using Application.Interfaces.AnswerGemini;
using Application.Interfaces.Auth;
using Application.Interfaces.Embedding;
using Application.Interfaces.Entity;
using Application.Interfaces.File;
using Domain.Entity.Auth;
using Domain.Repostry;
using Domain.Repostry.file;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services.Command.Auth;
using Infrastructure.Services.Command.Check;
using Infrastructure.Services.Command.EmailServices;
using Infrastructure.Services.Command.file;
using Infrastructure.Services.Query;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration )
        {
            services.AddDbContext<AppDbContext>(opation =>
            {
                opation.UseSqlServer(configuration.GetConnectionString("sql"),
                    
                    s=>s.MigrationsAssembly("Infrastructure"));
            });
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IEmbeddingService, EmbeddingService>();
            services.AddScoped<ITextChunker, TextChunker>();
            services.AddScoped<IPdfTextExtractor, PdfTextExtractor>();
            services.AddSingleton<ChunkingOptions>();
            services.AddScoped<IVectorSearchService, VectorService>();
            services.AddScoped<IGetDocumentId, GetDocumentId>();
            services.AddScoped<IGeminiService,GeminiService>();
            services.AddHttpClient();
            services.AddDefaultIdentity<Appuser>(opation =>
            {
                opation.SignIn.RequireConfirmedEmail = true;
                opation.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider; //بيولد token لللايمال
                opation.Password.RequireNonAlphanumeric = false;
                opation.Password.RequiredLength = 8;
                opation.Password.RequireLowercase = true;
                opation.Password.RequireUppercase = false;
                opation.Password.RequiredUniqueChars = 1;
                opation.Password.RequireDigit = true;

            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddAuthentication(opation =>
            {
                opation.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opation.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opation.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(opation =>
            {
                opation.SaveToken = false;
                opation.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidAudience = configuration["jwt:Audience"],
                    ValidIssuer = configuration["jwt:Issuer"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]!))
                };
            });
            return services;
        }
    }
}
