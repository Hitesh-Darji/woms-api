using System.ComponentModel.DataAnnotations;

namespace WOMS.Application.Features.View.DTOs
{
    public class CreateViewDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public List<string> SelectedColumns { get; set; } = new List<string>();
    }

    public class ViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string> SelectedColumns { get; set; } = new List<string>();
    }
}
