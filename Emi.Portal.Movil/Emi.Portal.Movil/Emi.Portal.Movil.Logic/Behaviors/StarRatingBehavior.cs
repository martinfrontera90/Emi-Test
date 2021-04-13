namespace Emi.Portal.Movil.Logic.Behaviors
{
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;

    /// <summary>
    /// Star rating behavior.
    /// </summary>
    public class StarRatingBehavior : Behavior<View>
    {
        private TapGestureRecognizer tapRecognizer;
        private static List<StarRatingBehavior> defaultBehaviors = new List<StarRatingBehavior>();
        private static Dictionary<string, List<StarRatingBehavior>> starGroups;

        /// <summary>
        /// The group name property.
        /// </summary>
        public static readonly BindableProperty GroupNameProperty = BindableProperty.Create(
                                                                    "GroupName",
                                                                    typeof(string),
                                                                    typeof(StarRatingBehavior),
                                                                    null,
                                                                    propertyChanged: OnGroupNameChanged);
        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        /// <value>The name of the group.</value>
        public string GroupName
        {
            set => SetValue(GroupNameProperty, value);
            get => (string)GetValue(GroupNameProperty);
        }

        /// <summary>
        /// The rating property.
        /// </summary>
        public static readonly BindableProperty RatingProperty = BindableProperty.Create(
                                                                 "Rating",
                                                                 typeof(int),
                                                                 typeof(StarRatingBehavior),
                                                                 default(int),
                                                                 defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>The rating.</value>
        public int Rating
        {
            set => SetValue(RatingProperty, value);
            get => (int)GetValue(RatingProperty);
        }

        /// <summary>
        /// Ons the group name changed.
        /// </summary>
        /// <param name="bindable">Bindable.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void OnGroupNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            StarRatingBehavior behavior = (StarRatingBehavior)bindable;
            string oldGroupName = (string)oldValue;
            string newGroupName = (string)newValue;

            // Remove existing behavior from Group
            if (string.IsNullOrEmpty(oldGroupName))
            {
                defaultBehaviors.Remove(behavior);
            }
            else
            {
                List<StarRatingBehavior> behaviors = starGroups[oldGroupName];
                behaviors.Remove(behavior);

                if (behaviors.Count == 0)
                {
                    starGroups.Remove(oldGroupName);
                }
            }

            // Add New Behavior to the group
            if (string.IsNullOrEmpty(newGroupName))
            {
                defaultBehaviors.Add(behavior);
            }
            else
            {
                List<StarRatingBehavior> behaviors = null;
                if (starGroups.ContainsKey(newGroupName))
                {
                    behaviors = starGroups[newGroupName];
                }
                else
                {
                    behaviors = new List<StarRatingBehavior>();
                    starGroups.Add(newGroupName, behaviors);
                }

                behaviors.Add(behavior);
            }
        }

        /// <summary>
        /// The is starred property.
        /// </summary>
        public static readonly BindableProperty IsStarredProperty = BindableProperty.Create(
                                                                    "IsStarred",
                                                                    typeof(bool),
                                                                    typeof(StarRatingBehavior),
                                                                    false,
                                                                    propertyChanged: OnIsStarredChanged);
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:AppSalud.App.Behavior.StarRatingBehavior"/> is starred.
        /// </summary>
        /// <value><c>true</c> if is starred; otherwise, <c>false</c>.</value>
        public bool IsStarred
        {
            set => SetValue(IsStarredProperty, value);
            get => (bool)GetValue(IsStarredProperty);
        }

        /// <summary>
        /// Ons the is starred changed.
        /// </summary>
        /// <param name="bindable">Bindable.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void OnIsStarredChanged(BindableObject bindable, object oldValue, object newValue)
        {
            object context = bindable.BindingContext;

            StarRatingBehavior behavior = (StarRatingBehavior)bindable;

            if ((bool)newValue)
            {
                string groupName = behavior.GroupName;
                List<StarRatingBehavior> behaviors = null;

                if (string.IsNullOrEmpty(groupName))
                {
                    behaviors = defaultBehaviors;
                }
                else
                {
                    behaviors = starGroups[groupName];
                }

                bool itemReached = false;
                int count = 1, position = 0;

                // All positions to left IsStarred = true and all position to the right is false
                foreach (StarRatingBehavior item in behaviors)
                {
                    if (item != behavior && !itemReached)
                    {
                        item.IsStarred = true;
                    }
                    if (item == behavior)
                    {
                        itemReached = true;
                        item.IsStarred = true;
                        position = count;
                    }
                    if (item != behavior && itemReached)
                    {
                        item.IsStarred = false;
                    }

                    item.Rating = position;
                    count++;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AppSalud.App.Behavior.StarRatingBehavior"/> class.
        /// </summary>
        public StarRatingBehavior()
        {
            starGroups = new Dictionary<string, List<StarRatingBehavior>>();
            defaultBehaviors.Add(this);
        }

        /// <summary>
        /// Ons the attached to.
        /// </summary>
        /// <param name="bindable">View.</param>
        protected override void OnAttachedTo(View bindable)
        {
            tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += OnTapRecognizerTapped;
            bindable.GestureRecognizers.Add(tapRecognizer);
        }

        /// <summary>
        /// Ons the detaching from.
        /// </summary>
        /// <param name="bindable">View.</param>
        protected override void OnDetachingFrom(View bindable)
        {
            bindable.GestureRecognizers.Remove(tapRecognizer);
            tapRecognizer.Tapped -= OnTapRecognizerTapped;
        }

        /// <summary>
        /// Ons the tap recognizer tapped.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        private void OnTapRecognizerTapped(object sender, EventArgs args)
        {
            IsStarred = false;
            IsStarred = true;
        }
    }
}