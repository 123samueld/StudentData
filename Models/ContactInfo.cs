namespace StudentData.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}
