using System.ComponentModel.DataAnnotations;

namespace Interview_Exercise
{
    public class Country
    {
        string _name;
        string _code;

        [Required]
        public string Name { get; set; }

        [MaxLength(3)]
        [Required]
        public string Code {
            get
            {
                return _code.ToUpper();
            }

            set
            {
                _code = value.ToUpper();
            }
        }
    }
}