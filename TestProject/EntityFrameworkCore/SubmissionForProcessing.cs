namespace EntityFrameworkCore
{
    using MongoDB.Bson;

    public class SubmissionForProcessing : IEntity<ObjectId>
    {
        public ObjectId Id { get; set; }

        public int SubmissionId { get; set; }

        public bool Processing { get; set; }
        
        public bool Processed { get; set; }
    }
}
