namespace ORM_MVC.Models
{
    public class Book
    {
        public virtual int Id { get; set; } 
        public virtual string Title { get; set; }
        public virtual string Author { get; set; }
        public virtual string Genre { get; set; }
    }
}
 