// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// This file is inspired from the MvvmLight library (lbugnion/MvvmLight),
// more info in ThirdPartyNotices.txt in the root of the project.

using System.Windows.Input;

namespace AlexandreHtrb.AvaloniaUITest;

public sealed class UITestRelayCommand : ICommand
{
    private readonly Action execute;
    private readonly Func<bool>? canExecute;

    public UITestRelayCommand(Action execute, Func<bool>? canExecute = null)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => this.canExecute?.Invoke() ?? true;
    public void Execute(object? parameter) => this.execute();

    public void RaiseCanExecuteChanged() =>
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}