using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models.ViewModels
{
    public class NewTask
    {
        [Required(ErrorMessage = "Task name is required")]
        public string? Task { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? TaskDesc { get; set; }
        [Required(ErrorMessage = "Task start time is required")]
        public TimeSpan TaskBeginTime { get; set; }
        [Required(ErrorMessage = "Task end time is required")]
        public TimeSpan TaskEndTime { get; set; }
        [Required(ErrorMessage = "Task start date is required")]
        public DateTime TaskBeginDate { get; set; }
        [Required(ErrorMessage = "Task end date is required")]
        public DateTime TaskEndDate { get; set; }
    }
    public class TaskViewModel
    {
        public TaskViewModel(List<SingleTask> tasks, List<DateTime> dates, List<Subordinate> subordinates)
        {
            Tasks = tasks;
            Dates = dates;
            Subordinates = subordinates;
        }
        public List<SingleTask>? Tasks { get; set; }
        public List<DateTime>? Dates { get; set; }
        public List<Subordinate> Subordinates { get; set; }
    }

    public class SingleTask
    {
        public SingleTask() { }
        public SingleTask(List<TaskSchedule> tasks)
        {
            Tasks = new List<SingleTask>();

            foreach (var t in tasks)
            {
                Tasks.Add(new SingleTask()
                {
                    Task = t.Task,
                    TaskId = t.TaskId,
                    TaskDesc = t.TaskDesc,
                    TaskBegin = t.TaskBegin,
                    TaskDeadline = t.TaskDeadline
                });
            }
        }
        public string TaskId { get; set; }
        public string Task { get; set; }
        public string TaskDesc { get; set; }
        public DateTime TaskBegin { get; set; }
        public DateTime TaskDeadline { get; set; }
        public List<SingleTask> Tasks { get; set; }
    }
    public class Subordinate
    {
        public Subordinate() { }
        public Subordinate(List<Employee> employees)
        {
            Subordinates = new List<Subordinate>();
            Subordinates.Add(new Subordinate { 
                Name = "My schedule",
                ID = 0
            });
            foreach(var e in employees)
            {
                Subordinates.Add(new Subordinate {
                    Name = e.Firstname +' '+ e.Surname,
                    ID = e.EmployeeId
                });
            }
        }
        public string? Name { get; set; }
        public int ID { get; set; }
        public List<Subordinate> Subordinates { get; set; }
    }
}
