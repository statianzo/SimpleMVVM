using System.Windows;
using System.Windows.Input;

namespace SimpleMVVM.Behaviors
{
	public class Behavior : FrameworkElement
	{
		public static readonly DependencyProperty CommandParameterProperty =
			DependencyProperty.Register("CommandParameter", typeof(object), typeof(Behavior));

		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register("Command", typeof(ICommand), typeof(Behavior));

		public static readonly DependencyProperty EventNameProperty =
			DependencyProperty.Register("EventName", typeof(string), typeof(Behavior)); 

		public string EventName
		{
			get { return (string) GetValue(EventNameProperty); }
			set { SetValue(EventNameProperty, value); }
		}

		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public ICommand Command
		{
			get { return (ICommand) GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}
	}
}