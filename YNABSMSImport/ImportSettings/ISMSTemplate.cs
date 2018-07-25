using System;

namespace YNABSMSImport.ImportSettings
{
    internal interface ISMSTemplate
    {
        string RegExPattern { get; set; }
    }
}