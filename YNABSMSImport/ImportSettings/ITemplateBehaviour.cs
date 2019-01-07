using System.Threading.Tasks;

namespace YNABSMSImport.ImportSettings
{
    internal interface ITemplateBehaviour
    {
        Task ProcessMessage(string message);
    }
}