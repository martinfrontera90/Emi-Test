namespace Emi.Portal.Movil.Logic.Behaviors
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using Xamarin.Forms;

    public class PickerBindBehavior : BindingContextBehavior<Picker>
    {
        bool updatingValue;

        #region ItemsProperty
        /// <summary>
        /// Items bindable property
        /// </summary>
        public static BindableProperty ItemsProperty =
            BindableProperty.Create("Items", typeof(IEnumerable),
                typeof(PickerBindBehavior), null,
                propertyChanged: ItemsChanged);

        /// <summary>
        /// Get or set the collection of items for the Picker to display.
        /// The behavior will add the textual (ToString) representation for each item.
        /// </summary>
        /// <value>Any IEnumerable of items to display.</value>
        public IEnumerable Items
        {
            get { return (IEnumerable)base.GetValue(ItemsProperty); }
            set { base.SetValue(ItemsProperty, value); }
        }

        static void ItemsChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var behavior = bindableObject as PickerBindBehavior;
            if (behavior != null)
                behavior.OnItemsChanged((IEnumerable)oldValue, (IEnumerable)newValue);
        }
        #endregion

        #region SelectedItemProperty
        /// <summary>
        /// The currently selected item
        /// </summary>
        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem",
                typeof(object), typeof(PickerBindBehavior),
                null,
                BindingMode.TwoWay,
                propertyChanged: SelectedItemChanged);

        /// <summary>
        /// The currently selected object in the Picker.
        /// This is the actual instance, not just a string.
        /// </summary>
        /// <value>The selected item.</value>
        public object SelectedItem
        {
            get { return base.GetValue(SelectedItemProperty); }
            set { base.SetValue(SelectedItemProperty, value); }
        }

        static void SelectedItemChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var behavior = bindableObject as PickerBindBehavior;
            if (behavior != null)
                behavior.OnSelectedItemChanged(oldValue, newValue);
        }
        #endregion

        /// <summary>
        /// Called when this behavior is attached to a visual.
        /// </summary>
        /// <param name="bindable">Visual owner</param>
        protected override void OnAttachedTo(Picker bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.SelectedIndexChanged += OnSelectedIndexChanged;
            OnItemsChanged(null, Items);
            OnSelectedItemChanged(null, SelectedItem);
        }

        /// <summary>
        /// Called when this behavior is detached from a visual
        /// </summary>
        /// <param name="bindable">Visual owner</param>
        protected override void OnDetachingFrom(Picker bindable)
        {
            bindable.SelectedIndexChanged -= OnSelectedIndexChanged;
            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        /// This method is called when the <see cref="Items"/> property is changed.
        /// It will update the picker visual and also add a change handler if the 
        /// passed enumerable implements <see cref="INotifyCollectionChanged"/>
        /// </summary>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private void OnItemsChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            INotifyCollectionChanged notifyCollectionChanged = oldValue as INotifyCollectionChanged;
            if (notifyCollectionChanged != null)
            {
                notifyCollectionChanged.CollectionChanged -= OnCollectionChanged;
            }

            if (AssociatedObject == null)
                return;

            AssociatedObject.Items.Clear();

            if (newValue == null)
                return;

            foreach (var item in newValue)
            {
                AssociatedObject.Items.Add((item ?? "").ToString());
            }

            notifyCollectionChanged = newValue as INotifyCollectionChanged;
            if (notifyCollectionChanged != null)
            {
                notifyCollectionChanged.CollectionChanged += OnCollectionChanged;
            }
        }

        /// <summary>
        /// This method is called if the data-bound Items implements collection-change
        /// notifications. It will update the Picker visuals based on the collection changes.
        /// </summary>
        /// <param name="sender">The collection</param>
        /// <param name="e">EventArgs</param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Debug.Assert(ReferenceEquals(sender, Items));

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    AssociatedObject.Items.Add((item ?? "").ToString());
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    string value = (item ?? "").ToString();
                    AssociatedObject.Items.Remove(value);
                }
            }

            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                AssociatedObject.Items.Clear();
                foreach (var item in Items)
                {
                    AssociatedObject.Items.Add((item ?? "").ToString());
                }
            }

            if (SelectedItem != null && AssociatedObject.SelectedIndex == -1)
            {
                OnSelectedItemChanged(null, SelectedItem);
            }
        }

        /// <summary>
        /// This is called when the behavior's <see cref="SelectedItem"/> property is changed.
        /// It will update the Picker's SelectedIndex property.
        /// </summary>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        void OnSelectedItemChanged(object oldValue, object newValue)
        {
            if (AssociatedObject == null || updatingValue)
                return;

            if (Object.Equals(oldValue, newValue))
                return;

            updatingValue = true;
            try
            {
                if (newValue == null)
                {
                    AssociatedObject.SelectedIndex = -1;
                }

                var items = Items;
                if (items == null)
                    return;

                int index = -1;
                IList itemList = items as IList;
                if (itemList != null)
                {
                    index = itemList.IndexOf(newValue);
                }
                else
                {
                    foreach (object testValue in items)
                    {
                        index++;
                        if (Equals(testValue, newValue))
                            break;
                    }
                }
                AssociatedObject.SelectedIndex = index;
            }
            finally
            {
                updatingValue = false;
            }
        }

        /// <summary>
        /// This is called when the Picker's SelectedIndex property is changed
        /// by the visual control. It will update the <see cref="SelectedItem"/> property.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (AssociatedObject == null || updatingValue)
                return;

            var items = Items;
            if (items == null)
                return;

            int selectedIndex = AssociatedObject.SelectedIndex;
            if (selectedIndex == -1)
            {
                SelectedItem = null;
                return;
            }

            object value = null;

            IList itemList = items as IList;
            if (itemList != null)
            {
                value = itemList[selectedIndex];
            }
            else
            {
                int index = 0;
                foreach (object testValue in items)
                {
                    if (index == selectedIndex)
                    {
                        value = testValue;
                        break;
                    }
                    index++;
                }
            }

            SelectedItem = value;
        }
    }
}
