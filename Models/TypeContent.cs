using ApiWebKut.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace ApiWebKut.Models
{
    public class TypeContent
    {
        [Key]
        public int Id { get; set; }

        public PostType Content { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
