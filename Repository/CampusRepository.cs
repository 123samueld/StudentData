namespace StudentData.Repository
{
    public class CampusRepository : ICampusRepository
    {
        private readonly DataContext _context;

        public CampusRepository(DataContext context)
        {
            _context = context;
        }

        public bool CampusExists(int campusId)
        {
            return _context.Campuses.Any(c => c.Id == campusId);
        }

        public bool CreateCampus(Campus campus)
        {
            _context.Add(campus);
            return Save();

        }

        public bool DeleteCampus(Campus campus)
        {
            _context.Remove(campus);
            return Save();
        }

        public Campus GetCampus(int campusId)
        {
            return _context.Campuses
                .Where(c => c.Id == campusId)
                .FirstOrDefault();
        }

        public ICollection<Campus> GetCampuses()
        {
            return _context.Campuses.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCampus(Campus campus)
        {
            _context.Update(campus);
            return Save();
        }
    }
}
