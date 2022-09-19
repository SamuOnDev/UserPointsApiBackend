using System.ComponentModel.DataAnnotations;

namespace UserPointsApiBackend.Models.DataModels
{
    public class User : BaseEntity
    {
        [Required, StringLength(30)]
        public string UserName { get; set; } = string.Empty;

        [Required, StringLength(30)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(30)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(30)]
        public string Password { get; set; } = string.Empty;
    }
}
