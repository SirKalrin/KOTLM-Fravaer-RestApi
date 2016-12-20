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
using KOTLM_Fravaer_DLL.Entities;
using KOTLM_Fravaer_DLL.Facade;
using KOTLM_Fravaer_DLL.Interfaces;
using KOTLM_Fravaer_DLL.Models;

namespace KOTLM_Fravaer_RestApi.Controllers
{
    public class AbsencesController : ApiController
    {
        private IAbsenceRepository _absenceRepository = new DLLFacade().GetAbsenceRepository(new ApplicationDbContext());

        // GET: api/Absences
        [Authorize]
        public IQueryable<Absence> GetAbsences()
        {
            return new EnumerableQuery<Absence>(_absenceRepository.ReadAll());
        }
        // GET: api/Absences?startDate=12-13-1970&endDate=12-13-2019
        public IQueryable<Absence> GetIntervalAbsence(DateTime startDate, DateTime endDate)
        {
            return new EnumerableQuery<Absence>(_absenceRepository.ReadInterval(startDate, endDate));
        }

        // GET: api/Absences/5
        [Authorize]
        [ResponseType(typeof(Absence))]
        public IHttpActionResult GetAbsence(int id)
        {
            Absence absence = _absenceRepository.Read(id);
            if (absence == null)
            {
                return NotFound();
            }

            return Ok(absence);
        }

        // PUT: api/Absences/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAbsence(int id, Absence absence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != absence.Id)
            {
                return BadRequest();
            }

            _absenceRepository.Update(absence);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Absences
        [Authorize]
        [ResponseType(typeof(Absence))]
        public IHttpActionResult PostAbsence(Absence absence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _absenceRepository.Create(absence);

            return CreatedAtRoute("DefaultApi", new { id = absence.Id }, absence);
        }

        // DELETE: api/Absences/5
        [Authorize]
        [ResponseType(typeof(Absence))]
        public IHttpActionResult DeleteAbsence(int id)
        {
            if (!AbsenceExists(id))
            {
                return NotFound();
            }

            _absenceRepository.Delete(id);

            return Ok();
        }

        /*
         * Checks if an absence with the given Id exists in the database
         */
        private bool AbsenceExists(int id)
        {
            return _absenceRepository.Read(id) != null;
        }

        
    }
}