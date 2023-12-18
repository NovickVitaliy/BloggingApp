using AutoMapper;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.Main.Blogs;

namespace BloggingApp.Web.Configurations.AutoMapperConverters;

public class TagRequestToTagConverter : IValueConverter<List<TagRequest>, List<Tag>>
{
    public List<Tag> Convert(List<TagRequest> sourceMember, ResolutionContext context)
    {
        var list = sourceMember.Select(request => new Tag { Name = request.Name }).ToList();

        return list;
    }
}