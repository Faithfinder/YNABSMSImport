using System;

namespace YNABConnector
{
    internal static class YNABPaths
    {
        internal const string Base = "https://api.youneedabudget.com/";
        internal const string Budgets = "v1/budgets";

        internal static string Accounts(Guid budget_id)
        {
            return $"v1/budgets/{budget_id}/accounts";
        }

        internal static string Transactions(Guid budget_id)
        {
            return $"v1/budgets/{budget_id}/transactions";
        }
    }
}