using Infrastructure.AutomapperProfile;
using Infrastructure.Context;
using Infrastructure.Services.AttachmentService.AttachmentService;
using Infrastructure.Services.AttachmentService.FileService;
using Infrastructure.Services.ContactService;
using Infrastructure.Services.GroupMemberService;
using Infrastructure.Services.GroupService;
using Infrastructure.Services.MassageService;
using Infrastructure.Services.StatusService;
using Infrastructure.Services.UserService;
using Infrastructure.Services.UserService.PhotoService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IGroupMemberService, GroupMemberService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(conf => conf.UseNpgsql(connection));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(ServiceProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
