using Parakeet.ViewModel;

namespace Parakeet.Model
{
    public class ChangeRule
    {
        private string _old;
        private string _new;
        private Target _target;
        private bool _activated;

        public ChangeRule()
        {
            _old = "";
            _new = "";
            _activated = false;
            _target = Target.All;
        }

        public ChangeRule(string old, string __new, bool isActivated, Target target)
        {
            _old = old;
            _new = __new;
            _activated = isActivated;
            _target = target;
        }

        public string Old
        {
            get { return _old; }
            set { _old = value; }
        }

        public string New
        {
            get { return _new; }
            set { _new = value; }
        }

        public Target Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public bool IsActivate
        {
            get { return _activated; }

            set { _activated = value; }
        }
    }
}