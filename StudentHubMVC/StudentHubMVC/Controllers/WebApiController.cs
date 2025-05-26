using StudentHubMVC.Repositories;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Configuration;
using StudentHubMVC.Helpers;
using StudentHubMVC.Models.ViewModels;
using StudentHubMVC.Liason;
using System.Web;

namespace StudentHubMVC.Controllers
{
    [AllowCrossSiteJson]
    public class WebApiController : Controller
    {
        readonly RepResults _repResults = new RepResults();
        readonly string key = ConfigurationManager.AppSettings["ApiKey"].ToString();
        readonly RepResultSsc _repResultSsc = new RepResultSsc();
        readonly RepResultEamcet _repResultEamcet = new RepResultEamcet();
        readonly RepResultInter _repResultInter = new RepResultInter();
        readonly RepResultsPolycet _repResultPolycet = new RepResultsPolycet();
        readonly RepResultsIcet _repResultIcet = new RepResultsIcet();

        readonly JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet;

        [HttpGet]
        public JsonResult GetExamNotifications(string ApiKey)
        {
            //JsonSerializer ser = new JsonSerializer();
            if (IsKeyInvalid(ApiKey))
            {
                return Json("Invalid Request", JsonRequestBehavior.AllowGet);
            }
            // string imagesPath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["NewsPath"]);
            var db = new Entities();
            var newsAlers = (from rslts in db.NewsAlerts.Where(_ => _.NewsTypeId == 156)
                             orderby rslts.UpdatedDate descending
                             select new NewsAlertsInfo()
                             {
                                 NewsId = rslts.NewsId,
                                 Title = rslts.Title,
                                 Description = rslts.Description,
                                 CoverImage = rslts.CoverImage,
                                 CreatedDate = rslts.UpdatedDate.ToString(),
                                 UrlKeyWord = rslts.UrlKeyword
                             }).ToList();
            foreach (var item in newsAlers)
            {
                item.Description = WebUtility.HtmlDecode(item.Description);
            }
            return Json(newsAlers, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetNewsAlerts(string ApiKey)
        {
            //JsonSerializer ser = new JsonSerializer();
            if (IsKeyInvalid(ApiKey))
            {
                return Json("Invalid Request", JsonRequestBehavior.AllowGet);
            }
           // string imagesPath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["NewsPath"]);
            var db = new Entities();
          var newsAlers = (from rslts in db.NewsAlerts.Where(_ => _.Status == "Active" && _.NewsTypeId != 156)  orderby rslts.NewsId descending
             select new NewsAlertsInfo()
             {
                 NewsId = rslts.NewsId,
                 Title = rslts.Title,
                 Description = rslts.Description,
                 CoverImage =rslts.CoverImage,
                 CreatedDate = rslts.UpdatedDate.ToString(),
                 UrlKeyWord = rslts.UrlKeyword
             }).ToList();


            foreach (var item in newsAlers)
            {
                item.CoverImage = "http://studenthub.in/" + "assets/img/news/" + item.CoverImage;
            }
            return Json(newsAlers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLatestNews(string ApiKey, int NumberOfNews)
        {
            //JsonSerializer ser = new JsonSerializer();
            if (IsKeyInvalid(ApiKey))
            {
                return Json("Invalid Request", JsonRequestBehavior.AllowGet);
            }
           // string imagesPath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["NewsPath"]);
            var db = new Entities();

            List<NewsAlertsInfo> newsAlers  = (from rslts in db.NewsAlerts.Where(_ => _.Status == "Active" && _.NewsTypeId!=156) orderby rslts.NewsId descending
                             select new NewsAlertsInfo()
                             {
                                 NewsId = rslts.NewsId,
                                 Title = rslts.Title,
                                 Description = rslts.Description,
                                 CoverImage = rslts.CoverImage,
                                 CreatedDate = rslts.UpdatedDate.ToString(),
                                 UrlKeyWord = rslts.UrlKeyword,
                                 NewsTypeId = rslts.NewsTypeId
                             }).Take(NumberOfNews).ToList();


            foreach (var item in newsAlers)
            {
                item.CoverImage = "http://studenthub.in/" + "assets/img/news/" + item.CoverImage;
                item.NewsType = db.DropdownTypes.FirstOrDefault((x) => x.GroupName == "NEWS" && x.DropdownTypeId == item.NewsTypeId).TypeValue;
            }
            return Json(newsAlers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetNewsDetails(string ApiKey, string UrlKeyWord)
        {
            //JsonSerializer ser = new JsonSerializer();
            if (IsKeyInvalid(ApiKey))
            {
                return Json("Invalid Request", JsonRequestBehavior.AllowGet);
            }
           // string imagesPath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["NewsPath"]);
            var db = new Entities();
            List<NewsAlertsInfo> newsAlers = (from rslts in db.NewsAlerts.Where(_ => _.UrlKeyword == UrlKeyWord)
                             select new NewsAlertsInfo()
                             {
                                 NewsId = rslts.NewsId,
                                 Title = rslts.Title,
                                 Description = rslts.Description,
                                 CoverImage = rslts.CoverImage,
                                 CreatedDate = rslts.UpdatedDate.ToString(),
                                 UrlKeyWord = rslts.UrlKeyword,
                                 KeyWords = rslts.Keywords
                             }).Take(1).ToList();
            
                newsAlers[0].CoverImage = "http://studenthub.in/" + "assets/img/news/" + newsAlers[0].CoverImage;
            newsAlers[0].Description = WebUtility.HtmlDecode(newsAlers[0].Description);
            return Json(newsAlers[0], JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetCollegesFilters(string ApiKey)
        {
            //JsonSerializer ser = new JsonSerializer();
            if (IsKeyInvalid(ApiKey))
            {
                return Json("Invalid Request", JsonRequestBehavior.AllowGet);
            }
            var db = new Entities();
            db.Configuration.ProxyCreationEnabled = false;
            var collegesinfo = db.Colleges.Select((x) => new { x.coll_ID, x.coll_Name, x.coll_State, x.coll_City, x.coll_Logo, x.coll_CoverPic, x.coll_Type, x.coll_SubType }).Distinct().ToList();
            List<StateCities> statesandcityinfo = new List<StateCities>();              
            var statewisecolleges = collegesinfo.GroupBy(x => x.coll_State);
            foreach (var item in statewisecolleges)
            {
                statesandcityinfo.Add(new StateCities(item.Key, item.Select(x => x.coll_City).Distinct().ToList()));
            }

            CollegesResultsFilters filters = new CollegesResultsFilters();
            filters.StatesAndCities = statesandcityinfo;
            filters.CollegeTypes = new List<string>() { "Government", "Deemed University", "Autonomous", "Private" };
            filters.CollegeSubTypes = new List<string>() { "IIT", "NIT", "IIIT", "BITS", "Other" };
            return Json(filters, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetColleges(string ApiKey)
        {
            //JsonSerializer ser = new JsonSerializer();
            if (IsKeyInvalid(ApiKey))
            {
                return Json("Invalid Request", JsonRequestBehavior.AllowGet);
            }
            var db = new Entities();
            db.Configuration.ProxyCreationEnabled = false;
            var collegesinfo = db.Colleges.Select((x) => new { x.coll_ID, x.coll_Name, x.coll_State, x.coll_City, x.coll_Logo, x.coll_CoverPic, x.coll_TypeOfInstitute, x.coll_SubType }).Distinct().ToList();
            List<BasicCollegeDetails> colleges = new List<BasicCollegeDetails>();
            string imagesPath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["Colleges"]);
            foreach (var college in collegesinfo)
            {
                  BasicCollegeDetails collegeDetails = new BasicCollegeDetails() { ID = college.coll_ID, Name = college.coll_Name, State = college.coll_State, City = college.coll_City, Logo=college.coll_Logo, CoverPic = college.coll_CoverPic, Type =college.coll_TypeOfInstitute, SubType=college.coll_SubType };
                collegeDetails.CoverPic = "http://studenthub.in/" + "assets/img/colleges/" + college.coll_CoverPic;
                collegeDetails.Logo = "http://studenthub.in/" + "assets/img/colleges/" + college.coll_Logo;
                colleges.Add(collegeDetails);
            }
      
            var jsonResult = Json(new CollegeResults() { collegesCount = db.Colleges.Count(), colleges = colleges },JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        
        [HttpGet]
        public JsonResult GetCollegeDetails(int id, string ApiKey)
        {
            if (IsKeyInvalid(ApiKey))
            {
                return Json("Invalid Request", JsonRequestBehavior.AllowGet);
            }
            var db = new Entities();
            db.Configuration.ProxyCreationEnabled = false;
            var college = (from dbcollege in db.Colleges.Where((x) => x.coll_ID == id)
                           select new CollegeDetails()
                           {
                               ID = dbcollege.coll_ID,
                               Name = dbcollege.coll_Name,
                               UrlKeyword = dbcollege.coll_UrlKeyword,
                               Type = dbcollege.coll_Type,
                               Description = dbcollege.coll_Description,
                               Logo = dbcollege.coll_Logo,
                               CoverPic = dbcollege.coll_CoverPic,
                               Address = dbcollege.coll_Address,
                               Knownas = dbcollege.coll_Knownas,
                               AcademicDepartments = dbcollege.coll_AcademicDepartments,
                               AcademicCentres = dbcollege.coll_AcademicCentres,
                               FacultyNo = dbcollege.coll_FacultyNo,
                               StudentsIntake = dbcollege.coll_StudentsIntake,
                               StudentsDiversity = dbcollege.coll_StudentsDiversity,
                               FacultytoStudentRatio = dbcollege.coll_FacultytoStudentRatio,
                               AnnualExpenditure = dbcollege.coll_AnnualExpenditure,
                               ResearchDetails = dbcollege.coll_ResearchDetails,
                               TypeOfInstitute = dbcollege.coll_TypeOfInstitute,
                               SubType = dbcollege.coll_SubType,
                               Degree_4Years = dbcollege.coll_4YearsDegree,
                               Degree_5Years = dbcollege.coll_5YearsDegree,
                               Degree_3Years = dbcollege.coll_3YearsDegree,
                               ModeOfAdmission = dbcollege.coll_ModeOfAdmission,
                               FeesStructure = dbcollege.coll_FeesStructure,
                               HostelFee = dbcollege.coll_HostelFee,
                               FeeWaivers = dbcollege.coll_FeeWaivers,
                               Scholarships = dbcollege.coll_Scholarships,
                               CampusFacilities = dbcollege.coll_CampusFacilities,
                               Rankings = dbcollege.coll_Rankings,
                               Connectivity = dbcollege.coll_Connectivity,
                               City = dbcollege.coll_City,
                               State = dbcollege.coll_State,
                               Pincode = dbcollege.coll_Pincode,
                               Country = dbcollege.coll_Country,
                               Location = dbcollege.coll_Location,
                               Approval = dbcollege.coll_Approval,
                               Website = dbcollege.coll_Website,
                               Phone1 = dbcollege.coll_Phone1,
                               Phone2 = dbcollege.coll_Phone2,
                               Email1 = dbcollege.coll_Email1,
                               Email2 = dbcollege.coll_Email2,
                               Facebook = dbcollege.coll_Facebook,
                               Twitter = dbcollege.coll_Twitter,
                               HighestPackage = dbcollege.coll_HighestPackage,
                               AveragePackage = dbcollege.coll_AveragePackage,
                               LowestPackage = dbcollege.coll_LowestPackage,
                               InternationalPackage = dbcollege.coll_InternationalPackage,
                               Recruiters = dbcollege.coll_Recruiters,
                               CampusSize = dbcollege.coll_CampusSize,
                               EstablishedYear = dbcollege.coll_EstablishedYear
                           }).FirstOrDefault();
            //string imagesPath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["Colleges"]);
            //string logo_path = Path.Combine(imagesPath, "IIT-MANDI-LOGO.jpg");
            ////@"D:\Studenthub\StudentHubMVC\StudentHubMVC\assets\img\colleges\IIT-MANDI-LOGO.jpg";
            //if (!string.IsNullOrWhiteSpace(college.Logo) && System.IO.File.Exists(Path.Combine(imagesPath, college.Logo)))
            //{
            //    logo_path = Path.Combine(imagesPath, college.Logo);
            //}
            //byte[] bytes = System.IO.File.ReadAllBytes(logo_path);
            //college.Logo = Convert.ToBase64String(bytes);

            //string coverPic_path = Path.Combine(imagesPath, "IIT-Mandi.jpg");

            //if (!string.IsNullOrWhiteSpace(college.CoverPic) && System.IO.File.Exists(Path.Combine(imagesPath, college.CoverPic)))
            //{
            //    coverPic_path = Path.Combine(imagesPath, college.CoverPic);
            //}
            //bytes = System.IO.File.ReadAllBytes(coverPic_path);
            //college.CoverPic = Convert.ToBase64String(bytes);
            college.CoverPic = "http://studenthub.in/" + "assets/img/colleges/" + college.CoverPic;
            college.Logo = "http://studenthub.in/" + "assets/img/colleges/" + college.Logo;

            return Json(college, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var webApi = new VMWebApi();

            var db = new Entities();
            var dbWebApiUrls = db.rslt_WebApiDetails.ToList();
            var dbExamTypes = db.rslt_CourseTypes.ToList();
            var webApiUrls = new List<Models.ViewModels.ExamWebApi>();

            if(dbWebApiUrls.Any())
            {
                foreach(var url in dbWebApiUrls)
                {
                    if(dbExamTypes.Any(_ => _.corseTyp_Sno == url.webapi_ExamType))
                    {
                        var examType = dbExamTypes.FirstOrDefault(_ => _.corseTyp_Sno == url.webapi_ExamType);
                        webApiUrls.Add(new Models.ViewModels.ExamWebApi()
                        {
                            Title = url.webapi_Title,
                            Description = url.webapi_Description,
                            Url = url.webapi_Url,
                            ExamTypeName = examType.corseTyp_Name,
                            EducationType = examType.corseTyp_ExamType,
                            State = examType.corseTyp_State
                        });
                    }
                }
            }

            webApi.WebApiList = webApiUrls;

            webApi.ResultsSearchFilters = new RepResultSearch().GetResultSearchFilters();

            return View(webApi);
        }

        public ActionResult AvailableExamResults()
        {
            var examResults = new List<Models.ViewModels.ExamResultsAvailable>();

            var db = new Entities();
            examResults = (from rslts in db.rslt_UploadedResults.Where(_ => _.upld_Status == Constants_FileUploadStatus.Done)
                           join exam in db.rslt_CourseTypes on rslts.upld_ExamType equals exam.corseTyp_Sno
                           join api in db.rslt_WebApiDetails on exam.corseTyp_Sno equals api.webapi_ExamType
                           select new Models.ViewModels.ExamResultsAvailable()
                           {
                               ExamName = exam.corseTyp_Name,
                               ExamYear = rslts.upld_ExamYear,
                               EducationType = exam.corseTyp_ExamType,
                               State = exam.corseTyp_State,
                               ApiReferenceUrl = api.webapi_Url,
                               Description = api.webapi_Description
                           }).ToList();

            return View(examResults);
        }

        [HttpPost]
        public JsonResult AvailableExamResults(int? x)
        {
            var examResults = new List<Models.ViewModels.ExamResultsAvailable>();

            var db = new Entities();
            examResults = (from rslts in db.rslt_UploadedResults
                           join exam in db.rslt_CourseTypes on rslts.upld_ExamType equals exam.corseTyp_Sno
                           join api in db.rslt_WebApiDetails on exam.corseTyp_Sno equals api.webapi_ExamType
                           select new Models.ViewModels.ExamResultsAvailable()
                           {
                               ExamName = exam.corseTyp_Name,
                               ExamYear = rslts.upld_ExamYear,
                               EducationType = exam.corseTyp_ExamType,
                               State = exam.corseTyp_State,
                               ApiReferenceUrl = api.webapi_Url,
                               Description = api.webapi_Description
                           }).ToList();

            return Json(examResults, JsonRequestBehavior.DenyGet); 
        }

        public ActionResult SampleData()
        {
            return View();
        }


        #region -------------------------------------------------------------------------------------------------- GENERAL

        private bool IsKeyInvalid(string ApiKey)
        {
            return string.IsNullOrEmpty(ApiKey) || key != ApiKey;
        }

        #endregion


        #region -------------------------------------------------------------------------------------------------- RESULTS

        [HttpPost]
        public JsonResult GetResultSearchFilters(string ApiKey)
        {
            if (IsKeyInvalid(ApiKey))
            {
                return Json("Invalid Request", JsonRequestBehavior.AllowGet);
            }
            return Json(new RepResultSearch().GetResultSearchFilters(), jsonRequestBehavior);
        }

        [HttpPost]
        public JsonResult GetResultsSearchResults(ResultsSearchCriteria searchCriteria, string ApiKey)
        {
            if (IsKeyInvalid(ApiKey))
            {
                return Json("Invalid Request", JsonRequestBehavior.AllowGet);
            }
            return Json(new RepResultSearch().GetResultsSearchResults(searchCriteria), jsonRequestBehavior);
        }

        [HttpPost]
        public JsonResult GetResultsMain(ResultsSearchParamsPost PostedResultsSearchParams)
        {
            object objResults = new object();

            if (IsKeyInvalid(PostedResultsSearchParams.ApiKey))
            {
                objResults = "Invalid Request";
            }

            if (string.IsNullOrEmpty(PostedResultsSearchParams.Value))
            {
                objResults = "Invalid Search";
                return Json(objResults, jsonRequestBehavior);
            }

            if (PostedResultsSearchParams.RequestType == Constants_SearchTypes.HallTicket)
            {
                objResults = GetResultsByHallTicket(PostedResultsSearchParams);
            }
            else if (PostedResultsSearchParams.RequestType == Constants_SearchTypes.Name)
            {
                objResults = GetResultsByName(PostedResultsSearchParams);
            }
            
            return Json(objResults, jsonRequestBehavior); 
        }

        private object GetResultsByHallTicket(ResultsSearchParamsPost PostedResultsSearchParams)
        {
            object objResults = new object();
            bool NoResult = false;

            switch(PostedResultsSearchParams.ExamId)
            {
                // TS INTER REG YR 1
                case Constants_ExamIds.TelanganaIntermediateYear1Regular_TSINTR1REG:
                    var tsInterRegYr1Result = _repResultInter.GetTSInterYr1RegularResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!tsInterRegYr1Result.IsResultSet)
                        NoResult = true;
                    else
                        objResults = tsInterRegYr1Result;
                    break;

                // TS INTER VOC YR 1
                case Constants_ExamIds.TelanganaIntermediateYear1Vocational_TSINTR1VOC:
                    var tsInterVocYr1Result = _repResultInter.GetTSInterYr1VocationalResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!tsInterVocYr1Result.IsResultSet)
                        NoResult = true;
                    else
                        objResults = tsInterVocYr1Result;
                    break;

                // TS INTER REG YR 2
                case Constants_ExamIds.TelanganaIntermediateYear2Regular_TSINTR2REG:
                    var tsInterRegYr2Result = _repResultInter.GetTSInterYr2RegularResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!tsInterRegYr2Result.IsResultSet)
                        NoResult = true;
                    else
                        objResults = tsInterRegYr2Result;
                    break;

                // TS INTER VOC YR 2
                case Constants_ExamIds.TelanganaIntermediateYear2Vocational_TSINTR2VOC:
                    var tsInterVocYr2Result = _repResultInter.GetTSInterYr2VocationalResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!tsInterVocYr2Result.IsResultSet)
                        NoResult = true;
                    else
                        objResults = tsInterVocYr2Result;
                    break;

                // AP SSC
                case Constants_ExamIds.AndhraPradeshSSC_APSSC:
                    var apSSCResult = _repResultSsc.GetAPSSCResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!apSSCResult.IsResultSet)
                        NoResult = true;
                    else
                        objResults = apSSCResult;
                    break;

                // TS SSC
                case Constants_ExamIds.TelanganaSSC_TSSSC:
                    var tsSSCResult = _repResultSsc.GetTSSSCResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!tsSSCResult.IsResultSet)
                        NoResult = true;
                    else
                        objResults = tsSSCResult;
                    break;

                // AP INTER REG YR 1
                case Constants_ExamIds.AndhraPradeshIntermediateYear1Regular_APINTR1REG:
                    var apInterRegYr1Result = _repResultInter.GetAPInterYr1RegularResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!apInterRegYr1Result.IsResultSet)
                        NoResult = true;
                    else
                        objResults = apInterRegYr1Result;
                    break;

                // AP INTER VOC YR 1
                case Constants_ExamIds.AndhraPradeshIntermediateYear1Vocational_APINTR1VOC:
                    var apInterVocYr1Result = _repResultInter.GetAPInterYr1VocationalResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!apInterVocYr1Result.IsResultSet)
                        NoResult = true;
                    else
                        objResults = apInterVocYr1Result;
                    break;

                // AP INTER REG YR 2
                case Constants_ExamIds.AndhraPradeshIntermediateYear2Regular_APINTR2REG:
                    var apInterRegYr2Result = _repResultInter.GetAPInterYr2RegularResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!apInterRegYr2Result.IsResultSet)
                        NoResult = true;
                    else
                        objResults = apInterRegYr2Result;
                    break;

                // AP INTER VOC YR 2
                case Constants_ExamIds.AndhraPradeshIntermediateYear2Vocational_APINTR2VOC:
                    var apInterVocYr2Result = _repResultInter.GetAPInterYr2VocationalResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!apInterVocYr2Result.IsResultSet)
                        NoResult = true;
                    else
                        objResults = apInterVocYr2Result;
                    break;

                // AP EAPCET ENG
                case Constants_ExamIds.AndhraPradeshEAPCETEngineering_APEAPENG:
                    var apEapcetEngResult = _repResultEamcet.GetAPEapcetEngineeringResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!apEapcetEngResult.IsResultSet)
                        NoResult = true;
                    else
                        objResults = apEapcetEngResult;
                    break;

                // AP EAPCET AM
                case Constants_ExamIds.AndhraPradeshEAPCETAgricultureMedical_APEAPAM:
                    var apEapcetAMResult = _repResultEamcet.GetAPEapcetAgriMedicalResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!apEapcetAMResult.IsResultSet)
                        NoResult = true;
                    else
                        objResults = apEapcetAMResult;
                    break;

                // TS EAMCET ENG
                case Constants_ExamIds.TelanganaEAMCETEngineering_TSEAMENG:
                    var apEamcetEngResult = _repResultEamcet.GetTSEamcetEngineeringResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!apEamcetEngResult.IsResultSet)
                        NoResult = true;
                    else
                        objResults = apEamcetEngResult;
                    break;

                // TS EAMCET AM
                case Constants_ExamIds.TelanganaEAMCETAgricultureMedical_TSEAMAM:
                    var tsEamcetAMResult = _repResultEamcet.GetTSEamcetAgriMedicalResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!tsEamcetAMResult.IsResultSet)
                        NoResult = true;
                    else
                        objResults = tsEamcetAMResult;
                    break;

