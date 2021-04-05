using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LacinaTmsApi.Models
{
    //Table design on assumption that All fields except 'State' are required on entities creation (our tasks are set in time, estimation are done before)  
    [Table("MainTask")]
    public class MainTask
    {
        public MainTask()
        {
            SubTasks = new List<SubTask>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public State? State { get; set; }

        public List<SubTask> SubTasks { get; set; }
    }
}
