using System;
using System.Collections.Generic;

namespace YNABSMSImport.ImportSettings
{
    internal class UserSettings : IDataProviderSettings
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Sender { get; set; }

        public List<SMSTemplate> Templates { get; set; }

        public UserSettings()
        {
            Templates = new List<SMSTemplate>();
        }

        public static UserSettings TemporaryOtkritie()
        {
            return new UserSettings
            {
                Sender = "OTKRITIE",
                Templates = new List<SMSTemplate> {
                    new AddTransaction
                    {
                        AccountID = Guid.Parse("126cda04-98fb-f0bf-1760-b96a7fa27d42"),
                        BudgetID = Guid.Parse("7ffba7b3-81b2-4ce1-a546-1b176368cc1b"),
                        UserTemplate = @"Платёж [Amount] р. в [Payee]. Карта *[RandomText]. Доступно [RandomText] р."
                    }
                }
            };
        }

        public SMSTemplate ChooseTemplate(string messageText)
        {
            foreach (var template in Templates)
            {
                if (template.isMatch(messageText))
                {
                    return template;
                }
            }
            return new IgnoreSMS();
        }
    }
}