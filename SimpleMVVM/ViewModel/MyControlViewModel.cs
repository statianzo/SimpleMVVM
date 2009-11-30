using System.Windows.Input;

namespace SimpleMVVM.ViewModel
{
	internal class MyControlViewModel : ViewModelBase
	{
		private string labelText;
		private ICommand setTextCommand;

		public ICommand SetTextCommand
		{
			get
			{
				if (setTextCommand == null)
					setTextCommand = new RelayCommand(x => setText((string) x));
				return setTextCommand;
			}
		}

		public string LabelText
		{
			get
			{
				if (labelText == null)
					labelText = "Click something!";
				return labelText;
			}
			set
			{
				if (labelText != value)
				{
					labelText = value;
					OnPropertyChanged("LabelText");
				}
			}
		}

		private void setText(string text)
		{
			LabelText = "You clicked: " + text;
		}
	}
}