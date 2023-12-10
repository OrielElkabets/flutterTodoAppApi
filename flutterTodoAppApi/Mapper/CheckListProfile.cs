using AutoMapper;
using flutterTodoAppApi.Data.DTO.CheckList;
using flutterTodoAppApi.Data.Entities;

namespace flutterTodoAppApi.Mapper
{
    public class CheckListProfile : Profile
    {
        public CheckListProfile()
        {
            CreateMap<CheckListItemEO, CheckListItemDTO>().ReverseMap();
            CreateMap<CheckListEO, CheckListDTO>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom((src, _, _, context) => src.CheckListItems.Select(context.Mapper.Map<CheckListItemEO>).ToList()));
            CreateMap<CheckListDTO, CheckListEO>()
                .ForMember(dest => dest.CheckListItems, opt => opt.MapFrom((src, _, _, context) => src.Items.Select(context.Mapper.Map<CheckListItemDTO>).ToList()));

        }
    }
}
