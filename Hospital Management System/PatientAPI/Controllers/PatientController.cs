using PatientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace PatientAPI.Controllers
{
    public class PatientController : ApiController
    {
        // GET: api/Patient
        PatientDBHandle patientDB = new PatientDBHandle();
        public IHttpActionResult Get(int start, int length,string search)
        {
            try
            {
                List<Patient> patients = patientDB.GetPatients(start, length, search);
                if(patients.Count!=0)
                    return Ok(patients);

                return NotFound();
            }catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Patient/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                Patient patient = patientDB.GetPatient(id);
                if (patient != null)
                    return Ok(patient);

                return NotFound();

            }catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Patient

        public IHttpActionResult Post([FromBody]Patient patient)
        {
            try
            {
                if (patientDB.AddPatient(patient))
                    return Created("","Success");
                return Conflict();
            }catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Patient/5
        public IHttpActionResult Put(int id,[FromBody]Patient patient)
        {
            try
            {
                if (patientDB.GetPatient(id) != null)
                {
                    patient.PatientId = id;
                    patientDB.UpdateDetails(patient);
                    return Ok("Success");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Patient/5
        public IHttpActionResult Delete(int id)
        {
            try
            {

                if (patientDB.GetPatient(id) != null)
                {
                    patientDB.DeletePatient(id);
                    return Ok("Success");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
