using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using KOTLM_Fravaer_DLL.Context;
using KOTLM_Fravaer_DLL.Entities;
using KOTLM_Fravaer_DLL.Facade;
using KOTLM_Fravaer_DLL.Interfaces;

namespace KOTLM_Fravaer_RestApi.Controllers
{
    public class DepartmentsController : ApiController
    {
        private IRepository<Department, int> _departmentRepository = new DLLFacade().GetDepartmentRepository();

        // GET: api/Departments
        [Authorize]
        public IQueryable<Department> GetDepartments()
        {
            return new EnumerableQuery<Department>(_departmentRepository.ReadAll());
        }

        // GET: api/Departments/5
        [Authorize]
        [ResponseType(typeof(Department))]
        public IHttpActionResult GetDepartment(int id)
        {
            Department department = _departmentRepository.Read(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // PUT: api/Departments/5
        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartment(int id, Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != department.Id)
            {
                return BadRequest();
            }

            _departmentRepository.Update(department);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Departments
        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(Department))]
        public IHttpActionResult PostDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _departmentRepository.Create(department);

            return CreatedAtRoute("DefaultApi", new { id = department.Id }, department);
        }

        // DELETE: api/Departments/5
        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(Department))]
        public IHttpActionResult DeleteDepartment(int id)
        {
            if (!DepartmentExists(id))
            {
                return NotFound();
            }

            _departmentRepository.Delete(id);

            return Ok();
        }

        private bool DepartmentExists(int id)
        {
            return _departmentRepository.Read(id) != null;
        }
    }
}