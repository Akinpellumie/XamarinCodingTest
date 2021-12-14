using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Steer73.FormsApp.Framework;
using Steer73.FormsApp.Model;
using Steer73.FormsApp.ViewModels;

namespace Steer73.FormsApp.Tests.ViewModels
{
    [TestFixture]
    public class UsersViewModelTests
    {
        [Test]
        public async Task InitializeFetchesTheData()
        {
            //prepare
            var userService = new Mock<IUserService>();
            var messageService = new Mock<IMessageService>();

            var viewModel = new UsersViewModel(
                userService.Object,
                messageService.Object);

            //act
            userService
                .Setup(p => p.GetUsers())
                .Returns(Task.FromResult(Enumerable.Empty<User>()))
                .Verifiable();

            Task.Run(async () =>
            {
                await viewModel.Initialize();
            }).GetAwaiter().GetResult();

            //verify that the service was called.
            //note that verifyAll will assert that all setUp calls were actually called
            userService.VerifyAll();

            //assert that the call paased without any exceptions.
            //assert will verify that all methods are called and no exception occured
            Assert.Pass();
        }

        [Test]
        public async Task InitializeShowErrorMessageOnFetchingError()
        {
            // ?
        }
    }
}
