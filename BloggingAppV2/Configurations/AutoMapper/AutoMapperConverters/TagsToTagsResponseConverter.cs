using AutoMapper;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.Main.Blogs;

namespace BloggingApp.Web.Configurations.AutoMapperConverters;

public class TagsToTagsResponseConverter : IValueConverter<IEnumerable<Tag>, IEnumerable<TagResponse>>
{
    public IEnumerable<TagResponse> Convert(IEnumerable<Tag> sourceMember, ResolutionContext context)
    {
        return sourceMember.Select(tag => new TagResponse() { Name = tag.Name });
    }
}