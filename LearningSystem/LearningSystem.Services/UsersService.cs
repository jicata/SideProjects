using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LearningSystem.Data;
using LearningSystem.Models.BindingModels.Users;
using LearningSystem.Models.EntityModels;
using LearningSystem.Models.ViewModels.Users;
using LearningSystem.Services.Interfaces;

namespace LearningSystem.Services
{
    public class UsersService : Service, IUsersService
    {
        public UsersService(ILearningSystemContext context) : base(context)
        {
        }

        public Student GetCurrentStudent(string userName)
        {
            var user = this.Context.Users.FirstOrDefault(applicationUser => applicationUser.UserName == userName);
            Student student = this.Context.Students.FirstOrDefault(student1 => student1.User.Id == user.Id);
            return student;
        }

        public void EnrollStudentInCourse(int courseId, Student student)
        {
            Course wantedCourse = this.Context.Courses.Find(courseId);
            student.Courses.Add(wantedCourse);
            this.Context.SaveChanges();
        }

        public ProfileVm GetProfileVm(string userName)
        {
            ApplicationUser currentUser = this.Context.Users.FirstOrDefault(user => user.UserName == userName);
            ProfileVm vm = Mapper.Map<ApplicationUser, ProfileVm>(currentUser);
            Student currentStudent = this.Context.Students.FirstOrDefault(student => student.User.Id == currentUser.Id);
            vm.EnrolledCourses = Mapper.Map<IEnumerable<Course>, IEnumerable<UserCourseVm>>(currentStudent.Courses);
            return vm;
        }

        public EditUserVm GetEditVm(string userName)
        {
            ApplicationUser user =
                this.Context.Users.FirstOrDefault(applicationUser => applicationUser.UserName == userName);
            EditUserVm vm = Mapper.Map<ApplicationUser, EditUserVm>(user);
            return vm;
        }

        public void EditUser(EditUserBm bind, string currentUsername)
        {
            ApplicationUser user =
        this.Context.Users.FirstOrDefault(applicationUser => applicationUser.UserName == currentUsername);
            user.Name = bind.Name;
            user.Email = bind.Email;
            this.Context.SaveChanges();
        }
    }
}
