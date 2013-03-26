using Cirrious.MvvmCross.Plugins.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiMobile.Core.Models
{
    public class Patient : Model
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public GenderEnum Gender { get; set; }

        [MaxLength(8)]
        public string DateOfBirth { get; set; }
        
        [MaxLength(11), Unique]
        public string Niss { get; set; }

        [MaxLength(50), Unique]
        public string OARegistration { get; set; }

        [MaxLength(50), Unique]
        public string OAIdentification { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} ({2}), {3}", FirstName, LastName, Gender, DateOfBirth);
        }
    }
}
