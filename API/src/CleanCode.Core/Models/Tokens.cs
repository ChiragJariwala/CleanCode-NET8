using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Core.Models
{
	public class Tokens  //Used for Authentication Token
	{
		public string Token { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public string MobileNo { get; set; }
        public string RefreshToken { get; set; }

        public List<UserDepartmentDesignationStatus> lstDepartmentDesignation { get; set; }
	}

	public class UserDepartmentDesignationStatus
    {
        public int VolunteerTxnId { get; set; }
        public int VolunteerId { get; set; }
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
        public int DesignationId { get; set; }

        public string DesignationName { get; set; }
        public bool Status { get; set; }
    }
}
