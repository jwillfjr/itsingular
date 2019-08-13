using ITSingular.Domain.Entities;
using ITSingular.WebAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ITSingular.WebAPI.Controllers
{
    [RoutePrefix("api/communication")]
    public class CommunicationController : ApiController
    {
        ICommunicationRepository communicationRepository;

        public CommunicationController(ICommunicationRepository communicationRepository)
        {
            this.communicationRepository = communicationRepository;
        }

        [HttpPost]
        [Route("setmachinestate")]
        public IHttpActionResult SetMachineState([FromBody]string machineName)
        {
            try
            {
                return Ok(communicationRepository.SetMachineState(machineName));
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        [Route("getcountmachineoffline")]
        public IHttpActionResult GetCountMachineOffline()
        {
            try
            {
                return Ok(communicationRepository.GetCountMachineOffline());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getcountmachineonline")]
        public IHttpActionResult GetCountMachineOnline()
        {
            try
            {
                return Ok(communicationRepository.GetCountMachineOnline());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getcountmachinealert")]
        public IHttpActionResult GetCountMachineAlert()
        {
            try
            {
                return Ok(communicationRepository.GetCountMachineAlert());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}