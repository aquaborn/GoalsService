using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pet.Api.Contracts.Models.Project;
using Pet.Common.Repositories.Interfaces;
using Pet.Common.Storage;
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
        /// Get task by id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}")]
        [ProducesResponseType(typeof(ProjectModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid projectId)
        {
            try
            {
                var project = await _repository.FindByIdAsync(projectId);
                var response = _mapper.Map<ProjectModel>(project);

                return Ok(response);
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        /// <summary>
        /// Create a project
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PostProjectModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(PostProjectModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newPerson = _mapper.Map<Project>(request);
                    var project = await _repository.InsertAsync(newPerson);
                    return Ok(new PostProjectModel { Id = project.Id });
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        /// <summary>
        /// Edit person
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(PutProjectModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit(PutProjectModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var editCostModel = _mapper.Map<Project>(request);
                    var costId = await _repository.UpdateAsync(editCostModel);
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        /// <summary>
        /// Delete specified project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete("{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid projectId)
        {
            try
            {
                var project = await _repository.FindByIdAsync(projectId);
                await _repository.Delete(project);
                return Ok();
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

    }
}
