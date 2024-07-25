using System.ComponentModel.DataAnnotations;

namespace RingoMedia.DataAccess.Entities
{
    public class Reminder
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
    }
}
