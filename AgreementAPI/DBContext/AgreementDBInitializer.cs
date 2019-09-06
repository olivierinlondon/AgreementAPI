﻿using System;
using System.Data.Entity;
using AgreementAPI.Models;

namespace AgreementAPI.DBContext
{
    public class AgreementDBInitializer : DropCreateDatabaseAlways<AgreementContext>
    {
        protected override void Seed(AgreementContext context)
        {
            Customer c1 = new Customer
            {
                Id = 67812203006,
                FirstName = "Goras",
                LastName = "Trusevičius"
            };
            context.Customers.Add(c1);

            Customer c2 = new Customer
            {
                Id = 78706151287,
                FirstName = "Dange",
                LastName = "Kulkavičiutė"
            };
            context.Customers.Add(c2);

            Agreement ag1 = new Agreement(1.6)
            {
                Id = 1,
                CustomerDetails = c1,
                Amount = 12000,
                BaseCodeRate = "VILIBOR3m",
                Duration = 60
            };

            context.Agreements.Add(ag1);


            ag1 = new Agreement(2.2)
            {
                Id = 2,
                CustomerDetails = c2,
                Amount = 8000,
                BaseCodeRate = "VILIBOR1y",
                Duration = 36
            };

            context.Agreements.Add(ag1);

            ag1 = new Agreement(1.85)
            {
                Id = 3,
                CustomerDetails = c2,
                Amount = 1000,
                BaseCodeRate = "VILIBOR6m",
                Duration = 24
            };

            context.Agreements.Add(ag1);

        }

    }
}