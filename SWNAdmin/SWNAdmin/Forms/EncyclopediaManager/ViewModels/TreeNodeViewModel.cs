using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SWNAdmin.Forms.EncyclopediaManager.ViewModels
{
    public class TreeNodeViewModel : Notifier
    {
        private readonly ObservableCollection<TreeNodeViewModel> children;

        private bool expanded;
        public string logicalParent;
        private bool match = true;
        public TreeNodeViewModel Parent;
        private bool selected;
        public string selectedNode;

        public TreeNodeViewModel(string name, IEnumerable<TreeNodeViewModel> children, TreeNodeViewModel parent)
        {
            Name = name;
            this.children = new ObservableCollection<TreeNodeViewModel>(children);
            foreach (var item in children)
            {
                item.Parent = this;
            }
            Parent = parent;
        }

        public TreeNodeViewModel(string name)
            : this(name, Enumerable.Empty<TreeNodeViewModel>(), null)
        {
        }

        public IEnumerable<TreeNodeViewModel> Children
        {
            get { return children; }
        }

        public string Name { get; }

        public bool IsExpanded
        {
            get { return expanded; }
            set
            {
                if (value == expanded)
                    return;

                expanded = value;
                if (expanded)
                {
                    foreach (var child in Children)
                        child.IsMatch = true;
                }
                OnPropertyChanged("IsExpanded");
            }
        }

        public bool IsSelected
        {
            get { return selected; }
            set
            {
                selected = value;
                selectedNode = Name;
                OnPropertyChanged("IsSelected");
            }
        }

        public bool IsMatch
        {
            get { return match; }
            set
            {
                if (value == match)
                    return;

                match = value;
                OnPropertyChanged("IsMatch");
            }
        }

        public bool IsLeaf
        {
            get { return !Children.Any(); }
        }

        public override string ToString()
        {
            return Name;
        }


        private bool IsCriteriaMatched(string criteria)
        {
            return string.IsNullOrEmpty(criteria) || Name.Contains(criteria);
        }

        public void ApplyCriteria(string criteria, Stack<TreeNodeViewModel> ancestors)
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
                IsMatch = false;

            ancestors.Push(this);
            foreach (var child in Children)
                child.ApplyCriteria(criteria, ancestors);

            ancestors.Pop();
        }
    }
}