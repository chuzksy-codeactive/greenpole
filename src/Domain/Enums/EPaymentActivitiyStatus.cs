namespace Domain.Enums
{
    public enum EPaymentActivitiyStatus
    {
        CREATED,
        REVIEWED,
        APPROVED,
        REJECTED
    }

    public enum EDividendStatus
    {
        CREATED,
        REVIEWED,
        APPROVED,
        REJECTED
    }

    public enum EPaymentStatus
    {
        PENDING,
        INPROGRESS,
        COMPLETED
    }

    public enum EDebitStatus
    {
        SUCCESSFUL,
        FAILED
    }

    public enum EPaymentGateway
    {
        NEFT,
        NIP
    }

    public enum ETransactionStatus
    {
        SUCCESSFUL,
        FAILED
    }

    public enum EPaymentEventType
    {
        DEBIT,
        CREDIT
    }
}
