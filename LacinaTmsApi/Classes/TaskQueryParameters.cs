using System;
using LacinaTmsApi.Models;

namespace LacinaTmsApi.Classes
{
   //TODO use this class for future filters 
    public class TaskQueryParameters : QueryParameters
    {
        public State? State { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }
    }
}
