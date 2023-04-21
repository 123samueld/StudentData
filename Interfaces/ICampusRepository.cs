namespace StudentData.Interfaces
{
    public interface ICampusRepository
    {
        ICollection<Campus> GetCampuses();
        Campus GetCampus(int campusId);
        bool CreateCampus(Campus campus);
        bool UpdateCampus(Campus campus);
        bool DeleteCampus(Campus campus);
        bool CampusExists(int campusId);
        bool Save();
    }
}
