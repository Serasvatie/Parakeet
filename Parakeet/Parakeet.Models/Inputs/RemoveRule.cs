namespace Parakeet.Models.Inputs
{
    public class RemoveRule
    {
        private string _string;
        private bool _extension;
        private bool _activated;

        public RemoveRule(string __string, bool extension, bool activated)
        {
            _string = __string;
            _extension = extension;
            _activated = activated;
        }

        public RemoveRule()
        {
            _string = "";
            _extension = false;
            _activated = false;
        }

        public string Strings
        {
            get { return _string; }
            set { _string = value; }
        }

        public bool IsExtension
        {
            get
            {
                return _extension;
            }
            set { _extension = value; }
        }

        public bool IsActivated
        {
            get { return _activated; }
            set { _activated = value; }
        }
    }
}