using AutoMapper;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.Main.Blogs;

namespace BloggingApp.Web.Configurations.AutoMapperConverters;

public class TagToTagRequestConverter : IValueConverter<List<Tag>, List<TagRequest>>
{
    public List<TagRequest> Convert(List<Tag> sourceMember, ResolutionContext context)
    {
        var tagRequests = sourceMember.Select(tag => new TagRequest() { Name = tag.Name }).ToList();

        return tagRequests;
    }
}