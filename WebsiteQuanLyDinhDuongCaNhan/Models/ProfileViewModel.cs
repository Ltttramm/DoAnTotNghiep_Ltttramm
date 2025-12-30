using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebsiteQuanLyDinhDuongCaNhan.Models;

public class ProfileViewModel
{
    public int Id { get; set; } // Thêm ID người dùng

    [StringLength(255)]
    public string FullName { get; set; }

    [Required]
    [StringLength(255)]
    public string Email { get; set; }

    [Column(TypeName = "date")]
    public DateTime? DateOfBirth { get; set; }

    public int? Age
    {
        get
        {
            return DateOfBirth.HasValue ? (int?)(DateTime.Now.Year - DateOfBirth.Value.Year) : null;
        }

    }
    [StringLength(50)]
    public string Gender { get; set; }
    public double? Height { get; set; }
    public double? Weight { get; set; }

    [StringLength(50)]
    public string ActivityLevel { get; set; }

    [StringLength(50)]
    public string Goal { get; set; }

    [StringLength(50)]
    public string PreferredDiet { get; set; }

    [Column(TypeName = "text")]
    public string Allergy { get; set; }

   

    // Constructor từ User model
    public ProfileViewModel(User user)
    {
        if (user != null)
        {
            Id = user.UserID;
            FullName = user.FullName;
            Email = user.Email;
            DateOfBirth = user.DateOfBirth;
            //Age = user.Age;
            Gender = user.Gender;
            Height = user.Height;
            Weight = user.Weight;
            ActivityLevel = user.ActivityLevel;
            Goal = user.Goal;
            PreferredDiet = user.PreferredDiet;
            Allergy = user.Allergy;
            
        }
    }

    // Constructor mặc định
    public ProfileViewModel() { }

}
