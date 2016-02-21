namespace MySurveys.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Data.Common;

    public class Question : DeletableEntity
    {
        private ICollection<PossibleAnswer> possibleAnswers;
        private ICollection<Answer> answers;

        public Question()
        {
            this.possibleAnswers = new HashSet<PossibleAnswer>();
            this.answers = new HashSet<Answer>();
        }

        [Key]
        [Index]
        public int Id { get; set; }

        [Required]
        [StringLength(200), MinLength(2)]
        public string Content { get; set; }

        public int SurveyId { get; set; }

        public virtual Survey Survey { get; set; }

        public string  ParentContent { get; set; }

        public bool IsDependsOn { get; set; }

        public virtual ICollection<PossibleAnswer> PossibleAnswers
        {
            get { return this.possibleAnswers; }
            set { this.possibleAnswers = value; }
        }

        public virtual ICollection<Answer> Answers
        {
            get { return this.answers; }
            set { this.answers = value; }
        }
    }
}
