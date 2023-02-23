using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetCommon.Repositories.Interfaces;
using System;

namespace Pet.Api.Controllers
{
    public class ProjectController : ApiBaseController
    {
        private readonly IProjectRepository _repository;
        private readonly ILogger<ProjectController> _logger;
        private readonly IMapper _mapper;


        public ProjectController(IProjectRepository repository, ILogger<ProjectController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get project by id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}")]
        public async Task<IActionResult> Get(Guid projectId)
        {
            try
            {
                var project = await _repository.FindByIdAsync(projectId);
                //var response = mapper.Map<GetPersonResponse>(person);

                return Ok();
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        ///// <summary>
        ///// Create a project
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        ////[ProducesResponseType(typeof(CreatePersonResponse), StatusCodes.Status200OK)]
        ////pe(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Create(CreatePersonRequest request)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var newPerson = mapper.Map<Person>(request);
        //            var personId = await _repository(newPerson);
        //            return Ok(new CreatePersonResponse { Id = personId });
        //        }

        //        return BadRequest();
        //    }
        //    catch (Exception e)
        //    {
        //        return ExceptionResult(e);
        //    }
        //}

        ///// <summary>
        ///// Edit person
        ///// </summary>
        ///// <returns></returns>
        //[HttpPut]
        //[ProducesResponseType(typeof(EditPersonResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Edit(EditPersonRequest request)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var editCostModel = mapper.Map<EditPersonModel>(request);
        //            var costId = await personService.EditAsync(editCostModel);
        //            return Ok(new EditPersonResponse { Id = costId });
        //        }

        //        return BadRequest();
        //    }
        //    catch (Exception e)
        //    {
        //        return ExceptionResult(e);
        //    }
        //}

        ///// <summary>
        ///// Delete specified person
        ///// </summary>
        ///// <param name="personId"></param>
        ///// <returns></returns>
        //[HttpDelete("{personId}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> Delete(Guid personId)
        //{
        //    try
        //    {
        //        await personService.DeleteAsync(personId);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return ExceptionResult(e);
        //    }
        //}

    }
}
