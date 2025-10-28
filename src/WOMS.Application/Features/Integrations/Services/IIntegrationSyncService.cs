using System.Threading.Tasks;

namespace WOMS.Application.Features.Integrations.Services
{
    public interface IIntegrationSyncService
    {
        Task<SyncResult> SyncAsync(string integrationName, string configuration);
    }

    public class SyncResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int ItemsProcessed { get; set; }
        public Dictionary<string, object>? Metadata { get; set; }
    }
}


