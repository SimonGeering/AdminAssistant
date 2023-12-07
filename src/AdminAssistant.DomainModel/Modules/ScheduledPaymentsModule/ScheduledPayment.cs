namespace AdminAssistant.Modules.ScheduledPaymentsModule;

public sealed record ScheduledPayment : IPersistable
{
    public ScheduledPaymentId ScheduledPaymentId { get; set; } = ScheduledPaymentId.Default;

    public string PaymentName { get; set; } = string.Empty;

    public Id PrimaryKey => ScheduledPaymentId;
}
public sealed record ScheduledPaymentId(int Value) : Id(Value)
{
    public static ScheduledPaymentId Default => new(Constants.UnknownRecordID);
}
