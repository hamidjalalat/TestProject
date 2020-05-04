using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModels.Users
{
    public class CreateViewModel
    {
        public CreateViewModel() : base()
        {
        }
        // **********
        public int Id { get; set; }
        // **********

        // **********

        [Remote(action: "CheckUsername", controller: "Account", areaName: "",
         ErrorMessage ="نام کاربری تکراری می باشد")]
        [Display(Name = "نام کاربری")]
        [StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessageResourceType = typeof(Resources.Users),
            ErrorMessageResourceName = Resources.Strings.UsersKeys.minimumlength)]
        public string Name { get; set; }
        // **********

        // **********
        [Display(Name = "شماره همراه")]
        [RegularExpression(pattern: "09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}",
        ErrorMessage = "شماره موبایل معتبر نمی باشد")]
        [Required(AllowEmptyStrings = false,ErrorMessage ="وارد کردن شماره موبایل الزامی می باشد") ]

        public string Mobile { get; set; }
        // **********

        // **********
        [Display(Name = "آدرس")]
        [Required(AllowEmptyStrings = false,ErrorMessage ="وارد کردن آدرس الزامی می باشد")]
        public string Address { get; set; }
        // **********

        // **********
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.Users), Name = Resources.Strings.UsersKeys.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resources.Users)
            , ErrorMessageResourceName = Resources.Strings.UsersKeys.RequiredFieldValidator)]
        public string Password { get; set; }
        // **********

        // **********
        [Display(ResourceType = typeof(Resources.Users), Name = Resources.Strings.UsersKeys.ConfirmPassword)]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resources.Users)
            , ErrorMessageResourceName = Resources.Strings.UsersKeys.RequiredFieldValidator)]
        [System.ComponentModel.DataAnnotations.Compare(otherProperty: "Password",
            ErrorMessageResourceType = typeof(Resources.Users), ErrorMessageResourceName = Resources.Strings.UsersKeys.donotmatch)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        // **********



    }
}
