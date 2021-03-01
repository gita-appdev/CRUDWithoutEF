using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD.Models;
using CRUDWithoutEF.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CRUDWithoutEF.Controllers
{
    public class PersonnelsController : Controller
    {
        private readonly IConfiguration _configuration;

        public PersonnelsController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: Personnels
        public IActionResult Index()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("spGetAllPersonnel", sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dtbl);
            }
            return View(dtbl);
        }

        // GET: Personnels/AddOrEdit/5
        public IActionResult AddOrEdit(int? id)
        {
            Personnel personnel = new Personnel();
            if (id > 0)
                personnel = FetchPersonnelById(id);
            return View(personnel);
        }

        // POST: Personnels/AddOrEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, [Bind("Personnel_ID,Lname,Fname,Mname,Nickname,EmpType,ARank,ABranch,Active,Email,DliHire,OfficePhone,Location1,Location2,AltOfficeSymbol")] Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spPersonnelAddOrEdit", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Personnel_ID", personnel.Personnel_ID);
                    cmd.Parameters.AddWithValue("@L_Name", personnel.Lname);
                    cmd.Parameters.AddWithValue("@F_Name", personnel.Fname);
                    cmd.Parameters.AddWithValue("@M_Name", personnel.Mname);
                    cmd.Parameters.AddWithValue("@Nickname", personnel.Nickname);
                    cmd.Parameters.AddWithValue("@Emp_Type", personnel.EmpType);
                    cmd.Parameters.AddWithValue("@A_Rank", personnel.ARank);
                    cmd.Parameters.AddWithValue("@A_Branch", personnel.ABranch);
                    cmd.Parameters.AddWithValue("@Active", personnel.Active);
                    cmd.Parameters.AddWithValue("@Email", personnel.Email);
                    cmd.Parameters.AddWithValue("@DLI_Hire", personnel.DliHire);
                    cmd.Parameters.AddWithValue("@Office_Phone", personnel.OfficePhone);
                    cmd.Parameters.AddWithValue("@Location_1", personnel.Location1);
                    cmd.Parameters.AddWithValue("@Location_2", personnel.Location2);
                    cmd.Parameters.AddWithValue("@Alt_Office_Symbol", personnel.AltOfficeSymbol);
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Personnels/Delete/5
        public IActionResult Delete(int? id)
        {
            Personnel personnel = FetchPersonnelById(id);
            return View(personnel);
        }

        // POST: Personnels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("spDeletePersonnel", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Personnel_ID", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }
        [NonAction]
        public Personnel FetchPersonnelById(int? id)
        {
            Personnel personnel = new Personnel();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DataTable dtbl = new DataTable();
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("spGetPersonnelById", sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("Personnel_ID", id);
                adapter.Fill(dtbl);
                if (dtbl.Rows.Count == 1)
                {
                    personnel.Personnel_ID = Convert.ToInt32( dtbl.Rows[0]["Personnel_ID"].ToString());
                    personnel.Lname = dtbl.Rows[0]["L_Name"].ToString();
                    personnel.Fname = dtbl.Rows[0]["F_Name"].ToString();
                    personnel.Mname = dtbl.Rows[0]["M_Name"].ToString();
                    personnel.Nickname = dtbl.Rows[0]["Nickname"].ToString();
                    personnel.EmpType = dtbl.Rows[0]["Emp_Type"].ToString();
                    personnel.ARank = dtbl.Rows[0]["A_Rank"].ToString();
                    personnel.ABranch = dtbl.Rows[0]["A_Rank"].ToString();
                    personnel.Active = Convert.ToBoolean( dtbl.Rows[0]["Active"].ToString());
                    personnel.Email = dtbl.Rows[0]["Email"].ToString();
                    personnel.DliHire = Convert.ToDateTime(dtbl.Rows[0]["DLI_Hire"].ToString());
                    personnel.OfficePhone = dtbl.Rows[0]["Office_Phone"].ToString();
                    personnel.Location1 = Convert.ToInt32(dtbl.Rows[0]["Location_1"].ToString());
                    personnel.Location2 = Convert.ToInt32(dtbl.Rows[0]["Location_2"].ToString());
                    personnel.AltOfficeSymbol = Convert.ToInt32(dtbl.Rows[0]["Alt_Office_Symbol"].ToString());
                }
                return personnel;
            }
        }

       
    }
}
