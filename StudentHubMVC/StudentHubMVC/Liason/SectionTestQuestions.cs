using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StudentHubMVC.Liason
{
    public class SectionTestQuestions
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();


        // ---------------------------------------------------------------------------- PROCESS DATA
        public string ProcessData_TestQuestion(int course, int sectionid, int testid, FormCollection collection)
        {
            string Mode = "";
            string QId = "";

            //if (Mode == "ADD")
            if (collection["Sno"] == "")
                Mode = "AddTestQuestion";

            //else if (Mode == "UPDATE")
            else
            {
                Mode = "UpdateTestQuestion";
                QId = collection["Sno"];
            }

            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            Liason.EntityLayer.SectionTestQuestionEN ChapterTestEN = new EntityLayer.SectionTestQuestionEN();
            ChapterTestEN = FormDataToEntity(QId, collection);

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@TestId", testid.ToString()));
            parameters.Add(new KeyValuePair<string, string>("@Question", ChapterTestEN.Question));
            parameters.Add(new KeyValuePair<string, string>("@OptionA", ChapterTestEN.OptionA));
            parameters.Add(new KeyValuePair<string, string>("@OptionB", ChapterTestEN.OptionB));
            parameters.Add(new KeyValuePair<string, string>("@OptionC", ChapterTestEN.OptionC));
            parameters.Add(new KeyValuePair<string, string>("@OptionD", ChapterTestEN.OptionD));
            parameters.Add(new KeyValuePair<string, string>("@Answer", ChapterTestEN.Answer));

            parameters.Add(new KeyValuePair<string, string>("@User", ChapterTestEN.UserId));
            parameters.Add(new KeyValuePair<string, string>("@Sno", ChapterTestEN.Sno));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", Mode));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("SectionTestQuestionsData", parameters, outparameters);

            return Result[0].Value;
        }


        private Liason.EntityLayer.SectionTestQuestionEN FormDataToEntity(string ID, FormCollection collection)
        {
            Liason.EntityLayer.SectionTestQuestionEN ChapterTestEN = new Liason.EntityLayer.SectionTestQuestionEN();

            //ChapterTestEN.ChapterId = collection["ChapterId"];
            ChapterTestEN.Question = collection["Question"];
            ChapterTestEN.OptionA = collection["OptionA"];
            ChapterTestEN.OptionB = collection["OptionB"];
            ChapterTestEN.OptionC = collection["OptionC"];
            ChapterTestEN.OptionD = collection["OptionD"];
            ChapterTestEN.Answer = collection["Answer"];

            ChapterTestEN.UserId = "1";
            ChapterTestEN.Sno = ID;

            return ChapterTestEN;
        }


        // ---------------------------------------------------------------------------- EDIT TEST QUESTION
        public Liason.EntityLayer.SectionTestQuestionEN EditTestQuestion(string CourseID)
        {
            Liason.EntityLayer.SectionTestQuestionEN obj_ChapterTestEN = new EntityLayer.SectionTestQuestionEN();

            DataTable dt_EditTestQuestion = new DataTable();

            dt_EditTestQuestion = obj_dbconnect.procedure_exec_2_data_adapter("GetChaptersData", "OperateMode", "EDITCHAPTER", "KeyValue", CourseID);

            if (dt_EditTestQuestion.Rows.Count > 0)
            {
                //obj_ChapterTestEN.ChapterId = dt_EditTestQuestion.Rows[0]["ChapterId"].ToString();
                obj_ChapterTestEN.Question = dt_EditTestQuestion.Rows[0]["Question"].ToString();
                obj_ChapterTestEN.OptionA = dt_EditTestQuestion.Rows[0]["OptionA"].ToString();
                obj_ChapterTestEN.OptionB = dt_EditTestQuestion.Rows[0]["OptionB"].ToString();
                obj_ChapterTestEN.OptionC = dt_EditTestQuestion.Rows[0]["OptionC"].ToString();
                obj_ChapterTestEN.OptionD = dt_EditTestQuestion.Rows[0]["OptionD"].ToString();
                obj_ChapterTestEN.Answer = dt_EditTestQuestion.Rows[0]["Answer"].ToString();
            }

            return obj_ChapterTestEN;
        }


        // ---------------------------------------------------------------------------- GET ALL QUESTIONS
        public DataSet GetAllQuestions(string TestId)
        {
            DataSet dt_AllQuestions = new DataSet();

            dt_AllQuestions = obj_dbconnect.procedure_exec_2_data_adapter_dataset("GetTestQuestions", "OperateMode", "ALLQUESTIONS", "KeyValue", TestId);

            return dt_AllQuestions;
        }


        // ---------------------------------------------------------------------------- DELETE QUESTIOIN
        public string DeleteQuestion(string QuestionId)
        {
            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@TestId", ""));
            parameters.Add(new KeyValuePair<string, string>("@Question", ""));
            parameters.Add(new KeyValuePair<string, string>("@OptionA", ""));
            parameters.Add(new KeyValuePair<string, string>("@OptionB", ""));
            parameters.Add(new KeyValuePair<string, string>("@OptionC", ""));
            parameters.Add(new KeyValuePair<string, string>("@OptionD", ""));
            parameters.Add(new KeyValuePair<string, string>("@Answer", ""));

            parameters.Add(new KeyValuePair<string, string>("@User", ""));
            parameters.Add(new KeyValuePair<string, string>("@Sno", QuestionId));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", "DeleteTestQuestion"));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("SectionTestQuestionsData", parameters, outparameters);

            return Result[0].Value;
        }
    }
}