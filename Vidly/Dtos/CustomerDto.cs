using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubsrcibedToNewsletter { get; set; }


        /*
         This is not needed here, can be deleted, as well as the display attributes of other members
        [Display(Name = "Membership type")]
        // a navigation property: it allows us to navigate from one type to another
        public MembershipType MembershipType { get; set; }
        */

        // Entity framework will recognize this as a FK: !!!!!!!!!!! by its naming based on E.F.'s naming convenction rules
        public byte MembershipTypeId { get; set; }

        [Min18YearsIfAMember]
        public DateTime? Birthday { get; set; }
    }
}