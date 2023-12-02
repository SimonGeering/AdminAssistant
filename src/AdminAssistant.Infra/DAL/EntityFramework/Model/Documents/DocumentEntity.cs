using AdminAssistant.Modules.DocumentsModule;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Documents;

public sealed class DocumentEntity : IMapFrom<Document>, IMapTo<Document>
{
    public int DocumentID { get; set; }
    public int AuditID { get; internal set; }
    public int OwnerID { get; internal set; }
    public string FileName { get; set; } = string.Empty;

    public Core.AuditEntity Audit { get; internal set; } = null!;

    public void MapFrom(AutoMapper.Profile profile) => profile
        .CreateMap<Document, DocumentEntity>()
        .ForMember(x => x.OwnerID, opt => opt.Ignore())
        .ForMember(x => x.AuditID, opt => opt.Ignore())
        .ForMember(x => x.Audit, opt => opt.Ignore());
}
