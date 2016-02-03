namespace MySurveys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Data.Common;

    public class Question : DeletableEntity
    {
        private ICollection<PossibleAnswer> possibleAnswers;
        private ICollection<string> answers;

        public Question()
        {
            this.CreatedOn = DateTime.Now;
            this.possibleAnswers = new HashSet<PossibleAnswer>();
            this.answers = new HashSet<string>();
        }

        [Key]
        [Index]
        public int Id { get; set; }

        [Required]
        [StringLength(200), MinLength(3)]
        public string Title { get; set; }

        public virtual ICollection<PossibleAnswer> PossibleAnswers
        {
            get { return this.possibleAnswers; }
            set { this.possibleAnswers = value; }
        }

        public virtual ICollection<string> Answers
        {
            get { return this.answers; }
            set { this.answers = value; }
        }
    }
}
