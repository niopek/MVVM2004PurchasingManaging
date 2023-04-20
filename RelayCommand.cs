using System;
using System.Windows.Input;

namespace MVVM2004PurchasingManaging;

public class RelayCommand : ICommand
{
    private Action<object> execute;
    private Func<object, bool>? canExecute;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
    {
        if (execute == null) throw new ArgumentNullException(nameof(execute));
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        if (canExecute == null) return true;
        else return canExecute(parameter!);
    }

    public void Execute(object? parameter)
    {
        execute(parameter!);

    }
}
