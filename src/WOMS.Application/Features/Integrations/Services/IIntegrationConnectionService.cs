namespace WOMS.Application.Features.Integrations.Services
{
    public interface IIntegrationConnectionService
    {
        Task<bool> ValidateConfigurationAsync(string integrationName, string configuration);
        Task ConnectAsync(string integrationName, string configuration);
    }
}

