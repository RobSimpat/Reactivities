

var builder = WebApplication.CreateBuilder(args);

var AllowSpecificOrigins = "AllowSpecificOrigins";

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowSpecificOrigins,
    policy  =>
    {
        policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers(
    opt =>
    {
        var policy= new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        opt.Filters.Add(new AuthorizeFilter(policy));
    }
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Db.Context.ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbConnection")));
builder.Services.AddIdentityCore<Db.Models.AppUser>(opt =>{})
    .AddEntityFrameworkStores<Db.Context.ApplicationDbContext>()
    .AddSignInManager<SignInManager<Db.Models.AppUser>>();
using(ServiceProvider serviceProvider = builder.Services.BuildServiceProvider()) {

    await Database.Seed.SeedData(serviceProvider.GetRequiredService<UserManager<AppUser>>());
}

var emailConfig = builder.Configuration.GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecret secret key"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt=>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddScoped<TokenService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(AllowSpecificOrigins);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
