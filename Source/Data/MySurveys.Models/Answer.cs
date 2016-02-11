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

        public int SurveyId { get; set; }

        public virtual Survey Survey { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public int PossibleAnswerId { get; set; }

        public virtual PossibleAnswer PossibleAnswer { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
