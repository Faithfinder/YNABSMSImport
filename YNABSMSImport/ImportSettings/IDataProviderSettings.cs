using System;
using System.Collections.Generic;

namespace YNABSMSImport.ImportSettings
{
    internal interface IDataProviderSettings
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Sender { get; set; }
        List<SMSTemplate> Templates { get; set; }

        SMSTemplate ChooseTemplate(string messageText);
    }
}