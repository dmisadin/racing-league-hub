using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1StatsServer.Util
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
