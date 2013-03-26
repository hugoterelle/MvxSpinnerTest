using Cirrious.MvvmCross.Plugins.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiMobile.Core.Models
{
    public class City : Model
    {
        [Indexed(Name = "city_idx_1", Unique = true, Order=1)]
        public int ID { get; set; }

        [Indexed(Name = "city_idx_1", Unique = true, Order=2)]
        public Language Language { get; set; }

        [MaxLength(200)]
        public string CityName { get; set; }

        [MaxLength(6)]
        public string Zip { get; set; }

        [Indexed]
        public int CountryID { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", CityName);
        }
    }
}
