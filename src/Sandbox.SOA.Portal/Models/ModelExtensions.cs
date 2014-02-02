using System;

using Sandbox.SOA.Common.Contracts.People;

namespace Sandbox.SOA.Portal.Models
{
    public static class ModelExtensions
    {
        public static string Full(this PersonName person)
        {
            if (person == null) throw new ArgumentNullException("person");

            var name = string.Format("{0} {1}", person.First, person.Last);

            return string.IsNullOrWhiteSpace(name)
                       ? "(not set)"
                       : name;
        }
    }
}