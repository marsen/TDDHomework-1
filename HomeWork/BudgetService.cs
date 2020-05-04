﻿using System;
using System.Linq;

namespace HomeWork
{
    public class BudgetService
    {
        private IBudgetRepo _budgetRepo;

        public BudgetService(IBudgetRepo budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public decimal Query(DateTime start, DateTime end)
        {
            if (start > end)
                return 0;

            if (end.Month - start.Month >= 1)
            {
                var daysInStartMonth = DateTime.DaysInMonth(start.Year, start.Month);
                var amountOfStartMonth = GetBudget(start.ToString("yyyyMM")).Amount;
                var queryDaysInStart = DateTime.DaysInMonth(start.Year, start.Month) - start.Day + 1;
                int startAmount =  queryDaysInStart * (amountOfStartMonth / daysInStartMonth);
                
                int middleAmount = 0;
                for (int i = 1; i < end.Month - start.Month; i++)
                {
                    var middleDate = start.AddMonths(i);

                    var daysInMiddleMonth = DateTime.DaysInMonth(middleDate.Year, middleDate.Month);
                    var amountOfMiddleMonth = GetBudget(middleDate.ToString("yyyyMM")).Amount;
                    var queryDaysInMiddle = DateTime.DaysInMonth(middleDate.Year, middleDate.Month);
                    middleAmount += queryDaysInMiddle * (amountOfMiddleMonth / daysInMiddleMonth);
                }

                var daysInEndMonth = DateTime.DaysInMonth(end.Year, end.Month);
                var amountOfEndMonth = GetBudget(end.ToString("yyyyMM")).Amount;
                var queryDaysInEnd = (end.Day);
                int endAmount = queryDaysInEnd * (amountOfEndMonth / daysInEndMonth);

                return startAmount + middleAmount + endAmount;
            }

            int totalDay = (end - start).Days + 1;
            var days3 = DateTime.DaysInMonth(start.Year, start.Month);
            var amount3 = _budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == start.ToString("yyyyMM")).Amount;
            return amount3 / days3 * totalDay;
        }

        private Budget GetBudget(string yearMonth)
        {
            var budgetRepo = _budgetRepo;
            return budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == yearMonth);
        }
    }
}