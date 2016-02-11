namespace MySurveys.Models
{
    using System.ComponentModel.DataAnnotations;
    using Data.Common;

    public class PossibleAnswer : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200), MinLength(2)]
        public string Content { get; set; }

        public virtual Question Question { get; set; }
    }
}
