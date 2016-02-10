namespace MySurveys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Common;

    public class Survey : DeletableEntity
    {
        private ICollection<Question> questions;
        //// private ICollection<Answer> answers;

        public Survey()
        {
            this.CreatedOn = DateTime.Now;
            this.questions = new HashSet<Question>();
            //// this.answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100), MinLength(3)]
        public string Title { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<Question> Questions
        {
            get { return this.questions; }
            set { this.questions = value; }
        }

        //// public virtual ICollection<Answer> Answers
        //// {
        ////    get { return this.answers; }
        ////    set { this.answers = value; }
        ////}
    }
}