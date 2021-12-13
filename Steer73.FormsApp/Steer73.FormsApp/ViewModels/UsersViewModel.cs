using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Steer73.FormsApp.Framework;
using Steer73.FormsApp.Model;
using System.Linq;
using Xamarin.Forms;

namespace Steer73.FormsApp.ViewModels
{
    public class UsersViewModel : ViewModel
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public UsersViewModel(
            IUserService userService,
            IMessageService messageService)
        {
            _userService = userService;
            _messageService = messageService;
            RefreshCommand = new Command(async () => await RefreshCommandExecute());
        }

        #region Properties

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        private string emptyUser;
        public string EmptyUser
        {
            get => emptyUser;
            set
            {
                emptyUser = value;
                OnPropertyChanged(nameof(EmptyUser));
            }
        }
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        #endregion

        #region Commands
        public Command RefreshCommand { get; }
        #endregion
        private async Task RefreshCommandExecute()
        {
            IsRefreshing = true;
            await Task.Delay(100);

            await Initialize();

            IsRefreshing = false;
        }

        public async Task Initialize()
        {
            try
            {
                IsBusy = true;
                EmptyUser = "Fetching user. Please wait...";

                //delay task for 2secs to show the loader before showing the list
                await Task.Delay(2000);

                var users = await _userService.GetUsers();

                //check if users list is not empty
                if(users.Count() >= 1)
                {
                    foreach (var user in users)
                    {
                        user.FullName = $"{user.FirstName} {user.LastName}";
                        user.Initial = $"{user.FirstName.Substring(0, 1).ToUpper()}{user.LastName.Substring(0, 1).ToUpper()}";
                    }

                    //order the list alphabetically using OrderByDescending(Linq)
                    var ordered = users.OrderByDescending(v => v.FullName.StartsWith("A"))
                        .ThenBy(v => v.FullName);
                    Users = new ObservableCollection<User>(ordered);

                }
                else
                {
                    //inform user there's no item in the user list
                    EmptyUser = "No user found.";
                }
            }
            catch (Exception ex)
            {
                await _messageService.ShowError(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
