using AutoMapper;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

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
           CreateMap<Notification,NotificationViewModel>().ReverseMap();
           CreateMap<Workflow,WorkflowViewModel>().ReverseMap();
           CreateMap<Leave,LeaveViewModel>().ReverseMap();
           CreateMap<User, UserViewModel>().ReverseMap();
			CreateMap<Template, TemplateViewModel>().ReverseMap();
			CreateMap<TableMetadata, TableMetadataViewModel>().ReverseMap();
			CreateMap<ColumnMetadata, ColumnMetadataViewModel>().ReverseMap();
		}

    }
}


