using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentHubMVC;
using StudentHubMVC.Models.ViewModels;
using System.IO;
using System.IO.Compression;
using MySql.Data.MySqlClient;
using System.Data;
using AutoMapper;
using System.Configuration;
using System.Data.OleDb;

namespace StudentHubMVC.Repositories
{
    public class RepResults
    {
        readonly Liason.Help obj_Help = new Liason.Help();
        readonly Entities db = new Entities();
        readonly Liason.DBConnect _dbConnect = new Liason.DBConnect();

        readonly string ConfigTableSheetName = ConfigurationManager.AppSettings["TableSheetName"];
        readonly string ConfigResultsDoNotDeletePath = ConfigurationManager.AppSettings["ResultsDoNotDeletePath"];


        #region ------------------------------------------------------------------------------------------------------------ COMMON

        public List<rslt_CourseTypes> GetResultsExamTypes()
        {
            return db.rslt_CourseTypes.OrderBy(_ => _.corseTyp_Name).ToList();
        }

        public rslt_CourseTypes GetResultsExamTypeById(string Id)
        {
            return db.rslt_CourseTypes.FirstOrDefault(_ => _.corseTyp_ID == Id);
        }

        public List<rslt_CourseTypes> GetResultsExamTypesByState(string State)
        {
            return db.rslt_CourseTypes.Where(_ => _.corseTyp_State == State).OrderBy(_ => _.corseTyp_Name).ToList();
        }

        public List<rslt_CourseTypes> GetResultsExamTypesByEducation(string EducationType)
        {
            return db.rslt_CourseTypes.Where(_ => _.corseTyp_ExamType == EducationType).OrderBy(_ => _.corseTyp_Name).ToList();
        }

        public List<rslt_CourseTypes> GetResultsExamTypesByStateEducation(string State, string EducationType)
        {
            return db.rslt_CourseTypes.Where(_ => _.corseTyp_State == State && _.corseTyp_ExamType == EducationType).OrderBy(_ => _.corseTyp_Name).ToList();
        }


        // FILES UPDLOAD LOG
        public rslt_UploadedResults AddFileUploadLog(int ExamType, string ExamYear, string FileName, string ExamMonth, string ResultsReleaseDate)
        {
            var dbUploadResult = db.rslt_UploadedResults.Where(_ => _.upld_ExamType == ExamType && _.upld_ExamYear == ExamYear && _.upld_Status == Constants_FileUploadStatus.Done).ToList();
            if(dbUploadResult != null && dbUploadResult.Any())
            {
                foreach(var result in dbUploadResult)
                {
                    result.upld_Status = Constants_FileUploadStatus.Old;
                    UpdateFileUploadStatus(result);
                }
            }

            var uploadResult = new rslt_UploadedResults{ 
                upld_ExamType = ExamType,
                upld_ExamYear = ExamYear,
                upld_FileName = FileName,
                upld_ExamMonth = ExamMonth,
                upld_ResultReleaseDate = ResultsReleaseDate,
                upld_Status = Constants_FileUploadStatus.New,
                upld_CreatedDate = DateTime.Now
            };

            db.rslt_UploadedResults.Add(uploadResult);
            db.SaveChanges();

            return uploadResult;
        }

        public void UpdateFileUploadStatus(rslt_UploadedResults uploadResult)
        {
            uploadResult.upld_UpdatedDate = DateTime.Now;
            db.rslt_UploadedResults.Add(uploadResult);
            db.Entry(uploadResult).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public List<UploadedResults> GetUploadedResultsList()
        {
            var uploadedResults = new List<UploadedResults>();
            var dbUploadedResults = db.rslt_UploadedResults.Where(_ => _.upld_Status == Constants_FileUploadStatus.Done).ToList();
            var examTypes = db.rslt_CourseTypes.ToList();

            foreach(var dbResult in dbUploadedResults)
            {
                var examType = examTypes.FirstOrDefault(_ => _.corseTyp_Sno == dbResult.upld_ExamType);

                if(examType != null)
                {
                    uploadedResults.Add(new UploadedResults()
                    {
                        ExamYear = dbResult.upld_ExamYear,
                        ExamType = examType.corseTyp_Name,
                        EducationType = examType.corseTyp_ExamType,
                        State = examType.corseTyp_State
                    });
                }
            }

            return uploadedResults;
        }

        public rslt_UploadedResults GetLatestExamTypeLog(int ExamType, string ExamYear)
        {
            return db.rslt_UploadedResults.Where(_ => _.upld_ExamType == ExamType
                    && _.upld_ExamYear == ExamYear
                    && _.upld_Status == Constants_FileUploadStatus.Done)
                .OrderByDescending(_ => _.upld_Sno).FirstOrDefault();
        }

        #endregion


        #region ------------------------------------------------------------------------------------------------------------ DISTRICTS

        public List<rslt_DistrictsMaster> GetResultsDistricts()
        {
            return db.rslt_DistrictsMaster.OrderBy(_ => _.dstrct_ExamType).ThenBy(_ => _.dstrct_Name).ToList();
        }

        public string UpdateResultDistrict(VMResultsDistrictsMaster vMResultsDistrictsMaster)
        {
            string response = "District Saved";

            try
            {
                var district = new rslt_DistrictsMaster();

                if (vMResultsDistrictsMaster.District.dstrct_Sno > 0)
                {
                    district = db.rslt_DistrictsMaster.FirstOrDefault(_ => _.dstrct_Sno == vMResultsDistrictsMaster.District.dstrct_Sno);

                    if (district != null)
                    {
                        district.dstrct_ID = vMResultsDistrictsMaster.District.dstrct_ID;
                        district.dstrct_ExamType = vMResultsDistrictsMaster.District.dstrct_ExamType;
                        district.dstrct_Name = vMResultsDistrictsMaster.District.dstrct_Name;
                        district.dstrct_UpdatedDate = DateTime.Now;

                        response = "District Details Updated";
                    }
                }
                else
                {
                    vMResultsDistrictsMaster.District.dstrct_CreatedDate = DateTime.Now;
                    db.rslt_DistrictsMaster.Add(vMResultsDistrictsMaster.District);
                    response = "New District Details Added";
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                obj_Help.ExcpetionsHandling(ex);
                response = "Error occured while updating districts";
            }

            return response;
        }

        public string UpdateDistrictsFromUpload(int ExamTypeID, HttpPostedFileBase file)
        {
            StreamReader reader = new StreamReader(file.InputStream);
            string districtsData = reader.ReadToEnd();
            string response = "";

            var arrDistricts = districtsData.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (arrDistricts != null && arrDistricts.Length > 0)
            {
                using (var context = db.Database.BeginTransaction())
                {
                    try
                    {
                        var examType = db.rslt_CourseTypes.FirstOrDefault(_ => _.corseTyp_Sno == ExamTypeID);
                        var dbDistricts = db.rslt_DistrictsMaster.Where(_ => _.dstrct_ExamType == ExamTypeID).ToList();
                        int newDistricts = 0;
                        int updatedDistricts = 0;

                        foreach (string district in arrDistricts)
                        {
                            string code = district.Substring(0, 2);
                            string name = district.Substring(2, district.Length - 2);
                            name = name.Trim();

                            if (dbDistricts.Any(_ => _.dstrct_ID == code))
                            {
                                dbDistricts.Find(_ => _.dstrct_ID == code).dstrct_Name = name;
                                updatedDistricts++;
                            }
                            else
                            {
                                db.rslt_DistrictsMaster.Add(new rslt_DistrictsMaster()
                                {
                                    dstrct_ID = code,
                                    dstrct_Name = name,
                                    dstrct_ExamType = ExamTypeID,
                                    dstrct_CreatedDate = DateTime.Now
                                });
                                newDistricts++;
                            }
                        }

                        db.SaveChanges();
                        context.Commit();

                        response = string.Format("Added {0} District(s) & Updated {1} District(s) \n for {2}", newDistricts, updatedDistricts, examType.corseTyp_Name);
                    }
                    catch (Exception ex)
                    {
                        obj_Help.ExcpetionsHandling(ex);
                        context.Rollback();
                        response = "Error occured while updating districts";
                        //return RedirectToAction("DistrictsMaster", new { message = "Error occured while uploading districts" });
                    }
                }
            }

            return response;
        }

        #endregion

        #region ------------------------------------------------------------------------------------------------------------ COURSES

        public List<rst_CoursesMaster> GetResultsCourses()
        {
            return db.rst_CoursesMaster.OrderBy(_ => _.corse_CourseType).ThenBy(_ => _.corse_Name).ToList();
        }

        public string UpdateResultsCourse(VMResultsCoursesMaster vMResultsCoursesMaster)
        {
            string response = "Course Saved";

            try
            {
                var course = new rst_CoursesMaster();

                if (vMResultsCoursesMaster.ResultCourse.corse_Sno > 0)
                {
                    course = db.rst_CoursesMaster.FirstOrDefault(_ => _.corse_Sno == vMResultsCoursesMaster.ResultCourse.corse_Sno);

                    if (course != null)
                    {
                        course.corse_ID = vMResultsCoursesMaster.ResultCourse.corse_ID;
                        course.corse_CourseType = vMResultsCoursesMaster.ResultCourse.corse_CourseType;
                        course.corse_Name = vMResultsCoursesMaster.ResultCourse.corse_Name;
                        course.corse_ModifiedDate = DateTime.Now;

                        response = "Course Details Updated";
                    }
                }
                else
                {
                    vMResultsCoursesMaster.ResultCourse.corse_CreatedDate = DateTime.Now;
                    db.rst_CoursesMaster.Add(vMResultsCoursesMaster.ResultCourse);
                    response = "New Course Details Added";
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                obj_Help.ExcpetionsHandling(ex);
                response = "Error occured while updating course details";
            }

            return response;
        }

        public string UpdateResultsCoursesFromUpload(int ExamTypeID, HttpPostedFileBase file)
        {
            StreamReader reader = new StreamReader(file.InputStream);
            string coursesData = reader.ReadToEnd();
            string response = "";

            var arrCourses = coursesData.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (arrCourses != null && arrCourses.Length > 0)
            {
                using (var context = db.Database.BeginTransaction())
                {
                    try
                    {
                        var examType = db.rslt_CourseTypes.FirstOrDefault(_ => _.corseTyp_Sno == ExamTypeID);
                        var dbCourses = db.rst_CoursesMaster.Where(_ => _.corse_CourseType == ExamTypeID).ToList();
                        int newCourses = 0;
                        int updatedCourses = 0;

                        foreach (string course in arrCourses)
                        {
                            if (!string.IsNullOrWhiteSpace(course))
                            {
                                string code = course.Substring(0, 3);
                                string name = course.Substring(3, course.Length - 3);
                                code = code.Trim();
                                name = name.Trim();

                                if (dbCourses.Any(_ => _.corse_ID == code))
                                {
                                    dbCourses.Find(_ => _.corse_ID == code).corse_Name = name;
                                    updatedCourses++;
                                }
                                else
                                {
                                    db.rst_CoursesMaster.Add(new rst_CoursesMaster()
                                    {
                                        corse_ID = code,
                                        corse_Name = name,
                                        corse_CourseType = ExamTypeID,
                                        corse_CreatedDate = DateTime.Now
                                    });
                                    newCourses++;
                                }
                            }
                        }

                        db.SaveChanges();
                        context.Commit();

                        response = string.Format("Added {0} Course(s) & Updated {1} Course(s) \n for {2}", newCourses, updatedCourses, examType.corseTyp_Name);
                    }
                    catch (Exception ex)
                    {
                        obj_Help.ExcpetionsHandling(ex);
                        context.Rollback();
                        response = "Error occured while updating Courses";
                    }
                }
            }

            return response;
        }

        #endregion

        #region ------------------------------------------------------------------------------------------------------------ SUBJECTS

        public List<rslt_SubjectsMaster> GetResultsSubjects()
        {
            return db.rslt_SubjectsMaster.OrderBy(_ => _.sbjct_CourseID).ThenBy(_ => _.sbjct_Name).ToList();
        }

        public string UpdateResultsSubject(VMResultsSubjectsMaster vMResultsSubjectsMaster)
        {
            string response = "Subject Saved";

            try
            {
                var subject = new rslt_SubjectsMaster();

                if (vMResultsSubjectsMaster.ResultSubject.sbjct_Sno > 0)
                {
                    subject = db.rslt_SubjectsMaster.FirstOrDefault(_ => _.sbjct_Sno == vMResultsSubjectsMaster.ResultSubject.sbjct_Sno);

                    if (subject != null)
                    {
                        subject.sbjct_ID = vMResultsSubjectsMaster.ResultSubject.sbjct_ID;
                        subject.sbjct_CourseID = vMResultsSubjectsMaster.ResultSubject.sbjct_CourseID;
                        subject.sbjct_Name = vMResultsSubjectsMaster.ResultSubject.sbjct_Name;
                        subject.sbjct_UpdatedDate = DateTime.Now;

                        response = "Course Details Updated";
                    }
                }
                else
                {
                    vMResultsSubjectsMaster.ResultSubject.sbjct_CreatedDate = DateTime.Now;
                    db.rslt_SubjectsMaster.Add(vMResultsSubjectsMaster.ResultSubject);
                    response = "New Subject Details Added";
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                obj_Help.ExcpetionsHandling(ex);
                response = "Error occured while updating subjcet details";
            }

            return response;
        }

        public string UpdateResultsSubjectsFromUpload(int ExamTypeID, HttpPostedFileBase file)
        {
            StreamReader reader = new StreamReader(file.InputStream);
            string subjectsData = reader.ReadToEnd();
            string response = "";

            var arrSubjects = subjectsData.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (arrSubjects != null && arrSubjects.Length > 0)
            {
                using (var context = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Configuration.AutoDetectChangesEnabled = false;
                        db.Configuration.ValidateOnSaveEnabled = false;

                        var examType = db.rslt_CourseTypes.FirstOrDefault(_ => _.corseTyp_Sno == ExamTypeID);
                        var dbSubjects = db.rslt_SubjectsMaster.Where(_ => _.sbjct_CourseID == ExamTypeID).ToList();
                        int newSubects = 0;
                        int updatedSubjects = 0;
                        int countFlag = 0;

                        foreach (string subject in arrSubjects)
                        {
                            if (!string.IsNullOrWhiteSpace(subject))
                            {
                                string code = "";
                                string name = "";

                                if (ExamTypeID == (int)Enums_ExamTypes.Telangana_Intermediate_Year_2_Regular)
                                {
                                    code = subject.Substring(0, 2);
                                    name = subject.Substring(2, subject.Length - 2);
                                }
                                else if (ExamTypeID == (int)Enums_ExamTypes.Telangana_Intermediate_Year_1_Regular ||
                                    ExamTypeID == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_1_Regular ||
                                    ExamTypeID == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_2_Regular)
                                {
                                    code = subject.Substring(0, 3);
                                    name = subject.Substring(3, subject.Length - 3);
                                }
                                else if (ExamTypeID == (int)Enums_ExamTypes.Telangana_Intermediate_Year_2_Vocational ||
                                    ExamTypeID == (int)Enums_ExamTypes.Telangana_Intermediate_Year_1_Vocational ||
                                    ExamTypeID == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_1_Vocational ||
                                    ExamTypeID == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_2_Vocational)
                                {
                                    code = subject.Substring(0, 5);
                                    name = subject.Substring(5, subject.Length - 5);
                                }

                                code = code.Trim();
                                name = name.Trim();

                                if (dbSubjects.Any(_ => _.sbjct_ID == code))
                                {
                                    dbSubjects.Find(_ => _.sbjct_ID == code).sbjct_Name = name;
                                    updatedSubjects++;
                                }
                                else
                                {
                                    db.rslt_SubjectsMaster.Add(new rslt_SubjectsMaster()
                                    {
                                        sbjct_ID = code,
                                        sbjct_Name = name,
                                        sbjct_CourseID = ExamTypeID,
                                        sbjct_CreatedDate = DateTime.Now
                                    });
                                    newSubects++;
                                }

                                countFlag++;

                                if (countFlag > 1000)
                                {
                                    db.SaveChanges();
                                    countFlag = 0;
                                }
                            }
                        }

                        db.SaveChanges();
                        context.Commit();

                        response = string.Format("Added {0} Subject(s) & Updated {1} Subject(s) \n for {2}", newSubects, updatedSubjects, examType.corseTyp_Name);
                    }
                    catch (Exception ex)
                    {
                        obj_Help.ExcpetionsHandling(ex);
                        context.Rollback();
                        response = "Error occured while updating Subjects";
                    }
                }
            }

            return response;
        }

        #endregion

        #region ------------------------------------------------------------------------------------------------------------ TELANGANA RESULTS

        public string UpdateInterResultsFromUpload(int ExamTypeID, string ExamYear, string UploadFilePath, string ExamMonth, string ResultsReleaseDate)
        {
            StreamReader reader = new StreamReader(File.OpenRead(UploadFilePath));
            string subjectsData = reader.ReadToEnd();
            string response = "";

            var arrResults = subjectsData.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (arrResults != null && arrResults.Length > 0)
            {
                var uploadResult = AddFileUploadLog(ExamTypeID, ExamYear, Path.GetFileName(UploadFilePath), ExamMonth, ResultsReleaseDate);
                
                if(ExamTypeID == (int)Enums_ExamTypes.Telangana_Intermediate_Year_2_Regular ||
                    ExamTypeID == (int)Enums_ExamTypes.Telangana_Intermediate_Year_1_Regular)
                {
                    response = UpdateInterGeneralResults(ExamTypeID, ExamYear, arrResults);
                }
                else if (ExamTypeID == (int)Enums_ExamTypes.Telangana_Intermediate_Year_2_Vocational ||
                    ExamTypeID == (int)Enums_ExamTypes.Telangana_Intermediate_Year_1_Vocational)
                {
                    response = UpdateInterVocationalResults(ExamTypeID, ExamYear, arrResults);
                }
                else if (ExamTypeID == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_1_Regular ||
                    ExamTypeID == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_2_Regular)
                {
                    response = UpdateInterGeneralResults(ExamTypeID, ExamYear, arrResults);
                }
                else if (ExamTypeID == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_1_Vocational ||
                    ExamTypeID == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_2_Vocational)
                {
                    response = UpdateInterVocationalResults(ExamTypeID, ExamYear, arrResults);
                }

                if(response.ToLower().Contains("error"))
                {
                    uploadResult.upld_Status = Constants_FileUploadStatus.Incomplete;
                    UpdateFileUploadStatus(uploadResult);
                }
                else
                {
                    uploadResult.upld_Status = Constants_FileUploadStatus.Done;
                    UpdateFileUploadStatus(uploadResult);
                }
            }

            return response;
        }

        private string RunUpdateResultsSP(string ExamType, string ExamYear, string Response)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["bntstudenthub_db"].ToString()))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "bnt_studenthub.UpdateResults";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ExamType", ExamType);
                        cmd.Parameters["@ExamType"].Direction = ParameterDirection.Input;

                        cmd.Parameters.AddWithValue("@ExamYear", ExamYear);
                        cmd.Parameters["@ExamYear"].Direction = ParameterDirection.Input;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                obj_Help.ExcpetionsHandling(ex);
                Response = "Error occured while updating Results from temp to main";
            }

