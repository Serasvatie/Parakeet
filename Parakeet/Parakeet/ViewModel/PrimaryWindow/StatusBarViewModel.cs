using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parakeet.Model;

namespace Parakeet.ViewModel.PrimaryWindow
{
    class StatusBarViewModel
    {
        public StatusBarViewModel()
        {
        }

        public string FileName => Data.getInstance().FileTitle;

        public string StatusTask => "";
    }
}