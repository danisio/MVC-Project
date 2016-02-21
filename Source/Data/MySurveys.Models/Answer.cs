namespace MySurveys.Models
{
    using System.ComponentModel.DataAnnotations;
    using Data.Common;

    public class Answer : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public int PossibleAnswerId { get; set; }

        public virtual PossibleAnswer PossibleAnswer { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public int ResponseId { get; set; }

        public virtual Response Response { get; set; }
    }
}
