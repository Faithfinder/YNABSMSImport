using System.Threading.Tasks;

namespace YNABSMSImport.ImportSettings
{
    internal class IgnoreSMSBehaviour : ITemplateBehaviour
    {
        public Task ProcessMessage(string message)
        {
            //TODO Logging?
            return Task.CompletedTask;
        }
    }
}