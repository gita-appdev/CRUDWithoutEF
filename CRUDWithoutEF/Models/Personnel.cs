using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class Personnel
    {
		[Key]
		public int Personnel_ID { get; set; }
		[Required]
		public string Lname { get; set; }
		[Required]
		public string Fname { get; set; }
		public string Mname { get; set; }
		public string Nickname { get; set; }
		[Required]
		public string EmpType { get; set; }
		public string ARank { get; set; }
		public string ABranch { get; set; }
		[Required]
		public Boolean Active { get; set; }
		public string Email { get; set; }
		[Required]
		public DateTime DliHire { get; set; }
		public string OfficePhone { get; set; }
		public int Location1 { get; set; }
		public int Location2 { get; set; }
		public int AltOfficeSymbol { get; set; }
		
    }
}
