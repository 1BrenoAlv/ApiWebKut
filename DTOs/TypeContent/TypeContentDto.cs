using ApiWebKut.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace ApiWebKut.DTOs.TypeContent
{
    public class TypeContentDto
    {
        [Key]
        public int Id { get; set; }
        public PostType Content { get; set; }
        public string Description { get; set; }
    }
    public class CreateTypeContentDto
    {
        [Required]
        public PostType Content { get; set; }

        [Required]
        public string Description { get; set; }
    }
    public class UpdateTypeContentDto
    {
        [Required]
        public PostType Content { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
