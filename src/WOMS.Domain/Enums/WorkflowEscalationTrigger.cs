namespace WOMS.Domain.Enums
{
    public enum WorkflowEscalationTrigger
    {
        TimeElapsed = 0,
        NoResponse = 1,
        MissedDeadline = 2,
        ConditionMet = 3
    }
}
