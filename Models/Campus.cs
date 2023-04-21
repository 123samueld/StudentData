namespace StudentData.Models
{
    public class Campus
    {
        public int Id { get; set; }
        public string CampusName { get; set; }
        public string Factulty { get; set; }
        public string CampusAddress { get; set; }
        public ICollection<Student> Students { get; set; }

    }
}
