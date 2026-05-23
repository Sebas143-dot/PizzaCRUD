using CommunityToolkit.Mvvm.Input;
using PizzaCRUD.Models;

namespace PizzaCRUD.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}