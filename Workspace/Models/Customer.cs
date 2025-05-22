using System.ComponentModel.DataAnnotations;

namespace Workspace.Models
{
	public class CustomerUser
	{
		[Required(ErrorMessage = "This field is required for employee's data")]
		public string Fname { get; set; }
		[Required(ErrorMessage = "This field is required for employee's data")]
		public string Lname { get; set; }
		[Required(ErrorMessage = "This field is required for employee's data")]
		[EmailAddress(ErrorMessage = "kindly enter the email address with the a format of name@spacedomain.com")]
		public string Email { get; set; }
		[Required(ErrorMessage = "kindly select the employee employment status")]
		public int status { get; set; }
		[Required(ErrorMessage = "This field is required for employee's data")]
		[DataType(DataType.PhoneNumber, ErrorMessage = "phone Number  is ")]
		public int phoneno { get; set; }
		[Required(ErrorMessage = "kindly enter the employees work ID")]
		[Range(1, int.MaxValue, ErrorMessage = "kindly enter a valid Work ID for the new employee")]
		public int ID { get; set; }

	

		[Required(ErrorMessage = "please enter the employee credentials")]
		[MinLength(3, ErrorMessage = "The username is too short")]
		public string username { get; set; }

		[Required(ErrorMessage = "kindly enter the employee credentials")]

		[MinLength(5, ErrorMessage = "The password  is too short/weak,Password should at least be five characters")]
		public string Pass { get; set; }









		[Required]
		public string gender { get; set; }


		[Required]
		[Range(1, 100, ErrorMessage = "kindly enter a valid age ")]
		public int age { get; set; }

 








	}
}
