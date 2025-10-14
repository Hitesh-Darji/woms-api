namespace WOMS.Domain.Enums
{
    public enum WorkflowCompletionAction
    {
        NoAdditionalAction = 0,
        SendCompletionNotification = 1,
        ArchiveWorkOrder = 2,
        GenerateCompletionReport = 3
    }
}
