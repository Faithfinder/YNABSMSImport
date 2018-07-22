using Newtonsoft.Json;

namespace YNABConnector.YNABObjectModel
{
    public class SaveTransactionWrapper
    {
        public SaveTransaction Transaction { get; set; }

        public string Serialize()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new LowercaseContractResolver()
            };
            return JsonConvert.SerializeObject(this, settings);
        }
    }
}