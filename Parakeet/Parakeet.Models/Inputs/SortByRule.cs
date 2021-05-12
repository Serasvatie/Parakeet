namespace Parakeet.Models.Inputs
{
    public class SortByRule
    {
        private string _rule;
        private bool _activated;

        public SortByRule()
        {
            _rule = "";
            _activated = false;
        }

        public SortByRule(string rule, bool activated)
        {
            _rule = rule;
            _activated = activated;
        }

        public string Strings
        {
            get { return _rule; }
            set { _rule = value; }
        }

        public bool IsActivated
        {
            get { return _activated; }
            set { _activated = value; }
        }
    }
}
