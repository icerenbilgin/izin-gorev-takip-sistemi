namespace api.Dtos
{
    public class LeaveTypesDto
    {
        public int? LeaveTypeId { get; set; }
        public string? LeaveTypeName { get; set; } = string.Empty;
        public bool? IsActive { get; set; } = true;

    }
}