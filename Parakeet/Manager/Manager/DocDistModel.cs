using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.Manager;

namespace Manager
{
    public class DocDistModel
    {
        private int _threshold;
        private Target _target;

        public DocDistModel()
        {
            _threshold = 50;
            _target = Target.Folder;
        }

        public DocDistModel(int _thres, Target target)
        {
            _threshold = _thres;
            _target = target;
        }

        public int Threshold
        {
            get { return _threshold; }
            set { _threshold = value; }
        }

        public Target Target
        {
            get { return _target; }
            set { _target = value; }
        }
    }
}
