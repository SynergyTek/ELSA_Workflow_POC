using AutoMapper;
using Synergy.App.Data.Model;
using Synergy.App.Data.ViewModel;

namespace Synergy.App.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapModel();
        }


        private void MapModel()
        {
            CreateMap<WorkflowModel, WorkflowViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<TemplateModel, TemplateViewModel>().ReverseMap();
            CreateMap<TableModel, TableViewModel>().ReverseMap();
            CreateMap<ColumnModel, ColumnViewModel>().ReverseMap();
        }
    }
}