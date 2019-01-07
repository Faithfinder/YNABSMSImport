using System;
using System.Text.RegularExpressions;

namespace YNABSMSImport.ImportSettings
{
    internal abstract class SMSTemplate
    {
        public string UserTemplate { get; set; }

        public bool IsMatch(string message)
        {
            var regEx = new Regex(UserToRegEx());
            return regEx.Match(message).Success;
        }

        public string UserToRegEx()
        {
            var pattern = Regex.Escape(UserTemplate);

            foreach (var key in ExtractionKeys.GetAll())
            {
                pattern = pattern.Replace($"\\[{key}]", key.RegExPattern);
            }

            return pattern;
        }

        internal ITemplateBehaviour Behaviour;

        internal void ProcessMessage(string message) => Behaviour.ProcessMessage(message);
    }
}