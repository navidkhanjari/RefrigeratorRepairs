using System.ComponentModel.DataAnnotations;

namespace RefrigeratorRepairs.MODEL.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
