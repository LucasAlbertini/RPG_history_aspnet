namespace RPGAPI.Models.DTOs
{
    public class GoalDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ProgressMade { get; set; }
        public int ProgressNecessary { get; set; }
    }
}