                // AP POLYCET
                case Constants_ExamIds.APPOLYCET_APPOLYCET:
                    var apPolyCetResult = _repResultPolycet.GetAPPolycetResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!apPolyCetResult.IsResultSet)
                        NoResult = true;
                    else
                        objResults = apPolyCetResult;
                    break;

                // TS POLYCET
                case Constants_ExamIds.TSPOLYCET_TSPOLYCET:
                    var tsPolyCetResult = _repResultPolycet.GetTSPolycetResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!tsPolyCetResult.IsResultSet)
                        NoResult = true;
                    else
                        objResults = tsPolyCetResult;
                    break;

                // AP ICET
                case Constants_ExamIds.APICET_APICET:
                    var apICetResult = _repResultIcet.GetAPIcetResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!apICetResult.IsResultSet)
                        NoResult = true;
                    else
                        objResults = apICetResult;
                    break;

                // TS ICET
                case Constants_ExamIds.TSICET_TSICET:
                    var tsICetResult = _repResultIcet.GetTSIcetResult(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    if (!tsICetResult.IsResultSet)
                        NoResult = true;
                    else
                        objResults = tsICetResult;
                    break;
            }

            if (NoResult)
                objResults = "No Results Found. Please Enter Correct Hall Ticket Number";

            return objResults;
        }

        private object GetResultsByName(ResultsSearchParamsPost PostedResultsSearchParams)
        {
            object objResults = new object();

            switch (PostedResultsSearchParams.ExamId)
            {
                // TS INTER REG YR 1
                case Constants_ExamIds.TelanganaIntermediateYear1Regular_TSINTR1REG:
                    objResults = _repResultInter.GetTSInterYr1RegularResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // TS INTER VOC YR 1
                case Constants_ExamIds.TelanganaIntermediateYear1Vocational_TSINTR1VOC:
                    objResults = _repResultInter.GetTSInterYr1VocationalResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // TS INTER REG YR 2
                case Constants_ExamIds.TelanganaIntermediateYear2Regular_TSINTR2REG:
                    objResults = _repResultInter.GetTSInterYr2RegularResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // TS INTER VOC YR 2
                case Constants_ExamIds.TelanganaIntermediateYear2Vocational_TSINTR2VOC:
                    objResults = _repResultInter.GetTSInterYr2VocationalResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // AP SSC
                case Constants_ExamIds.AndhraPradeshSSC_APSSC:
                    objResults = _repResultSsc.GetAPSSCResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // TS SSC
                case Constants_ExamIds.TelanganaSSC_TSSSC:
                    objResults = _repResultSsc.GetTSSSCResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year); 
                    break;

                // AP INTER REG YR 1
                case Constants_ExamIds.AndhraPradeshIntermediateYear1Regular_APINTR1REG:
                    objResults = _repResultInter.GetAPInterYr1RegularResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // AP INTER VOC YR 1
                case Constants_ExamIds.AndhraPradeshIntermediateYear1Vocational_APINTR1VOC:
                    objResults = _repResultInter.GetAPInterYr1VocationalResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // AP INTER REG YR 2
                case Constants_ExamIds.AndhraPradeshIntermediateYear2Regular_APINTR2REG:
                    objResults = _repResultInter.GetAPInterYr2RegularResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // AP INTER VOC YR 2
                case Constants_ExamIds.AndhraPradeshIntermediateYear2Vocational_APINTR2VOC:
                    objResults = _repResultInter.GetAPInterYr2VocationalResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // AP EAPCET ENG
                case Constants_ExamIds.AndhraPradeshEAPCETEngineering_APEAPENG:
                    objResults = _repResultEamcet.GetAPEapcetEngineeringResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // AP EAPCET AM
                case Constants_ExamIds.AndhraPradeshEAPCETAgricultureMedical_APEAPAM:
                    objResults = _repResultEamcet.GetAPEapcetAgriMedicalResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // TS EAMCET ENG
                case Constants_ExamIds.TelanganaEAMCETEngineering_TSEAMENG:
                    objResults = _repResultEamcet.GetTSEamcetEngineeringNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // TS EAMCET AM
                case Constants_ExamIds.TelanganaEAMCETAgricultureMedical_TSEAMAM:
                    objResults = _repResultEamcet.GetTSEamcetAgriMedicalResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // AP POLYCET
                case Constants_ExamIds.APPOLYCET_APPOLYCET:
                    objResults = _repResultPolycet.GetAPPolycetResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // TS POLYCET
                case Constants_ExamIds.TSPOLYCET_TSPOLYCET:
                    objResults = _repResultPolycet.GetTSPolycetResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // AP ICET
                case Constants_ExamIds.APICET_APICET:
                    objResults = _repResultIcet.GetAPIcetResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;

                // TS ICET
                case Constants_ExamIds.TSICET_TSICET:
                    objResults = _repResultIcet.GetTSIcetResultNameSearch(PostedResultsSearchParams.Value, PostedResultsSearchParams.Year);
                    break;
            }

            if(!((List<ExamResultsNameSearchResult>)objResults).Any())
                objResults = "No Results Found.";

            return objResults;
        }

        #endregion


        #region -------------------------------------------------------------------------------------------------- TELANGANA RESULTS

        [HttpPost]
        public JsonResult GetTSInterYr1RegularResult(string HallTicketNumber, string ExamYear, string ApiKey)
        {
            if (IsKeyInvalid(ApiKey))
                return Json("Invalid Request", JsonRequestBehavior.DenyGet);

            if (string.IsNullOrEmpty(HallTicketNumber))
                return Json("Invalid Hall Ticket Number", JsonRequestBehavior.DenyGet);

            var result = _repResults.GetTSYr2RegResult(HallTicketNumber, (int)Enums_ExamTypes.Telangana_Intermediate_Year_1_Regular, ExamYear);

            if (string.IsNullOrEmpty(result.HallTicketNumber))
                return Json("No Results Found. Please Enter Correct Hall Ticket Number", JsonRequestBehavior.DenyGet);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetTSInterYr1VocationalResult(string HallTicketNumber, string ExamYear, string ApiKey)
        {
            
            if (IsKeyInvalid(ApiKey))
                return Json("Invalid Request", JsonRequestBehavior.DenyGet);

            if (string.IsNullOrEmpty(HallTicketNumber))
                return Json("Invalid Hall Ticket Number", JsonRequestBehavior.DenyGet);

            var result = _repResults.GetTSYr2VocResult(HallTicketNumber, (int)Enums_ExamTypes.Telangana_Intermediate_Year_1_Vocational, ExamYear);

            if (string.IsNullOrEmpty(result.HallTicketNumber))
                return Json("No Results Found. Please Enter Correct Hall Ticket Number", JsonRequestBehavior.DenyGet);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetTSInterYr2RegularResult(string HallTicketNumber, string ExamYear, string ApiKey)
        {
            
            if(IsKeyInvalid(ApiKey))
                return Json("Invalid Request", JsonRequestBehavior.DenyGet);

            if (string.IsNullOrEmpty(HallTicketNumber))
                return Json("Invalid Hall Ticket Number", JsonRequestBehavior.DenyGet);

            var result = _repResults.GetTSYr2RegResult(HallTicketNumber, (int)Enums_ExamTypes.Telangana_Intermediate_Year_2_Regular, ExamYear);

            if (string.IsNullOrEmpty(result.HallTicketNumber))
                return Json("No Results Found. Please Enter Correct Hall Ticket Number", JsonRequestBehavior.DenyGet);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetTSInterYr2VocationalResult(string HallTicketNumber, string ExamYear, string ApiKey)
        {
            
            if (IsKeyInvalid(ApiKey))
                return Json("Invalid Request", JsonRequestBehavior.DenyGet);

            if (string.IsNullOrEmpty(HallTicketNumber))
                return Json("Invalid Hall Ticket Number", JsonRequestBehavior.DenyGet);

            var result = _repResults.GetTSYr2VocResult(HallTicketNumber, (int)Enums_ExamTypes.Telangana_Intermediate_Year_2_Vocational, ExamYear);

            if (string.IsNullOrEmpty(result.HallTicketNumber))
                return Json("No Results Found. Please Enter Correct Hall Ticket Number", JsonRequestBehavior.DenyGet);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetTSSSCResult(string HallTicketNumber, string ExamYear, string ApiKey)
        {
            
            if (IsKeyInvalid(ApiKey))
                return Json("Invalid Request", JsonRequestBehavior.DenyGet);

            if (string.IsNullOrEmpty(HallTicketNumber))
                return Json("Invalid Hall Ticket Number", JsonRequestBehavior.DenyGet);

            var result = "COMING SOON";// _repResults.GetTSYr2VocResult(HallTicketNumber, (int)Enums_ExamTypes.Telangana_Intermediate_Year_2_Vocational, ExamYear);

            if (string.IsNullOrEmpty(result))
                return Json("No Results Found. Please Enter Correct Hall Ticket Number", JsonRequestBehavior.DenyGet);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetTSEamcetEngineeringResult(string HallTicketNumber, string ExamYear, string ApiKey)
        {
            
            if (IsKeyInvalid(ApiKey))
                return Json("Invalid Request", JsonRequestBehavior.DenyGet);

            if (string.IsNullOrEmpty(HallTicketNumber))
                return Json("Invalid Hall Ticket Number", JsonRequestBehavior.DenyGet);

            var result = _repResults.GetTSEamcetEngResult(HallTicketNumber, ExamYear);

            if (string.IsNullOrEmpty(result.HallTicketNumber))
                return Json("No Results Found. Please Enter Correct Hall Ticket Number", JsonRequestBehavior.DenyGet);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetTSEamcetAgriMedicalResult(string HallTicketNumber, string ExamYear, string ApiKey)
        {
            
            if (IsKeyInvalid(ApiKey))
                return Json("Invalid Request", JsonRequestBehavior.DenyGet);

            if (string.IsNullOrEmpty(HallTicketNumber))
                return Json("Invalid Hall Ticket Number", JsonRequestBehavior.DenyGet);

            var result = _repResults.GetTSEamcetAMResult(HallTicketNumber, ExamYear);

            if (string.IsNullOrEmpty(result.HallTicketNumber))
                return Json("No Results Found. Please Enter Correct Hall Ticket Number", JsonRequestBehavior.DenyGet);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        #endregion


        #region -------------------------------------------------------------------------------------------------- ANDHRA PRADESH RESULTS

        [HttpPost]
        public JsonResult GetAPInterYr1RegularResult(string HallTicketNumber, string ExamYear, string ApiKey)
        {
            
            if (IsKeyInvalid(ApiKey))
                return Json("Invalid Request", JsonRequestBehavior.DenyGet);

            if (string.IsNullOrEmpty(HallTicketNumber))
                return Json("Invalid Hall Ticket Number", JsonRequestBehavior.DenyGet);

            var result = _repResults.GetAPRegResult(HallTicketNumber, (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_1_Regular, ExamYear);

            if (string.IsNullOrEmpty(result.HallTicketNumber))
                return Json("No Results Found. Please Enter Correct Hall Ticket Number", JsonRequestBehavior.DenyGet);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetAPInterYr1VocationalResult(string HallTicketNumber, string ExamYear, string ApiKey)
        {
            
            if (IsKeyInvalid(ApiKey))
                return Json("Invalid Request", JsonRequestBehavior.DenyGet);

            if (string.IsNullOrEmpty(HallTicketNumber))
                return Json("Invalid Hall Ticket Number", JsonRequestBehavior.DenyGet);

            var result = _repResults.GetAPVocResult(HallTicketNumber, (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_1_Vocational, ExamYear);

            if (string.IsNullOrEmpty(result.HallTicketNumber))
                return Json("No Results Found. Please Enter Correct Hall Ticket Number", JsonRequestBehavior.DenyGet);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetAPInterYr2RegularResult(string HallTicketNumber, string ExamYear, string ApiKey)
        {
            
            if (IsKeyInvalid(ApiKey))
                return Json("Invalid Request", JsonRequestBehavior.DenyGet);

            if (string.IsNullOrEmpty(HallTicketNumber))
                return Json("Invalid Hall Ticket Number", JsonRequestBehavior.DenyGet);

            var result = _repResults.GetAPRegResult(HallTicketNumber, (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_2_Regular, ExamYear);

            if (string.IsNullOrEmpty(result.HallTicketNumber))
                return Json("No Results Found. Please Enter Correct Hall Ticket Number", JsonRequestBehavior.DenyGet);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetAPInterYr2VocationalResult(string HallTicketNumber, string ExamYear, string ApiKey)
        {
            
            if (IsKeyInvalid(ApiKey))
                return Json("Invalid Request", JsonRequestBehavior.DenyGet);

            if (string.IsNullOrEmpty(HallTicketNumber))
                return Json("Invalid Hall Ticket Number", JsonRequestBehavior.DenyGet);

            var result = _repResults.GetAPVocResult(HallTicketNumber, (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_2_Vocational, ExamYear);

            if (string.IsNullOrEmpty(result.HallTicketNumber))
                return Json("No Results Found. Please Enter Correct Hall Ticket Number", JsonRequestBehavior.DenyGet);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetAPSSCResult(string HallTicketNumber, string ExamYear, string ApiKey)
        {
            
            if (IsKeyInvalid(ApiKey))
                return Json("Invalid Request", JsonRequestBehavior.DenyGet);

            if (string.IsNullOrEmpty(HallTicketNumber))
                return Json("Invalid Hall Ticket Number", JsonRequestBehavior.DenyGet);

            var result = _repResults.GetAPSSCResult(HallTicketNumber, ExamYear);

            if (string.IsNullOrEmpty(result.RollNo))
                return Json("No Results Found. Please Enter Correct Hall Ticket Number", JsonRequestBehavior.DenyGet);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetAPEapcetEngineeringResult(string HallTicketNumber, string ExamYear, string ApiKey)
        {
            
            if (IsKeyInvalid(ApiKey))
                return Json("Invalid Request", JsonRequestBehavior.DenyGet);

            if (string.IsNullOrEmpty(HallTicketNumber))
                return Json("Invalid Hall Ticket Number", JsonRequestBehavior.DenyGet);

            var result = _repResults.GetAPEapcetEngResult(HallTicketNumber, ExamYear);

            if (string.IsNullOrEmpty(result.HallTicketNumber))
                return Json("No Results Found. Please Enter Correct Hall Ticket Number", JsonRequestBehavior.DenyGet);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetAPEapcetAgriMedicalResult(string HallTicketNumber, string ExamYear, string ApiKey)
        {
            
            if (IsKeyInvalid(ApiKey))
                return Json("Invalid Request", JsonRequestBehavior.DenyGet);

            if (string.IsNullOrEmpty(HallTicketNumber))
                return Json("Invalid Hall Ticket Number", JsonRequestBehavior.DenyGet);

            var result = _repResults.GetAPEapcetAMResult(HallTicketNumber, ExamYear);

            if (string.IsNullOrEmpty(result.HallTicketNumber))
                return Json("No Results Found. Please Enter Correct Hall Ticket Number", JsonRequestBehavior.DenyGet);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        #endregion

    }
}
