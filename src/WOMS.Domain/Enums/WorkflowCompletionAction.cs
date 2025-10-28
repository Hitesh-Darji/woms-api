namespace WOMS.Domain.Enums
{
    public enum WorkflowCompletionAction
    {
        NoAdditionalAction = 0,
        SendNotification = 1,
        CreateFollowUp = 2,
        ArchiveWorkOrder = 3
    }
}
