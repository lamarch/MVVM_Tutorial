namespace MVVM_Tutorial.ViewModels;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

internal abstract class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual void Dispose() { }

    // Error handling region

    private readonly Dictionary<string, List<string>> propertyNameToErrorsDictionnary = new();

    public bool HasErrors => propertyNameToErrorsDictionnary.Any();

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    private void OnErrorsChanged(DataErrorsChangedEventArgs e)
    {
        ErrorsChanged?.Invoke(this, e);
    }

    public virtual IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName is null) return new List<string>();

        return propertyNameToErrorsDictionnary.GetValueOrDefault(propertyName, new List<string>());
    }

    protected virtual void AddError(string errorMessage, string propertyName)
    {
        if (!propertyNameToErrorsDictionnary.ContainsKey(propertyName))
        {
            propertyNameToErrorsDictionnary[propertyName] = new List<string>();
        }
        propertyNameToErrorsDictionnary[propertyName].Add(errorMessage);

        OnErrorsChanged(new(propertyName));
    }

    protected virtual void ClearErrors(string propertyName)
    {
        propertyNameToErrorsDictionnary.Remove(propertyName);

        OnErrorsChanged(new(propertyName));
    }
}
