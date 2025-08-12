using System.Runtime.Versioning;

namespace RPGAPI.Models
{
    public class Faction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Resource> Resources { get; set; }
        public List<Goal> Goals { get; set; }
    }
}
