namespace StudentData.Interfaces
{
    public interface IContactInfoRepository
    {
        ICollection<ContactInfo> GetAllContactInfo();
        ContactInfo GetContactInfoByStudent(int studentId);
        bool ContactInfoExists(int contactInfoId);
        bool CreateContactInfo(int studentId, ContactInfo contactInfo);
        bool UpdateContactInfo(int studentId, ContactInfo contactInfo);
        bool DeleteContactInfo(ContactInfo contactInfo);
        bool Save();
    }
}
