﻿using FluentAssertions;
using NSpec;

namespace RPGAAS
{

    using NSpec;

    class describe_contexts : nspec
    {
        //context methods are discovered by NSpec (any method that contains underscores)
        void describe_Account()
        {
            //contexts can be nested n-deep and contain befores and specifications
            context["when withdrawing cash"] = () =>
            {
                before = () => account = new Account();

                context["account is in credit"] = () =>
                {
                    before = () => account.Balance = 500;

                    it["the Account dispenses cash"] = () =>
                    {
                        account.CanWithdraw(60).should_be_true();
                    }
                };

                context["account is overdrawn"] = () =>
                {
                    before = () => account.Balance = -500;

                    it["the Account does not dispense cash"] = () =>
                    {
                        account.CanWithdraw(60).should_be_false();
                    }
                };
            };
        }

        Account account;
    }
}