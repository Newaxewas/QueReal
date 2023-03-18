namespace QueReal.DAL.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }

        public DateTime? DeletedTime { get; set; }
    }
}
