namespace StudentData.Repository
{
    public class ContactInfoRepository : IContactInfoRepository
    {
        private readonly DataContext _context;

        public ContactInfoRepository(DataContext context)
        {   
            _context = context;
        }

        public bool ContactInfoExists(int contactInfoId)
        {
            return _context.ContactInfos.Any(ci => ci.Id == contactInfoId);
        }

        public bool CreateContactInfo(int studentId, ContactInfo contactInfo)
        {
            _context.Add(contactInfo);
            return Save();
        }

        public bool DeleteContactInfo(ContactInfo contactInfo)
        {
            _context.Remove(contactInfo);
            return Save();
        }

        public ICollection<ContactInfo> GetAllContactInfo()
        {
            return _context.ContactInfos
                .OrderBy(ci => ci.Id)
                .ToList();
        }

        public ContactInfo GetContactInfoByStudent(int studentId)
        {
            return _context.ContactInfos
                .Where(si => si.StudentId == studentId)
                .FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateContactInfo(int studentId, ContactInfo contactInfo)
        {
            _context.Update(contactInfo);
            return Save();
        }
    }
}
