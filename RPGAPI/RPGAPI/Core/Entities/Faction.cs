using System.Runtime.Versioning;

namespace RPGAPI.Core.Entities
{
    public class Faction
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string> Resources { get; set; } = new List<string>();
        public List<Goal> Goals { get; set; } = new List<Goal>();
    }
}
