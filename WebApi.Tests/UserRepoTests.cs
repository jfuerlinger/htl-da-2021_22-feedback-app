using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using FeedbackApp.Persistence;
using FeedbackApp.Persistence.Repositories;
using FeedbackApp.Core.Model;
using FeedbackApp.WebApi.Authentication;

namespace FeedbackApp.Tests
{
    [TestClass]
    public class UserRepoTests
    {
        public readonly DbContextOptions<FeedbackDbContext> dbContextOptions;

        public UserRepoTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<FeedbackDbContext>()
                .UseInMemoryDatabase(databaseName: "TestStudentRepoDb")
                .Options;
        }

        [TestMethod]
        public async Task CreateOneStudent_ShouldReturnCount1()
        {
            int userCount = 0;
            var feedbackDbContext = new FeedbackDbContext(dbContextOptions);
            UserRepository userRepository = new UserRepository(feedbackDbContext);
            UnitOfWork unitOfWork = new UnitOfWork(feedbackDbContext);

            List<User> users = new List<User>
            {
                new User { IdentityId = "001-001-001" , Role = UserRoles.pupil}
            };

            foreach (User user in users)
            {
                await userRepository.CreateUserAsync(user);
            }
            await unitOfWork.SaveChangesAsync();
            userCount = await userRepository.CountAsync();

            Assert.AreEqual(1, userCount, "One Student should be in the DB");

            // ClearDB (Other tests will be not correct when not cleared)
            feedbackDbContext.Users.RemoveRange(users);
            await unitOfWork.SaveChangesAsync();
        }

        [TestMethod]
        public async Task CreateFiveStudents_ShouldReturnCount5()
        {
            int userCount = 0;
            var feedbackDbContext = new FeedbackDbContext(dbContextOptions);
            UserRepository userRepository = new UserRepository(feedbackDbContext);
            UnitOfWork unitOfWork = new UnitOfWork(feedbackDbContext);

            List<User> users = new List<User>
            {
                new User { IdentityId = "001-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "002-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "003-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "004-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "004-001-001" , Role = UserRoles.pupil}
            };

            foreach (User user in users)
            {
                await userRepository.CreateUserAsync(user);
            }
            await unitOfWork.SaveChangesAsync();
            userCount = await userRepository.CountAsync();

            Assert.AreEqual(5, userCount, "5 Students should be in the DB");

            // ClearDB (Other tests will be not correct when not cleared)
            feedbackDbContext.Users.RemoveRange(users);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
