namespace RPGAPI.Core.Entities
{
    public class Goal
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ProgressMade { get; set; }
        public int ProgressNecessary { get; set; }
        public int FactionId { get; set; }
        public Faction? Faction { get; set; }
    }
}
