﻿using System;

namespace HomeWork
{
    public class Budget
    {
        public string YearMonth { get; set; }

        public int Amount { get; set; }

        public decimal GetDaysInMonth()
        {
            decimal daysInMonth = DateTime.DaysInMonth(
                DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Year,
                DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Month);
            return daysInMonth;
        }

        public decimal GetDailyAmount()
        {
            var dailyAmount = Amount / GetDaysInMonth();
            return dailyAmount;
        }
    }
}