using System.ComponentModel.DataAnnotations.Schema;


namespace Synergy.App.Data.Models
{
    public class Template : BaseModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        [ForeignKey("TemplateCategory")] public Guid TemplateCategoryId { get; set; }
        public TemplateCategory TemplateCategory { get; set; }

        [ForeignKey("TableMetadata")] public Guid TableMetadataId { get; set; }
        public TableMetadata TableMetadata { get; set; }


        [ForeignKey("UdfTemplate")] public Guid UdfTemplateId { get; set; }
        public Template UdfTemplate { get; set; }

        [ForeignKey("UdfTableMetadata")] public Guid UdfTableMetadataId { get; set; }
        public TableMetadata UdfTableMetadata { get; set; }

        public string Json { get; set; }
        public string OtherAttachmentId { get; set; }
        public string GroupCode { get; set; }
        public Guid RecordId { get; set; }
    }
}