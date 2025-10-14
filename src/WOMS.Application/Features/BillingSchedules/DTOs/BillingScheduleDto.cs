using WOMS.Domain.Enums;

namespace WOMS.Application.Features.BillingSchedules.DTOs
{
    public class BillingScheduleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public BillingScheduleFrequency Frequency { get; set; }
        public int? DayOfWeek { get; set; }
        public int? DayOfMonth { get; set; }
        public string Time { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        // Associated templates
        public List<Guid> TemplateIds { get; set; } = new List<Guid>();
    }

    public class CreateBillingScheduleDto
    {
        public string Name { get; set; } = string.Empty;
        public BillingScheduleFrequency Frequency { get; set; } = BillingScheduleFrequency.Monthly;
        public int? DayOfWeek { get; set; }
        public int? DayOfMonth { get; set; }
        public string Time { get; set; } = string.Empty; // HH:mm
        public bool IsActive { get; set; } = true;
        public List<Guid> TemplateIds { get; set; } = new List<Guid>();
    }

    public class UpdateBillingScheduleDto
    {
        public string Name { get; set; } = string.Empty;
        public BillingScheduleFrequency Frequency { get; set; } = BillingScheduleFrequency.Monthly;
        public int? DayOfWeek { get; set; }
        public int? DayOfMonth { get; set; }
        public string Time { get; set; } = string.Empty; // HH:mm
        public bool IsActive { get; set; } = true;
        public List<Guid> TemplateIds { get; set; } = new List<Guid>();
    }
}


