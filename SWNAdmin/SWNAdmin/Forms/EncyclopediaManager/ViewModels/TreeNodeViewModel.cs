using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SWNAdmin.Forms.EncyclopediaManager.ViewModels
{
    public class TreeNodeViewModel : Notifier
    {
        private readonly ObservableCollection<TreeNodeViewModel> _children;

        private bool _expanded;
        private bool _match = true;
        private bool _selected;
        public string LogicalParent;
        public TreeNodeViewModel Parent;
        public string SelectedNode;

        public TreeNodeViewModel( string name, IEnumerable<TreeNodeViewModel> children, TreeNodeViewModel parent )
        {
            Name = name;
            if (children != null)
            {
                var treeNodeViewModels = children as IList<TreeNodeViewModel> ?? children.ToList();
                _children = new ObservableCollection<TreeNodeViewModel>(treeNodeViewModels);
                foreach (var item in treeNodeViewModels)
                {
                    item.Parent = this;
                }
            }
            Parent = parent;
        }

        public TreeNodeViewModel( string name ) : this(name, Enumerable.Empty<TreeNodeViewModel>(), null)
        {
        }

        public IEnumerable<TreeNodeViewModel> Children => _children;

        public string Name { get; }

        public bool IsExpanded
        {
            get
            {
                return _expanded;
            }
            set
            {
                if (value == _expanded)
                {
                    return;
                }

                _expanded = value;
                if (_expanded)
                {
                    foreach (var child in Children)
                    {
                        child.IsMatch = true;
                    }
                }
                OnPropertyChanged("IsExpanded");
            }
        }

        public bool IsSelected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                SelectedNode = Name;
                OnPropertyChanged("IsSelected");
            }
        }

        public bool IsMatch
        {
            get
            {
                return _match;
            }
            set
            {
                if (value == _match)
                {
                    return;
                }

                _match = value;
                OnPropertyChanged("IsMatch");
            }
        }

        public bool IsLeaf => !Children.Any();

        public override string ToString()
        {
            return Name;
        }

        private bool IsCriteriaMatched( string criteria )
        {
            return string.IsNullOrEmpty(criteria) || Name.Contains(criteria);
        }

        public void ApplyCriteria( string criteria, Stack<TreeNodeViewModel> ancestors )
        {
            if (IsCriteriaMatched(criteria))
            {
                IsMatch = true;
                foreach (var ancestor in ancestors)
                {
                    ancestor.IsMatch = true;
                    ancestor.IsExpanded = !string.IsNullOrEmpty(criteria);
                }
            }
            else
            {
                IsMatch = false;
            }

            ancestors.Push(this);
            foreach (var child in Children)
            {
                child.ApplyCriteria(criteria, ancestors);
            }

            ancestors.Pop();
        }
    }
}