using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pet.Api.Contracts.Models.Project;
using Pet.Api.Contracts.Models.Task;
using Pet.Common.Repositories.Interfaces;
using Pet.Common.Storage;
using System;

namespace Pet.Api.Controllers
{
    public class TaskController : ApiBaseController
    {
        private readonly ITaskRepository _repository;
        private readonly ILogger<TaskController> _logger;
        private readonly IMapper _mapper;


        public TaskController(ITaskRepository repository, ILogger<TaskController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get task by id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet("{taskId}")]
        [ProducesResponseType(typeof(TaskModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid taskId)
        {
            try
            {
                var task = await _repository.FindByIdAsync(taskId);
                var response = _mapper.Map<TaskModel>(task);

                return Ok(response);
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        /// <summary>
        /// Get task with subtasks by id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet("{taskId/childs}")]
        [ProducesResponseType(typeof(TaskModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecursionTasks(Guid taskId)
        {
            try
            {
                var task = await _repository.FindByIdAsync(taskId);
                var subTasks = await _repository.FindAllByWhereOrderedDescendingAsync(x => x.ParentTaskId == taskId, x => x.DueDate);
                var taskList = _mapper.Map<List<TaskModel>>(subTasks); 
                var response = _mapper.Map<TaskModel>(task);
                taskList.Add(response);
                return Ok(taskList);
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        /// <summary>
        /// Create a task
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PostTaskModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(PostTaskModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTask = _mapper.Map<Goal>(request);
                    var task = await _repository.InsertAsync(newTask);
                    return Ok(new PostTaskModel { TaskId = task.Id });
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        /// <summary>
        /// Edit task
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(PutTaskModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit(PutTaskModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var editTaskModel = _mapper.Map<Goal>(request);
                    var taskId = await _repository.UpdateAsync(editTaskModel);
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
        /// Delete specified task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpDelete("{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid taskId)
        {
            try
            {
                var task = await _repository.FindByIdAsync(taskId);
                await _repository.Delete(task);
                return Ok();
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

    }
}
