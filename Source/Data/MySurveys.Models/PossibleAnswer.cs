namespace MySurveys.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Common;

    public class PossibleAnswer : DeletableEntity
    {
        public PossibleAnswer()
        {
            this.CreatedOn = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200), MinLength(2)]
        public string Content { get; set; }

        [Required]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
