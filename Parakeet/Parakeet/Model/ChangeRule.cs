
namespace Parakeet.Model
{
    public class ChangeRule
    {
        private string _old;
        private string _new;
        private bool _activated;

        public ChangeRule()
        {
            _old = "";
            _new = "";
        }

        public ChangeRule(string old, string __new, bool isActivated)
        {
            _old = old;
            _new = __new;
            _activated = isActivated;
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

        public bool IsActivate
        {
            get
            {
                return _activated;
            }

            set
            {
                _activated = value;
            }
        }
    }
}
