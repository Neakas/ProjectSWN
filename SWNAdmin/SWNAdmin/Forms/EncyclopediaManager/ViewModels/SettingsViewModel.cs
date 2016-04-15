using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using SWNAdmin.Forms.EncyclopediaManager.Interaction;

namespace SWNAdmin.Forms.EncyclopediaManager.ViewModels
{
    public class SettingsViewModel : Notifier
    {
        private readonly ObservableCollection<string> _previousCriteria = new ObservableCollection<string>();

        private readonly ObservableCollection<TreeNodeViewModel> _roots = new ObservableCollection<TreeNodeViewModel>();
        private string _currentCriteria = string.Empty;
        private string _selectedCriteria = string.Empty;

        public SettingsViewModel( IEnumerable<TreeNodeViewModel> roots )
        {
            foreach (var node in roots)
            {
                _roots.Add(node);
                node.PropertyChanged += ChangedNode;
            }

            StoreInPreviousCommand = new Command(StoreInPrevious);
        }

        public ICommand StoreInPreviousCommand { get; }

        public IEnumerable<TreeNodeViewModel> Roots => _roots;

        public IEnumerable<string> PreviousCriteria => _previousCriteria;

        public string SelectedCriteria
        {
            get
            {
                return _selectedCriteria;
            }
            set
            {
                if (value == _selectedCriteria)
                {
                    return;
                }

                _selectedCriteria = value;
                OnPropertyChanged("SelectedCriteria");
            }
        }

        public string CurrentCriteria
        {
            get
            {
                return _currentCriteria;
            }
            set
            {
                if (value == _currentCriteria)
                {
                    return;
                }

                _currentCriteria = value;
                OnPropertyChanged("CurrentCriteria");
                ApplyFilter();
            }
        }

        private void StoreInPrevious( object dummy )
        {
            if (string.IsNullOrEmpty(CurrentCriteria))
            {
                return;
            }

            if (!_previousCriteria.Contains(CurrentCriteria))
            {
                _previousCriteria.Add(CurrentCriteria);
            }

            SelectedCriteria = CurrentCriteria;
        }

        private static void ChangedNode( object sender, PropertyChangedEventArgs e )
        {
        }

        private void ApplyFilter()
        {
            foreach (var node in _roots)
            {
                node.ApplyCriteria(CurrentCriteria, new Stack<TreeNodeViewModel>());
            }
        }
    }
}