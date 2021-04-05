using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using LacinaTmsApi.Classes;
using LacinaTmsApi.Data.Migrations;
using LacinaTmsApi.Models;
using LacinaTmsApi.Services;
//using System.Text.Json;
//using Newtonsoft.Json;

namespace LacinaTmsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskRepository> _logger;
        //using this to show other them Repository way, probably will be migrated to ITaskRepository after 
        private readonly TaskDbContext _context;

        public TasksController(ITaskRepository itaskRepository, ILogger<TaskRepository> logger, TaskDbContext context)
        {
            _taskRepository = itaskRepository;
            _logger = logger;
            _context = context;
            _context.Database.EnsureCreated();
        }

        #region MainTask Endpoints

        //GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MainTask>>> GetManTasks([FromQuery] string queryParameters)
        {
            try
            {
                var maiAsyncTasks = await _taskRepository.GetAllMainTasks(queryParameters);
                if (!maiAsyncTasks.Any())
                {
                    return NotFound();
                }
                return Ok(maiAsyncTasks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetMaTasks, details: {ex}");
                return StatusCode(500);
            }
        }

        //GET: api/tasks/{parentId}
        [HttpGet("{parentId}")]
        public async Task<IActionResult> GetMainTask([FromRoute] int parentId)
        {
            try
            {
                var maiAsyncTask = await _taskRepository.GetMainTaskById(parentId);
                if (maiAsyncTask == null)
                {
                    return NotFound();
                }
                return Ok(maiAsyncTask);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAllMainTasks, details: {ex}");
                return StatusCode(500);
            }
        }

        //DELETE: api/tasks/{parentId}
        [HttpDelete("{parentId}")]
        public async Task<IActionResult> DeleteMainTask([FromRoute] int parentId)
        {
            try
            {
                var maiAsyncTask = await _taskRepository.GetMainTaskById(parentId);
                if (maiAsyncTask == null)
                {
                    return NotFound();
                }
                _taskRepository.DeleteMainTask(maiAsyncTask);
                if (await _taskRepository.SaveChangesAsyncTask())
                {
                    return NoContent();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on DeleteMainTask, details: {ex}");
                return StatusCode(500);
            }
        }

        //POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> PostMainTask([FromBody] MainTask mainTask)
        {
            try
            {
                if (mainTask == null || !ModelState.IsValid)
                {
                    return BadRequest();
                }
                _taskRepository.AddMainTask(mainTask);
                if (await _taskRepository.SaveChangesAsyncTask())
                {
                    return StatusCode(201, mainTask);
                }
                return BadRequest(mainTask);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on PostMainTask, details: {ex}");
                return StatusCode(500);
            }
        }

        //PUT: api/tasks/{parentId}
        [HttpPut("{parentId}")]
        public async Task<IActionResult> PutMainTask([FromRoute] int parentId, [FromBody] MainTask mainTask)
        {
            try
            {
                if (parentId != mainTask.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _taskRepository.UpdateMainTask(parentId, mainTask);
                if (await _taskRepository.SaveChangesAsyncTask())
                {
                    return StatusCode(200, mainTask);
                }
                return BadRequest(mainTask);
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError($"DbUpdateConcurrencyException on PutMainTask, details: {e}");
                if (!_taskRepository.AnyTaskExists(parentId))
                {
                    return NotFound();
                }
                return BadRequest(mainTask);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on PutMainTask, details: {ex}");
                return StatusCode(500);
            }
        }

        #endregion

        #region MainTask Endpoints

        //GET: api/tasks/{parentId}/subtasks/{id}
        [HttpGet("{parentId}/subtasks/{id}")]
        public async Task<IActionResult> GetSubTaskById([FromRoute] int parentId, int id)
        {
            try
            {
                //checking if main task exists
                if (!_taskRepository.AnyTaskExists(parentId))
                {
                    return NotFound();
                }
                var subTaskAsync = await _taskRepository.GetSubTaskById(id, parentId);
                if (subTaskAsync == null)
                {
                    return NotFound();
                }
                return Ok(subTaskAsync);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetSubTaskById, details: {ex}");
                return StatusCode(500);
            }
        }

        //GET:  api/tasks/{parentId}/subtasks
        [HttpGet("{parentId}/subtasks")]
        public async Task<ActionResult<IEnumerable<MainTask>>> GetParenAllSubTasks([FromRoute] int parentId)
        {
            try
            {
                //checking if main task exists
                if (!_taskRepository.AnyTaskExists(parentId))
                {
                    return NotFound();
                }
                var subAsyncTasks = await _taskRepository.GetParentAllSubTasks(parentId);
                if (!subAsyncTasks.Any())
                {
                    return NotFound();
                }
                return Ok(subAsyncTasks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetParentAllSubTasks, details: {ex}");
                return StatusCode(500);
            }
        }

        //POST: api/tasks/{parentId}/subtasks
        [HttpPost("{parentId}/subtasks")]
        public async Task<IActionResult> PostSubTaskTask([FromRoute] int parentId, [FromBody] SubTask subTask)
        {
            try
            {
                //checking if main task exists
                if (!_taskRepository.AnyTaskExists(parentId))
                {
                    return NotFound();
                }
                if (subTask == null || !ModelState.IsValid)
                {
                    return BadRequest();
                }
                _taskRepository.AddSubTask(subTask, parentId);
                if (await _taskRepository.SaveChangesAsyncTask())
                {
                    return StatusCode(201, subTask);
                }
                return BadRequest(subTask);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on PostSubTaskTask, details: {ex}");
                return StatusCode(500);
            }
        }

        //DELETE: api/tasks/{parentId}/subtasks/{id}
        [HttpDelete("{parentId}/subtasks/{id}")]
        public async Task<IActionResult> DeleteSubTask([FromRoute] int parentId, int id)
        {
            try
            {
                //checking if main task exists
                if (!_taskRepository.AnyTaskExists(parentId))
                {
                    return NotFound();
                }
                var subTask = await _taskRepository.GetSubTaskById(id, parentId);
                if (subTask == null)
                {
                    return NotFound();
                }
                _taskRepository.DeleteSubtask(subTask, parentId);

                if (await _taskRepository.SaveChangesAsyncTask())
                {
                    return NoContent();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on DeleteMainTask, details: {ex}");
                return StatusCode(500);
            }
        }

        //PUT: api/tasks/{parentId}/subtask/{id}
        [HttpPut("{parentId}/subtasks/{id}")]
        public async Task<IActionResult> PutSubTask([FromRoute] int parentId, int id, [FromBody] SubTask subTask)
        {
            try
            {
                if (!_taskRepository.AnyTaskExists(id))
                {
                    return NotFound();
                }
                if (id != subTask.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _taskRepository.UpdateSubTask(parentId, subTask);

                if (await _taskRepository.SaveChangesAsyncTask())
                {
                    return StatusCode(200, subTask);
                }
                return BadRequest(subTask);
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError($"DbUpdateConcurrencyException on PutSubTask, details: {e}");

                if (!_taskRepository.AnyTaskExists(id))
                {
                    return NotFound();
                }
                return BadRequest(subTask);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on PutSubTask, details: {ex}");
                return StatusCode(500);
            }
        }

        #endregion

        #region Report

        //TODO use this logic letter
        /*
        [HttpGet("report")]
        public async Task<IActionResult> GetTasksForReport([FromQuery] TaskQueryParameters queryParameters)
        {
            // //helper code for creating and testing Postman URL: 
            // TaskQueryParameters myTaskQueryParameters = new TaskQueryParameters();
            // //myTaskQueryParameters.StartDate = DateTime.Today.AddDays(-1);
            // myTaskQueryParameters.FinishDate = DateTime.Today.AddDays(-1);
            // var queryForPostman = JsonConvert.SerializeObject(myTaskQueryParameters);
            // Console.WriteLine(queryForPostman);  //{"State":null,"StartDate":"2021-04-04T00:00:00+01:00","FinishDate":"2021-04-04T00:00:00+01:00","Page":0,"Size":50,"SortBy":"Id","SortOrder":"asc"}
            // queryParameters = myTaskQueryParameters;
            try
            {
                IQueryable<MainTask> mainTasks = _context.MainTasks.Where(x => x.State == State.Planned);  //TODO State.InProgress
                //filtering for Start day being a given date 
                mainTasks = mainTasks.Where(p => queryParameters != null && p.StartDate.Date == queryParameters.StartDate.Date);

                return Ok(await mainTasks.ToArrayAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetTasksForReport, details: {ex}");
                return StatusCode(500);
            }
        }
        */

        //this method can was tested using Google Chrome https://localhost:44365/api/tasks/report/export?StartDate=2021-04-04T10:34
        //returning: C:\Users\jacek\Downloads\taskManagementReport (1).csv 
        [HttpGet("report/export")]
        public dynamic GetTasksForReportExport([FromQuery] TaskQueryParameters queryParameters)
        {
            try
            {
                IQueryable<MainTask> mainTasks = _context.MainTasks.Where(x => x.State == State.Planned); //TODO State.InProgress
                //filtering for Start day being a given date 
                mainTasks = mainTasks.Where(p => queryParameters != null && p.StartDate.Date == queryParameters.StartDate.Date);

                List<MainTask> list = new List<MainTask>();
                foreach (var task in mainTasks) list.Add(task);

                //logic for exporting to CSV
                var exportService = new ExportService<MainTask>();
                var content = exportService.ConvertListToBytes(list);
                var contentType = "text/csv";
                var fileName = "taskManagementReport.csv";
                return File(content, contentType, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetTasksForReportExport, details: {ex}");
                return StatusCode(500);
            }
        }

        #endregion
    }
}
