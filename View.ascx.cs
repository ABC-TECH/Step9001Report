/*
' Copyright (c) 2012  DotNetNuke Corporation
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Net;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Web.Script.Serialization;
using OfficeOpenXml;
using System.Reflection;

using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using System.Configuration;

using ABC.BusinessObjects.SharedBusiness;
using ABC.BusinessObjects.StepApplicationBusiness.StepApplicationObjects;

namespace DotNetNuke.Modules.Step9001Report
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from Step9001ReportModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Step9001ReportModuleBase
    {
        private const int FIRST_YEAR_OF_STEP = 1998;


        #region Event Handlers

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PopulateApplicationYears();
                    PopulateChapters();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void ExportToExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            String sqlConnection1 = ConfigurationManager.ConnectionStrings["AmsSqlServer"].ConnectionString;
            var con = new SqlConnection(sqlConnection1);
            var cmd = new SqlCommand("report_step_9001_full_company_sp", con);

            cmd.CommandType = CommandType.StoredProcedure;

            if (ddlChapter.SelectedValue != "0")
            {
                cmd.Parameters.Add(new SqlParameter("@chapter_id", Convert.ToInt32(ddlChapter.SelectedValue)));
            }
            
            cmd.Parameters.Add(new SqlParameter("@year", Convert.ToInt32(ddlApplicationYear.SelectedValue)));

            cmd.Parameters.Add(new SqlParameter("@program_type_code", (ddlProgram.SelectedValue)));

            SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            sqlDa.Fill(dt);



            string sExcelFileName = string.Format("Find Contractors {0}", "Step 9001 Report");
            ExcelPackage excel = new ExcelPackage();
            var worksheet = excel.Workbook.Worksheets.Add(sExcelFileName);

            var totalCols = dt.Columns.Count;
            var totalRows = dt.Rows.Count;

            for (var col = 1; col <= totalCols; col++)
            {
                worksheet.Cells[1, col].Value = dt.Columns[col - 1];
            }

            for (var row = 1; row < totalRows + 1; row++)
            {
                for (var col = 0; col < totalCols; col++)
                {
                    worksheet.Cells[row + 1, col + 1].Value = dt.Rows[row - 1][col].ToString();
                    
                }
            }

            using (var memoryStream = new MemoryStream())
            {
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachemnt; filename=Step9001Report" + "_" + DateTime.Today.ToShortDateString() + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        #endregion

        private void PopulateChapters()
        {
            List<KeyValuePair<int, string>> listChapters = SharedHelper.GetChapters(Cache);

            ddlChapter.Items.Clear();
            ddlChapter.Items.Insert(0, new ListItem("-- All Chapters --", "0"));
            foreach (KeyValuePair<int, string> kvChapter in listChapters)
            {
                ddlChapter.Items.Add(new ListItem(kvChapter.Value, kvChapter.Key.ToString()));
            }
        }

        private void PopulateApplicationYears()
        {
            ddlApplicationYear.Items.Clear();
            for (int applicationYear = DateTime.Now.Year; applicationYear >= FIRST_YEAR_OF_STEP; applicationYear--)
            {
                ddlApplicationYear.Items.Add(new ListItem(applicationYear.ToString()));
            }
            ddlApplicationYear.Items.Add(new ListItem("-- All Time --", "0"));
            ddlApplicationYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        #region Optional Interfaces

        //public ModuleActionCollection ModuleActions
        //{
        //    get
        //    {
        //        ModuleActionCollection Actions = new ModuleActionCollection();
        //        Actions.Add(GetNextActionID(), Localization.GetString("EditModule", this.LocalResourceFile), "", "", "", EditUrl(), false, SecurityAccessLevel.Edit, true, false);
        //        return Actions;
        //    }
        //}

        #endregion

    }

}
