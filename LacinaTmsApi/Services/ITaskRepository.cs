using System.Collections.Generic;
using System.Threading.Tasks;
using LacinaTmsApi.Models;

namespace LacinaTmsApi.Services
{
    public interface ITaskRepository
    {
        #region MainTasks

        public void AddMainTask(MainTask mainTask);
        public Task UpdateMainTask(int parentId, MainTask mainTask);
        public void DeleteMainTask(MainTask mainTask);
        public Task<MainTask> GetMainTaskById(int parentId);
        public Task<IEnumerable<MainTask>> GetAllMainTasks(string queryParameters);

        #endregion


        #region SubTask

        public void AddSubTask(SubTask subTask, int parentId);
        public void UpdateSubTask(int parentId, SubTask subTask);
        public void DeleteSubtask(SubTask subtTask, int parentId);
        public Task<SubTask> GetSubTaskById(int id, int parentId);
        //public Task<IQueryable<SubTask>> GetParentAllSubTasks(int parentId);
        public Task<SubTask[]> GetParentAllSubTasks(int parentId);

        #endregion


        #region Helpers 

        public Task<bool> SaveChangesAsyncTask();
        bool AnyTaskExists(int id);

        #endregion
    }
}
