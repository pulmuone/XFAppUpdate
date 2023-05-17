using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFAppUpdate.Services;

namespace XFAppUpdate.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        INavigation Navigation => Application.Current.MainPage.Navigation;

        private string _id;
        private string _password;

        public string Id { get => this._id; set => SetProperty(ref this._id, value); }
        public string Password { get => this._password; set => SetProperty(ref this._password, value); }

        
        public ICommand LoginCommand { get; private set; }
        public LoginViewModel()
        {
            LoginCommand = new Command<object>(async (obj) => await Login(obj), (obj) => IsControlEnable);
        }


        private async Task Login(object obj)
        {
            IsControlEnable = false;
            IsBusy = true;
            (LoginCommand as Command).ChangeCanExecute();

            if (VersionCheck.Instance.IsNetworkAccess())
            {
                if (await VersionCheck.Instance.IsUpdate())
                {
                    await VersionCheck.Instance.UpdateCheck();

                    IsControlEnable = true;
                    IsBusy = false;
                    (LoginCommand as Command).ChangeCanExecute();

                    return;
                }

                //ToDo, 로그인 프로세스 진행



            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("인터넷 연결 오류", "인터넷 연결 후\n다시 처리해 주세요.", "OK");

                IsControlEnable = true;
                IsBusy = false;
                (LoginCommand as Command).ChangeCanExecute();

                return;
            }

            IsControlEnable = true;
            IsBusy = false;
            (LoginCommand as Command).ChangeCanExecute();
        }
    }
}
