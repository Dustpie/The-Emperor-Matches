using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers;

/**
 * This class is used to map the properties of the User entity to the properties of the MemberDto
 * This is done using the AutoMapper library
 */
public class AutoMapperProfiles : Profile
{
    
    public AutoMapperProfiles()
    {
        CreateMap<User, MemberDto>()
            .ForMember(dest => dest.Age, 
                options => options.MapFrom(source => source.DateOfBirth.CalculateAge()))
            .ForMember(dest => dest.PhotoUrl,
                options => options.MapFrom(
                    source => source.Photos.FirstOrDefault(photo => photo.IsMain)!.Url));
        CreateMap<Photo, PhotoDto>();
        CreateMap<MemberUpdateDTO, User>();
    }

}