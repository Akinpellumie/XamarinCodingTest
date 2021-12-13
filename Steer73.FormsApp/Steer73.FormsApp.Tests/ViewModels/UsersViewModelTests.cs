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

            userService
                .Setup(p => p.GetUsers())
                .Returns(Task.FromResult(Enumerable.Empty<User>()))
                .Verifiable();

            //act
            await viewModel.Initialize();

            userService.VerifyAll();

            // Assert
            //Assert.IsTrue(!result.IsError, result.ErrorMessage);
            //Console.WriteLine(JsonConvert.SerializeObject(result.Result));
        }

        [Test]
        public async Task InitializeShowErrorMessageOnFetchingError()
        {
            // ?
        }
    }
}
