﻿
using System;
using System.Collections.Generic;

namespace DomainModel
{
    public class User
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Family { get; private set; }

        public int Age { get; private set; }

        public string NationalCode { get; private set; }

        public string Email { get; private set; }

        public int AdminId { get; private set; }

        public DateOnly ExpiractionDate { get; private set; }

        public virtual List<Interaction> Interactions { get; private set; } = new List<Interaction>();

        public virtual Admin Admin { get; private set; }

        User(string name, string family, int age, string nationalCode, string email, int adminId)
        {
            Name = name;
            Family = family;
            Age = age;
            NationalCode = nationalCode;
            Email = email;
            AdminId = adminId;
            ExpiractionDate = new DateOnly().AddYears(1);
        }

        private User() { }

        public static User Create(string name, string family, int age, string nationalCode, string email, int adminId)
        => new(name, family, age, nationalCode, email, adminId);

        public void Modify(string name, string family, int age, string email, int adminid)
        {
            Name = name;
            Family = family;
            Age = age;
            Email = email;
            AdminId = adminid;
        }


    }
}