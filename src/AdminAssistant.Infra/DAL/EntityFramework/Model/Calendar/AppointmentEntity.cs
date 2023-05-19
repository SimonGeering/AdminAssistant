namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Calendar;

public sealed class AppointmentEntity
{
    // Table "Calendar.Appointment"
    public int AppointmentD { get; set; } // PK
    public int AuditID { get; internal set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
}
