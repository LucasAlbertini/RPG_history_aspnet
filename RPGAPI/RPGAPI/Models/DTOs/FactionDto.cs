namespace RPGAPI.Models.DTOs
{
    public class FactionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string> Resources { get; set; } = new();
        public List<GoalDto> Goals { get; set; } = new();
    }
}
