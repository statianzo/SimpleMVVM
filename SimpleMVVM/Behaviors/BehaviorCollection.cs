using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace SimpleMVVM.Behaviors
{
	[ContentProperty("Behaviors")]
	public class BehaviorCollection : FrameworkElement, IEnumerable<Behavior>
	{
		public BehaviorCollection()
		{
			DataContextChanged += BehaviorCollectionDataContextChanged;
		}

		void BehaviorCollectionDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			foreach (var behavior in this)
			{
				BindingOperations.SetBinding(behavior,
					DataContextProperty,
					new Binding("DataContext") { Source = this });
			}
		}

		private ObservableCollection<Behavior> behaviors;
		public ObservableCollection<Behavior> Behaviors
		{
			get
			{
				if (behaviors == null)
				{
					behaviors = new ObservableCollection<Behavior>();
				}
				return behaviors;
			}
		}

		public IEnumerator<Behavior> GetEnumerator()
		{
			return Behaviors.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Behaviors.GetEnumerator();
		}
	}
}