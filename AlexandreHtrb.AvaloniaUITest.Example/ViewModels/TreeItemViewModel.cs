using System.Collections.ObjectModel;

namespace AlexandreHtrb.AvaloniaUITest.Example.ViewModels;

public class TreeItemViewModel : UITestBaseViewModel
{
    private readonly ObservableCollection<TreeItemViewModel> parentCollection;

    public bool IsExpanded { get; set => ChangeProperty(ref field, value); }

    public ObservableCollection<TreeItemViewModel> Items { get; }

    public string Name { get; set => ChangeProperty(ref field, value); }

    public string NewChildName { get; set => ChangeProperty(ref field, value); }

    public UITestRelayCommand AddChildCmd { get; }

    public TreeItemViewModel(ObservableCollection<TreeItemViewModel> parentCollection, string name)
    {
        this.parentCollection = parentCollection;
        Items = new();
        Name = name;
        NewChildName = string.Empty;
        AddChildCmd = new(AddChild);
    }

    private void AddChild()
    {
        TreeItemViewModel newChild = new(Items, NewChildName);
        Items.Add(newChild);
        if (Items.Count == 1)
        {
            IsExpanded = true;
        }
        MainWindowViewModel.Instance.TreeSelectedItem = newChild;
    }

    public void DeleteThis() => this.parentCollection.Remove(this);
}
