namespace QTEMP.Models
{
    public class Response
    {
        public bool IsSuccess { get; set; }

        public virtual string StatusCode { get; set; }

        public virtual string Id { get; set; }

        public virtual string Description { get; set; }

        public virtual string Code { get; set; }
        
        public virtual string Name { get; set; }

        public virtual string ProfileImageUrl { get; set; }
    }
}