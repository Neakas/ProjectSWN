using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SWNAdmin.UI.ViewModels
{
    public class TreeNodeViewModel : Notifier
    {
        private readonly ObservableCollection<TreeNodeViewModel> children;
        public TreeNodeViewModel Parent = null;
        private readonly string name;

        private bool expanded;
        private bool selected;
        public string selectedNode;
        public string logicalParent;
        private bool match = true;

        public TreeNodeViewModel(string name, IEnumerable<TreeNodeViewModel> children, TreeNodeViewModel parent)
        {
            this.name = name;
            this.children = new ObservableCollection<TreeNodeViewModel>(children);
            foreach (var item in children)
            {
                item.Parent = this;
            }
            this.Parent = parent;
        }

        public TreeNodeViewModel(string name)
            : this(name, Enumerable.Empty<TreeNodeViewModel>(),null)
        {
        }

        public override string ToString()
        {
            return name;
        }


        private bool IsCriteriaMatched(string criteria)
        {
            return String.IsNullOrEmpty(criteria) || name.Contains(criteria);
        }

        public void ApplyCriteria(string criteria, Stack<TreeNodeViewModel> ancestors)
        {
            if (IsCriteriaMatched(criteria))
            {
                IsMatch = true;
                foreach (var ancestor in ancestors)
                {
                    ancestor.IsMatch = true;
                    ancestor.IsExpanded = !String.IsNullOrEmpty(criteria);
                }
            }
            else
                IsMatch = false;

            ancestors.Push(this);
            foreach (var child in Children)
                child.ApplyCriteria(criteria, ancestors);

            ancestors.Pop();
        }

        public IEnumerable<TreeNodeViewModel> Children
        {
            get { return children; }
        }

        public string Name
        {
            get { return name; }
        }

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
                selectedNode = name;
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
    }
}
