namespace WOMS.Application.Features.Integrations.Commands.SyncIntegration
{
    public class SyncIntegrationResult
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
        public DateTime? LastSyncOn { get; set; }
        public Domain.Enums.SyncStatus? SyncStatus { get; set; }
    }
}


