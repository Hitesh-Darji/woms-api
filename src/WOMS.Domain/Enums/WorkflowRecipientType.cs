namespace WOMS.Domain.Enums
{
    public enum WorkflowRecipientType
    {
        WorkflowCreator = 0,
        CurrentAssignee = 1,
        SpecificUser = 2,
        RoleBased = 3,
        ExternalEmail = 4
    }
}
