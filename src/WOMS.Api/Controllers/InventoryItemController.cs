using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Domain.Repositories;
using WOMS.Domain.Entities;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryItemController : BaseController
    {
        private readonly IRepository<InventoryItem> _inventoryItemRepository;
        private readonly IRepository<Location> _locationRepository;

        public InventoryItemController(
            IRepository<InventoryItem> inventoryItemRepository,
            IRepository<Location> locationRepository)
        {
            _inventoryItemRepository = inventoryItemRepository;
            _locationRepository = locationRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<object>>> GetAllInventoryItems()
        {
            try
            {
                var inventoryItems = await _inventoryItemRepository.GetAllAsync();
                var result = inventoryItems.Select(item => new
                {
                    Id = item.Id,
                    PartNumber = item.PartNumber,
                    Description = item.Description,
                    Category = item.Category,
                    Manufacturer = item.Manufacturer,
                    UnitOfMeasure = item.UnitOfMeasure,
                    UnitCost = item.UnitCost,
                    IsActive = item.IsActive
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving inventory items: {ex.Message}");
            }
        }

        [HttpGet("locations")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<object>>> GetAllLocations()
        {
            try
            {
                var locations = await _locationRepository.GetAllAsync();
                var result = locations.Select(location => new
                {
                    Id = location.Id,
                    Name = location.Name,
                    Type = location.Type,
                    Address = location.Address,
                    Manager = location.Manager,
                    IsActive = location.IsActive
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving locations: {ex.Message}");
            }
        }
    }
}
