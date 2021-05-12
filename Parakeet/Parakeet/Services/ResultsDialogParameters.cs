using Parakeet.Models.Outputs;
using Prism.Services.Dialogs;

namespace Parakeet.Services
{
	public class ResultsDialogParameters : DialogParameters
	{
		public ResultOutput Result { get; }

		public ResultsDialogParameters(ResultOutput result)
		{
			Result = result;
		}
	}
}
