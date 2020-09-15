using AdminAssistant.DomainModel.Modules.DocumentsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Documents
{
    public class DocumentEntity : IMapFrom<Document>, IMapTo<Document>
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

        // TODO: Resolve mapping of properties from DocumentEntity to Document
        //public void MapTo(AutoMapper.Profile profile) => profile
        //    .CreateMap<DocumentEntity, Document>()
        //    .ForMember(x => x.PayeeName, opt => opt.Ignore())
        //    .ForMember(x => x.Symbol, opt => opt.Ignore())
        //    .ForMember(x => x.DecimalFormat, opt => opt.Ignore())
        //    .ForMember(x => x.Balance, opt => opt.Ignore());
    }
}
