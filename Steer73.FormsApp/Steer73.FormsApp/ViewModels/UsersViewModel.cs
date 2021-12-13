using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Steer73.FormsApp.Framework;
using Steer73.FormsApp.Model;
using System.Linq;

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
        private bool showUser;
        public bool ShowUser
        {
            get => showUser;
            set
            {
                showUser = value;
                OnPropertyChanged(nameof(ShowUser));
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

        public async Task Initialize()
        {
            try
            {
                IsBusy = true;
                ShowUser = false;

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
                    Users = new ObservableCollection<User>(users);

                    ShowUser = true;
                }
                else
                {
                    ShowUser = false;
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


        //public ICollection<User> Users { get; } = new List<User>();
    }
}
