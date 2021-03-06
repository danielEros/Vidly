﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubsrcibedToNewsletter { get; set; }

        
        [Display(Name = "Membership type")]
        // a navigation property: it allows us to navigate from one type to another
        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership type")]
        // Entity framework will recognize this as a FK: !!!!!!!!!!! by its naming based on E.F.'s naming convenction rules
        public byte MembershipTypeId { get; set; }

        [Display(Name = "Date of birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthday { get; set; }
    }
}