using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.Services;
using TrackWiseModel.Models;
using System.Configuration;

namespace TrackWiseModel.Controllers
{
    public class TrackWiseModelController : Controller
    {

        // Database connection Name
        string conn = ConfigurationManager.ConnectionStrings["DBCS"].ToString();
        // GET: TrackWiseModel
        public ActionResult Index(string txtInput)
        {
            List<TrackWise> trackmodel = null;

            if (string.IsNullOrEmpty(txtInput))
            {
               // TempData["DDLMessage"].ToString();
                return View();
            }
            else
            {
                trackmodel = GetSearchRecord(txtInput);
                if (trackmodel != null)
                {
                    return View(trackmodel.ToList());
                }
                else
                {
                    return View();
                }

            }
        }

        public List<TrackWise> GetSearchRecord(string prno)
        {
            List<TrackWise> trackmodels = new List<TrackWise>();

            // TempData["temp202"] = List202.ToList();
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "select tbt.Tid,tbt.PRNO,tbt.SId,tbs.HId,tbs.StateName,tbh.Header,tbh.PdfDocument,tbt.CreateDate from tblTrack tbt inner join tblStates tbs on tbt.SId =tbs.SId inner join tblHeader tbh on tbs.HId=tbh.HId where tbt.PRNO Like '%" + prno + "%'";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        con.Open();

                        //  var files = Directory.GetFiles(Server.MapPath(@"~/document" + prno));

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                trackmodels.Add(new TrackWise
                                {
                                    HID = Convert.ToInt32(sdr["HID"]),
                                    Tid = Convert.ToInt32(sdr["Tid"]),
                                    SId = Convert.ToInt32(sdr["SId"]),
                                    Header = Convert.ToString(sdr["Header"]).Trim(),
                                    PdfDocument = Convert.ToString(sdr["PdfDocument"]).Trim(),
                                    // PdfDocument = filePath, 

                                    StateName = Convert.ToString(sdr["StateName"]).Trim(),
                                    PRNO = Convert.ToInt32(sdr["PRNO"]),
                                    CreateDate = Convert.ToDateTime(sdr["CreateDate"])

                                });
                            }
                        }



                        con.Close();
                    }

                    if (trackmodels.Count == 0)
                    {
                        return null;
                    }

                    // List<UserAppModel> list = UserAppModels.Where(x => x.UserName.Contains(txtInput)).Select(x => new UserAppModel {UserId=x.UserId,UserName=x.UserName ,Password = x.Password, FirstName = x.FirstName, LastName = x.LastName, Email = x.Email, PhoneNo = x.PhoneNo, BirthDate = x.BirthDate, LastLogin = x.LastLogin, ModifiedDate = x.ModifiedDate, CreatedDate = x.CreatedDate, Gender = x.Gender, DepartmentName = x.DepartmentName }).ToList();
                    //  TempData["temp206"] = list;
                }
            }
            catch (Exception ex)
            {
                throw ex;
             // TempData["ErrorException"] = Exception(ex).ToString();
            }
            return trackmodels.ToList();
        }

        public string Exception(Exception ex)
        {
            string message = string.Format(ex.Message);
            return message;
        }


    }
}