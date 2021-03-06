﻿using System;
using System.Linq;

namespace HomeWork
{
    public class BudgetService
    {
        private readonly IBudgetRepo _budgetRepo;

        public BudgetService(IBudgetRepo budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public decimal Query(DateTime start, DateTime end)
        {
            return _budgetRepo
                .GetAll()
                .Sum(budget => budget.GetPeriodAmount(new Period(start, end)));
        }
    }
}