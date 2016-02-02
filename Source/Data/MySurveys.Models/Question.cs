namespace MySurveys.Models
{
    using System;
    using Data.Common;

    public class Question : AuditInfo, IDeletableEntity
    {
        public Question()
        {
            this.CreatedOn = DateTime.Now;
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
