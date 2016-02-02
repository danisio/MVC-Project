namespace MySurveys.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Common;

    public class Question : DeletableEntity
    {
        public Question()
        {
            this.CreatedOn = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200), MinLength(3)]
        public string Title { get; set; }
    }
}
