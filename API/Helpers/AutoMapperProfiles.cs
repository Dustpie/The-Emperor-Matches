using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    
    public AutoMapperProfiles()
    {
        CreateMap<User, MemberDto>()
            .ForMember(destinationMember => destinationMember.MainPhotoUrl,
                options => options.MapFrom(
                    source => source.Photos.FirstOrDefault(photo => photo.IsMain)!.Url));
        CreateMap<Photo, PhotoDto>();
    }

}