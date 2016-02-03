namespace MySurveys.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PossibleAnswer
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public int NextQuestionId { get; set; }

        public virtual Question NextQuestion { get; set; }
    }
}
