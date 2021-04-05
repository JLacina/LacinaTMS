using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LacinaTmsApi.Data.Migrations;
using LacinaTmsApi.Models;


namespace LacinaTmsApi.Services
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;
        private readonly ILogger<TaskRepository> _logger;

        public TaskRepository(TaskDbContext context, ILogger<TaskRepository> logger)
        {
            _context = context;
            _context.Database.EnsureCreated();
            _logger = logger;
        }

        public async Task<bool> SaveChangesAsyncTask()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        #region MainTasks
        public void DeleteMainTask(MainTask mainTask)
        {
            try
            {
                _logger.LogInformation($"Removing new task with name: of {mainTask.Name}.");
                _context.Remove(mainTask);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while removing task with name: of {mainTask.Name}, exception: {e} ");
                throw;
            }
        }

        public void AddMainTask(MainTask mainTask)
        {
            try
            {
                _logger.LogInformation($"Adding new task with name: of {mainTask.Name}.");
                _context.Add(mainTask);
            }
            catch (Exception e)
            {
                _logger.LogError(
                    $"Error while adding new task with name: of {mainTask.Name}, exception: {e} ");
                throw;
            }
        }

        public async Task UpdateMainTask(int id, MainTask mainTask)
        {
            try
            {
                _logger.LogInformation($"Updating task with name: of {mainTask.Name}.");
                var existingTask = _context.MainTasks.Where(t => t.Id == mainTask.Id).AsNoTracking().FirstOrDefaultAsync();
                if (existingTask != null)
                {
                    //checking if [main-task (has NO children) and it's (state is not set)] based on default value from point '4c => Planned in all other cases.' TODO confirm requirements with BA
                    if (!MainTaskChildrenExists(id) && mainTask.State == null)
                    {
                        mainTask.State = State.Planned;
                    }
                    //calculating State for Main-task WITH children (sub-tasks) (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs4014)
                    else
                    {
                        await SettingStateForMainTaskWithChildren(mainTask);
                    }
                    _context.Entry(mainTask).State = EntityState.Modified;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while updating task with name: of {mainTask.Name}, exception: {e} ");
                throw;
            }
        }

        public async Task<MainTask> GetMainTaskById(int parentId)
        {
            try
            {
                _logger.LogInformation($"Getting  task with id: of {parentId}.");
                return await _context.MainTasks.Where(t => t.Id == parentId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while finding task with id: of {parentId}, exception: {e} ");
                throw;
            }
        }

        //TODO  add latter queryParameters staff TBC
        public async Task<IEnumerable<MainTask>> GetAllMainTasks(string queryParameters)
        {
            try
            {
                if (!string.IsNullOrEmpty(queryParameters))
                {
                    if (_context.MainTasks.Any(a => a.Name == queryParameters))
                    {
                        //using AsNoTracking() for better performance  
                        return await _context.MainTasks?.Where(a => a.Name == queryParameters).OrderBy(t => t.Id).AsNoTracking().ToListAsync();
                    }
                }
                return await _context.MainTasks.OrderBy(t => t.Id).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while returning  tasks, exception: {e} ");
                //return Enumerable.Empty<MainTask>();
                throw;
            }
        }

        #endregion


        #region Subtasks
        public void AddSubTask(SubTask subTask, int parentId)
        {
            try
            {
                _logger.LogInformation($"Adding new subTask with name: of {subTask.Name}, for parent Id: {parentId}");
                _context.Add(subTask);
            }
            catch (Exception e)
            {
                _logger.LogError(
                    $"Error while adding new task with name: of {subTask.Name} for parent Id; {parentId}, exception: {e} ");
                throw;
            }
        }

        public void UpdateSubTask(int parentId, SubTask subTask)
        {
            try
            {
                _logger.LogInformation($"Updating UpdateSubTask for parentId: {parentId}, with name: of {subTask.Name}.");
                var existingTask = _context.SubTasks.Where(t => t.Id == subTask.Id).AsNoTracking().FirstOrDefaultAsync();
                if (existingTask != null)
                {
                    //fixing issue with duplicates https://stackoverflow.com/questions/62253837/the-instance-of-entity-type-cannot-be-tracked-because-another-instance-with-the
                    //_context.Entry(subTask).State = EntityState.Detached; 
                    _context.Entry(subTask).State = EntityState.Modified;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while updating SubTask with name: of {subTask.Name}, inner exception: {e.InnerException?.Message} ");
                throw;
            }
        }

        public async Task<SubTask> GetSubTaskById(int id, int parentId)
        {
            try
            {
                _logger.LogInformation($"Getting  sub-task with id:{id}, for a parent with parentId: {parentId}.");
                return await _context.SubTasks.Where(s => s.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while finding sub-task with id: of {id}, exception: {e} ");
                throw;
            }
        }

        public async Task<SubTask[]> GetParentAllSubTasks(int parentId)
        {
            try
            {
                _logger.LogInformation($"Getting  all sub-task for parentId: {parentId}.");
                //IQueryable<SubTask> query = _context.MainTasks.Include(x => x.SubTasks).Where(y => y.Id == parentId);
                IQueryable<SubTask> query = _context.MainTasks.Include(x => x.SubTasks)
                    .Where(x => x.Id == parentId).Where(x => x.SubTasks.Count > 0).SelectMany(x => x.SubTasks);

                return await query.AsNoTracking().ToArrayAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while GetParentAllSubTasks for parentId: {parentId}, exception: {e} ");
                throw;
            }
        }

        public void DeleteSubtask(SubTask subTask, int parentId)
        {
            try
            {
                _logger.LogInformation($"Removing subTask task with name: of {subTask.Name}, for parent Id=. {parentId}");
                _context.Remove(subTask);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while removing subTask with name: of {subTask.Name}, for parentId: {parentId}, exception: {e} ");
                throw;
            }
        }

        #endregion


        #region Helper - validation  methods

        public bool AnyTaskExists(int id)
        {
            return _context.MainTasks.Any(e => e.Id == id);
        }

        //checking if main tasks has children (sub-tasks) or not
        private bool MainTaskChildrenExists(int id)
        {
            try
            {
                IQueryable<SubTask> query = _context.MainTasks.Include(x => x.SubTasks)
                    .Where(x => x.Id == id).Where(x => x.SubTasks.Count > 0).SelectMany(x => x.SubTasks);

                if (query.GetEnumerator().Current != null)
                {
                    //has children
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //setting state for main task with children 
        private async Task SettingStateForMainTaskWithChildren(MainTask mainTask)
        {
            try
            {
                var taskChildern = GetParentAllSubTasks(mainTask.Id);
                int count = 0;
                int completedCount = 0;
                int inProgresCount = 0;
                //int plannedCount = 0;

                foreach (var subtask in await taskChildern)
                {
                    count++;
                    switch (subtask.State)
                    {
                        // case State.Planned:
                        //     plannedCount++;
                        //     break;
                        case State.Completed:
                            completedCount++;
                            break;
                        case State.InProgress:
                            inProgresCount++;
                            break;
                    }
                }

                //all sub-tasks (children are completed) 
                if (count != 0 && count == completedCount)
                {
                    mainTask.State = State.Completed;
                }
                //at least 1 sub-task is still in progress
                if (count != 0 && inProgresCount > 0)
                {
                    mainTask.State = State.InProgress;
                }
                //default setting it to planned 
                else
                {
                    mainTask.State = State.Planned;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion
    }
}
