namespace MySurveys.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Common;

    public class Survey : DeletableEntity
    {
        private ICollection<Question> questions;
        private ICollection<Response> responses;

        public Survey()
        {
            this.questions = new HashSet<Question>();
            this.responses = new HashSet<Response>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100), MinLength(3)]
        public string Title { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public bool IsPublic { get; set; }

        public virtual ICollection<Question> Questions
        {
            get { return this.questions; }
            set { this.questions = value; }
        }

        public virtual ICollection<Response> Responses
        {
            get { return this.responses; }
            set { this.responses = value; }
        }
    }
}