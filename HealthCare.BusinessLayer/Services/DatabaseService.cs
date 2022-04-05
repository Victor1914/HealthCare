namespace HealthCare.BusinessLayer.Services
{
    using System.Linq;
   
    using DataLayer;
    using Exceptions;
    using Interfaces;
    using Utilities.Enums;
    using Utilities.Helpers;

    public class DatabaseService : IStorageService
    {
        private readonly HealthCareDbContext _dbContext;

        public DatabaseService(HealthCareDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public RoleType RetrieveUserRole(int userId, string username)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Id == userId && x.Username == username);
            ValidationUtils.ValidateAndThrow<IncorrectUserDataException>(() => user == null);

            return user.RoleType;
        }
    }
}
