using System.ComponentModel.DataAnnotations.Schema;

namespace Synergy.App.Data.Models
{
    public class TableMetadata : BaseModel
    {
    
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Alias { get; set; }
        public string Schema { get; set; }

    
        public bool CreateTable { get; set; }
        public string Query { get; set; }


     
    
    }
   
}
