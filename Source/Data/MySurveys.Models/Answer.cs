namespace MySurveys.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Common;

    public class Answer : DeletableEntity
    {
        public Answer()
        {
            this.CreatedOn = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200), MinLength(5)]
        public string Content { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
