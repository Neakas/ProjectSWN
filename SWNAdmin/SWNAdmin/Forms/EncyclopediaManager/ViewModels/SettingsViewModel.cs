using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using SWNAdmin.Forms.EncyclopediaManager.Interaction;

namespace SWNAdmin.Forms.EncyclopediaManager.ViewModels
{
    public class SettingsViewModel : Notifier
    {
        private readonly ObservableCollection<string> previousCriteria = new ObservableCollection<string>();

        private readonly ObservableCollection<TreeNodeViewModel> roots = new ObservableCollection<TreeNodeViewModel>();
        private string changedNode = string.Empty;
        private string currentCriteria = string.Empty;
        private string selectedCriteria = string.Empty;

        public SettingsViewModel(IEnumerable<TreeNodeViewModel> roots)
        {
            foreach (var node in roots)
            {
                this.roots.Add(node);
                node.PropertyChanged += ChangedNode;
            }

            StoreInPreviousCommand = new Command(StoreInPrevious);
        }

        public ICommand StoreInPreviousCommand { get; }

        public IEnumerable<TreeNodeViewModel> Roots
        {
            get { return roots; }
        }

        public IEnumerable<string> PreviousCriteria
        {
            get { return previousCriteria; }
        }

        public string SelectedCriteria
        {
            get { return selectedCriteria; }
            set
            {
                if (value == selectedCriteria)
                    return;

                selectedCriteria = value;
                OnPropertyChanged("SelectedCriteria");
            }
        }

        public string CurrentCriteria
        {
            get { return currentCriteria; }
            set
            {
                if (value == currentCriteria)
                    return;

                currentCriteria = value;
                OnPropertyChanged("CurrentCriteria");
                ApplyFilter();
            }
        }

        private void StoreInPrevious(object dummy)
        {
            if (string.IsNullOrEmpty(CurrentCriteria))
                return;

            if (!previousCriteria.Contains(CurrentCriteria))
                previousCriteria.Add(CurrentCriteria);

            SelectedCriteria = CurrentCriteria;
        }

        private void ChangedNode(object Sender, PropertyChangedEventArgs e)
        {
            changedNode = e.PropertyName;
        }

        private void ApplyFilter()
        {
            foreach (var node in roots)
                node.ApplyCriteria(CurrentCriteria, new Stack<TreeNodeViewModel>());
        }
    }
}