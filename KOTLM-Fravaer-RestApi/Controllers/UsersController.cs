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
    public class UsersController : ApiController
    {
        private IRepository<User, int> _userRepository = new DLLFacade().GetUserRepository();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return new EnumerableQuery<User>(_userRepository.ReadAll());
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = _userRepository.Read(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            _userRepository.Update(user);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userRepository.Create(user);

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            var user =_userRepository.Delete(id);

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _userRepository.Read(id) != null;
        }
    }
}