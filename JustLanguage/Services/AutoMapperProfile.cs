using AutoMapper;
using JustLanguage.DTOs;
using JustLanguage.Entities;

namespace JustLanguage.Services;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ParseRule, ParseRuleDTO>().ReverseMap();
    }
}