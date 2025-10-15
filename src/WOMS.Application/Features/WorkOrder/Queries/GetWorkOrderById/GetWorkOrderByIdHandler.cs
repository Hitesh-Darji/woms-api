using AutoMapper;
using MediatR;
using WOMS.Application.Features.WorkOrder.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.WorkOrder.Queries.GetWorkOrderById
{
    public class GetWorkOrderByIdHandler : IRequestHandler<GetWorkOrderByIdQuery, WorkOrderDto?>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly AutoMapper.IMapper _mapper;

        public GetWorkOrderByIdHandler(IWorkOrderRepository workOrderRepository, AutoMapper.IMapper mapper)
        {
            _workOrderRepository = workOrderRepository;
            _mapper = mapper;
        }

        public async Task<WorkOrderDto?> Handle(GetWorkOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var workOrder = await _workOrderRepository.GetByIdAsync(request.Id, cancellationToken);
            
            if (workOrder == null)
            {
                return null;
            }

            return _mapper.Map<WorkOrderDto>(workOrder);
        }
    }
}