            return Response;
        }

        private string UpdateInterGeneralResults(int ExamTypeID, string ExamYear, string[] arrResults)
        {
            string response = "";
            var examType = db.rslt_CourseTypes.FirstOrDefault(_ => _.corseTyp_Sno == ExamTypeID);
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE rslt_IntermediateYear2Regular_Temp;");
            int i = 0;

            try
            {
                for (i = 0; i <= (arrResults.Length / 1000) + 1; i++)
                {
                    Entities _db = new Entities();
                    _db.Configuration.AutoDetectChangesEnabled = false;
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    List<rslt_IntermediateYear2Regular_Temp> temp = new List<rslt_IntermediateYear2Regular_Temp>();

                    foreach (string resultRecord in arrResults.Skip(i * 1000).Take(1000))
                    {
                        if (!string.IsNullOrWhiteSpace(resultRecord) && resultRecord.Length > 50)
                        {
                            int recordLength = resultRecord.Length;

                            temp.Add(new rslt_IntermediateYear2Regular_Temp()
                            {
                                inter2reg_ExamType = ExamTypeID,
                                inter2reg_ExamYear = ExamYear,
                                inter2reg_ROLLNO = resultRecord.Substring(0, 10),
                                //inter2reg_CNAME = resultRecord.Substring(12, 30),
                                inter2reg_FullRecord = resultRecord,
                                inter2reg_CreatedDate = DateTime.Now
                            });
                        }
                    }

                    _db.rslt_IntermediateYear2Regular_Temp.AddRange(temp);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                obj_Help.ExcpetionsHandling(ex);
                response = "Error occured while updating Results for " + ExamTypeID + " after " + (i * 1000) + " records";
            }

            if(!response.ToLower().Contains("error"))
            {
                response = string.Format("Processed {0} Results Record(s) ({1}) for Year {2}", examType.corseTyp_Name, arrResults.Length, ExamYear);
                response = RunUpdateResultsSP(ExamTypeID.ToString(), ExamYear, response);
            }

            return response;
        }

        private string UpdateInterVocationalResults(int ExamTypeID, string ExamYear, string[] arrResults)
        {
            string response = "";
            var examType = db.rslt_CourseTypes.FirstOrDefault(_ => _.corseTyp_Sno == ExamTypeID);
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE rslt_IntermediateYear2Voc_Temp;");
            int i = 0;

            try
            {
                for (i = 0; i <= (arrResults.Length / 1000) + 1; i++)
                {
                    Entities _db = new Entities();
                    _db.Configuration.AutoDetectChangesEnabled = false;
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    List<rslt_IntermediateYear2Voc_Temp> temp = new List<rslt_IntermediateYear2Voc_Temp>();

                    foreach (string resultRecord in arrResults.Skip(i * 1000).Take(1000))
                    {
                        if (!string.IsNullOrWhiteSpace(resultRecord) && resultRecord.Length > 50)
                        {
                            int recordLength = resultRecord.Length;

                            temp.Add(new rslt_IntermediateYear2Voc_Temp()
                            {
                                inter2voc_ExamType = ExamTypeID,
                                inter2voc_ExamYear = ExamYear,
                                inter2voc_FullRecord = resultRecord,
                                inter2voc_ROLLNO = resultRecord.Substring(0, 10),
                                //inter2voc_CNAME = resultRecord.Substring(12, 30),
                                inter2voc_CreatedDate = DateTime.Now
                            });
                        }
                    }

                    _db.rslt_IntermediateYear2Voc_Temp.AddRange(temp);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                obj_Help.ExcpetionsHandling(ex);
                response = "Error occured while updating Results for " + ExamTypeID + " after " + (i * 1000) + " records";
            }

            if(!response.ToLower().Contains("error"))
            {
                response = string.Format("Processed {0} Results Record(s) ({1}) for Year {2}", examType.corseTyp_Name, arrResults.Length, ExamYear);
                response = RunUpdateResultsSP(ExamTypeID.ToString(), ExamYear, response);
            }

            return response;
        }

        private rslt_IntermediateYear2Regular UnwrapTsRegDBResult(string HallTicketNumber, int ExamType, string ExamYear)
        {
            var dbResult = db.rslt_IntermediateYear2Regular.FirstOrDefault(_ => _.inter2reg_ROLLNO == HallTicketNumber && _.inter2reg_ExamType == ExamType && _.inter2reg_ExamYear == ExamYear);

            if (dbResult != null)
            {
                if (ExamType == (int)Enums_ExamTypes.Telangana_Intermediate_Year_1_Regular)
                {
                    dbResult.inter2reg_ROLLNO = dbResult.inter2reg_FullRecord.Substring(0, 10);
                    dbResult.inter2reg_DIST = dbResult.inter2reg_FullRecord.Substring(10, 2);
                    dbResult.inter2reg_CNAME = dbResult.inter2reg_FullRecord.Substring(12, 30);
                    dbResult.inter2reg_YR1PC1 = dbResult.inter2reg_FullRecord.Substring(42, 2);
                    dbResult.inter2reg_YR1MKS1 = dbResult.inter2reg_FullRecord.Substring(44, 3);
                    dbResult.inter2reg_YR1IND1 = dbResult.inter2reg_FullRecord.Substring(47, 1);
                    dbResult.inter2reg_YR1RS1 = dbResult.inter2reg_FullRecord.Substring(48, 1);
                    dbResult.inter2reg_YR1PC2 = dbResult.inter2reg_FullRecord.Substring(49, 2);
                    dbResult.inter2reg_YR1MKS2 = dbResult.inter2reg_FullRecord.Substring(51, 3);
                    dbResult.inter2reg_YR1IND2 = dbResult.inter2reg_FullRecord.Substring(54, 1);
                    dbResult.inter2reg_YR1RS2 = dbResult.inter2reg_FullRecord.Substring(55, 1);
                    dbResult.inter2reg_YR1PC3 = dbResult.inter2reg_FullRecord.Substring(56, 2);
                    dbResult.inter2reg_YR1MKS3 = dbResult.inter2reg_FullRecord.Substring(58, 3);
                    dbResult.inter2reg_YR1IND3 = dbResult.inter2reg_FullRecord.Substring(61, 1);
                    dbResult.inter2reg_YR1RS3 = dbResult.inter2reg_FullRecord.Substring(62, 1);
                    dbResult.inter2reg_YR1PC4 = dbResult.inter2reg_FullRecord.Substring(63, 2);
                    dbResult.inter2reg_YR1MKS4 = dbResult.inter2reg_FullRecord.Substring(65, 3);
                    dbResult.inter2reg_YR1IND4 = dbResult.inter2reg_FullRecord.Substring(68, 1);
                    dbResult.inter2reg_YR1RS4 = dbResult.inter2reg_FullRecord.Substring(69, 1);
                    dbResult.inter2reg_YR1PC5 = dbResult.inter2reg_FullRecord.Substring(70, 2);
                    dbResult.inter2reg_YR1MKS5 = dbResult.inter2reg_FullRecord.Substring(72, 3);
                    dbResult.inter2reg_YR1IND5 = dbResult.inter2reg_FullRecord.Substring(75, 1);
                    dbResult.inter2reg_YR1RS5 = dbResult.inter2reg_FullRecord.Substring(76, 1);
                    dbResult.inter2reg_YR1PC6 = dbResult.inter2reg_FullRecord.Substring(77, 2);
                    dbResult.inter2reg_YR1MKS6 = dbResult.inter2reg_FullRecord.Substring(79, 3);
                    dbResult.inter2reg_YR1IND6 = dbResult.inter2reg_FullRecord.Substring(82, 1);
                    dbResult.inter2reg_YR1RS6 = dbResult.inter2reg_FullRecord.Substring(83, 1);

                    dbResult.inter2reg_YR2PC1 = "";
                    dbResult.inter2reg_YR2MKS1 = "";
                    dbResult.inter2reg_YR2IND1 = "";
                    dbResult.inter2reg_YR2RS1 = "";
                    dbResult.inter2reg_YR2PC2 = "";
                    dbResult.inter2reg_YR2MKS2 = "";
                    dbResult.inter2reg_YR2IND2 = "";
                    dbResult.inter2reg_YR2RS2 = "";
                    dbResult.inter2reg_YR2PC3 = "";
                    dbResult.inter2reg_YR2MKS3 = "";
                    dbResult.inter2reg_YR2IND3 = "";
                    dbResult.inter2reg_YR2RS3 = "";
                    dbResult.inter2reg_YR2PC4 = "";
                    dbResult.inter2reg_YR2MKS4 = "";
                    dbResult.inter2reg_YR2IND4 = "";
                    dbResult.inter2reg_YR2RS4 = "";
                    dbResult.inter2reg_YR2PC5 = "";
                    dbResult.inter2reg_YR2MKS5 = "";
                    dbResult.inter2reg_YR2IND5 = "";
                    dbResult.inter2reg_YR2RS5 = "";
                    dbResult.inter2reg_YR2PC6 = "";
                    dbResult.inter2reg_YR2MKS6 = "";
                    dbResult.inter2reg_YR2IND6 = "";
                    dbResult.inter2reg_YR2RS6 = "";
                    dbResult.inter2reg_YR2PC7 = "";
                    dbResult.inter2reg_YR2MKS7 = "";
                    dbResult.inter2reg_YR2IND7 = "";
                    dbResult.inter2reg_YR2RS7 = "";
                    dbResult.inter2reg_YR2PC8 = "";
                    dbResult.inter2reg_YR2MKS8 = "";
                    dbResult.inter2reg_YR2IND8 = "";
                    dbResult.inter2reg_YR2RS8 = "";
                    dbResult.inter2reg_YR2PC9 = "";
                    dbResult.inter2reg_YR2MKS9 = "";
                    dbResult.inter2reg_YR2IND9 = "";
                    dbResult.inter2reg_YR2RS9 = "";
                    dbResult.inter2reg_YR2PC10 = "";
                    dbResult.inter2reg_YR2MKS10 = "";
                    dbResult.inter2reg_YR2IND10 = "";
                    dbResult.inter2reg_YR2RS10 = "";

                    dbResult.inter2reg_TOTAL = dbResult.inter2reg_FullRecord.Substring(84, 3);
                    dbResult.inter2reg_RESULT = dbResult.inter2reg_FullRecord.Substring(87, 6);
                    dbResult.inter2reg_ADD_FLG = dbResult.inter2reg_FullRecord.Substring(93, 1);
                    dbResult.inter2reg_LINKNO = dbResult.inter2reg_FullRecord.Substring(94, 9);
                }
                else if (ExamType == (int)Enums_ExamTypes.Telangana_Intermediate_Year_2_Regular)
                {
                    dbResult.inter2reg_ROLLNO = dbResult.inter2reg_FullRecord.Substring(0, 10);
                    dbResult.inter2reg_DIST = dbResult.inter2reg_FullRecord.Substring(10, 2);
                    dbResult.inter2reg_CNAME = dbResult.inter2reg_FullRecord.Substring(12, 30);
                    dbResult.inter2reg_YR1PC1 = dbResult.inter2reg_FullRecord.Substring(42, 2);
                    dbResult.inter2reg_YR1MKS1 = dbResult.inter2reg_FullRecord.Substring(44, 3);
                    dbResult.inter2reg_YR1IND1 = dbResult.inter2reg_FullRecord.Substring(47, 1);
                    dbResult.inter2reg_YR1RS1 = dbResult.inter2reg_FullRecord.Substring(48, 1);
                    dbResult.inter2reg_YR1PC2 = dbResult.inter2reg_FullRecord.Substring(49, 2);
                    dbResult.inter2reg_YR1MKS2 = dbResult.inter2reg_FullRecord.Substring(51, 3);
                    dbResult.inter2reg_YR1IND2 = dbResult.inter2reg_FullRecord.Substring(54, 1);
                    dbResult.inter2reg_YR1RS2 = dbResult.inter2reg_FullRecord.Substring(55, 1);
                    dbResult.inter2reg_YR1PC3 = dbResult.inter2reg_FullRecord.Substring(56, 2);
                    dbResult.inter2reg_YR1MKS3 = dbResult.inter2reg_FullRecord.Substring(58, 3);
                    dbResult.inter2reg_YR1IND3 = dbResult.inter2reg_FullRecord.Substring(61, 1);
                    dbResult.inter2reg_YR1RS3 = dbResult.inter2reg_FullRecord.Substring(62, 1);
                    dbResult.inter2reg_YR1PC4 = dbResult.inter2reg_FullRecord.Substring(63, 2);
                    dbResult.inter2reg_YR1MKS4 = dbResult.inter2reg_FullRecord.Substring(65, 3);
                    dbResult.inter2reg_YR1IND4 = dbResult.inter2reg_FullRecord.Substring(68, 1);
                    dbResult.inter2reg_YR1RS4 = dbResult.inter2reg_FullRecord.Substring(69, 1);
                    dbResult.inter2reg_YR1PC5 = dbResult.inter2reg_FullRecord.Substring(70, 2);
                    dbResult.inter2reg_YR1MKS5 = dbResult.inter2reg_FullRecord.Substring(72, 3);
                    dbResult.inter2reg_YR1IND5 = dbResult.inter2reg_FullRecord.Substring(75, 1);
                    dbResult.inter2reg_YR1RS5 = dbResult.inter2reg_FullRecord.Substring(76, 1);
                    dbResult.inter2reg_YR1PC6 = dbResult.inter2reg_FullRecord.Substring(77, 2);
                    dbResult.inter2reg_YR1MKS6 = dbResult.inter2reg_FullRecord.Substring(79, 3);
                    dbResult.inter2reg_YR1IND6 = dbResult.inter2reg_FullRecord.Substring(82, 1);
                    dbResult.inter2reg_YR1RS6 = dbResult.inter2reg_FullRecord.Substring(83, 1);
                    dbResult.inter2reg_YR2PC1 = dbResult.inter2reg_FullRecord.Substring(84, 2);
                    dbResult.inter2reg_YR2MKS1 = dbResult.inter2reg_FullRecord.Substring(86, 3);
                    dbResult.inter2reg_YR2IND1 = dbResult.inter2reg_FullRecord.Substring(89, 1);
                    dbResult.inter2reg_YR2RS1 = dbResult.inter2reg_FullRecord.Substring(90, 1);
                    dbResult.inter2reg_YR2PC2 = dbResult.inter2reg_FullRecord.Substring(91, 2);
                    dbResult.inter2reg_YR2MKS2 = dbResult.inter2reg_FullRecord.Substring(93, 3);
                    dbResult.inter2reg_YR2IND2 = dbResult.inter2reg_FullRecord.Substring(96, 1);
                    dbResult.inter2reg_YR2RS2 = dbResult.inter2reg_FullRecord.Substring(97, 1);
                    dbResult.inter2reg_YR2PC3 = dbResult.inter2reg_FullRecord.Substring(98, 2);
                    dbResult.inter2reg_YR2MKS3 = dbResult.inter2reg_FullRecord.Substring(100, 3);
                    dbResult.inter2reg_YR2IND3 = dbResult.inter2reg_FullRecord.Substring(103, 1);
                    dbResult.inter2reg_YR2RS3 = dbResult.inter2reg_FullRecord.Substring(104, 1);
                    dbResult.inter2reg_YR2PC4 = dbResult.inter2reg_FullRecord.Substring(105, 2);
                    dbResult.inter2reg_YR2MKS4 = dbResult.inter2reg_FullRecord.Substring(107, 3);
                    dbResult.inter2reg_YR2IND4 = dbResult.inter2reg_FullRecord.Substring(110, 1);
                    dbResult.inter2reg_YR2RS4 = dbResult.inter2reg_FullRecord.Substring(111, 1);
                    dbResult.inter2reg_YR2PC5 = dbResult.inter2reg_FullRecord.Substring(112, 2);
                    dbResult.inter2reg_YR2MKS5 = dbResult.inter2reg_FullRecord.Substring(114, 3);
                    dbResult.inter2reg_YR2IND5 = dbResult.inter2reg_FullRecord.Substring(117, 1);
                    dbResult.inter2reg_YR2RS5 = dbResult.inter2reg_FullRecord.Substring(118, 1);
                    dbResult.inter2reg_YR2PC6 = dbResult.inter2reg_FullRecord.Substring(119, 2);
                    dbResult.inter2reg_YR2MKS6 = dbResult.inter2reg_FullRecord.Substring(121, 3);
                    dbResult.inter2reg_YR2IND6 = dbResult.inter2reg_FullRecord.Substring(124, 1);
                    dbResult.inter2reg_YR2RS6 = dbResult.inter2reg_FullRecord.Substring(125, 1);
                    dbResult.inter2reg_YR2PC7 = dbResult.inter2reg_FullRecord.Substring(126, 2);
                    dbResult.inter2reg_YR2MKS7 = dbResult.inter2reg_FullRecord.Substring(128, 3);
                    dbResult.inter2reg_YR2IND7 = dbResult.inter2reg_FullRecord.Substring(131, 1);
                    dbResult.inter2reg_YR2RS7 = dbResult.inter2reg_FullRecord.Substring(132, 1);
                    dbResult.inter2reg_YR2PC8 = dbResult.inter2reg_FullRecord.Substring(133, 2);
                    dbResult.inter2reg_YR2MKS8 = dbResult.inter2reg_FullRecord.Substring(135, 3);
                    dbResult.inter2reg_YR2IND8 = dbResult.inter2reg_FullRecord.Substring(138, 1);
                    dbResult.inter2reg_YR2RS8 = dbResult.inter2reg_FullRecord.Substring(139, 1);
                    dbResult.inter2reg_YR2PC9 = dbResult.inter2reg_FullRecord.Substring(140, 2);
                    dbResult.inter2reg_YR2MKS9 = dbResult.inter2reg_FullRecord.Substring(142, 3);
                    dbResult.inter2reg_YR2IND9 = dbResult.inter2reg_FullRecord.Substring(145, 1);
                    dbResult.inter2reg_YR2RS9 = dbResult.inter2reg_FullRecord.Substring(146, 1);
                    dbResult.inter2reg_YR2PC10 = dbResult.inter2reg_FullRecord.Substring(147, 2);
                    dbResult.inter2reg_YR2MKS10 = dbResult.inter2reg_FullRecord.Substring(149, 3);
                    dbResult.inter2reg_YR2IND10 = dbResult.inter2reg_FullRecord.Substring(152, 1);
                    dbResult.inter2reg_YR2RS10 = dbResult.inter2reg_FullRecord.Substring(153, 1);
                    dbResult.inter2reg_TOTAL = dbResult.inter2reg_FullRecord.Substring(154, 3);
                    dbResult.inter2reg_RESULT = dbResult.inter2reg_FullRecord.Substring(157, 6);
                    dbResult.inter2reg_ADD_FLG = dbResult.inter2reg_FullRecord.Substring(163, 1);
                    dbResult.inter2reg_LINKNO = dbResult.inter2reg_FullRecord.Substring(164, 9);
                }
            }

            return dbResult;
        }

        public TSYr2RegResult GetTSYr2RegResult(string HallTicketNumber, int ExamType, string ExamYear)
        {
            TSYr2RegResult result = new TSYr2RegResult();

            if(!string.IsNullOrEmpty(HallTicketNumber))
            {
                var dbResult = UnwrapTsRegDBResult(HallTicketNumber, ExamType, ExamYear);

                if(dbResult != null)
                {
                    var subjects = db.rslt_SubjectsMaster.Where(_ => _.sbjct_CourseID == ExamType).ToList();
                    var dbExamType = db.rslt_CourseTypes.FirstOrDefault(_ => _.corseTyp_Sno == ExamType);

                    result.Exam = dbExamType == null ? "" : dbExamType.corseTyp_Name;
                    result.FullName = dbResult.inter2reg_CNAME.Trim();
                    result.HallTicketNumber = dbResult.inter2reg_ROLLNO.Trim();

                    result.Yr1Subject1 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC1) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC1).sbjct_Name.Trim() : "";
                    result.Yr1Subject2 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC2) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC2).sbjct_Name.Trim() : "";
                    result.Yr1Subject3 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC3) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC3).sbjct_Name.Trim() : "";
                    result.Yr1Subject4 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC4) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC4).sbjct_Name.Trim() : "";
                    result.Yr1Subject5 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC5) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC5).sbjct_Name.Trim() : "";
                    result.Yr1Subject6 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC6) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC6).sbjct_Name.Trim() : "";
                    result.Yr2Subject1 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC1) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC1).sbjct_Name.Trim() : "";
                    result.Yr2Subject2 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC2) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC2).sbjct_Name.Trim() : "";
                    result.Yr2Subject3 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC3) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC3).sbjct_Name.Trim() : "";
                    result.Yr2Subject4 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC4) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC4).sbjct_Name.Trim() : "";
                    result.Yr2Subject5 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC5) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC5).sbjct_Name.Trim() : "";
                    result.Yr2Subject6 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC6) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC6).sbjct_Name.Trim() : "";
                    result.Yr2Subject7 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC7) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC7).sbjct_Name.Trim() : "";
                    result.Yr2Subject8 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC8) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC8).sbjct_Name.Trim() : "";
                    result.Yr2Subject9 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC9) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC9).sbjct_Name.Trim() : "";
                    result.Yr2Subject10 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC10) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC10).sbjct_Name.Trim() : "";

                    result.Yr1Subject1_Marks = dbResult.inter2reg_YR1MKS1.Trim();
                    result.Yr1Subject1_Indicator = dbResult.inter2reg_YR1IND1.Trim();
                    result.Yr1Subject1_Result = dbResult.inter2reg_YR1RS1.Trim();

                    result.Yr1Subject2_Marks = dbResult.inter2reg_YR1MKS2.Trim();
                    result.Yr1Subject2_Indicator = dbResult.inter2reg_YR1IND2.Trim();
                    result.Yr1Subject2_Result = dbResult.inter2reg_YR1RS2.Trim();

                    result.Yr1Subject3_Marks = dbResult.inter2reg_YR1MKS3.Trim();
                    result.Yr1Subject3_Indicator = dbResult.inter2reg_YR1IND3.Trim();
                    result.Yr1Subject3_Result = dbResult.inter2reg_YR1RS3.Trim();

                    result.Yr1Subject4_Marks = dbResult.inter2reg_YR1MKS4.Trim();
                    result.Yr1Subject4_Indicator = dbResult.inter2reg_YR1IND4.Trim();
                    result.Yr1Subject4_Result = dbResult.inter2reg_YR1RS4.Trim();

                    result.Yr1Subject5_Marks = dbResult.inter2reg_YR1MKS5.Trim();
                    result.Yr1Subject5_Indicator = dbResult.inter2reg_YR1IND5.Trim();
                    result.Yr1Subject5_Result = dbResult.inter2reg_YR1RS5.Trim();

                    result.Yr1Subject6_Marks = dbResult.inter2reg_YR1MKS6.Trim();
                    result.Yr1Subject6_Indicator = dbResult.inter2reg_YR1IND6.Trim();
                    result.Yr1Subject6_Result = dbResult.inter2reg_YR1RS6.Trim();

                    result.Yr2Subject1_Marks = dbResult.inter2reg_YR2MKS1.Trim();
                    result.Yr2Subject1_Indicator = dbResult.inter2reg_YR2IND1.Trim();
                    result.Yr2Subject1_Result = dbResult.inter2reg_YR2RS1.Trim();

                    result.Yr2Subject2_Marks = dbResult.inter2reg_YR2MKS2.Trim();
                    result.Yr2Subject2_Indicator = dbResult.inter2reg_YR2IND2.Trim();
                    result.Yr2Subject2_Result = dbResult.inter2reg_YR2RS2.Trim();

                    result.Yr2Subject3_Marks = dbResult.inter2reg_YR2MKS3.Trim();
                    result.Yr2Subject3_Indicator = dbResult.inter2reg_YR2IND3.Trim();
                    result.Yr2Subject3_Result = dbResult.inter2reg_YR2RS3.Trim();

                    result.Yr2Subject4_Marks = dbResult.inter2reg_YR2MKS4.Trim();
                    result.Yr2Subject4_Indicator = dbResult.inter2reg_YR2IND4.Trim();
                    result.Yr2Subject4_Result = dbResult.inter2reg_YR2RS4.Trim();

                    result.Yr2Subject5_Marks = dbResult.inter2reg_YR2MKS5.Trim();
                    result.Yr2Subject5_Indicator = dbResult.inter2reg_YR2IND5.Trim();
                    result.Yr2Subject5_Result = dbResult.inter2reg_YR2RS5.Trim();

                    result.Yr2Subject6_Marks = dbResult.inter2reg_YR2MKS6.Trim();
                    result.Yr2Subject6_Indicator = dbResult.inter2reg_YR2IND6.Trim();
                    result.Yr2Subject6_Result = dbResult.inter2reg_YR2RS6.Trim();

                    result.Yr2Subject7_Marks = dbResult.inter2reg_YR2MKS7.Trim();
                    result.Yr2Subject7_Indicator = dbResult.inter2reg_YR2IND7.Trim();
                    result.Yr2Subject7_Result = dbResult.inter2reg_YR2RS7.Trim();

                    result.Yr2Subject8_Marks = dbResult.inter2reg_YR2MKS8.Trim();
                    result.Yr2Subject8_Indicator = dbResult.inter2reg_YR2IND8.Trim();
                    result.Yr2Subject8_Result = dbResult.inter2reg_YR2RS8.Trim();

                    result.Yr2Subject9_Marks = dbResult.inter2reg_YR2MKS9.Trim();
                    result.Yr2Subject9_Indicator = dbResult.inter2reg_YR2IND9.Trim();
                    result.Yr2Subject9_Result = dbResult.inter2reg_YR2RS9.Trim();

                    result.Yr2Subject10_Marks = dbResult.inter2reg_YR2MKS10.Trim();
                    result.Yr2Subject10_Indicator = dbResult.inter2reg_YR2IND10.Trim();
                    result.Yr2Subject10_Result = dbResult.inter2reg_YR2RS10.Trim();

                    result.Total = dbResult.inter2reg_TOTAL.Trim();
                    result.Result = dbResult.inter2reg_RESULT.Trim();
                    result.AdditionalFlag = dbResult.inter2reg_ADD_FLG.Trim();

                    result.IsResultSet = true;
                }
            }

            return result;
        }

        private rslt_IntermediateYear2Voc UnwrapTsVocDBResult(string HallTicketNumber, int ExamType, string ExamYear)
        {
            var dbResult = db.rslt_IntermediateYear2Voc.FirstOrDefault(_ => _.inter2voc_ROLLNO == HallTicketNumber && _.inter2voc_ExamType == ExamType && _.inter2voc_ExamYear == ExamYear);

            if(dbResult != null)
            {
                if (ExamType == (int)Enums_ExamTypes.Telangana_Intermediate_Year_1_Vocational)
                {
                    dbResult.inter2voc_DIST = dbResult.inter2voc_FullRecord.Substring(10, 2);
                    dbResult.inter2voc_CNAME = dbResult.inter2voc_FullRecord.Substring(12, 30);
                    dbResult.inter2voc_COURSE = dbResult.inter2voc_FullRecord.Substring(42, 3);
                    dbResult.inter2voc_YR1FPC1 = dbResult.inter2voc_FullRecord.Substring(45, 2);
                    dbResult.inter2voc_YR1FMKS1 = dbResult.inter2voc_FullRecord.Substring(47, 3);
                    dbResult.inter2voc_YR1FIND1 = dbResult.inter2voc_FullRecord.Substring(50, 1);
                    dbResult.inter2voc_YR1FRES1 = dbResult.inter2voc_FullRecord.Substring(51, 1);
                    dbResult.inter2voc_YR1FPC2 = dbResult.inter2voc_FullRecord.Substring(52, 2);
                    dbResult.inter2voc_YR1FMKS2 = dbResult.inter2voc_FullRecord.Substring(54, 3);
                    dbResult.inter2voc_YR1FIND2 = dbResult.inter2voc_FullRecord.Substring(57, 1);
                    dbResult.inter2voc_YR1FRES2 = dbResult.inter2voc_FullRecord.Substring(58, 1);
                    dbResult.inter2voc_YR1TPC1 = dbResult.inter2voc_FullRecord.Substring(59, 2);
                    dbResult.inter2voc_YR1TMKS1 = dbResult.inter2voc_FullRecord.Substring(61, 3);
                    dbResult.inter2voc_YR1TIND1 = dbResult.inter2voc_FullRecord.Substring(64, 1);
                    dbResult.inter2voc_YR1TRES1 = dbResult.inter2voc_FullRecord.Substring(65, 1);
                    dbResult.inter2voc_YR1TPC2 = dbResult.inter2voc_FullRecord.Substring(66, 2);
                    dbResult.inter2voc_YR1TMKS2 = dbResult.inter2voc_FullRecord.Substring(68, 3);
                    dbResult.inter2voc_YR1TIND2 = dbResult.inter2voc_FullRecord.Substring(71, 1);
                    dbResult.inter2voc_YR1TRES2 = dbResult.inter2voc_FullRecord.Substring(72, 1);
                    dbResult.inter2voc_YR1TPC3 = dbResult.inter2voc_FullRecord.Substring(73, 2);
                    dbResult.inter2voc_YR1TMKS3 = dbResult.inter2voc_FullRecord.Substring(75, 3);
                    dbResult.inter2voc_YR1TIND3 = dbResult.inter2voc_FullRecord.Substring(78, 1);
                    dbResult.inter2voc_YR1TRES3 = dbResult.inter2voc_FullRecord.Substring(79, 1);
                    dbResult.inter2voc_YR1PPC1 = dbResult.inter2voc_FullRecord.Substring(80, 2);
                    dbResult.inter2voc_YR1PMKS1 = dbResult.inter2voc_FullRecord.Substring(82, 3);
                    dbResult.inter2voc_YR1PIND1 = dbResult.inter2voc_FullRecord.Substring(85, 1);
                    dbResult.inter2voc_YR1PRES1 = dbResult.inter2voc_FullRecord.Substring(86, 1);
                    dbResult.inter2voc_YR1PPC2 = dbResult.inter2voc_FullRecord.Substring(87, 2);
                    dbResult.inter2voc_YR1PMKS2 = dbResult.inter2voc_FullRecord.Substring(89, 3);
                    dbResult.inter2voc_YR1PIND2 = dbResult.inter2voc_FullRecord.Substring(92, 1);
                    dbResult.inter2voc_YR1PRES2 = dbResult.inter2voc_FullRecord.Substring(93, 1);
                    dbResult.inter2voc_YR1PPC3 = dbResult.inter2voc_FullRecord.Substring(94, 2);
                    dbResult.inter2voc_YR1PMKS3 = dbResult.inter2voc_FullRecord.Substring(96, 3);
                    dbResult.inter2voc_YR1PIND3 = dbResult.inter2voc_FullRecord.Substring(99, 1);
                    dbResult.inter2voc_YR1PRES3 = dbResult.inter2voc_FullRecord.Substring(100, 1);
                    dbResult.inter2voc_YR1PPC4 = dbResult.inter2voc_FullRecord.Substring(101, 2);
                    dbResult.inter2voc_YR1PMKS4 = dbResult.inter2voc_FullRecord.Substring(103, 3);
                    dbResult.inter2voc_YR1PIND4 = dbResult.inter2voc_FullRecord.Substring(106, 1);
                    dbResult.inter2voc_YR1PRES4 = dbResult.inter2voc_FullRecord.Substring(107, 1);

                    dbResult.inter2voc_YR2FPC1 = "";
                    dbResult.inter2voc_YR2FMKS1 = "";
                    dbResult.inter2voc_YR2FIND1 = "";
                    dbResult.inter2voc_YR2FRES1 = "";
                    dbResult.inter2voc_YR2FPC2 = "";
                    dbResult.inter2voc_YR2FMKS2 = "";
                    dbResult.inter2voc_YR2FIND2 = "";
                    dbResult.inter2voc_YR2FRES2 = "";
                    dbResult.inter2voc_YR2TPC1 = "";
                    dbResult.inter2voc_YR2TMKS1 = "";
                    dbResult.inter2voc_YR2TIND1 = "";
                    dbResult.inter2voc_YR2TRES1 = "";
                    dbResult.inter2voc_YR2TPC2 = "";
                    dbResult.inter2voc_YR2TMKS2 = "";
                    dbResult.inter2voc_YR2TIND2 = "";
                    dbResult.inter2voc_YR2TRES2 = "";
                    dbResult.inter2voc_YR2TPC3 = "";
                    dbResult.inter2voc_YR2TMKS3 = "";
                    dbResult.inter2voc_YR2TIND3 = "";
                    dbResult.inter2voc_YR2TRES3 = "";
                    dbResult.inter2voc_YR2PPC1 = "";
                    dbResult.inter2voc_YR2PMKS1 = "";
                    dbResult.inter2voc_YR2PIND1 = "";
                    dbResult.inter2voc_YR2PRES1 = "";
                    dbResult.inter2voc_YR2PPC2 = "";
                    dbResult.inter2voc_YR2PMKS2 = "";
                    dbResult.inter2voc_YR2PIND2 = "";
                    dbResult.inter2voc_YR2PRES2 = "";
                    dbResult.inter2voc_YR2PPC3 = "";
                    dbResult.inter2voc_YR2PMKS3 = "";
                    dbResult.inter2voc_YR2PIND3 = "";
                    dbResult.inter2voc_YR2PRES3 = "";
                    dbResult.inter2voc_YR2PPC4 = "";
                    dbResult.inter2voc_YR2PMKS4 = "";
                    dbResult.inter2voc_YR2PIND4 = "";
                    dbResult.inter2voc_YR2PRES4 = "";

                    dbResult.inter2voc_TOTAL = dbResult.inter2voc_FullRecord.Substring(108, 4);
                    dbResult.inter2voc_RESULT = dbResult.inter2voc_FullRecord.Substring(112, 6);
                }
                else if (ExamType == (int)Enums_ExamTypes.Telangana_Intermediate_Year_2_Vocational)
                {
                    dbResult.inter2voc_DIST = dbResult.inter2voc_FullRecord.Substring(10, 2);
                    dbResult.inter2voc_CNAME = dbResult.inter2voc_FullRecord.Substring(12, 30);
                    dbResult.inter2voc_COURSE = dbResult.inter2voc_FullRecord.Substring(42, 3);
                    dbResult.inter2voc_YR1FPC1 = dbResult.inter2voc_FullRecord.Substring(45, 2);
                    dbResult.inter2voc_YR1FMKS1 = dbResult.inter2voc_FullRecord.Substring(47, 3);
                    dbResult.inter2voc_YR1FIND1 = dbResult.inter2voc_FullRecord.Substring(50, 1);
                    dbResult.inter2voc_YR1FRES1 = dbResult.inter2voc_FullRecord.Substring(51, 1);
                    dbResult.inter2voc_YR1FPC2 = dbResult.inter2voc_FullRecord.Substring(52, 2);
                    dbResult.inter2voc_YR1FMKS2 = dbResult.inter2voc_FullRecord.Substring(54, 3);
                    dbResult.inter2voc_YR1FIND2 = dbResult.inter2voc_FullRecord.Substring(57, 1);
                    dbResult.inter2voc_YR1FRES2 = dbResult.inter2voc_FullRecord.Substring(58, 1);
                    dbResult.inter2voc_YR1TPC1 = dbResult.inter2voc_FullRecord.Substring(59, 2);
                    dbResult.inter2voc_YR1TMKS1 = dbResult.inter2voc_FullRecord.Substring(61, 3);
                    dbResult.inter2voc_YR1TIND1 = dbResult.inter2voc_FullRecord.Substring(64, 1);
                    dbResult.inter2voc_YR1TRES1 = dbResult.inter2voc_FullRecord.Substring(65, 1);
                    dbResult.inter2voc_YR1TPC2 = dbResult.inter2voc_FullRecord.Substring(66, 2);
                    dbResult.inter2voc_YR1TMKS2 = dbResult.inter2voc_FullRecord.Substring(68, 3);
                    dbResult.inter2voc_YR1TIND2 = dbResult.inter2voc_FullRecord.Substring(71, 1);
                    dbResult.inter2voc_YR1TRES2 = dbResult.inter2voc_FullRecord.Substring(72, 1);
                    dbResult.inter2voc_YR1TPC3 = dbResult.inter2voc_FullRecord.Substring(73, 2);
                    dbResult.inter2voc_YR1TMKS3 = dbResult.inter2voc_FullRecord.Substring(75, 3);
                    dbResult.inter2voc_YR1TIND3 = dbResult.inter2voc_FullRecord.Substring(78, 1);
                    dbResult.inter2voc_YR1TRES3 = dbResult.inter2voc_FullRecord.Substring(79, 1);
                    dbResult.inter2voc_YR1PPC1 = dbResult.inter2voc_FullRecord.Substring(80, 2);
                    dbResult.inter2voc_YR1PMKS1 = dbResult.inter2voc_FullRecord.Substring(82, 3);
                    dbResult.inter2voc_YR1PIND1 = dbResult.inter2voc_FullRecord.Substring(85, 1);
                    dbResult.inter2voc_YR1PRES1 = dbResult.inter2voc_FullRecord.Substring(86, 1);
                    dbResult.inter2voc_YR1PPC2 = dbResult.inter2voc_FullRecord.Substring(87, 2);
                    dbResult.inter2voc_YR1PMKS2 = dbResult.inter2voc_FullRecord.Substring(89, 3);
                    dbResult.inter2voc_YR1PIND2 = dbResult.inter2voc_FullRecord.Substring(92, 1);
                    dbResult.inter2voc_YR1PRES2 = dbResult.inter2voc_FullRecord.Substring(93, 1);
                    dbResult.inter2voc_YR1PPC3 = dbResult.inter2voc_FullRecord.Substring(94, 2);
                    dbResult.inter2voc_YR1PMKS3 = dbResult.inter2voc_FullRecord.Substring(96, 3);
                    dbResult.inter2voc_YR1PIND3 = dbResult.inter2voc_FullRecord.Substring(99, 1);
                    dbResult.inter2voc_YR1PRES3 = dbResult.inter2voc_FullRecord.Substring(100, 1);
                    dbResult.inter2voc_YR1PPC4 = dbResult.inter2voc_FullRecord.Substring(101, 2);
                    dbResult.inter2voc_YR1PMKS4 = dbResult.inter2voc_FullRecord.Substring(103, 3);
                    dbResult.inter2voc_YR1PIND4 = dbResult.inter2voc_FullRecord.Substring(106, 1);
                    dbResult.inter2voc_YR1PRES4 = dbResult.inter2voc_FullRecord.Substring(107, 1);
                    dbResult.inter2voc_YR2FPC1 = dbResult.inter2voc_FullRecord.Substring(108, 2);
                    dbResult.inter2voc_YR2FMKS1 = dbResult.inter2voc_FullRecord.Substring(110, 3);
                    dbResult.inter2voc_YR2FIND1 = dbResult.inter2voc_FullRecord.Substring(113, 1);
                    dbResult.inter2voc_YR2FRES1 = dbResult.inter2voc_FullRecord.Substring(114, 1);
                    dbResult.inter2voc_YR2FPC2 = dbResult.inter2voc_FullRecord.Substring(115, 2);
                    dbResult.inter2voc_YR2FMKS2 = dbResult.inter2voc_FullRecord.Substring(117, 3);
                    dbResult.inter2voc_YR2FIND2 = dbResult.inter2voc_FullRecord.Substring(120, 1);
                    dbResult.inter2voc_YR2FRES2 = dbResult.inter2voc_FullRecord.Substring(121, 1);
                    dbResult.inter2voc_YR2TPC1 = dbResult.inter2voc_FullRecord.Substring(122, 2);
                    dbResult.inter2voc_YR2TMKS1 = dbResult.inter2voc_FullRecord.Substring(124, 3);
                    dbResult.inter2voc_YR2TIND1 = dbResult.inter2voc_FullRecord.Substring(127, 1);
                    dbResult.inter2voc_YR2TRES1 = dbResult.inter2voc_FullRecord.Substring(128, 1);
                    dbResult.inter2voc_YR2TPC2 = dbResult.inter2voc_FullRecord.Substring(129, 2);
                    dbResult.inter2voc_YR2TMKS2 = dbResult.inter2voc_FullRecord.Substring(131, 3);
                    dbResult.inter2voc_YR2TIND2 = dbResult.inter2voc_FullRecord.Substring(134, 1);
                    dbResult.inter2voc_YR2TRES2 = dbResult.inter2voc_FullRecord.Substring(135, 1);
                    dbResult.inter2voc_YR2TPC3 = dbResult.inter2voc_FullRecord.Substring(136, 2);
                    dbResult.inter2voc_YR2TMKS3 = dbResult.inter2voc_FullRecord.Substring(138, 3);
                    dbResult.inter2voc_YR2TIND3 = dbResult.inter2voc_FullRecord.Substring(141, 1);
                    dbResult.inter2voc_YR2TRES3 = dbResult.inter2voc_FullRecord.Substring(142, 1);
                    dbResult.inter2voc_YR2PPC1 = dbResult.inter2voc_FullRecord.Substring(143, 2);
                    dbResult.inter2voc_YR2PMKS1 = dbResult.inter2voc_FullRecord.Substring(145, 3);
                    dbResult.inter2voc_YR2PIND1 = dbResult.inter2voc_FullRecord.Substring(148, 1);
                    dbResult.inter2voc_YR2PRES1 = dbResult.inter2voc_FullRecord.Substring(149, 1);
                    dbResult.inter2voc_YR2PPC2 = dbResult.inter2voc_FullRecord.Substring(150, 2);
                    dbResult.inter2voc_YR2PMKS2 = dbResult.inter2voc_FullRecord.Substring(152, 3);
                    dbResult.inter2voc_YR2PIND2 = dbResult.inter2voc_FullRecord.Substring(155, 1);
                    dbResult.inter2voc_YR2PRES2 = dbResult.inter2voc_FullRecord.Substring(156, 1);
                    dbResult.inter2voc_YR2PPC3 = dbResult.inter2voc_FullRecord.Substring(157, 2);
                    dbResult.inter2voc_YR2PMKS3 = dbResult.inter2voc_FullRecord.Substring(159, 3);
                    dbResult.inter2voc_YR2PIND3 = dbResult.inter2voc_FullRecord.Substring(162, 1);
                    dbResult.inter2voc_YR2PRES3 = dbResult.inter2voc_FullRecord.Substring(163, 1);
                    dbResult.inter2voc_YR2PPC4 = dbResult.inter2voc_FullRecord.Substring(164, 2);
                    dbResult.inter2voc_YR2PMKS4 = dbResult.inter2voc_FullRecord.Substring(166, 3);
                    dbResult.inter2voc_YR2PIND4 = dbResult.inter2voc_FullRecord.Substring(169, 1);
                    dbResult.inter2voc_YR2PRES4 = dbResult.inter2voc_FullRecord.Substring(170, 1);
                    dbResult.inter2voc_TOTAL = dbResult.inter2voc_FullRecord.Substring(171, 4);
                    dbResult.inter2voc_RESULT = dbResult.inter2voc_FullRecord.Substring(175, 6);
                }
            }

            return dbResult;
        }

        public TSYr2VocResult GetTSYr2VocResult(string HallTicketNumber, int ExamType, string ExamYear)
        {
            TSYr2VocResult result = new TSYr2VocResult();

            if (!string.IsNullOrEmpty(HallTicketNumber))
            {
                var dbResult = UnwrapTsVocDBResult(HallTicketNumber, ExamType, ExamYear);// db.rslt_IntermediateYear2Voc.FirstOrDefault(_ => _.inter2voc_ROLLNO == HallTicketNumber);

                if (dbResult != null)
                {
                    var course = db.rst_CoursesMaster.Any(_ => _.corse_CourseType == ExamType && _.corse_ID == dbResult.inter2voc_COURSE) ? db.rst_CoursesMaster.FirstOrDefault(_ => _.corse_CourseType == ExamType && _.corse_ID == dbResult.inter2voc_COURSE) : new rst_CoursesMaster();
                    var subjects = db.rslt_SubjectsMaster.Where(_ => _.sbjct_CourseID == ExamType).ToList();
                    var dbExamType = db.rslt_CourseTypes.FirstOrDefault(_ => _.corseTyp_Sno == ExamType);
                    
                    List<ResultsSubjectsMasterMini> subjectsMini = new List<ResultsSubjectsMasterMini>();
                    foreach(var subject in subjects)
                    {
                        subjectsMini.Add(new ResultsSubjectsMasterMini()
                        {
                            FullID = subject.sbjct_ID,
                            Name = subject.sbjct_Name,
                            ID = subject.sbjct_ID.Length == 5 ? subject.sbjct_ID.Substring(3, 2) : subject.sbjct_ID
                        });
                    }

                    result.Exam = dbExamType == null ? "" : dbExamType.corseTyp_Name;
                    result.FullName = dbResult.inter2voc_CNAME.Trim();
                    result.HallTicketNumber = dbResult.inter2voc_ROLLNO.Trim();
                    result.Course = course.corse_Name != null ? course.corse_Name.Trim() : "";
                    string CourseCode = course.corse_ID != null ? course.corse_ID.Trim() : (dbResult.inter2voc_COURSE ?? "");

                    result.Year1Subject1 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1FPC1) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1FPC1).Name.Trim() : "";
                    result.Year1Subject2 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1FPC2) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1FPC2).Name.Trim() : "";
                    result.Year1Subject3 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1TPC1) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1TPC1).Name.Trim() : "";
                    result.Year1Subject4 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1TPC2) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1TPC2).Name.Trim() : "";
                    result.Year1Subject5 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1TPC3) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1TPC3).Name.Trim() : "";
                    result.Year1Subject6 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC1) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC1).Name.Trim() : "";
                    result.Year1Subject7 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC2) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC2).Name.Trim() : "";
                    result.Year1Subject8 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC3) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC3).Name.Trim() : "";
                    result.Year1Subject9 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC4) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC4).Name.Trim() : "";

                    result.Year2Subject1 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2FPC1) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2FPC1).Name.Trim() : "";
                    result.Year2Subject2 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2FPC2) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2FPC2).Name.Trim() : "";
                    result.Year2Subject3 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2TPC1) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2TPC1).Name.Trim() : "";
                    result.Year2Subject4 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2TPC2) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2TPC2).Name.Trim() : "";
                    result.Year2Subject5 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2TPC3) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2TPC3).Name.Trim() : "";
                    result.Year2Subject6 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC1) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC1).Name.Trim() : "";
                    result.Year2Subject7 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC2) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC2).Name.Trim() : "";
                    result.Year2Subject8 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC3) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC3).Name.Trim() : "";
                    result.Year2Subject9 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC4) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC4).Name.Trim() : "";

                    result.Year1Subject1_Marks = dbResult.inter2voc_YR1FMKS1.Trim();
                    result.Year1Subject1_Indicator = dbResult.inter2voc_YR1FIND1.Trim();
                    result.Year1Subject1_Result = dbResult.inter2voc_YR1FRES1.Trim();

                    result.Year1Subject2_Marks = dbResult.inter2voc_YR1FMKS2.Trim();
                    result.Year1Subject2_Indicator = dbResult.inter2voc_YR1FIND2.Trim();
                    result.Year1Subject2_Result = dbResult.inter2voc_YR1FRES2.Trim();

                    result.Year1Subject3_Marks = dbResult.inter2voc_YR1TMKS1.Trim();
                    result.Year1Subject3_Indicator = dbResult.inter2voc_YR1TIND1.Trim();
                    result.Year1Subject3_Result = dbResult.inter2voc_YR1TRES1.Trim();

                    result.Year1Subject4_Marks = dbResult.inter2voc_YR1TMKS2.Trim();
                    result.Year1Subject4_Indicator = dbResult.inter2voc_YR1TIND2.Trim();
                    result.Year1Subject4_Result = dbResult.inter2voc_YR1TRES2.Trim();

                    result.Year1Subject5_Marks = dbResult.inter2voc_YR1TMKS3.Trim();
                    result.Year1Subject5_Indicator = dbResult.inter2voc_YR1TIND3.Trim();
                    result.Year1Subject5_Result = dbResult.inter2voc_YR1TRES3.Trim();

                    result.Year1Subject6_Marks = dbResult.inter2voc_YR1PMKS1.Trim();
                    result.Year1Subject6_Indicator = dbResult.inter2voc_YR1PIND1.Trim();
                    result.Year1Subject6_Result = dbResult.inter2voc_YR1PRES1.Trim();

                    result.Year1Subject7_Marks = dbResult.inter2voc_YR1PMKS2.Trim();
                    result.Year1Subject7_Indicator = dbResult.inter2voc_YR1PIND2.Trim();
                    result.Year1Subject7_Result = dbResult.inter2voc_YR1PRES2.Trim();

                    result.Year1Subject8_Marks = dbResult.inter2voc_YR1PMKS3.Trim();
                    result.Year1Subject8_Indicator = dbResult.inter2voc_YR1PIND3.Trim();
                    result.Year1Subject8_Result = dbResult.inter2voc_YR1PRES3.Trim();

                    result.Year1Subject9_Marks = dbResult.inter2voc_YR1PMKS4.Trim();
                    result.Year1Subject9_Indicator = dbResult.inter2voc_YR1PIND4.Trim();
                    result.Year1Subject9_Result = dbResult.inter2voc_YR1PRES4.Trim();

                    result.Year2Subject1_Marks = dbResult.inter2voc_YR2FMKS1.Trim();
                    result.Year2Subject1_Indicator = dbResult.inter2voc_YR2FIND1.Trim();
                    result.Year2Subject1_Result = dbResult.inter2voc_YR2FRES1.Trim();

                    result.Year2Subject2_Marks = dbResult.inter2voc_YR2FMKS2.Trim();
                    result.Year2Subject2_Indicator = dbResult.inter2voc_YR2FIND2.Trim();
                    result.Year2Subject2_Result = dbResult.inter2voc_YR2FRES2.Trim();

                    result.Year2Subject3_Marks = dbResult.inter2voc_YR2TMKS1.Trim();
                    result.Year2Subject3_Indicator = dbResult.inter2voc_YR2TIND1.Trim();
                    result.Year2Subject3_Result = dbResult.inter2voc_YR2TRES1.Trim();

                    result.Year2Subject4_Marks = dbResult.inter2voc_YR2TMKS2.Trim();
                    result.Year2Subject4_Indicator = dbResult.inter2voc_YR2TIND2.Trim();
                    result.Year2Subject4_Result = dbResult.inter2voc_YR2TRES2.Trim();

                    result.Year2Subject5_Marks = dbResult.inter2voc_YR2TMKS3.Trim();
                    result.Year2Subject5_Indicator = dbResult.inter2voc_YR2TIND3.Trim();
                    result.Year2Subject5_Result = dbResult.inter2voc_YR2TRES3.Trim();

                    result.Year2Subject6_Marks = dbResult.inter2voc_YR2PMKS1.Trim();
                    result.Year2Subject6_Indicator = dbResult.inter2voc_YR2PIND1.Trim();
                    result.Year2Subject6_Result = dbResult.inter2voc_YR2PRES1.Trim();

                    result.Year2Subject7_Marks = dbResult.inter2voc_YR2PMKS2.Trim();
                    result.Year2Subject7_Indicator = dbResult.inter2voc_YR2PIND2.Trim();
                    result.Year2Subject7_Result = dbResult.inter2voc_YR2PRES2.Trim();

                    result.Year2Subject8_Marks = dbResult.inter2voc_YR2PMKS3.Trim();
                    result.Year2Subject8_Indicator = dbResult.inter2voc_YR2PIND3.Trim();
                    result.Year2Subject8_Result = dbResult.inter2voc_YR2PRES3.Trim();

                    result.Year2Subject9_Marks = dbResult.inter2voc_YR2PMKS4.Trim();
                    result.Year2Subject9_Indicator = dbResult.inter2voc_YR2PIND4.Trim();
                    result.Year2Subject9_Result = dbResult.inter2voc_YR2PRES4.Trim();

                    result.Total = dbResult.inter2voc_TOTAL.Trim();
                    result.Result = dbResult.inter2voc_RESULT.Trim();

                    result.IsResultSet = true;
                }
            }

            return result;
        }

        public TSSSCResults GetTSSSCResult(string HallTicketNumber, string ExamYear)
        {
            TSSSCResults TSResult = new TSSSCResults();

            var fileLog = db.rslt_UploadedResults.Where(_ => _.upld_ExamType == (int)Enums_ExamTypes.Telangana_SSC && _.upld_ExamYear == ExamYear && _.upld_Status == Constants_FileUploadStatus.Done).OrderByDescending(_ => _.upld_Sno).FirstOrDefault();

            if (fileLog != null && !string.IsNullOrEmpty(fileLog.upld_FileName))
            {
                string path = HttpContext.Current.Server.MapPath(ConfigResultsDoNotDeletePath);

                DataTable dt = _dbConnect.GetMSAccessData(path, fileLog.upld_FileName, ConfigTableSheetName, Constants_FileFields.SSC_HallTicket_TS, HallTicketNumber);

                if (dt != null && dt.Rows.Count == 1)
                {
                    TSResult.IsResultSet = true;
                    TSResult.District = dt.Rows[0]["DIST"].ToString();
                    TSResult.SchoolCode = dt.Rows[0]["SC_CODE"].ToString();
                    TSResult.RollNo = dt.Rows[0]["HTNO"].ToString();
                    TSResult.FullName = dt.Rows[0]["CNAME"].ToString();
                    TSResult.PHDescription = dt.Rows[0]["PH_DESC"].ToString();
                    TSResult.Language1Flag = dt.Rows[0]["L1_FLG"].ToString();
                    TSResult.Language1InternalGrade = dt.Rows[0]["L1_INTGRD"].ToString();
                    TSResult.Language1Grade1 = dt.Rows[0]["L1_GRADE1"].ToString();
                    TSResult.Language1FinalGrade = dt.Rows[0]["L1_GRADE"].ToString();
                    TSResult.Language1Points = dt.Rows[0]["L1_POINTS"].ToString();
                    TSResult.Language2Flag = dt.Rows[0]["L2_FLG"].ToString();
                    TSResult.Language2InternalGrade = dt.Rows[0]["L2_INTGRD"].ToString();
                    TSResult.Language2Grade1 = dt.Rows[0]["L2_GRADE1"].ToString();
                    TSResult.Language2FinalGrade = dt.Rows[0]["L2_GRADE"].ToString();
                    TSResult.Language2Points = dt.Rows[0]["L2_POINTS"].ToString();
                    TSResult.Language3Flag = dt.Rows[0]["L3_FLG"].ToString();
                    TSResult.Language3InternalGrade = dt.Rows[0]["L3_INTGRD"].ToString();
                    TSResult.Language3Grade1 = dt.Rows[0]["L3_GRADE1"].ToString();
                    TSResult.Language3FinalGrade = dt.Rows[0]["L3_GRADE"].ToString();
                    TSResult.Language3Points = dt.Rows[0]["L3_POINTS"].ToString();
                    TSResult.MathsFlag = dt.Rows[0]["MAT_FLG"].ToString();
                    TSResult.MathsInternalGrade = dt.Rows[0]["MAT_INTGRD"].ToString();
                    TSResult.MathsGrade1 = dt.Rows[0]["MAT_GRADE1"].ToString();
                    TSResult.MathsFinalGrade = dt.Rows[0]["MAT_GRADE"].ToString();
                    TSResult.MathsPoints = dt.Rows[0]["MAT_POINTS"].ToString();
                    TSResult.ScienceFlag = dt.Rows[0]["SCI_FLG"].ToString();
                    TSResult.ScienceInternalGrade = dt.Rows[0]["SCI_INTGRD"].ToString();
                    TSResult.ScienceGrade1 = dt.Rows[0]["SCI_GRADE1"].ToString();
                    TSResult.ScienceFinalGrade = dt.Rows[0]["SCI_GRADE"].ToString();
                    TSResult.SciencePoints = dt.Rows[0]["SCI_POINTS"].ToString();
                    TSResult.SocialFlag = dt.Rows[0]["SOC_FLG"].ToString();
                    TSResult.SocialInternalGrade = dt.Rows[0]["SOC_INTGRD"].ToString();
                    TSResult.SocialGrade1 = dt.Rows[0]["SOC_GRADE1"].ToString();
                    TSResult.SocialFinalGrade = dt.Rows[0]["SOC_GRADE"].ToString();
                    TSResult.SocialPoints = dt.Rows[0]["SOC_POINTS"].ToString();
                    TSResult.GPA = dt.Rows[0]["GPA_9"].ToString();
                    TSResult.Result = dt.Rows[0]["RESULTS"].ToString();
                    TSResult.OptionalLanguageFlag = dt.Rows[0]["OL_FLG"].ToString();
                    TSResult.OptionalLanguageInternalGrade = dt.Rows[0]["OL_INTGRD"].ToString();
                    TSResult.OptionalLanguageGrade1 = dt.Rows[0]["OL_GRADE1"].ToString();
                    TSResult.OptionalLanguageFinalGrade = dt.Rows[0]["OL_GRADE"].ToString();
                    TSResult.OptionalLanguagePoints = dt.Rows[0]["OL_POINTS"].ToString();
                    TSResult.ValueEducationLifeSkillsGrade = dt.Rows[0]["VAL_GRADE"].ToString();
                    TSResult.ValueEducationLifeSkillsPoints = dt.Rows[0]["VAL_POINTS"].ToString();
                    TSResult.ArtCulturalEducationGrade = dt.Rows[0]["ART_GRADE"].ToString();
                    TSResult.ArtCulturalEducationGrade = dt.Rows[0]["ART_POINTS"].ToString();
                    TSResult.WorkComputerEducationGrade = dt.Rows[0]["WRK_GRADE"].ToString();
                    TSResult.WorkComputerEducationGrade = dt.Rows[0]["WRK_POINTS"].ToString();
                    TSResult.PhysicalHealthEducationGrade = dt.Rows[0]["PHY_GRADE"].ToString();
                    TSResult.PhysicalHealthEducationGrade = dt.Rows[0]["PHY_POINTS"].ToString();
                    TSResult.DistrictName = dt.Rows[0]["DT_NAME"].ToString();

                    TSResult.Exam = string.Format("{0} - {1}", GetResultsExamTypeById(Constants_ExamIds.TelanganaSSC_TSSSC).corseTyp_Name, ExamYear);
                }
            }

            return TSResult;
        }

        public TSEamcetEng GetTSEamcetEngResult(string HallTicketNumber, string ExamYear)
        {
            TSEamcetEng TSEamcetEngResult = new TSEamcetEng();

            var fileLog = db.rslt_UploadedResults.Where(_ => _.upld_ExamType == (int)Enums_ExamTypes.Telangana_EAMCET_Engineering_13 && _.upld_ExamYear == ExamYear && _.upld_Status == Constants_FileUploadStatus.Done).OrderByDescending(_ => _.upld_Sno).FirstOrDefault();

            if (fileLog != null && !string.IsNullOrEmpty(fileLog.upld_FileName))
            {
                string path = HttpContext.Current.Server.MapPath(ConfigResultsDoNotDeletePath);

                DataTable dt = _dbConnect.GetMSAccessData(path, fileLog.upld_FileName, ConfigTableSheetName, "HTNO", HallTicketNumber);

                if (dt != null && dt.Rows.Count == 1)
                {
                    TSEamcetEngResult.IsResultSet = true;
                    TSEamcetEngResult.HallTicketNumber = dt.Columns.Contains("HTNO") ? dt.Rows[0]["HTNO"].ToString() : "";
                    TSEamcetEngResult.FullName = dt.Columns.Contains("CNAME") ? dt.Rows[0]["CNAME"].ToString() : "";
                    TSEamcetEngResult.FathersName = dt.Columns.Contains("FNAME") ? dt.Rows[0]["FNAME"].ToString() : "";
                    TSEamcetEngResult.Gender = dt.Columns.Contains("GENDER") ? dt.Rows[0]["GENDER"].ToString() : "";
                    TSEamcetEngResult.LocalArea = dt.Columns.Contains("LOCALAREA") ? dt.Rows[0]["LOCALAREA"].ToString() : "";
                    TSEamcetEngResult.Category = dt.Columns.Contains("CATEGORY") ? dt.Rows[0]["CATEGORY"].ToString() : "";
                    TSEamcetEngResult.Maths = dt.Columns.Contains("MATHS") ? dt.Rows[0]["MATHS"].ToString() : "";
                    TSEamcetEngResult.Physics = dt.Columns.Contains("PHYSICS") ? dt.Rows[0]["PHYSICS"].ToString() : "";
                    TSEamcetEngResult.Chemistry = dt.Columns.Contains("CHEMISTRY") ? dt.Rows[0]["CHEMISTRY"].ToString() : "";
                    TSEamcetEngResult.TotalMarks = dt.Columns.Contains("EAMCETMARKS") ? dt.Rows[0]["EAMCETMARKS"].ToString() : "";
                    TSEamcetEngResult.Rank = dt.Columns.Contains("RANK") ? dt.Rows[0]["RANK"].ToString() : "";
                    TSEamcetEngResult.Result = dt.Columns.Contains("RESULT") ? dt.Rows[0]["RESULT"].ToString() : "";

                    TSEamcetEngResult.Exam = string.Format("{0} - {1}", GetResultsExamTypeById(Constants_ExamIds.TelanganaEAMCETAgricultureMedical_TSEAMAM).corseTyp_Name, ExamYear);
                }
            }

            return TSEamcetEngResult;
        }

        public TSEamcetAM GetTSEamcetAMResult(string HallTicketNumber, string ExamYear)
        {
            TSEamcetAM TSEamcetEngResult = new TSEamcetAM();

            var fileLog = db.rslt_UploadedResults.Where(_ => _.upld_ExamType == (int)Enums_ExamTypes.Telangana_EAMCET_AgricultureMedical_14 && _.upld_ExamYear == ExamYear && _.upld_Status == Constants_FileUploadStatus.Done).OrderByDescending(_ => _.upld_Sno).FirstOrDefault();

            if (fileLog != null && !string.IsNullOrEmpty(fileLog.upld_FileName))
            {
                string path = HttpContext.Current.Server.MapPath(ConfigResultsDoNotDeletePath);

                DataTable dt = _dbConnect.GetMSAccessData(path, fileLog.upld_FileName, ConfigTableSheetName, "HTNO", HallTicketNumber);

                if (dt != null && dt.Rows.Count == 1)
                {
                    TSEamcetEngResult.IsResultSet = true;
                    TSEamcetEngResult.HallTicketNumber = dt.Columns.Contains("HTNO") ? dt.Rows[0]["HTNO"].ToString() : "";
                    TSEamcetEngResult.FullName = dt.Columns.Contains("CNAME") ? dt.Rows[0]["CNAME"].ToString() : "";
                    TSEamcetEngResult.FathersName = dt.Columns.Contains("FNAME") ? dt.Rows[0]["FNAME"].ToString() : "";
                    TSEamcetEngResult.Gender = dt.Columns.Contains("GENDER") ? dt.Rows[0]["GENDER"].ToString() : "";
                    TSEamcetEngResult.LocalArea = dt.Columns.Contains("LOCALAREA") ? dt.Rows[0]["LOCALAREA"].ToString() : "";
                    TSEamcetEngResult.Category = dt.Columns.Contains("CATEGORY") ? dt.Rows[0]["CATEGORY"].ToString() : "";
                    TSEamcetEngResult.Botany = dt.Columns.Contains("BOTANY") ? dt.Rows[0]["BOTANY"].ToString() : "";
                    TSEamcetEngResult.Zoology = dt.Columns.Contains("ZOOLOGY") ? dt.Rows[0]["ZOOLOGY"].ToString() : "";
                    TSEamcetEngResult.Physics = dt.Columns.Contains("PHYSICS") ? dt.Rows[0]["PHYSICS"].ToString() : "";
                    TSEamcetEngResult.Chemistry = dt.Columns.Contains("CHEMISTRY") ? dt.Rows[0]["CHEMISTRY"].ToString() : "";
                    TSEamcetEngResult.TotalMarks = dt.Columns.Contains("EAMCETMARKS") ? dt.Rows[0]["EAMCETMARKS"].ToString() : "";
                    TSEamcetEngResult.Rank = dt.Columns.Contains("RANK") ? dt.Rows[0]["RANK"].ToString() : "";
                    TSEamcetEngResult.Result = dt.Columns.Contains("RESULT") ? dt.Rows[0]["RESULT"].ToString() : "";

                    TSEamcetEngResult.Exam = string.Format("{0} - {1}", GetResultsExamTypeById(Constants_ExamIds.TelanganaEAMCETEngineering_TSEAMENG).corseTyp_Name, ExamYear);
                }
            }

            return TSEamcetEngResult;
        }


        #endregion

        #region ------------------------------------------------------------------------------------------------------------ ANDHRA PRADESH RESULTS

        private rslt_IntermediateYear2Regular UnwrapAPRegDBResult(string HallTicketNumber, int ExamType, string ExamYear)
        {
            var dbResult = db.rslt_IntermediateYear2Regular.FirstOrDefault(_ => _.inter2reg_ROLLNO == HallTicketNumber && _.inter2reg_ExamType == ExamType && _.inter2reg_ExamYear == ExamYear);

            if (dbResult != null)
            {
                if (ExamType == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_1_Regular)
                {
                    dbResult.inter2reg_ROLLNO = dbResult.inter2reg_FullRecord.Substring(0, 10);
                    dbResult.inter2reg_DIST = dbResult.inter2reg_FullRecord.Substring(10, 2);
                    dbResult.inter2reg_CNAME = dbResult.inter2reg_FullRecord.Substring(12, 30);
                    dbResult.inter2reg_YR1PC1 = dbResult.inter2reg_FullRecord.Substring(42, 2);
                    dbResult.inter2reg_YR1MKS1 = dbResult.inter2reg_FullRecord.Substring(44, 3);
                    dbResult.inter2reg_YR1IND1 = dbResult.inter2reg_FullRecord.Substring(47, 1);
                    dbResult.inter2reg_YR1RS1 = dbResult.inter2reg_FullRecord.Substring(48, 1);
                    dbResult.inter2reg_YR1PC2 = dbResult.inter2reg_FullRecord.Substring(49, 2);
                    dbResult.inter2reg_YR1MKS2 = dbResult.inter2reg_FullRecord.Substring(51, 3);
                    dbResult.inter2reg_YR1IND2 = dbResult.inter2reg_FullRecord.Substring(54, 1);
                    dbResult.inter2reg_YR1RS2 = dbResult.inter2reg_FullRecord.Substring(55, 1);
                    dbResult.inter2reg_YR1PC3 = dbResult.inter2reg_FullRecord.Substring(56, 2);
                    dbResult.inter2reg_YR1MKS3 = dbResult.inter2reg_FullRecord.Substring(58, 3);
                    dbResult.inter2reg_YR1IND3 = dbResult.inter2reg_FullRecord.Substring(61, 1);
                    dbResult.inter2reg_YR1RS3 = dbResult.inter2reg_FullRecord.Substring(62, 1);
                    dbResult.inter2reg_YR1PC4 = dbResult.inter2reg_FullRecord.Substring(63, 2);
                    dbResult.inter2reg_YR1MKS4 = dbResult.inter2reg_FullRecord.Substring(65, 3);
                    dbResult.inter2reg_YR1IND4 = dbResult.inter2reg_FullRecord.Substring(68, 1);
                    dbResult.inter2reg_YR1RS4 = dbResult.inter2reg_FullRecord.Substring(69, 1);
                    dbResult.inter2reg_YR1PC5 = dbResult.inter2reg_FullRecord.Substring(70, 2);
                    dbResult.inter2reg_YR1MKS5 = dbResult.inter2reg_FullRecord.Substring(72, 3);
                    dbResult.inter2reg_YR1IND5 = dbResult.inter2reg_FullRecord.Substring(75, 1);
                    dbResult.inter2reg_YR1RS5 = dbResult.inter2reg_FullRecord.Substring(76, 1);
                    dbResult.inter2reg_YR1PC6 = dbResult.inter2reg_FullRecord.Substring(77, 2);
                    dbResult.inter2reg_YR1MKS6 = dbResult.inter2reg_FullRecord.Substring(79, 3);
                    dbResult.inter2reg_YR1IND6 = dbResult.inter2reg_FullRecord.Substring(82, 1);
                    dbResult.inter2reg_YR1RS6 = dbResult.inter2reg_FullRecord.Substring(83, 1);

                    dbResult.inter2reg_YR2PC1 = "";
                    dbResult.inter2reg_YR2MKS1 = "";
                    dbResult.inter2reg_YR2IND1 = "";
                    dbResult.inter2reg_YR2RS1 = "";
                    dbResult.inter2reg_YR2PC2 = "";
                    dbResult.inter2reg_YR2MKS2 = "";
                    dbResult.inter2reg_YR2IND2 = "";
                    dbResult.inter2reg_YR2RS2 = "";
                    dbResult.inter2reg_YR2PC3 = "";
                    dbResult.inter2reg_YR2MKS3 = "";
                    dbResult.inter2reg_YR2IND3 = "";
                    dbResult.inter2reg_YR2RS3 = "";
                    dbResult.inter2reg_YR2PC4 = "";
                    dbResult.inter2reg_YR2MKS4 = "";
                    dbResult.inter2reg_YR2IND4 = "";
                    dbResult.inter2reg_YR2RS4 = "";
                    dbResult.inter2reg_YR2PC5 = "";
                    dbResult.inter2reg_YR2MKS5 = "";
                    dbResult.inter2reg_YR2IND5 = "";
                    dbResult.inter2reg_YR2RS5 = "";
                    dbResult.inter2reg_YR2PC6 = "";
                    dbResult.inter2reg_YR2MKS6 = "";
                    dbResult.inter2reg_YR2IND6 = "";
                    dbResult.inter2reg_YR2RS6 = "";
                    dbResult.inter2reg_YR2PC7 = "";
                    dbResult.inter2reg_YR2MKS7 = "";
                    dbResult.inter2reg_YR2IND7 = "";
                    dbResult.inter2reg_YR2RS7 = "";
                    dbResult.inter2reg_YR2PC8 = "";
                    dbResult.inter2reg_YR2MKS8 = "";
                    dbResult.inter2reg_YR2IND8 = "";
                    dbResult.inter2reg_YR2RS8 = "";
                    dbResult.inter2reg_YR2PC9 = "";
                    dbResult.inter2reg_YR2MKS9 = "";
                    dbResult.inter2reg_YR2IND9 = "";
                    dbResult.inter2reg_YR2RS9 = "";
                    dbResult.inter2reg_YR2PC10 = "";
                    dbResult.inter2reg_YR2MKS10 = "";
                    dbResult.inter2reg_YR2IND10 = "";
                    dbResult.inter2reg_YR2RS10 = "";

                    dbResult.inter2reg_TOTAL = dbResult.inter2reg_FullRecord.Substring(84, 3);
                    dbResult.inter2reg_RESULT = dbResult.inter2reg_FullRecord.Substring(87, 6);
                    dbResult.inter2reg_ADD_FLG = "";
                    dbResult.inter2reg_LINKNO = "";
                }
                else if (ExamType == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_2_Regular)
                {
                    dbResult.inter2reg_ROLLNO = dbResult.inter2reg_FullRecord.Substring(0, 10);
                    dbResult.inter2reg_DIST = dbResult.inter2reg_FullRecord.Substring(10, 2);
                    dbResult.inter2reg_CNAME = dbResult.inter2reg_FullRecord.Substring(12, 30);
                    dbResult.inter2reg_YR1PC1 = dbResult.inter2reg_FullRecord.Substring(42, 2);
                    dbResult.inter2reg_YR1MKS1 = dbResult.inter2reg_FullRecord.Substring(44, 2);
                    dbResult.inter2reg_YR1IND1 = dbResult.inter2reg_FullRecord.Substring(46, 1);
                    dbResult.inter2reg_YR1RS1 = dbResult.inter2reg_FullRecord.Substring(47, 4);
                    dbResult.inter2reg_YR1PC2 = dbResult.inter2reg_FullRecord.Substring(51, 2);
                    dbResult.inter2reg_YR1MKS2 = dbResult.inter2reg_FullRecord.Substring(53, 2);
                    dbResult.inter2reg_YR1IND2 = dbResult.inter2reg_FullRecord.Substring(55, 1);
                    dbResult.inter2reg_YR1RS2 = dbResult.inter2reg_FullRecord.Substring(56, 4);
                    dbResult.inter2reg_YR1PC3 = dbResult.inter2reg_FullRecord.Substring(60, 2);
                    dbResult.inter2reg_YR1MKS3 = dbResult.inter2reg_FullRecord.Substring(62, 2);
                    dbResult.inter2reg_YR1IND3 = dbResult.inter2reg_FullRecord.Substring(64, 1);
                    dbResult.inter2reg_YR1RS3 = dbResult.inter2reg_FullRecord.Substring(65, 4);
                    dbResult.inter2reg_YR1PC4 = dbResult.inter2reg_FullRecord.Substring(69, 2);
                    dbResult.inter2reg_YR1MKS4 = dbResult.inter2reg_FullRecord.Substring(71, 2);
                    dbResult.inter2reg_YR1IND4 = dbResult.inter2reg_FullRecord.Substring(73, 1);
                    dbResult.inter2reg_YR1RS4 = dbResult.inter2reg_FullRecord.Substring(74, 4);
                    dbResult.inter2reg_YR1PC5 = dbResult.inter2reg_FullRecord.Substring(78, 2);
                    dbResult.inter2reg_YR1MKS5 = dbResult.inter2reg_FullRecord.Substring(80, 2);
                    dbResult.inter2reg_YR1IND5 = dbResult.inter2reg_FullRecord.Substring(82, 1);
                    dbResult.inter2reg_YR1RS5 = dbResult.inter2reg_FullRecord.Substring(83, 4);
                    dbResult.inter2reg_YR1PC6 = dbResult.inter2reg_FullRecord.Substring(87, 2);
                    dbResult.inter2reg_YR1MKS6 = dbResult.inter2reg_FullRecord.Substring(89, 2);
                    dbResult.inter2reg_YR1IND6 = dbResult.inter2reg_FullRecord.Substring(91, 1);
                    dbResult.inter2reg_YR1RS6 = dbResult.inter2reg_FullRecord.Substring(92, 4);
                    dbResult.inter2reg_YR2PC1 = dbResult.inter2reg_FullRecord.Substring(96, 2);
                    dbResult.inter2reg_YR2MKS1 = dbResult.inter2reg_FullRecord.Substring(98, 2);
                    dbResult.inter2reg_YR2IND1 = dbResult.inter2reg_FullRecord.Substring(100, 1);
                    dbResult.inter2reg_YR2RS1 = dbResult.inter2reg_FullRecord.Substring(101, 4);
                    dbResult.inter2reg_YR2PC2 = dbResult.inter2reg_FullRecord.Substring(105, 2);
                    dbResult.inter2reg_YR2MKS2 = dbResult.inter2reg_FullRecord.Substring(107, 2);
                    dbResult.inter2reg_YR2IND2 = dbResult.inter2reg_FullRecord.Substring(109, 1);
                    dbResult.inter2reg_YR2RS2 = dbResult.inter2reg_FullRecord.Substring(110, 4);
                    dbResult.inter2reg_YR2PC3 = dbResult.inter2reg_FullRecord.Substring(114, 2);
                    dbResult.inter2reg_YR2MKS3 = dbResult.inter2reg_FullRecord.Substring(116, 2);
                    dbResult.inter2reg_YR2IND3 = dbResult.inter2reg_FullRecord.Substring(118, 1);
                    dbResult.inter2reg_YR2RS3 = dbResult.inter2reg_FullRecord.Substring(119, 4);
                    dbResult.inter2reg_YR2PC4 = dbResult.inter2reg_FullRecord.Substring(123, 2);
                    dbResult.inter2reg_YR2MKS4 = dbResult.inter2reg_FullRecord.Substring(125, 2);
                    dbResult.inter2reg_YR2IND4 = dbResult.inter2reg_FullRecord.Substring(127, 1);
                    dbResult.inter2reg_YR2RS4 = dbResult.inter2reg_FullRecord.Substring(128, 4);
                    dbResult.inter2reg_YR2PC5 = dbResult.inter2reg_FullRecord.Substring(132, 2);
                    dbResult.inter2reg_YR2MKS5 = dbResult.inter2reg_FullRecord.Substring(134, 2);
                    dbResult.inter2reg_YR2IND5 = dbResult.inter2reg_FullRecord.Substring(136, 1);
                    dbResult.inter2reg_YR2RS5 = dbResult.inter2reg_FullRecord.Substring(137, 4);
                    dbResult.inter2reg_YR2PC6 = dbResult.inter2reg_FullRecord.Substring(141, 2);
                    dbResult.inter2reg_YR2MKS6 = dbResult.inter2reg_FullRecord.Substring(143, 2);
                    dbResult.inter2reg_YR2IND6 = dbResult.inter2reg_FullRecord.Substring(145, 1);
                    dbResult.inter2reg_YR2RS6 = dbResult.inter2reg_FullRecord.Substring(146, 4);
                    dbResult.inter2reg_YR2PC7 = dbResult.inter2reg_FullRecord.Substring(150, 2);
                    dbResult.inter2reg_YR2MKS7 = dbResult.inter2reg_FullRecord.Substring(152, 2);
                    dbResult.inter2reg_YR2IND7 = dbResult.inter2reg_FullRecord.Substring(154, 1);
                    dbResult.inter2reg_YR2RS7 = dbResult.inter2reg_FullRecord.Substring(155, 4);
                    dbResult.inter2reg_YR2PC8 = dbResult.inter2reg_FullRecord.Substring(159, 2);
                    dbResult.inter2reg_YR2MKS8 = dbResult.inter2reg_FullRecord.Substring(161, 2);
                    dbResult.inter2reg_YR2IND8 = dbResult.inter2reg_FullRecord.Substring(163, 1);
                    dbResult.inter2reg_YR2RS8 = dbResult.inter2reg_FullRecord.Substring(164, 4);
                    dbResult.inter2reg_YR2PC9 = dbResult.inter2reg_FullRecord.Substring(168, 2);
                    dbResult.inter2reg_YR2MKS9 = dbResult.inter2reg_FullRecord.Substring(170, 2);
                    dbResult.inter2reg_YR2IND9 = dbResult.inter2reg_FullRecord.Substring(172, 1);
                    dbResult.inter2reg_YR2RS9 = dbResult.inter2reg_FullRecord.Substring(173, 4);
                    dbResult.inter2reg_YR2PC10 = dbResult.inter2reg_FullRecord.Substring(177, 2);
                    dbResult.inter2reg_YR2MKS10 = dbResult.inter2reg_FullRecord.Substring(179, 2);
                    dbResult.inter2reg_YR2IND10 = dbResult.inter2reg_FullRecord.Substring(181, 1);
                    dbResult.inter2reg_YR2RS10 = dbResult.inter2reg_FullRecord.Substring(182, 4);
                    dbResult.inter2reg_TOTAL = dbResult.inter2reg_FullRecord.Substring(186, 6);
                    dbResult.inter2reg_RESULT = dbResult.inter2reg_FullRecord.Substring(192, 4);
                    dbResult.inter2reg_ADD_FLG = "";
                    dbResult.inter2reg_LINKNO = "";
                }
            }

            return dbResult;
        }

        public TSYr2RegResult GetAPRegResult(string HallTicketNumber, int ExamType, string ExamYear)
        {
            TSYr2RegResult result = new TSYr2RegResult();

            if (!string.IsNullOrEmpty(HallTicketNumber))
            {
                var dbResult = UnwrapAPRegDBResult(HallTicketNumber, ExamType, ExamYear);

                if (dbResult != null)
                {
                    var subjects = db.rslt_SubjectsMaster.Where(_ => _.sbjct_CourseID == ExamType).ToList();
                    var dbExamType = db.rslt_CourseTypes.FirstOrDefault(_ => _.corseTyp_Sno == ExamType);

                    result.Exam = dbExamType == null ? "" : dbExamType.corseTyp_Name;
                    result.FullName = dbResult.inter2reg_CNAME.Trim();
                    result.HallTicketNumber = dbResult.inter2reg_ROLLNO.Trim();

                    result.Yr1Subject1 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC1) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC1).sbjct_Name.Trim() : "";
                    result.Yr1Subject2 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC2) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC2).sbjct_Name.Trim() : "";
                    result.Yr1Subject3 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC3) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC3).sbjct_Name.Trim() : "";
                    result.Yr1Subject4 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC4) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC4).sbjct_Name.Trim() : "";
                    result.Yr1Subject5 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC5) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC5).sbjct_Name.Trim() : "";
                    result.Yr1Subject6 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC6) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR1PC6).sbjct_Name.Trim() : "";
                    result.Yr2Subject1 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC1) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC1).sbjct_Name.Trim() : "";
                    result.Yr2Subject2 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC2) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC2).sbjct_Name.Trim() : "";
                    result.Yr2Subject3 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC3) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC3).sbjct_Name.Trim() : "";
                    result.Yr2Subject4 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC4) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC4).sbjct_Name.Trim() : "";
                    result.Yr2Subject5 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC5) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC5).sbjct_Name.Trim() : "";
                    result.Yr2Subject6 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC6) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC6).sbjct_Name.Trim() : "";
                    result.Yr2Subject7 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC7) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC7).sbjct_Name.Trim() : "";
                    result.Yr2Subject8 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC8) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC8).sbjct_Name.Trim() : "";
                    result.Yr2Subject9 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC9) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC9).sbjct_Name.Trim() : "";
                    result.Yr2Subject10 = subjects.Any(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC10) ? subjects.FirstOrDefault(_ => _.sbjct_ID == dbResult.inter2reg_YR2PC10).sbjct_Name.Trim() : "";

                    result.Yr1Subject1_Marks = dbResult.inter2reg_YR1MKS1.Trim();
                    result.Yr1Subject1_Indicator = dbResult.inter2reg_YR1IND1.Trim();
                    result.Yr1Subject1_Result = dbResult.inter2reg_YR1RS1.Trim();

                    result.Yr1Subject2_Marks = dbResult.inter2reg_YR1MKS2.Trim();
                    result.Yr1Subject2_Indicator = dbResult.inter2reg_YR1IND2.Trim();
                    result.Yr1Subject2_Result = dbResult.inter2reg_YR1RS2.Trim();

                    result.Yr1Subject3_Marks = dbResult.inter2reg_YR1MKS3.Trim();
                    result.Yr1Subject3_Indicator = dbResult.inter2reg_YR1IND3.Trim();
                    result.Yr1Subject3_Result = dbResult.inter2reg_YR1RS3.Trim();

                    result.Yr1Subject4_Marks = dbResult.inter2reg_YR1MKS4.Trim();
                    result.Yr1Subject4_Indicator = dbResult.inter2reg_YR1IND4.Trim();
                    result.Yr1Subject4_Result = dbResult.inter2reg_YR1RS4.Trim();

                    result.Yr1Subject5_Marks = dbResult.inter2reg_YR1MKS5.Trim();
                    result.Yr1Subject5_Indicator = dbResult.inter2reg_YR1IND5.Trim();
                    result.Yr1Subject5_Result = dbResult.inter2reg_YR1RS5.Trim();

                    result.Yr1Subject6_Marks = dbResult.inter2reg_YR1MKS6.Trim();
                    result.Yr1Subject6_Indicator = dbResult.inter2reg_YR1IND6.Trim();
                    result.Yr1Subject6_Result = dbResult.inter2reg_YR1RS6.Trim();

                    result.Yr2Subject1_Marks = dbResult.inter2reg_YR2MKS1.Trim();
                    result.Yr2Subject1_Indicator = dbResult.inter2reg_YR2IND1.Trim();
                    result.Yr2Subject1_Result = dbResult.inter2reg_YR2RS1.Trim();

                    result.Yr2Subject2_Marks = dbResult.inter2reg_YR2MKS2.Trim();
                    result.Yr2Subject2_Indicator = dbResult.inter2reg_YR2IND2.Trim();
                    result.Yr2Subject2_Result = dbResult.inter2reg_YR2RS2.Trim();

                    result.Yr2Subject3_Marks = dbResult.inter2reg_YR2MKS3.Trim();
                    result.Yr2Subject3_Indicator = dbResult.inter2reg_YR2IND3.Trim();
                    result.Yr2Subject3_Result = dbResult.inter2reg_YR2RS3.Trim();

                    result.Yr2Subject4_Marks = dbResult.inter2reg_YR2MKS4.Trim();
                    result.Yr2Subject4_Indicator = dbResult.inter2reg_YR2IND4.Trim();
                    result.Yr2Subject4_Result = dbResult.inter2reg_YR2RS4.Trim();

                    result.Yr2Subject5_Marks = dbResult.inter2reg_YR2MKS5.Trim();
                    result.Yr2Subject5_Indicator = dbResult.inter2reg_YR2IND5.Trim();
                    result.Yr2Subject5_Result = dbResult.inter2reg_YR2RS5.Trim();

                    result.Yr2Subject6_Marks = dbResult.inter2reg_YR2MKS6.Trim();
                    result.Yr2Subject6_Indicator = dbResult.inter2reg_YR2IND6.Trim();
                    result.Yr2Subject6_Result = dbResult.inter2reg_YR2RS6.Trim();

                    result.Yr2Subject7_Marks = dbResult.inter2reg_YR2MKS7.Trim();
                    result.Yr2Subject7_Indicator = dbResult.inter2reg_YR2IND7.Trim();
                    result.Yr2Subject7_Result = dbResult.inter2reg_YR2RS7.Trim();

                    result.Yr2Subject8_Marks = dbResult.inter2reg_YR2MKS8.Trim();
                    result.Yr2Subject8_Indicator = dbResult.inter2reg_YR2IND8.Trim();
                    result.Yr2Subject8_Result = dbResult.inter2reg_YR2RS8.Trim();

                    result.Yr2Subject9_Marks = dbResult.inter2reg_YR2MKS9.Trim();
                    result.Yr2Subject9_Indicator = dbResult.inter2reg_YR2IND9.Trim();
                    result.Yr2Subject9_Result = dbResult.inter2reg_YR2RS9.Trim();

                    result.Yr2Subject10_Marks = dbResult.inter2reg_YR2MKS10.Trim();
                    result.Yr2Subject10_Indicator = dbResult.inter2reg_YR2IND10.Trim();
                    result.Yr2Subject10_Result = dbResult.inter2reg_YR2RS10.Trim();

                    result.Total = dbResult.inter2reg_TOTAL.Trim();
                    result.Result = dbResult.inter2reg_RESULT.Trim();
                    //result.AdditionalFlag = dbResult.inter2reg_ADD_FLG.Trim();

                    result.IsResultSet = true;
                }
            }

            return result;
        }

        private rslt_IntermediateYear2Voc UnwrapAPVocDBResult(string HallTicketNumber, int ExamType, string ExamYear)
        {
            var dbResult = db.rslt_IntermediateYear2Voc.FirstOrDefault(_ => _.inter2voc_ROLLNO == HallTicketNumber && _.inter2voc_ExamType == ExamType && _.inter2voc_ExamYear == ExamYear);

            if (dbResult != null)
            {
                if (ExamType == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_1_Vocational)
                {
                    dbResult.inter2voc_DIST = dbResult.inter2voc_FullRecord.Substring(10, 2);
                    dbResult.inter2voc_CNAME = dbResult.inter2voc_FullRecord.Substring(12, 30);
                    dbResult.inter2voc_COURSE = dbResult.inter2voc_FullRecord.Substring(42, 3);
                    dbResult.inter2voc_YR1FPC1 = dbResult.inter2voc_FullRecord.Substring(45, 2);
                    dbResult.inter2voc_YR1FMKS1 = dbResult.inter2voc_FullRecord.Substring(47, 3);
                    dbResult.inter2voc_YR1FIND1 = dbResult.inter2voc_FullRecord.Substring(50, 1);
                    dbResult.inter2voc_YR1FRES1 = dbResult.inter2voc_FullRecord.Substring(51, 1);
                    dbResult.inter2voc_YR1FPC2 = dbResult.inter2voc_FullRecord.Substring(52, 2);
                    dbResult.inter2voc_YR1FMKS2 = dbResult.inter2voc_FullRecord.Substring(54, 3);
                    dbResult.inter2voc_YR1FIND2 = dbResult.inter2voc_FullRecord.Substring(57, 1);
                    dbResult.inter2voc_YR1FRES2 = dbResult.inter2voc_FullRecord.Substring(58, 1);
                    dbResult.inter2voc_YR1TPC1 = dbResult.inter2voc_FullRecord.Substring(59, 2);
                    dbResult.inter2voc_YR1TMKS1 = dbResult.inter2voc_FullRecord.Substring(61, 3);
                    dbResult.inter2voc_YR1TIND1 = dbResult.inter2voc_FullRecord.Substring(64, 1);
                    dbResult.inter2voc_YR1TRES1 = dbResult.inter2voc_FullRecord.Substring(65, 1);
                    dbResult.inter2voc_YR1TPC2 = dbResult.inter2voc_FullRecord.Substring(66, 2);
                    dbResult.inter2voc_YR1TMKS2 = dbResult.inter2voc_FullRecord.Substring(68, 3);
                    dbResult.inter2voc_YR1TIND2 = dbResult.inter2voc_FullRecord.Substring(71, 1);
                    dbResult.inter2voc_YR1TRES2 = dbResult.inter2voc_FullRecord.Substring(72, 1);
                    dbResult.inter2voc_YR1TPC3 = dbResult.inter2voc_FullRecord.Substring(73, 2);
                    dbResult.inter2voc_YR1TMKS3 = dbResult.inter2voc_FullRecord.Substring(75, 3);
                    dbResult.inter2voc_YR1TIND3 = dbResult.inter2voc_FullRecord.Substring(78, 1);
                    dbResult.inter2voc_YR1TRES3 = dbResult.inter2voc_FullRecord.Substring(79, 1);
                    dbResult.inter2voc_YR1PPC1 = dbResult.inter2voc_FullRecord.Substring(80, 2);
                    dbResult.inter2voc_YR1PMKS1 = dbResult.inter2voc_FullRecord.Substring(82, 3);
                    dbResult.inter2voc_YR1PIND1 = dbResult.inter2voc_FullRecord.Substring(85, 1);
                    dbResult.inter2voc_YR1PRES1 = dbResult.inter2voc_FullRecord.Substring(86, 1);
                    dbResult.inter2voc_YR1PPC2 = dbResult.inter2voc_FullRecord.Substring(87, 2);
                    dbResult.inter2voc_YR1PMKS2 = dbResult.inter2voc_FullRecord.Substring(89, 3);
                    dbResult.inter2voc_YR1PIND2 = dbResult.inter2voc_FullRecord.Substring(92, 1);
                    dbResult.inter2voc_YR1PRES2 = dbResult.inter2voc_FullRecord.Substring(93, 1);
                    dbResult.inter2voc_YR1PPC3 = dbResult.inter2voc_FullRecord.Substring(94, 2);
                    dbResult.inter2voc_YR1PMKS3 = dbResult.inter2voc_FullRecord.Substring(96, 3);
                    dbResult.inter2voc_YR1PIND3 = dbResult.inter2voc_FullRecord.Substring(99, 1);
                    dbResult.inter2voc_YR1PRES3 = dbResult.inter2voc_FullRecord.Substring(100, 1);
                    dbResult.inter2voc_YR1PPC4 = dbResult.inter2voc_FullRecord.Substring(101, 2);
                    dbResult.inter2voc_YR1PMKS4 = dbResult.inter2voc_FullRecord.Substring(103, 3);
                    dbResult.inter2voc_YR1PIND4 = dbResult.inter2voc_FullRecord.Substring(106, 1);
                    dbResult.inter2voc_YR1PRES4 = dbResult.inter2voc_FullRecord.Substring(107, 1);

                    dbResult.inter2voc_YR2FPC1 = "";
                    dbResult.inter2voc_YR2FMKS1 = "";
                    dbResult.inter2voc_YR2FIND1 = "";
                    dbResult.inter2voc_YR2FRES1 = "";
                    dbResult.inter2voc_YR2FPC2 = "";
                    dbResult.inter2voc_YR2FMKS2 = "";
                    dbResult.inter2voc_YR2FIND2 = "";
                    dbResult.inter2voc_YR2FRES2 = "";
                    dbResult.inter2voc_YR2TPC1 = "";
                    dbResult.inter2voc_YR2TMKS1 = "";
                    dbResult.inter2voc_YR2TIND1 = "";
                    dbResult.inter2voc_YR2TRES1 = "";
                    dbResult.inter2voc_YR2TPC2 = "";
                    dbResult.inter2voc_YR2TMKS2 = "";
                    dbResult.inter2voc_YR2TIND2 = "";
                    dbResult.inter2voc_YR2TRES2 = "";
                    dbResult.inter2voc_YR2TPC3 = "";
                    dbResult.inter2voc_YR2TMKS3 = "";
                    dbResult.inter2voc_YR2TIND3 = "";
                    dbResult.inter2voc_YR2TRES3 = "";
                    dbResult.inter2voc_YR2PPC1 = "";
                    dbResult.inter2voc_YR2PMKS1 = "";
                    dbResult.inter2voc_YR2PIND1 = "";
                    dbResult.inter2voc_YR2PRES1 = "";
                    dbResult.inter2voc_YR2PPC2 = "";
                    dbResult.inter2voc_YR2PMKS2 = "";
                    dbResult.inter2voc_YR2PIND2 = "";
                    dbResult.inter2voc_YR2PRES2 = "";
                    dbResult.inter2voc_YR2PPC3 = "";
                    dbResult.inter2voc_YR2PMKS3 = "";
                    dbResult.inter2voc_YR2PIND3 = "";
                    dbResult.inter2voc_YR2PRES3 = "";
                    dbResult.inter2voc_YR2PPC4 = "";
                    dbResult.inter2voc_YR2PMKS4 = "";
                    dbResult.inter2voc_YR2PIND4 = "";
                    dbResult.inter2voc_YR2PRES4 = "";

                    dbResult.inter2voc_TOTAL = dbResult.inter2voc_FullRecord.Substring(108, 4);
                    dbResult.inter2voc_RESULT = dbResult.inter2voc_FullRecord.Substring(112, 6);
                }
                else if (ExamType == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_2_Vocational)
                {
                    dbResult.inter2voc_DIST = dbResult.inter2voc_FullRecord.Substring(10, 2);
                    dbResult.inter2voc_CNAME = dbResult.inter2voc_FullRecord.Substring(12, 30);
                    dbResult.inter2voc_COURSE = dbResult.inter2voc_FullRecord.Substring(42, 3);
                    dbResult.inter2voc_YR1FPC1 = dbResult.inter2voc_FullRecord.Substring(45, 2);
                    dbResult.inter2voc_YR1FMKS1 = dbResult.inter2voc_FullRecord.Substring(47, 3);
                    dbResult.inter2voc_YR1FIND1 = dbResult.inter2voc_FullRecord.Substring(50, 1);
                    dbResult.inter2voc_YR1FRES1 = dbResult.inter2voc_FullRecord.Substring(51, 4);
                    dbResult.inter2voc_YR1FPC2 = dbResult.inter2voc_FullRecord.Substring(55, 2);
                    dbResult.inter2voc_YR1FMKS2 = dbResult.inter2voc_FullRecord.Substring(57, 3);
                    dbResult.inter2voc_YR1FIND2 = dbResult.inter2voc_FullRecord.Substring(60, 1);
                    dbResult.inter2voc_YR1FRES2 = dbResult.inter2voc_FullRecord.Substring(61, 4);
                    dbResult.inter2voc_YR1TPC1 = dbResult.inter2voc_FullRecord.Substring(65, 2);
                    dbResult.inter2voc_YR1TMKS1 = dbResult.inter2voc_FullRecord.Substring(67, 3);
                    dbResult.inter2voc_YR1TIND1 = dbResult.inter2voc_FullRecord.Substring(70, 1);
                    dbResult.inter2voc_YR1TRES1 = dbResult.inter2voc_FullRecord.Substring(71, 4);
                    dbResult.inter2voc_YR1TPC2 = dbResult.inter2voc_FullRecord.Substring(75, 2);
                    dbResult.inter2voc_YR1TMKS2 = dbResult.inter2voc_FullRecord.Substring(77, 3);
                    dbResult.inter2voc_YR1TIND2 = dbResult.inter2voc_FullRecord.Substring(80, 1);
                    dbResult.inter2voc_YR1TRES2 = dbResult.inter2voc_FullRecord.Substring(81, 4);
                    dbResult.inter2voc_YR1TPC3 = dbResult.inter2voc_FullRecord.Substring(85, 2);
                    dbResult.inter2voc_YR1TMKS3 = dbResult.inter2voc_FullRecord.Substring(87, 3);
                    dbResult.inter2voc_YR1TIND3 = dbResult.inter2voc_FullRecord.Substring(90, 1);
                    dbResult.inter2voc_YR1TRES3 = dbResult.inter2voc_FullRecord.Substring(91, 4);
                    dbResult.inter2voc_YR1PPC1 = dbResult.inter2voc_FullRecord.Substring(95, 2);
                    dbResult.inter2voc_YR1PMKS1 = dbResult.inter2voc_FullRecord.Substring(97, 3);
                    dbResult.inter2voc_YR1PIND1 = dbResult.inter2voc_FullRecord.Substring(100, 1);
                    dbResult.inter2voc_YR1PRES1 = dbResult.inter2voc_FullRecord.Substring(101, 4);
                    dbResult.inter2voc_YR1PPC2 = dbResult.inter2voc_FullRecord.Substring(105, 2);
                    dbResult.inter2voc_YR1PMKS2 = dbResult.inter2voc_FullRecord.Substring(107, 3);
                    dbResult.inter2voc_YR1PIND2 = dbResult.inter2voc_FullRecord.Substring(110, 1);
                    dbResult.inter2voc_YR1PRES2 = dbResult.inter2voc_FullRecord.Substring(111, 4);
                    dbResult.inter2voc_YR1PPC3 = dbResult.inter2voc_FullRecord.Substring(115, 2);
                    dbResult.inter2voc_YR1PMKS3 = dbResult.inter2voc_FullRecord.Substring(117, 3);
                    dbResult.inter2voc_YR1PIND3 = dbResult.inter2voc_FullRecord.Substring(120, 1);
                    dbResult.inter2voc_YR1PRES3 = dbResult.inter2voc_FullRecord.Substring(121, 4);
                    dbResult.inter2voc_YR1PPC4 = dbResult.inter2voc_FullRecord.Substring(125, 2);
                    dbResult.inter2voc_YR1PMKS4 = dbResult.inter2voc_FullRecord.Substring(127, 3);
                    dbResult.inter2voc_YR1PIND4 = dbResult.inter2voc_FullRecord.Substring(130, 1);
                    dbResult.inter2voc_YR1PRES4 = dbResult.inter2voc_FullRecord.Substring(131, 4);
                    dbResult.inter2voc_YR2FPC1 = dbResult.inter2voc_FullRecord.Substring(135, 2);
                    dbResult.inter2voc_YR2FMKS1 = dbResult.inter2voc_FullRecord.Substring(137, 3);
                    dbResult.inter2voc_YR2FIND1 = dbResult.inter2voc_FullRecord.Substring(140, 1);
                    dbResult.inter2voc_YR2FRES1 = dbResult.inter2voc_FullRecord.Substring(141, 4);
                    dbResult.inter2voc_YR2FPC2 = dbResult.inter2voc_FullRecord.Substring(145, 2);
                    dbResult.inter2voc_YR2FMKS2 = dbResult.inter2voc_FullRecord.Substring(147, 3);
                    dbResult.inter2voc_YR2FIND2 = dbResult.inter2voc_FullRecord.Substring(150, 1);
                    dbResult.inter2voc_YR2FRES2 = dbResult.inter2voc_FullRecord.Substring(151, 4);
                    dbResult.inter2voc_YR2TPC1 = dbResult.inter2voc_FullRecord.Substring(155, 2);
                    dbResult.inter2voc_YR2TMKS1 = dbResult.inter2voc_FullRecord.Substring(157, 3);
                    dbResult.inter2voc_YR2TIND1 = dbResult.inter2voc_FullRecord.Substring(160, 1);
                    dbResult.inter2voc_YR2TRES1 = dbResult.inter2voc_FullRecord.Substring(161, 4);
                    dbResult.inter2voc_YR2TPC2 = dbResult.inter2voc_FullRecord.Substring(165, 2);
                    dbResult.inter2voc_YR2TMKS2 = dbResult.inter2voc_FullRecord.Substring(167, 3);
                    dbResult.inter2voc_YR2TIND2 = dbResult.inter2voc_FullRecord.Substring(170, 1);
                    dbResult.inter2voc_YR2TRES2 = dbResult.inter2voc_FullRecord.Substring(171, 4);
                    dbResult.inter2voc_YR2TPC3 = dbResult.inter2voc_FullRecord.Substring(175, 2);
                    dbResult.inter2voc_YR2TMKS3 = dbResult.inter2voc_FullRecord.Substring(177, 3);
                    dbResult.inter2voc_YR2TIND3 = dbResult.inter2voc_FullRecord.Substring(180, 1);
                    dbResult.inter2voc_YR2TRES3 = dbResult.inter2voc_FullRecord.Substring(181, 4);
                    dbResult.inter2voc_YR2PPC1 = dbResult.inter2voc_FullRecord.Substring(185, 2);
                    dbResult.inter2voc_YR2PMKS1 = dbResult.inter2voc_FullRecord.Substring(187, 3);
                    dbResult.inter2voc_YR2PIND1 = dbResult.inter2voc_FullRecord.Substring(190, 1);
                    dbResult.inter2voc_YR2PRES1 = dbResult.inter2voc_FullRecord.Substring(191, 4);
                    dbResult.inter2voc_YR2PPC2 = dbResult.inter2voc_FullRecord.Substring(195, 2);
                    dbResult.inter2voc_YR2PMKS2 = dbResult.inter2voc_FullRecord.Substring(197, 3);
                    dbResult.inter2voc_YR2PIND2 = dbResult.inter2voc_FullRecord.Substring(200, 1);
                    dbResult.inter2voc_YR2PRES2 = dbResult.inter2voc_FullRecord.Substring(201, 4);
                    dbResult.inter2voc_YR2PPC3 = dbResult.inter2voc_FullRecord.Substring(205, 2);
                    dbResult.inter2voc_YR2PMKS3 = dbResult.inter2voc_FullRecord.Substring(207, 3);
                    dbResult.inter2voc_YR2PIND3 = dbResult.inter2voc_FullRecord.Substring(210, 1);
                    dbResult.inter2voc_YR2PRES3 = dbResult.inter2voc_FullRecord.Substring(211, 4);
                    dbResult.inter2voc_YR2PPC4 = dbResult.inter2voc_FullRecord.Substring(215, 2);
                    dbResult.inter2voc_YR2PMKS4 = dbResult.inter2voc_FullRecord.Substring(217, 3);
                    dbResult.inter2voc_YR2PIND4 = dbResult.inter2voc_FullRecord.Substring(220, 1);
                    dbResult.inter2voc_YR2PRES4 = dbResult.inter2voc_FullRecord.Substring(221, 4);
                    dbResult.inter2voc_TOTAL = dbResult.inter2voc_FullRecord.Substring(225, 6);
                    dbResult.inter2voc_RESULT = dbResult.inter2voc_FullRecord.Substring(231, 4);
                }
            }

            return dbResult;
        }

        public TSYr2VocResult GetAPVocResult(string HallTicketNumber, int ExamType, string ExamYear)
        {
            TSYr2VocResult result = new TSYr2VocResult();

            if (!string.IsNullOrEmpty(HallTicketNumber))
            {
                var dbResult = UnwrapAPVocDBResult(HallTicketNumber, ExamType, ExamYear);

                if (dbResult != null)
                {
                    var course = db.rst_CoursesMaster.Any(_ => _.corse_CourseType == ExamType && _.corse_ID == dbResult.inter2voc_COURSE) ? db.rst_CoursesMaster.FirstOrDefault(_ => _.corse_CourseType == ExamType && _.corse_ID == dbResult.inter2voc_COURSE) : new rst_CoursesMaster();
                    var subjects = db.rslt_SubjectsMaster.Where(_ => _.sbjct_CourseID == ExamType).ToList();
                    var dbExamType = db.rslt_CourseTypes.FirstOrDefault(_ => _.corseTyp_Sno == ExamType);

                    List<ResultsSubjectsMasterMini> subjectsMini = new List<ResultsSubjectsMasterMini>();
                    foreach (var subject in subjects)
                    {
                        subjectsMini.Add(new ResultsSubjectsMasterMini()
                        {
                            FullID = subject.sbjct_ID,
                            Name = subject.sbjct_Name,
                            ID = subject.sbjct_ID.Length == 5 ? subject.sbjct_ID.Substring(3, 2) : subject.sbjct_ID
                        });
                    }

                    result.Exam = dbExamType == null ? "" : dbExamType.corseTyp_Name;
                    result.FullName = dbResult.inter2voc_CNAME.Trim();
                    result.HallTicketNumber = dbResult.inter2voc_ROLLNO.Trim();
                    result.Course = course.corse_Name != null ? course.corse_Name.Trim() : "";
                    string CourseCode = course.corse_ID != null ? course.corse_ID.Trim() : (dbResult.inter2voc_COURSE ?? "");

                    result.Year1Subject1 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1FPC1) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1FPC1).Name.Trim() : "";
                    result.Year1Subject2 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1FPC2) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1FPC2).Name.Trim() : "";
                    result.Year1Subject3 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1TPC1) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1TPC1).Name.Trim() : "";
                    result.Year1Subject4 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1TPC2) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1TPC2).Name.Trim() : "";
                    result.Year1Subject5 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1TPC3) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1TPC3).Name.Trim() : "";
                    result.Year1Subject6 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC1) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC1).Name.Trim() : "";
                    result.Year1Subject7 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC2) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC2).Name.Trim() : "";
                    result.Year1Subject8 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC3) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC3).Name.Trim() : "";
                    result.Year1Subject9 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC4) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR1PPC4).Name.Trim() : "";

                    result.Year2Subject1 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2FPC1) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2FPC1).Name.Trim() : "";
                    result.Year2Subject2 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2FPC2) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2FPC2).Name.Trim() : "";
                    result.Year2Subject3 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2TPC1) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2TPC1).Name.Trim() : "";
                    result.Year2Subject4 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2TPC2) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2TPC2).Name.Trim() : "";
                    result.Year2Subject5 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2TPC3) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2TPC3).Name.Trim() : "";
                    result.Year2Subject6 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC1) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC1).Name.Trim() : "";
                    result.Year2Subject7 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC2) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC2).Name.Trim() : "";
                    result.Year2Subject8 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC3) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC3).Name.Trim() : "";
                    result.Year2Subject9 = subjectsMini.Any(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC4) ? subjectsMini.FirstOrDefault(_ => _.FullID == CourseCode + dbResult.inter2voc_YR2PPC4).Name.Trim() : "";

                    result.Year1Subject1_Marks = dbResult.inter2voc_YR1FMKS1.Trim();
                    result.Year1Subject1_Indicator = dbResult.inter2voc_YR1FIND1.Trim();
                    result.Year1Subject1_Result = dbResult.inter2voc_YR1FRES1.Trim();

                    result.Year1Subject2_Marks = dbResult.inter2voc_YR1FMKS2.Trim();
                    result.Year1Subject2_Indicator = dbResult.inter2voc_YR1FIND2.Trim();
                    result.Year1Subject2_Result = dbResult.inter2voc_YR1FRES2.Trim();

                    result.Year1Subject3_Marks = dbResult.inter2voc_YR1TMKS1.Trim();
                    result.Year1Subject3_Indicator = dbResult.inter2voc_YR1TIND1.Trim();
                    result.Year1Subject3_Result = dbResult.inter2voc_YR1TRES1.Trim();

                    result.Year1Subject4_Marks = dbResult.inter2voc_YR1TMKS2.Trim();
                    result.Year1Subject4_Indicator = dbResult.inter2voc_YR1TIND2.Trim();
                    result.Year1Subject4_Result = dbResult.inter2voc_YR1TRES2.Trim();

                    result.Year1Subject5_Marks = dbResult.inter2voc_YR1TMKS3.Trim();
                    result.Year1Subject5_Indicator = dbResult.inter2voc_YR1TIND3.Trim();
                    result.Year1Subject5_Result = dbResult.inter2voc_YR1TRES3.Trim();

                    result.Year1Subject6_Marks = dbResult.inter2voc_YR1PMKS1.Trim();
                    result.Year1Subject6_Indicator = dbResult.inter2voc_YR1PIND1.Trim();
                    result.Year1Subject6_Result = dbResult.inter2voc_YR1PRES1.Trim();

                    result.Year1Subject7_Marks = dbResult.inter2voc_YR1PMKS2.Trim();
                    result.Year1Subject7_Indicator = dbResult.inter2voc_YR1PIND2.Trim();
                    result.Year1Subject7_Result = dbResult.inter2voc_YR1PRES2.Trim();

                    result.Year1Subject8_Marks = dbResult.inter2voc_YR1PMKS3.Trim();
                    result.Year1Subject8_Indicator = dbResult.inter2voc_YR1PIND3.Trim();
                    result.Year1Subject8_Result = dbResult.inter2voc_YR1PRES3.Trim();

                    result.Year1Subject9_Marks = dbResult.inter2voc_YR1PMKS4.Trim();
                    result.Year1Subject9_Indicator = dbResult.inter2voc_YR1PIND4.Trim();
                    result.Year1Subject9_Result = dbResult.inter2voc_YR1PRES4.Trim();

                    result.Year2Subject1_Marks = dbResult.inter2voc_YR2FMKS1.Trim();
                    result.Year2Subject1_Indicator = dbResult.inter2voc_YR2FIND1.Trim();
                    result.Year2Subject1_Result = dbResult.inter2voc_YR2FRES1.Trim();

                    result.Year2Subject2_Marks = dbResult.inter2voc_YR2FMKS2.Trim();
                    result.Year2Subject2_Indicator = dbResult.inter2voc_YR2FIND2.Trim();
                    result.Year2Subject2_Result = dbResult.inter2voc_YR2FRES2.Trim();

                    result.Year2Subject3_Marks = dbResult.inter2voc_YR2TMKS1.Trim();
                    result.Year2Subject3_Indicator = dbResult.inter2voc_YR2TIND1.Trim();
                    result.Year2Subject3_Result = dbResult.inter2voc_YR2TRES1.Trim();

                    result.Year2Subject4_Marks = dbResult.inter2voc_YR2TMKS2.Trim();
                    result.Year2Subject4_Indicator = dbResult.inter2voc_YR2TIND2.Trim();
                    result.Year2Subject4_Result = dbResult.inter2voc_YR2TRES2.Trim();

                    result.Year2Subject5_Marks = dbResult.inter2voc_YR2TMKS3.Trim();
                    result.Year2Subject5_Indicator = dbResult.inter2voc_YR2TIND3.Trim();
                    result.Year2Subject5_Result = dbResult.inter2voc_YR2TRES3.Trim();

                    result.Year2Subject6_Marks = dbResult.inter2voc_YR2PMKS1.Trim();
                    result.Year2Subject6_Indicator = dbResult.inter2voc_YR2PIND1.Trim();
                    result.Year2Subject6_Result = dbResult.inter2voc_YR2PRES1.Trim();

                    result.Year2Subject7_Marks = dbResult.inter2voc_YR2PMKS2.Trim();
                    result.Year2Subject7_Indicator = dbResult.inter2voc_YR2PIND2.Trim();
                    result.Year2Subject7_Result = dbResult.inter2voc_YR2PRES2.Trim();

                    result.Year2Subject8_Marks = dbResult.inter2voc_YR2PMKS3.Trim();
                    result.Year2Subject8_Indicator = dbResult.inter2voc_YR2PIND3.Trim();
                    result.Year2Subject8_Result = dbResult.inter2voc_YR2PRES3.Trim();

                    result.Year2Subject9_Marks = dbResult.inter2voc_YR2PMKS4.Trim();
                    result.Year2Subject9_Indicator = dbResult.inter2voc_YR2PIND4.Trim();
                    result.Year2Subject9_Result = dbResult.inter2voc_YR2PRES4.Trim();

                    result.Total = dbResult.inter2voc_TOTAL.Trim();
                    result.Result = dbResult.inter2voc_RESULT.Trim();

                    result.IsResultSet = true;
                }
            }

            return result;
        }

        public APSSCResults GetAPSSCResult(string HallTicketNumber, string ExamYear)
        {
            APSSCResults APResult = new APSSCResults();

            var fileLog = db.rslt_UploadedResults.Where(_ => _.upld_ExamType == (int)Enums_ExamTypes.Andhra_Pradesh_SSC && _.upld_ExamYear == ExamYear && _.upld_Status == Constants_FileUploadStatus.Done).OrderByDescending(_ => _.upld_Sno).FirstOrDefault();            

            if(fileLog != null && !string.IsNullOrEmpty(fileLog.upld_FileName))
            {
                string path = HttpContext.Current.Server.MapPath(ConfigResultsDoNotDeletePath);

                DataTable dt = _dbConnect.GetMSAccessData(path, fileLog.upld_FileName, ConfigTableSheetName, Constants_FileFields.SSC_HallTicket_AP, HallTicketNumber);

                if(dt != null && dt.Rows.Count == 1)
                {
                    APResult.IsResultSet = true;
                    APResult.District = dt.Rows[0]["DIST"].ToString();
                    APResult.SchoolCode = dt.Rows[0]["SC_CODE"].ToString();
                    APResult.RollNo = dt.Rows[0]["HTNO"].ToString();
                    APResult.FullName = dt.Rows[0]["CNAME"].ToString();
                    APResult.PHDescription = dt.Rows[0]["PH_DESC"].ToString();
                    APResult.Language1Flag = dt.Rows[0]["L1_FLG"].ToString();
                    APResult.Language1InternalGrade = dt.Rows[0]["L1_INTGRD"].ToString();
                    APResult.Language1Grade1 = dt.Rows[0]["L1_GRADE1"].ToString();
                    APResult.Language1FinalGrade = dt.Rows[0]["L1_GRADE"].ToString();
                    APResult.Language1Points = dt.Rows[0]["L1_POINTS"].ToString();
                    APResult.Language2Flag = dt.Rows[0]["L2_FLG"].ToString();
                    APResult.Language2InternalGrade = dt.Rows[0]["L2_INTGRD"].ToString();
                    APResult.Language2Grade1 = dt.Rows[0]["L2_GRADE1"].ToString();
                    APResult.Language2FinalGrade = dt.Rows[0]["L2_GRADE"].ToString();
                    APResult.Language2Points = dt.Rows[0]["L2_POINTS"].ToString();
                    APResult.Language3Flag = dt.Rows[0]["L3_FLG"].ToString();
                    APResult.Language3InternalGrade = dt.Rows[0]["L3_INTGRD"].ToString();
                    APResult.Language3Grade1 = dt.Rows[0]["L3_GRADE1"].ToString();
                    APResult.Language3FinalGrade = dt.Rows[0]["L3_GRADE"].ToString();
                    APResult.Language3Points = dt.Rows[0]["L3_POINTS"].ToString();
                    APResult.MathsFlag = dt.Rows[0]["MAT_FLG"].ToString();
                    APResult.MathsInternalGrade = dt.Rows[0]["MAT_INTGRD"].ToString();
                    APResult.MathsGrade1 = dt.Rows[0]["MAT_GRADE1"].ToString();
                    APResult.MathsFinalGrade = dt.Rows[0]["MAT_GRADE"].ToString();
                    APResult.MathsPoints = dt.Rows[0]["MAT_POINTS"].ToString();
                    APResult.ScienceFlag = dt.Rows[0]["SCI_FLG"].ToString();
                    APResult.ScienceInternalGrade = dt.Rows[0]["SCI_INTGRD"].ToString();
                    APResult.ScienceGrade1 = dt.Rows[0]["SCI_GRADE1"].ToString();
                    APResult.ScienceFinalGrade = dt.Rows[0]["SCI_GRADE"].ToString();
                    APResult.SciencePoints = dt.Rows[0]["SCI_POINTS"].ToString();
                    APResult.SocialFlag = dt.Rows[0]["SOC_FLG"].ToString();
                    APResult.SocialInternalGrade = dt.Rows[0]["SOC_INTGRD"].ToString();
                    APResult.SocialGrade1 = dt.Rows[0]["SOC_GRADE1"].ToString();
                    APResult.SocialFinalGrade = dt.Rows[0]["SOC_GRADE"].ToString();
                    APResult.SocialPoints = dt.Rows[0]["SOC_POINTS"].ToString();
                    APResult.GPA = dt.Rows[0]["GPA_9"].ToString();
                    APResult.Result = dt.Rows[0]["RESULTS"].ToString();
                    APResult.OptionalLanguageFlag = dt.Rows[0]["OL_FLG"].ToString();
                    APResult.OptionalLanguageInternalGrade = dt.Rows[0]["OL_INTGRD"].ToString();
                    APResult.OptionalLanguageGrade1 = dt.Rows[0]["OL_GRADE1"].ToString();
                    APResult.OptionalLanguageFinalGrade = dt.Rows[0]["OL_GRADE"].ToString();
                    APResult.OptionalLanguagePoints = dt.Rows[0]["OL_POINTS"].ToString();
                    APResult.ValueEducationLifeSkillsGrade = dt.Rows[0]["VAL_GRADE"].ToString();
                    APResult.ArtCulturalEducationGrade = dt.Rows[0]["ART_GRADE"].ToString();
                    APResult.WorkComputerEducationGrade = dt.Rows[0]["WRK_GRADE"].ToString();
                    APResult.PhysicalHealthEducationGrade = dt.Rows[0]["PHY_GRADE"].ToString();
                    APResult.DistrictName = dt.Rows[0]["DT_NAME"].ToString();

                    APResult.Exam = string.Format("{0} - {1}", GetResultsExamTypeById(Constants_ExamIds.AndhraPradeshSSC_APSSC).corseTyp_Name, ExamYear);
                }
            }

            return APResult;
        }

        public APEapcetEng GetAPEapcetEngResult(string HallTicketNumber, string ExamYear)
        {
            APEapcetEng APEapcetResult = new APEapcetEng();

            var fileLog = db.rslt_UploadedResults.Where(_ => _.upld_ExamType == (int)Enums_ExamTypes.Andhra_Pradesh_EAPCET_Engineering_11 && _.upld_ExamYear == ExamYear && _.upld_Status == Constants_FileUploadStatus.Done).OrderByDescending(_ => _.upld_Sno).FirstOrDefault();

            if (fileLog != null && !string.IsNullOrEmpty(fileLog.upld_FileName))
            {
                string path = HttpContext.Current.Server.MapPath(ConfigResultsDoNotDeletePath);

                DataTable dt = _dbConnect.GetMSAccessData(path, fileLog.upld_FileName, ConfigTableSheetName, "HTNO", HallTicketNumber);

                if (dt != null && dt.Rows.Count == 1)
                {
                    APEapcetResult.IsResultSet = true;
                    APEapcetResult.HallTicketNumber = dt.Columns.Contains("HTNO") ? dt.Rows[0]["HTNO"].ToString() : "";
                    APEapcetResult.FullName = dt.Columns.Contains("CNAME") ? dt.Rows[0]["CNAME"].ToString() : "";
                    APEapcetResult.FathersName = dt.Columns.Contains("FATHERs NAME") ? dt.Rows[0]["FATHERs NAME"].ToString() : "";
                    APEapcetResult.MothersName = dt.Columns.Contains("MOTHERs NAME") ? dt.Rows[0]["MOTHERs NAME"].ToString() : "";
                    APEapcetResult.Mathematics = dt.Columns.Contains("MATHEMATICS") ? dt.Rows[0]["MATHEMATICS"].ToString() : "";
                    APEapcetResult.Physics = dt.Columns.Contains("PHYSICS") ? dt.Rows[0]["PHYSICS"].ToString() : "";
                    APEapcetResult.Chemistry = dt.Columns.Contains("CHEMISTRY") ? dt.Rows[0]["CHEMISTRY"].ToString() : "";
                    APEapcetResult.Total = dt.Columns.Contains("TOTAL") ? dt.Rows[0]["TOTAL"].ToString() : "";
                    APEapcetResult.Rank = dt.Columns.Contains("RANK") ? dt.Rows[0]["RANK"].ToString() : "";
                    APEapcetResult.Status = dt.Columns.Contains("STATUS") ? dt.Rows[0]["STATUS"].ToString() : "";

                    APEapcetResult.Exam = string.Format("{0} - {1}", GetResultsExamTypeById(Constants_ExamIds.AndhraPradeshEAPCETEngineering_APEAPENG).corseTyp_Name, ExamYear);
                }
            }

            return APEapcetResult;
        }

        public APEapcetAM GetAPEapcetAMResult(string HallTicketNumber, string ExamYear)
        {
            APEapcetAM APEapcetResult = new APEapcetAM();

            var fileLog = db.rslt_UploadedResults.Where(_ => _.upld_ExamType == (int)Enums_ExamTypes.Andhra_Pradesh_EAPCET_AgricultureMedical_12 && _.upld_ExamYear == ExamYear && _.upld_Status == Constants_FileUploadStatus.Done).OrderByDescending(_ => _.upld_Sno).FirstOrDefault();

            if (fileLog != null && !string.IsNullOrEmpty(fileLog.upld_FileName))
            {
                string path = HttpContext.Current.Server.MapPath(ConfigResultsDoNotDeletePath);

                DataTable dt = _dbConnect.GetMSAccessData(path, fileLog.upld_FileName, ConfigTableSheetName, "HTNO", HallTicketNumber);

                if (dt != null && dt.Rows.Count == 1)
                {
                    APEapcetResult.IsResultSet = true;
                    APEapcetResult.HallTicketNumber = dt.Columns.Contains("HTNO") ? dt.Rows[0]["HTNO"].ToString() : "";
                    APEapcetResult.FullName = dt.Columns.Contains("CNAME") ? dt.Rows[0]["CNAME"].ToString() : "";
                    APEapcetResult.FathersName = dt.Columns.Contains("FATHERs NAME") ? dt.Rows[0]["FATHERs NAME"].ToString() : "";
                    APEapcetResult.MothersName = dt.Columns.Contains("MOTHERs NAME") ? dt.Rows[0]["MOTHERs NAME"].ToString() : "";
                    APEapcetResult.Botany = dt.Columns.Contains("BOTANY") ? dt.Rows[0]["BOTANY"].ToString() : "";
                    APEapcetResult.Zoology = dt.Columns.Contains("ZOOLOGY") ? dt.Rows[0]["ZOOLOGY"].ToString() : "";
                    APEapcetResult.Physics = dt.Columns.Contains("PHYSICS") ? dt.Rows[0]["PHYSICS"].ToString() : "";
                    APEapcetResult.Chemistry = dt.Columns.Contains("CHEMISTRY") ? dt.Rows[0]["CHEMISTRY"].ToString() : "";
                    APEapcetResult.Total = dt.Columns.Contains("TOTAL") ? dt.Rows[0]["TOTAL"].ToString() : "";
                    APEapcetResult.Rank = dt.Columns.Contains("RANK") ? dt.Rows[0]["RANK"].ToString() : "";
                    APEapcetResult.Status = dt.Columns.Contains("STATUS") ? dt.Rows[0]["STATUS"].ToString() : "";

                    APEapcetResult.Exam = string.Format("{0} - {1}", GetResultsExamTypeById(Constants_ExamIds.AndhraPradeshEAPCETAgricultureMedical_APEAPAM).corseTyp_Name, ExamYear);
                }
            }

            return APEapcetResult;
        }


        #endregion


        #region ------------------------------------------------------------------------------------------------------------ NAME SEARCH

        public List<ExamResultsNameSearchResult> GetNameSearchResults(string SearchField, string SearchValue, int ExamType, string ExamYear, string NameFieldLabel, string HallTicketFieldLabel, string ExamId)
        {
            var results = new List<ExamResultsNameSearchResult>();

            var fileLog = GetLatestExamTypeLog(ExamType, ExamYear);

            if (fileLog != null && !string.IsNullOrEmpty(fileLog.upld_FileName))
            {
                string path = HttpContext.Current.Server.MapPath(Constants_ConfigVars.ConfigResultsDoNotDeletePath);

                DataTable dt = _dbConnect.GetMSAccessData(path, fileLog.upld_FileName, Constants_ConfigVars.ConfigTableSheetName, SearchField, SearchValue, true);

                if (dt != null && dt.Rows.Count > 0)
                {
                    results =
                        (from res in dt.AsEnumerable()
                         select new ExamResultsNameSearchResult
                         {
                             ExamId = ExamId,
                             FullName = res.Field<string>(NameFieldLabel),
                             HallTicket = res.Field<string>(HallTicketFieldLabel),
                             Year = ExamYear
                         }).ToList();
                }
            }

            return results;
        }


        #endregion

        private void FillEmptyValues(object __object)
        {

        }
    }
}