using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class DocDistResultModel
    {
        private string _first;
        private string _second;
        private double _dist;
        private double _pourcent;

        public DocDistResultModel()
        {
            _first = "";
            _second = "";
            _dist = 0;
            _pourcent = 0;
        }

        public DocDistResultModel(string first, string second, double dist, double pourcent)
        {
            _first = first;
            _second = second;
            _dist = dist;
            _pourcent = pourcent;
        }

        public string First
        {
            get { return _first; }
            set { _first = value; }
        }

        public string Second
        {
            get { return _second; }
            set { _second = value; }
        }

        public double Distance
        {
            get { return _dist; }
            set { _dist = value; }
        }

        public double Percentage
        {
            get { return _pourcent; }
            set { _pourcent = value; }
        }
    }
}
