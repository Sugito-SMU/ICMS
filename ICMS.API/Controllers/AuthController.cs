using AutoMapper;
using ICMS.Entity.Entities;
using ICMS.Entity.ViewModels;
using ICMS.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ICMS.API.Controllers
{
    [CustomExceptionFilter]
    [AuthenticateAPI]
    [SanitizeAPI]
    public class AuthController : ApiController
    {
        private readonly IStudentManager studentManager;
        private readonly IAdminManager adminManager;

        public AuthController(IStudentManager studentManager, IAdminManager adminManager)
        {
            this.studentManager = studentManager;
            this.adminManager = adminManager;
        }

        [HttpGet]
        [HttpOptions]
        public IHttpActionResult Authenticate(string userName)
        {
            Student student;
            StudentViewModel userViewModel;

            student = studentManager.GetStudentByUsername(userName);

            if(student != null)
            {
                userViewModel = Mapper.Map<Student, StudentViewModel>(student);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, userViewModel));
            }
            else
            {
                Admin admin;

                admin = adminManager.GetAdminByUsername(userName);
                userViewModel = Mapper.Map<Admin, StudentViewModel>(admin);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, userViewModel));
            }
        }
    }
}
