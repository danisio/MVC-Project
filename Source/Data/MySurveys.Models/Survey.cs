namespace MySurveys.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Data.Common;

    public class Survey : DeletableEntity
    {
        private ICollection<Question> questions;

        public Survey()
        {
            this.questions = new HashSet<Question>();
        }

        [Key]
        [Index]
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<Question> Questions
        {
            get { return this.questions; }
            set { this.questions = value; }
        }
    }
}