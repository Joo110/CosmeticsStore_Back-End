using AutoMapper;
using CosmeticsStore.Application.User.AddUser;
using CosmeticsStore.Application.User.Auth;
using CosmeticsStore.Application.User.UpdateUser;
using CosmeticsStore.Dtos.User;
using AppUserResponse = CosmeticsStore.Application.User.AddUser.UserResponse;
using AppAuthResponse = CosmeticsStore.Application.User.Auth.AuthResponse;

namespace CosmeticsStore.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // AddUserRequest -> AddUserCommand
            CreateMap<AddUserRequest, AddUserCommand>()
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FullName))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password))
                .ForMember(d => d.Roles, opt => opt.MapFrom(s => s.Roles));

            // RegisterUserRequest -> RegisterUserCommand
            CreateMap<RegisterUserRequest, RegisterUserCommand>()
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FullName))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password))
                .ForMember(d => d.Roles, opt => opt.MapFrom(s => s.Roles));

            // LoginRequest -> LoginCommand
            CreateMap<LoginRequest, LoginCommand>();

            // UpdateUserRequest -> UpdateUserCommand
            CreateMap<UpdateUserRequest, UpdateUserCommand>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FullName))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password))
                .ForMember(d => d.Roles, opt => opt.MapFrom(s => s.Roles));

            // Application user response -> API DTO
            CreateMap<AppUserResponse, UserResponseDto>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FullName))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.Roles, opt => opt.MapFrom(s => s.Roles))
                .ForMember(d => d.CreatedAtUtc, opt => opt.MapFrom(s => s.CreatedAtUtc))
                .ForMember(d => d.ModifiedAtUtc, opt => opt.MapFrom(s => s.ModifiedAtUtc));

            // Auth response mapping
            CreateMap<AppAuthResponse, AuthResponseDto>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FullName))
                .ForMember(d => d.Token, opt => opt.MapFrom(s => s.Token))
                .ForMember(d => d.ExpiresAtUtc, opt => opt.MapFrom(s => s.ExpiresAtUtc));
        }
    }
}
