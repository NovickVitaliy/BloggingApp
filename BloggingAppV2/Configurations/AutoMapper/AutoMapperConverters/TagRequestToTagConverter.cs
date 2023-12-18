using AutoMapper;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.Main.Blogs;

namespace BloggingApp.Web.Configurations.AutoMapperConverters;

public class TagRequestToTagConverter : IValueConverter<IEnumerable<TagRequest>, IEnumerable<Tag>>
{
    public IEnumerable<Tag> Convert(IEnumerable<TagRequest> sourceMember, ResolutionContext context)
    {
        foreach (var tagRequest in sourceMember)
        {
            yield return new Tag() { Name = tagRequest.Name};
        }
    }
}