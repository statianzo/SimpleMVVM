using System;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace SimpleMVVM.Behaviors
{
	public static class AttachedBehaviorsManager
	{
		public static readonly DependencyProperty BehaviorsProperty =
			DependencyProperty.RegisterAttached("Behaviors", typeof (BehaviorCollection), typeof (AttachedBehaviorsManager),
			                                    new PropertyMetadata(BehaviorsChanged));

		public static BehaviorCollection GetBehaviors(DependencyObject obj)
		{
			return (BehaviorCollection) obj.GetValue(BehaviorsProperty);
		}

		public static void SetBehaviors(DependencyObject obj, BehaviorCollection value)
		{
			obj.SetValue(BehaviorsProperty, value);
		}

		private static void BehaviorsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			var frameworkElement = obj as FrameworkElement;
			var behaviors = e.NewValue as BehaviorCollection;
			if (frameworkElement != null && behaviors != null)
			{
				if (!frameworkElement.IsInitialized)
				{
					EventHandler callback = null;
					callback = new EventHandler((sender, args) =>
					{
						frameworkElement.Initialized -= callback;
						AttachBehaviors(frameworkElement, behaviors);
					});
					frameworkElement.Initialized += callback;
					return;
				}

				AttachBehaviors(frameworkElement, behaviors);
			}
		}

		private static void AttachBehaviors(FrameworkElement frameworkElement, BehaviorCollection behaviors)
		{
			BindingOperations.SetBinding(behaviors,
				FrameworkElement.DataContextProperty,
				new Binding("DataContext") { Source = frameworkElement, });

			foreach (var behavior in behaviors)
			{
				EventInfo ei = frameworkElement.GetType().GetEvent(behavior.EventName);
				if (ei == null) return;
				Behavior preventAccessToModifiedClosureBehavior = behavior;
				ei.AddEventHandler(frameworkElement, new RoutedEventHandler((x, y) =>
				{
					Behavior b = preventAccessToModifiedClosureBehavior;
					b.Command.Execute(b.CommandParameter);
				}));
			}
		}
	}
}