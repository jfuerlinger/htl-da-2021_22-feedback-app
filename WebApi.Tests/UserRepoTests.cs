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
        public async Task CreateOneUser()
        {
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
            int userCount = await userRepository.CountAllAsync();

            // ClearDB (Other tests will be not correct when not cleared)
            feedbackDbContext.Users.RemoveRange(users);
            await unitOfWork.SaveChangesAsync();

            Assert.AreEqual(1, userCount, "One User should be in the DB");
        }

        [TestMethod]
        public async Task CreateFiveUsers()
        {
            var feedbackDbContext = new FeedbackDbContext(dbContextOptions);
            UserRepository userRepository = new UserRepository(feedbackDbContext);
            UnitOfWork unitOfWork = new UnitOfWork(feedbackDbContext);

            List<User> users = new List<User>
            {
                new User { IdentityId = "001-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "002-001-001" , Role = UserRoles.teacher},
                new User { IdentityId = "003-001-001" , Role = UserRoles.teacher},
                new User { IdentityId = "004-001-001" , Role = UserRoles.admin},
                new User { IdentityId = "004-001-001" , Role = UserRoles.pupil}
            };

            foreach (User user in users)
            {
                await userRepository.CreateUserAsync(user);
            }
            await unitOfWork.SaveChangesAsync();
            int userCount = await userRepository.CountAllAsync();

            // ClearDB (Other tests will be not correct when not cleared)
            feedbackDbContext.Users.RemoveRange(users);
            await unitOfWork.SaveChangesAsync();

            Assert.AreEqual(5, userCount, "5 Users should be in the DB");
        }

        [TestMethod]
        public async Task CreateUserWithData()
        {
            var feedbackDbContext = new FeedbackDbContext(dbContextOptions);
            UserRepository userRepository = new UserRepository(feedbackDbContext);
            UnitOfWork unitOfWork = new UnitOfWork(feedbackDbContext);

            List<User> users = new List<User>
            {
                new User { IdentityId = "001-001-001" , Role = UserRoles.teacher,
                Title = "Dr.", FirstName = "Max", LastName = "Musterdoktor",
                    Birthdate = DateTime.Parse("01.02.1967"), School = "HTL-Muster"}
            };

            foreach (User user in users)
            {
                await userRepository.CreateUserAsync(user);
            }
            await unitOfWork.SaveChangesAsync();
            int userCount = await userRepository.CountAllAsync();

            // ClearDB (Other tests will be not correct when not cleared)
            feedbackDbContext.Users.RemoveRange(users);
            await unitOfWork.SaveChangesAsync();

            Assert.AreEqual(1, userCount, "One User should be in the DB");
        }

        [TestMethod]
        public async Task CountStudents()
        {
            var feedbackDbContext = new FeedbackDbContext(dbContextOptions);
            UserRepository userRepository = new UserRepository(feedbackDbContext);
            UnitOfWork unitOfWork = new UnitOfWork(feedbackDbContext);

            List<User> users = new List<User>
            {
                new User { IdentityId = "001-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "002-001-001" , Role = UserRoles.teacher},
                new User { IdentityId = "003-001-001" , Role = UserRoles.teacher},
                new User { IdentityId = "004-001-001" , Role = UserRoles.admin},
                new User { IdentityId = "005-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "006-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "007-001-001" , Role = UserRoles.pupil}
            };

            foreach (User user in users)
            {
                await userRepository.CreateUserAsync(user);
            }
            await unitOfWork.SaveChangesAsync();
            int userCount = await userRepository.CountStudentsAsync();

            // ClearDB (Other tests will be not correct when not cleared)
            feedbackDbContext.Users.RemoveRange(users);
            await unitOfWork.SaveChangesAsync();

            Assert.AreEqual(4, userCount, "4 Students should be in the DB");
        }

        [TestMethod]
        public async Task CountAdmins()
        {
            var feedbackDbContext = new FeedbackDbContext(dbContextOptions);
            UserRepository userRepository = new UserRepository(feedbackDbContext);
            UnitOfWork unitOfWork = new UnitOfWork(feedbackDbContext);

            List<User> users = new List<User>
            {
                new User { IdentityId = "001-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "002-001-001" , Role = UserRoles.teacher},
                new User { IdentityId = "003-001-001" , Role = UserRoles.teacher},
                new User { IdentityId = "004-001-001" , Role = UserRoles.admin},
                new User { IdentityId = "005-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "006-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "007-001-001" , Role = UserRoles.pupil}
            };

            foreach (User user in users)
            {
                await userRepository.CreateUserAsync(user);
            }
            await unitOfWork.SaveChangesAsync();
            int userCount = await userRepository.CountAdminsAsync();

            // ClearDB (Other tests will be not correct when not cleared)
            feedbackDbContext.Users.RemoveRange(users);
            await unitOfWork.SaveChangesAsync();

            Assert.AreEqual(1, userCount, "One Admin should be in the DB");
        }

        [TestMethod]
        public async Task CountTeachers()
        {
            var feedbackDbContext = new FeedbackDbContext(dbContextOptions);
            UserRepository userRepository = new UserRepository(feedbackDbContext);
            UnitOfWork unitOfWork = new UnitOfWork(feedbackDbContext);

            List<User> users = new List<User>
            {
                new User { IdentityId = "001-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "002-001-001" , Role = UserRoles.teacher},
                new User { IdentityId = "003-001-001" , Role = UserRoles.teacher},
                new User { IdentityId = "004-001-001" , Role = UserRoles.admin},
                new User { IdentityId = "005-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "006-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "007-001-001" , Role = UserRoles.pupil}
            };

            foreach (User user in users)
            {
                await userRepository.CreateUserAsync(user);
            }
            await unitOfWork.SaveChangesAsync();
            int userCount = await userRepository.CountTeachersAsync();

            // ClearDB (Other tests will be not correct when not cleared)
            feedbackDbContext.Users.RemoveRange(users);
            await unitOfWork.SaveChangesAsync();

            Assert.AreEqual(2, userCount, "2 Teachers should be in the DB");
        }

        [TestMethod]
        public async Task GetAllUsers()
        {
            var feedbackDbContext = new FeedbackDbContext(dbContextOptions);
            UserRepository userRepository = new UserRepository(feedbackDbContext);
            UnitOfWork unitOfWork = new UnitOfWork(feedbackDbContext);

            List<User> users = new List<User>
            {
                new User { IdentityId = "001-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "002-001-001" , Role = UserRoles.teacher},
                new User { IdentityId = "003-001-001" , Role = UserRoles.teacher},
                new User { IdentityId = "004-001-001" , Role = UserRoles.admin},
                new User { IdentityId = "005-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "006-001-001" , Role = UserRoles.pupil},
                new User { IdentityId = "007-001-001" , Role = UserRoles.pupil}
            };

            foreach (User user in users)
            {
                await userRepository.CreateUserAsync(user);
            }
            await unitOfWork.SaveChangesAsync();
            List<User> users2 = await userRepository.GetAllAsync();

            // ClearDB (Other tests will be not correct when not cleared)
            feedbackDbContext.Users.RemoveRange(users);
            await unitOfWork.SaveChangesAsync();

            Assert.AreEqual(7, users2.Count(), "7 User should be in the list");
        }

        [TestMethod]
        public async Task GetUserById()
        {
            int id = 1;
            string identityId = "001-111-001";
            var feedbackDbContext = new FeedbackDbContext(dbContextOptions);
            UserRepository userRepository = new UserRepository(feedbackDbContext);
            UnitOfWork unitOfWork = new UnitOfWork(feedbackDbContext);

            List<User> users = new List<User>
            {
                new User { Id = id, IdentityId = identityId , Role = UserRoles.pupil}
            };

            foreach (User user in users)
            {
                await userRepository.CreateUserAsync(user);
            }
            await unitOfWork.SaveChangesAsync();

            User user2 = await userRepository.GetByIdAsync(id);

            // ClearDB (Other tests will be not correct when not cleared)
            feedbackDbContext.Users.RemoveRange(users);
            await unitOfWork.SaveChangesAsync();

            Assert.AreEqual(identityId, user2.IdentityId, $"IdentityId should be {identityId}");
        }

        [TestMethod]
        public async Task GetUserByIdentityId()
        {
            int id = 1;
            string identityId = "001-222-001";
            var feedbackDbContext = new FeedbackDbContext(dbContextOptions);
            UserRepository userRepository = new UserRepository(feedbackDbContext);
            UnitOfWork unitOfWork = new UnitOfWork(feedbackDbContext);

            List<User> users = new List<User>
            {
                new User { Id = id, IdentityId = identityId , Role = UserRoles.pupil}
            };

            foreach (User user in users)
            {
                await userRepository.CreateUserAsync(user);
            }
            await unitOfWork.SaveChangesAsync();

            User user2 = await userRepository.GetByIdentityIdAsync(identityId);

            // ClearDB (Other tests will be not correct when not cleared)
            feedbackDbContext.Users.RemoveRange(users);
            await unitOfWork.SaveChangesAsync();

            Assert.AreEqual(identityId, user2.IdentityId, $"IdentityId should be {identityId}");
        }

        [TestMethod]
        public async Task DeleteUserByIdentityId()
        {
            int id = 1;
            string identityId = "001-222-001";
            var feedbackDbContext = new FeedbackDbContext(dbContextOptions);
            UserRepository userRepository = new UserRepository(feedbackDbContext);
            UnitOfWork unitOfWork = new UnitOfWork(feedbackDbContext);

            List<User> users = new List<User>
            {
                new User { Id = id, IdentityId = identityId , Role = UserRoles.pupil}
            };

            foreach (User user in users)
            {
                await userRepository.CreateUserAsync(user);
            }
            await unitOfWork.SaveChangesAsync();
            
            await userRepository.DeleteUserByIdentityIdAsync(identityId);
            await unitOfWork.SaveChangesAsync();
            int userCount = await userRepository.CountAllAsync();

            Assert.AreEqual(0, userCount, $"There should be no users");
        }

        [TestMethod]
        public async Task UpdateUserDataWithBirthdate()
        {
            int id = 1;
            string identityId = "001-222-001";
            string title = "Dr.";
            string firstName = "Max";
            string lastName = "Musterdoktor";
            DateTime birthdate = DateTime.Parse("01.02.1967");
            string school = "HTL-Muster";
            var feedbackDbContext = new FeedbackDbContext(dbContextOptions);
            UserRepository userRepository = new UserRepository(feedbackDbContext);
            UnitOfWork unitOfWork = new UnitOfWork(feedbackDbContext);

            List<User> users = new List<User>
            {
                new User { Id = id, IdentityId = identityId , Role = UserRoles.pupil}
            };

            foreach (User user in users)
            {
                await userRepository.CreateUserAsync(user);
            }
            await unitOfWork.SaveChangesAsync();

            await userRepository.UpdateUserAsync
                (identityId, title, firstName, lastName, birthdate, school);
            await unitOfWork.SaveChangesAsync();

            User user2 = await userRepository.GetByIdentityIdAsync(identityId);

            // ClearDB (Other tests will be not correct when not cleared)
            feedbackDbContext.Users.RemoveRange(users);
            await unitOfWork.SaveChangesAsync();

            Assert.AreEqual(identityId, user2.IdentityId, $"IdentityId should be identical");
            Assert.AreEqual(title, user2.Title, $"Title should be identical");
            Assert.AreEqual(firstName, user2.FirstName, $"Firstname should be identical");
            Assert.AreEqual(lastName, user2.LastName, $"Lastname should be identical");
            Assert.AreEqual(birthdate, user2.Birthdate, $"Birthdate should be identical");
            Assert.AreEqual(school, user2.School, $"School should be identical");
        }

        [TestMethod]
        public async Task UpdateUserDataWithoutBirthdate()
        {
            int id = 1;
            string identityId = "001-222-001";
            string title = "Dr.";
            string firstName = "Max";
            string lastName = "Musterdoktor";
            DateTime? birthdate = null;
            string school = "HTL-Muster";
            var feedbackDbContext = new FeedbackDbContext(dbContextOptions);
            UserRepository userRepository = new UserRepository(feedbackDbContext);
            UnitOfWork unitOfWork = new UnitOfWork(feedbackDbContext);

            List<User> users = new List<User>
            {
                new User { Id = id, IdentityId = identityId , Role = UserRoles.pupil}
            };

            foreach (User user in users)
            {
                await userRepository.CreateUserAsync(user);
            }
            await unitOfWork.SaveChangesAsync();

            await userRepository.UpdateUserAsync
                (identityId, title, firstName, lastName, birthdate, school);
            await unitOfWork.SaveChangesAsync();

            User user2 = await userRepository.GetByIdentityIdAsync(identityId);

            // ClearDB (Other tests will be not correct when not cleared)
            feedbackDbContext.Users.RemoveRange(users);
            await unitOfWork.SaveChangesAsync();

            Assert.AreEqual(identityId, user2.IdentityId, $"IdentityId should be identical");
            Assert.AreEqual(title, user2.Title, $"Title should be identical");
            Assert.AreEqual(firstName, user2.FirstName, $"Firstname should be identical");
            Assert.AreEqual(lastName, user2.LastName, $"Lastname should be identical");
            Assert.AreEqual(null, user2.Birthdate, $"Birthdate should be null");
            Assert.AreEqual(school, user2.School, $"School should be identical");
        }
    }
}
