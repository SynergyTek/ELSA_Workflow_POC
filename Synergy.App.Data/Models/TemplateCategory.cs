
using System.ComponentModel.DataAnnotations.Schema;


namespace Synergy.App.Data.Models
{
    public class TemplateCategory : BaseModel
    {
      
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentId { get; set; }
       

     
        public string IconFileId { get; set; }
        public string[] AllowedPortalIds { get; set; }
    }
}
