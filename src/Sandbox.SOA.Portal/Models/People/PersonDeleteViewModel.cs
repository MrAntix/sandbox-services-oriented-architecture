﻿using System;

namespace Sandbox.SOA.Portal.Models.People
{
    public class PersonDeleteViewModel
    {
        public Guid Identifier { get; set; }
        public PersonNameEditViewModel Name { get; set; }
    }
}