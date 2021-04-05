using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LacinaTmsApi.Models
{
    //Table design on assumption that All fields are required on entities creation (our tasks are set in time - estimation are done before) 
    public class SubTask
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public State State { get; set; } = State.Planned;

        public int MainTaskId { get; set; }

        [JsonIgnore]
        public virtual MainTask ParentMainTask { get; set; }
    }
}
