using System.Collections.Generic;

namespace Parakeet.Model
{
    public class OutputPathModel
    {
        private List<DirectoryModel> _path;

        public OutputPathModel()
        {
            _path = new List<DirectoryModel>();
        }

        public List<DirectoryModel> Path
        {
            get { return _path; }
        }

        public DirectoryModel AddDirectory(string path, bool activated)
        {
            DirectoryModel tmp = new DirectoryModel(path, activated);
            _path.Add(tmp);
            return tmp;
        }

        public void RemoveDirectory(DirectoryModel dir)
        {
            _path.Remove(dir);
        }
    }
}
