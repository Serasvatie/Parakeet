namespace Parakeet.Models
{
	public interface IProjectHelper
	{
		string FileName { get; }
		Project Project { get; }

		void New(string filename);

		void Load(string fileName);
		void Save();
		void Save(string fileTitle);

	}
}
