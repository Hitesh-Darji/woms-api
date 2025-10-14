using MediatR;
using WOMS.Application.Features.Workflow.DTOs;

namespace WOMS.Application.Features.Workflow.Queries.GetNodeTypes
{
    public class GetNodeTypesQuery : IRequest<List<NodeTypeInfoDto>>
    {
    }
}
