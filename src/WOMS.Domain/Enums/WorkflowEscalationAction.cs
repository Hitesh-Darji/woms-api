namespace WOMS.Domain.Enums
{
    public enum WorkflowEscalationAction
    {
        SendNotification = 0,
        ReassignWorkOrder = 1,
        ChangeStatus = 2,
        EscalateToHigherLevel = 3
    }
}
