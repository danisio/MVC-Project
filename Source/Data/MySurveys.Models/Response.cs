namespace MySurveys.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Common;

    public class Response : DeletableEntity
    {
        private ICollection<Answer> answers;

        public Response()
        {
            this.answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public string SurveyId { get; set; }

        public virtual Survey Survey { get; set; }

        public virtual ICollection<Answer> Answers
        {
            get { return this.answers; }
            set { this.answers = value; }
        }
    }
}
