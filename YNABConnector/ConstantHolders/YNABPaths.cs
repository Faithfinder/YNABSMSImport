using System;

namespace YNABConnector
{
    public static class YNABPaths
    {
        public const string AUTH_CODE_GRANT_FLOW = "https://app.youneedabudget.com/oauth/token";
        public const string AUTHORIZATION = "https://app.youneedabudget.com/oauth/authorize";
        internal const string BASE = "https://api.youneedabudget.com/";
        internal const string BUDGETS = "v1/budgets";

        internal static string Accounts(Guid budgetID)
        {
            return $"v1/budgets/{budgetID}/accounts";
        }

        internal static string Transactions(Guid budgetID)
        {
            return $"v1/budgets/{budgetID}/transactions";
        }
    }
}