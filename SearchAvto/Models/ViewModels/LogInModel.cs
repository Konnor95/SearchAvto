using System.ComponentModel.DataAnnotations;

namespace SearchAvto.Models.ViewModels
{
    public class LogInModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}