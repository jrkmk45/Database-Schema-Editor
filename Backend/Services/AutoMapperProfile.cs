using AutoMapper;
using Domain.Constants;
using Domain.Models;
using Services.Dtos.Attribute;
using Services.Dtos.Connection;
using Services.Dtos.Scheme;
using Services.Dtos.Table;
using Services.Dtos.User;
using Attribute = Domain.Models.Attribute;

namespace Repository
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateSchemeDTO, Scheme>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<Scheme, SchemeDTO>();

            CreateMap<CreateTableDTO, Table>();

            CreateMap<Table, TableDTO>();

            CreateMap<CreateAttributeDTO, Attribute>()
                .ForMember(dest => dest.DataTypeId, opt => opt.MapFrom(src => src.DataTypeId));

            CreateMap<CreateConnectionDTO, Connection>();

            CreateMap<PatchTableDTO, Table>()
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrEmpty(src.Name));
                    opt.MapFrom(src => src.Name);
                })
                .ForMember(dest => dest.X, opt =>
                {
                    opt.PreCondition(src => src.X != null);
                    opt.MapFrom(src => src.X);
                })
                .ForMember(dest => dest.Y, opt =>
                {
                    opt.PreCondition(src => src.Y != null);
                    opt.MapFrom(src => src.Y);
                })
                .ForMember(dest => dest.Attributes, opt =>
                {
                    opt.MapFrom(src => src.Attributes);
                })
                .ForAllMembers(dest => dest.Condition((src, dest, srcMember, destMember) => srcMember != null));


            CreateMap<Connection, ConnectionDTO>();

            CreateMap<Attribute, AttributeDTO>()
                .ForMember(dest => dest.DataType, opt => opt.MapFrom(src => src.DataType.Name))
                .ForMember(dest => dest.DataTypeId, opt => opt.MapFrom(src => src.DataTypeId));
                

            CreateMap<AttributeDTO, Attribute>()
                .ForMember(dest => dest.DataTypeId, opt => opt.MapFrom(src => src.DataTypeId))
                .ForMember(dest => dest.DataType, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.ConnectionsTo, opt => opt.Ignore())
                .ForMember(dest => dest.ConnectionsFrom, opt => opt.Ignore());

            
            CreateMap<string, DataType>()
                .ForMember(dest => dest.Name, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue());

            CreateMap<Scheme, SchemeListItemDTO>();
           

            CreateMap<RegisterUserDTO, User>();

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.ProfilePicture, opt =>
                {
                    opt.PreCondition(src => src.ProfilePicture != null);
                    opt.MapFrom(src => FilePaths.AvatarsPaths + src.ProfilePicture);
                });

            CreateMap<int, User>().ForMember(dest => dest.Id, opt => opt.MapFrom(e => e));

        }
    }
}
