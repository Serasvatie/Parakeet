
namespace Model
{
    public class DirectoryModel
    {
        private string _path;
        private bool _activated;

        public DirectoryModel()
        {
            _path = "";
            _activated = true;
        }

        public DirectoryModel(string path, bool activated)
        {
            _path = path;
            _activated = activated;
        }

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public bool Activated
        {
            get { return _activated; }
            set { _activated = value; }
        }
    }
}
