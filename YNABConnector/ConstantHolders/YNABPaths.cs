using System;

namespace YNABConnector
{
    public static class YNABPaths
    {
        public const string AuthCodeGrantFlow = "https://app.youneedabudget.com/oauth/token";
        public const string Authorization = "https://app.youneedabudget.com/oauth/authorize";
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