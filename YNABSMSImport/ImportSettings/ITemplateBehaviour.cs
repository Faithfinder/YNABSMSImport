namespace YNABSMSImport.ImportSettings
{
    internal interface ITemplateBehaviour
    {
        void ProcessMessage(string message, SMSTemplate template);
    }
}