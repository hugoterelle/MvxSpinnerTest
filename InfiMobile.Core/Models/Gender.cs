using Cirrious.MvvmCross.Plugins.Sqlite;
using System.Collections.Generic;

namespace InfiMobile.Core.Models
{
    public enum GenderEnum
    {
        Unknown, Male, Female
    }

    public class Gender : Model
    {
        [Indexed(Unique=true)]
        public int ID { get; set; }

        [Indexed(Name = "gender_idx_1", Unique = true, Order=1)]
        public GenderEnum GenderEnum { get; set; }
        
        public string Label { get; set; }
        
        [Indexed(Name = "gender_idx_1", Unique = true, Order=2)]
        public Language Language { get; set; }

        public Gender()
            : base()
        {
        }

        public Gender(int id, GenderEnum genderEnum, string label, Language language) : base()
        {
            ID = id;
            GenderEnum = genderEnum;
            Label = label;
            Language = language;
        }

        public static List<Gender> List()
        {
            return new List<Gender> 
            {
                new Gender(1, GenderEnum.Unknown, "Inconnu", Language.French),
                new Gender(1, GenderEnum.Male, "Homme", Language.French),
                new Gender(1, GenderEnum.Female, "Femme", Language.French)
            };
        }
    }
}
