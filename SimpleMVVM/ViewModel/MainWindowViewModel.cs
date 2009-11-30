namespace SimpleMVVM.ViewModel
{
	internal class MainWindowViewModel : ViewModelBase
	{
		private ViewModelBase innerControl;

		public ViewModelBase InnerControl
		{
			get
			{
				if (innerControl == null)
					innerControl = new MyControlViewModel();
				return innerControl;
			}
		}
	}
}