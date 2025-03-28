using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using UTSProject.Resources.Models;

namespace UTSProject.Resources.ViewModels
{
    [QueryProperty(nameof(ConnectionDetailsModel), "ConnectionDetails")]
    public partial class DetailPageViewModel : ObservableObject
    {
        [ObservableProperty]
        ConnectionDetailsModel _connectionDetails;
    }
}
