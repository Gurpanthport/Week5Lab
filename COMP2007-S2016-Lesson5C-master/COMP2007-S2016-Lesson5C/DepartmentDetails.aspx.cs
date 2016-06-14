using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements required for EF DB access
using university1.Models;
using System.Web.ModelBinding;


namespace university1
{
    public partial class DepartmentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetDepartment();
            }
        }

        protected void GetDepartment()
        {
            // populate the form with existing department data from the db
            int DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

            // connect to the EF DB
            using (DefaultConnection db = new DefaultConnection())
            {
                // populate a department instance with the departmentID from the URL parameter
                Department updatedDepartment = (from Department in db.Departments
                                                where Department.DepartmentID == DepartmentID
                                                select Department).FirstOrDefault();

                // map the Department properties to the form controls
                if (updatedDepartment != null)
                {
                    NameTextBox.Text = updatedDepartment.Name;
                    Budget.Text = updatedDepartment.Budget.ToString("{0:C}");
                }
            }
        }


        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to Students page
            Response.Redirect("~/Departments.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to connect to the server
            using (DefaultConnection db = new DefaultConnection())
            {
                // use the Department model to create a new Department object and
                // save a new record
                Department newDepartment = new Department();

                int DepartmentID = 0;

                if (Request.QueryString.Count > 0)
                {
                    // get the id from url
                    DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                    // get the current Department from EF DB
                    newDepartment = (from Department in db.Departments
                                     where Department.DepartmentID == DepartmentID
                                     select Department).FirstOrDefault();
                }

                // add form data to the new Department record
                newDepartment.Name = NameTextBox.Text;
                newDepartment.Budget = Convert.ToInt32(Budget.Text);

                //  Budget.Text =  updatedDepartment.Budget.ToString("{0:C}");

                // use LINQ to ADO.NET to add / insert new Department into the database

                // check to see if a new Department is being added
                if (DepartmentID == 0)
                {
                    db.Departments.Add(newDepartment);
                }

                // save our changes - run an update
                db.SaveChanges();

                // Redirect back to the updated Department page
                Response.Redirect("~/Departments.aspx");
            }
        }
    }
}