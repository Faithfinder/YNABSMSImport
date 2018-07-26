namespace YNABSMSImport.ImportSettings
{
    internal class IgnoreSMSBehaviour : ITemplateBehaviour
    {
        public void ProcessMessage(string message, SMSTemplate template)
        {
            //TODO Logging?
        }
    }
}