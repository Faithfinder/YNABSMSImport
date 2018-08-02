using System;
using System.Collections.Generic;

namespace YNABSMSImport.ImportSettings
{
    internal class UserSetting
    {
        public UserSetting()
        {
            Id = Guid.NewGuid();
            Name = "";
            Templates = new List<SMSTemplate>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Sender { get; set; }

        public List<SMSTemplate> Templates { get; set; }

        public static UserSetting TemporaryOtkritie()
        {
            return new UserSetting
            {
                Id = Guid.Parse("5C39B3AA-C7EF-42E4-8AAE-B5CA9B43160D"),
                Name = "Банк открытие",
                Sender = "OTKRITIE",
                Templates = new List<SMSTemplate>
                {
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
                if (template.IsMatch(messageText))
                    return template;

            return new IgnoreSMS();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}