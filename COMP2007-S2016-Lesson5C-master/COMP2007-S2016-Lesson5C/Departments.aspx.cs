using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements that are required to connect to EF DB
using university1.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace university1
{
    public partial class Departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if loading the page for the first time, populate the Department grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "DepartmentID";
                Session["SortDirection"] = "ASC";
                // Get the department data
                this.GetDepartment();
            }
        }

        /**
         * <summary>
         * This method gets the Department data from the DB
         * </summary>
         * 
         * @method Getdepartment
         * @returns {void}
         */
        protected void GetDepartment()
        {
            string sortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

            // connect to EF
            using (DefaultConnection db = new DefaultConnection())
            {
                // query the Department Table using EF and LINQ
                var Department = (from allDepartments in db.Departments
                                  select allDepartments);

                // bind the result to the GridView
                DepartmentGridView.DataSource = Department.AsQueryable().OrderBy(sortString).ToList();
                DepartmentGridView.DataBind();
            }
        }

        protected void DepartmentGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

            // get the selected StudentID using the Grid's DataKey Collection
            int DepartmentID = Convert.ToInt32(DepartmentGridView.DataKeys[selectedRow].Values["DepartmentID"]);

            // use EF to find the selected student from DB and remove it
            using (DefaultConnection db = new DefaultConnection())
            {
                Department deletedDepartment = (from DepartmentRecords in db.Departments
                                          where DepartmentRecords.DepartmentID == DepartmentID
                                          select DepartmentRecords).FirstOrDefault();

                // perform the removal in the DB
                db.Departments.Remove(deletedDepartment);
                db.SaveChanges();

                // refresh the grid
                this.GetDepartment();

            }
        }



        protected void DepartmentGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page number
            DepartmentGridView.PageIndex = e.NewPageIndex;

            // refresh the grid
            this.GetDepartment();
        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set new page size
            DepartmentGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the grid
            this.GetDepartment();
        }

        protected void DepartmentGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            // refresh the grid
            this.GetDepartment();

            // toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void DepartmentGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) // check to see if the click is on the header row
                {
                    LinkButton linkbutton = new LinkButton();

                    for (int index = 0; index < DepartmentGridView.Columns.Count; index++)
                    {
                        if (DepartmentGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = " <i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }
    }
}