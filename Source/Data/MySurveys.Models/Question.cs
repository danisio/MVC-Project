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

        public Question()
        {
            this.CreatedOn = DateTime.Now;
            this.possibleAnswers = new HashSet<PossibleAnswer>();
        }

        [Key]
        [Index]
        public int Id { get; set; }

        [Required]
        [StringLength(200), MinLength(3)]
        public string Content { get; set; }

        public int SurveyId { get; set; }

        public virtual Survey Survey { get; set; }

        public int? ParentPossibleAnswerId { get; set; }

        public bool IsDependsOn { get; set; }

        public virtual ICollection<PossibleAnswer> PossibleAnswers
        {
            get { return this.possibleAnswers; }
            set { this.possibleAnswers = value; }
        }
    }
}
