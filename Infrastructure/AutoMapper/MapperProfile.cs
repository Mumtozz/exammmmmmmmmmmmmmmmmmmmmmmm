using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.CourseDto;
using Domain.DTOs.FeedbackDto;
using Domain.DTOs.MaterialDto;
using Domain.DTOs.StudentDto;
using Domain.DTOs.SubmissionDto;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Student, AddStudentDto>().ReverseMap();
        CreateMap<Student, GetStudentDto>().ReverseMap();
        CreateMap<Student, UpdateStudentDto>().ReverseMap();
        CreateMap<Course, AddCourseDto>().ReverseMap();
        CreateMap<Course, GetCoursesDto>().ReverseMap();
        CreateMap<Course, UpdateCourseDto>().ReverseMap();
        CreateMap<Assignment, AddAssigmentDto>().ReverseMap();
        CreateMap<Assignment, GetAssigmentDto>().ReverseMap();
        CreateMap<Assignment, UpdateAssigmentDto>().ReverseMap();
        CreateMap<Feedback, AddFeedbackDto>().ReverseMap();
        CreateMap<Feedback, GetFeedbackDto>().ReverseMap();
        CreateMap<Feedback, UpdateFeedbackDto>().ReverseMap();
        CreateMap<Material, AddMaterialDto>().ReverseMap();
        CreateMap<Material, GetMaterialDto>().ReverseMap();
        CreateMap<Material, UpdateMaterialDto>().ReverseMap();
        CreateMap<Submission, AddSubmissionDto>().ReverseMap();
        CreateMap<Submission, GetSubmissionDto>().ReverseMap();
        CreateMap<Submission, UpdateSubmissionDto>().ReverseMap();

        
        // CreateMap<AddTimeTableDto,TimeTable>()
        // .ForMember(sDto => sDto.FromTime,opt => opt.MapFrom(s => TimeSpan.Parse(s.FromTime)))
        // .ForMember(x => x.ToTime,opt => opt.MapFrom(x => TimeSpan.Parse(x.ToTime)));




        // //ForMembers
        // CreateMap< Student,GetStudentDto>()
        //     .ForMember(sDto => sDto.FulName, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}"))
        //     .ForMember(sDto => sDto.EmailAddress, opt => opt.MapFrom(s =>s.Email));
        //
        // //Reverse map
        // CreateMap<BaseStudentDto,Student>().ReverseMap();
        //
        // // ignore
        // CreateMap<Student, AddStudentDto>()
        //     .ForMember(dest => dest.FirstName, opt => opt.Ignore());


    }
}