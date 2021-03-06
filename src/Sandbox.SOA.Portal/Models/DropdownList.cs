﻿namespace Sandbox.SOA.Portal.Models
{
    public class DropdownList
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public static implicit operator DropdownList(long value)
        {
            return new DropdownList{Value = value.ToString()};
        }
    }
}