using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Pet.Api.Contracts.Models;
using Pet.Api.Contracts.Models.Comment;
using Pet.Api.Contracts.Models.Project;
using Pet.Api.Contracts.Models.Task;
using Pet.Common.Repositories.Interfaces;
using Pet.Common.Storage;
using System;

namespace Pet.Api.Controllers
{
    public class CommentController : ApiBaseController
    {
        private readonly ICommentRepository _repository;
        private readonly ITaskRepository _taskrepository;
        private readonly IAttachmentRepository _attachmentrepository;
        private readonly ILogger<CommentController> _logger;
        private readonly IMapper _mapper;


        public CommentController(ICommentRepository repository, ITaskRepository taskrepository,
            IAttachmentRepository attachmentrepository, ILogger<CommentController> logger, IMapper mapper)
        {
            _repository = repository;
            _taskrepository = taskrepository;
            _attachmentrepository = attachmentrepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get comment by id
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [HttpGet("{commentId}")]
        [ProducesResponseType(typeof(CommentModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid commentId)
        {
            try
            {
                var comment = await _repository.FindByIdAsync(commentId);
                var response = _mapper.Map<CommentModel>(comment);

                return Ok(response);
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }
        /// <summary>
        /// Get comments by  task id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet("{comments/taskId}")]
        [ProducesResponseType(typeof(CommentModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCommentsByTaskId(Guid taskId)
        {
            try
            {
                var task = await _taskrepository.FindByIdAsync(taskId);
                if(task == null) { return BadRequest();}
                var comments = await _repository.FindAllByWhereOrderedDescendingAsync(x=>x.TaskId == taskId, x=>x.CreatedOn);
                var response = _mapper.Map<List<CommentModel>>(comments);

                return Ok(response);
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        /// <summary>
        /// Add comment to task
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PostTaskModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCommentToTask(PostCommentModel model)
        {
            try
            {
                var task = await _repository.FindByIdAsync(model.TaskId);
                if (ModelState.IsValid && task != null)
                {
                    var newComment = _mapper.Map<Comment>(model);
                    var comment = await _repository.InsertAsync(newComment);
                    return Ok(new PostCommentModel { Id = model.Id });
                    
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        /// <summary>
        /// Add Attachment to comment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(AttachmentModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(AttachmentModel model)
        {
            try
            {
                var comment = await _repository.FindByIdAsync(model.CommentId);
                if (ModelState.IsValid && comment != null)
                {
                    var newAttachment = _mapper.Map<Attachment>(model);
                    await _repository.AddAttachmentAsync(comment.Id, newAttachment);
                    return Ok(new AttachmentModel { CommentId = model.CommentId });
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        /// <summary>
        /// Edit comment
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(PostCommentModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit(PostCommentModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var editCommentModel = _mapper.Map<Comment>(request);
                    var commentId = await _repository.UpdateAsync(editCommentModel);
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
        /// Edit attachment
        /// </summary>
        /// <returns></returns>
        [HttpPut("{comments/editAttach")]
        [ProducesResponseType(typeof(AttachmentModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditAttachment(AttachmentModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var editAttachmentModel = _mapper.Map<Attachment>(request);
                    var attachmentId = await _attachmentrepository.UpdateAsync(editAttachmentModel);
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
        /// Delete specified comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [HttpDelete("{commentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid commentId)
        {
            try
            {
                var comment = await _repository.FindByIdAsync(commentId);
                await _repository.Delete(comment);
                return Ok();
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        /// <summary>
        /// Delete specified attachmnet
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        [HttpDelete("attachment/{attachmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAttachment(Guid attachmentId)
        {
            try
            {
                var attachment = await _attachmentrepository.FindByIdAsync(attachmentId);
                await _attachmentrepository.Delete(attachment);
                return Ok();
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

    }
}
