using BloggingApp.Web.DataBase;
using BloggingApp.Web.Models.Main;
using BloggingApp.Web.Repositories;
using BloggingApp.Web.ServicesContracts;

namespace BloggingApp.Web.Services;

public class MailBoxRepository : BaseRepository<MailBox>, IMailBoxRepository 
{
    public MailBoxRepository(ApplicationDbContext context) : base(context)
    { }
}