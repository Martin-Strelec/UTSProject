using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using UTSProject.Resources.Models;
using UTSProject.Resources.Services;
using UTSProject.Resources.Views;

namespace UTSProject.Resources.ViewModels
{
    [QueryProperty("ConnectionDetails", "ConnectionDetails")]
    public partial class DetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private ConnectionDetailsModel _connectionDetails;
    }
}
