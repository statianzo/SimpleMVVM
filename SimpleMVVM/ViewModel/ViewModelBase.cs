using System.ComponentModel;

namespace SimpleMVVM.ViewModel
{
	internal abstract class ViewModelBase : INotifyPropertyChanged
	{
		private string displayName;

		public virtual string DisplayName
		{
			get { return displayName; }
			set
			{
				displayName = value;
				OnPropertyChanged("DisplayName");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				var args = new PropertyChangedEventArgs(propertyName);
				PropertyChanged(this, args);
			}
		}
	}
}