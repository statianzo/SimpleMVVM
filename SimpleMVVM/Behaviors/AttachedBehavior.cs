using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace SimpleMVVM.Behaviors
{
	/// <summary>
	/// Attached properties for tying an event to a command.
	/// </summary>
	internal class AttachedBehavior
	{
		public static readonly DependencyProperty CommandParameterProperty =
			DependencyProperty.RegisterAttached("CommandParameter", typeof (object),
			                                    typeof (AttachedBehavior));

		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.RegisterAttached("Command", typeof (ICommand),
			                                    typeof (AttachedBehavior),
			                                    new UIPropertyMetadata(EventChanged));

		public static readonly DependencyProperty EventNameProperty =
			DependencyProperty.RegisterAttached("EventName", typeof (string),
			                                    typeof (AttachedBehavior),
			                                    new UIPropertyMetadata(EventChanged));

		private static RoutedEventHandler handler;

		public static ICommand GetCommand(DependencyObject obj)
		{
			return (ICommand) obj.GetValue(CommandProperty);
		}

		public static void SetCommand(DependencyObject obj, ICommand value)
		{
			obj.SetValue(CommandProperty, value);
		}

		public static object GetCommandParameter(DependencyObject obj)
		{
			return obj.GetValue(CommandParameterProperty);
		}

		public static void SetCommandParameter(DependencyObject obj, object value)
		{
			obj.SetValue(CommandParameterProperty, value);
		}

		public static string GetEventName(DependencyObject obj)
		{
			return (string) obj.GetValue(EventNameProperty);
		}

		public static void SetEventName(DependencyObject obj, string value)
		{
			obj.SetValue(EventNameProperty, value);
		}

		private static void EventChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			var uie = obj as UIElement;
			string eventName = GetEventName(obj);
			if (uie != null && eventName != null)
			{
				handler = HandlerMethod;
				EventInfo ei = uie.GetType().GetEvent(eventName);
				if (ei == null) return;
				ei.RemoveEventHandler(uie, handler);
				if (e.NewValue != null)
					ei.AddEventHandler(uie, handler);
			}
		}

		private static void HandlerMethod(object sender, RoutedEventArgs e)
		{
			var uie = sender as UIElement;
			ICommand cmd = GetCommand(uie);
			if (cmd != null && cmd.CanExecute(null))
				cmd.Execute(GetCommandParameter(uie));
		}
	}
}