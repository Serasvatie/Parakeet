using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parakeet.Model
{
    public class Data
    {
        private readonly OutputPathModel _path;

        public Data()
        {
            _path = new OutputPathModel();
        }

        public OutputPathModel Path
        {
            get { return _path; }
        }
    }
}
