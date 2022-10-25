namespace QueReal.DAL.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
