using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using StudentHubMVC.Models.ViewModels;

namespace StudentHubMVC
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<Mapping>();
            });

            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
    public class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<DropdownTypes, his_shub_dropdowntypes>()
                .ForMember(dest => dest.type_his_Id, opt => opt.Ignore())
                .ForMember(dest => dest.type_his_TransactionType, opt => opt.Ignore());
            //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name));

            CreateMap<MockTestQuestions, his_shub_mocktestquestions>()
                .ForMember(dest => dest.ques_his_Id, opt => opt.Ignore())
                .ForMember(dest => dest.ques_his_TransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.ques_his_CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ques_CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ques_UpdatedDate, opt => opt.Ignore());

            CreateMap<MockTests, his_shub_mocktests>()
                .ForMember(dest => dest.mocktest_his_Id, opt => opt.Ignore())
                .ForMember(dest => dest.mocktest_his_TransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.mocktest_his_CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

            CreateMap<Colleges, his_shub_colleges>()
                .ForMember(dest => dest.coll_his_Id, opt => opt.Ignore())
                .ForMember(dest => dest.coll_his_TransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.coll_his_CreatedDate, opt => opt.Ignore());
            //.ForMember(dest => dest.cre, opt => opt.Ignore());

            CreateMap<CollegeImages, his_shub_collegeimages>()
                .ForMember(dest => dest.coll_his_Id, opt => opt.Ignore())
                .ForMember(dest => dest.coll_his_TransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.coll_his_CreatedDate, opt => opt.Ignore());

            CreateMap<CollegeCoursesRelation, his_shub_collegecoursesrelation>()
                .ForMember(dest => dest.coll_his_Id, opt => opt.Ignore())
                .ForMember(dest => dest.coll_his_TransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.coll_his_CreatedDate, opt => opt.Ignore());

            CreateMap<CollegeFacilities, his_shub_collegefacilities>()
                .ForMember(dest => dest.coll_his_Id, opt => opt.Ignore())
                .ForMember(dest => dest.coll_his_TransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.coll_his_CreatedDate, opt => opt.Ignore());

            CreateMap<CollegeCourses, his_shub_collegecourse>()
                .ForMember(dest => dest.coll_his_Id, opt => opt.Ignore())
                .ForMember(dest => dest.coll_his_TransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.coll_his_CreatedDate, opt => opt.Ignore());

            CreateMap<Institutions, his_shub_institutions>()
                .ForMember(dest => dest.ins_his_Id, opt => opt.Ignore())
                .ForMember(dest => dest.ins_his_TransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.ins_his_CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ins_CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ins_UpdatedDate, opt => opt.Ignore());

            CreateMap<InstitutionCourses, his_shub_institutioncourses>()
                .ForMember(dest => dest.inscrs_his_Id, opt => opt.Ignore())
                .ForMember(dest => dest.inscrs_his_TransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.inscrs_his_CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.inscrs_CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.inscrs_UpdatedDate, opt => opt.Ignore());

            CreateMap<InstitutionCourseTrainingModes, his_shub_institutioncoursestrainingmodes>()
                .ForMember(dest => dest.trnmd_his_Id, opt => opt.Ignore())
                .ForMember(dest => dest.trnmd_his_TransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.trnmd_his_CreatedDate, opt => opt.Ignore());

            CreateMap<Addresses, his_shub_addresses>()
                .ForMember(dest => dest.addrs_his_Id, opt => opt.Ignore())
                .ForMember(dest => dest.addrs_his_TransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.addrs_his_CreatedDate, opt => opt.Ignore());

            CreateMap<MainCourses, his_shub_maincourses>()
                .ForMember(dest => dest.course_his_Id, opt => opt.Ignore())
                .ForMember(dest => dest.course_his_TransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.course_his_CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.course_CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.course_UpdatedDate, opt => opt.Ignore());

            //CreateMap<rslt_IntermediateYear2Regular, TSYr2RegResult>()
            //    .ForMember(dest => dest.IsResultSet, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr1Subject1, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr1Subject2, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr1Subject3, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr1Subject4, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr1Subject5, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr1Subject6, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject1, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject2, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject3, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject4, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject5, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject6, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject7, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject8, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject9, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject10, opt => opt.Ignore());

            //CreateMap<rslt_IntermediateYear2Voc, TSYr2VocResult>()
            //    .ForMember(dest => dest.isResultSet, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr1Subject1_Yr1FSubject1, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr1Subject2_Yr1FSubject2, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr1Subject3_Yr1TSubject1, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr1Subject4_Yr1TSubject2, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr1Subject5_Yr1TSubject3, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr1Subject6_Yr1PSubject1, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr1Subject7_Yr1PSubject2, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr1Subject8_Yr1PSubject3, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr1Subject9_Yr1PSubject4, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject1_Yr2FSubject1, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject2_Yr2FSubject2, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject3_Yr2TSubject1, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject4_Yr2TSubject2, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject5_Yr2TSubject3, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject6_Yr2PSubject1, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject7_Yr2PSubject2, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject8_Yr2PSubject3, opt => opt.Ignore())
            //    .ForMember(dest => dest.Yr2Subject9_Yr2PSubject4, opt => opt.Ignore());
        }
    }

    //public static IMappingExpression<TSource, TDestination> IgnoreAllNonMapped<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
    //{
    //    foreach (var property in Mapper.FindTypeMapFor<TSource, TDestination>().GetUnmappedPropertyNames())
    //    {
    //        expression.ForMember(property, opt => opt.Ignore());
    //    }

    //    return expression;
    //}

}