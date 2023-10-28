using lab2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        using (BankDepositsContext db = new BankDepositsContext())
        {
            var query = db.Contributors
                    .Join(db.Bankdeposits, c => c.Id, bd => bd.ContributorId, (c, bd) => new { c.FirstName, c.LastName, c.Patronymic })
                    .GroupBy(g => new { g.FirstName, g.LastName, g.Patronymic })
                    .Where(g => g.Count() > 1)
                    .Select(g => new
                    {
                        g.Key.FirstName,
                        g.Key.LastName,
                        g.Key.Patronymic,
                        Count = g.Count()
                    })
                    .ToList();

            foreach (var item in query)
            {
                Console.WriteLine($"Прізвище: {item.LastName}, Ім'я: {item.FirstName}, По батькові: {item.Patronymic}, Кількість: {item.Count}");
            }
        }
    }
}




