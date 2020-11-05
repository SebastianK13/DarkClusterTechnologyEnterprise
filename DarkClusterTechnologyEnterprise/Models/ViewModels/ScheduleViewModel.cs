using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models.ViewModels
{
    public class ScheduleViewModel
    {
        public SelectList Hours { get; set; }
        public SelectList Minutes { get; set; }
        //public SelectList Days {get;set;}
        //public SelectList Months {get;set;}
        //public SelectList Years {get;set;}

    }
    public class NewTask
    {
        public string? Task { get; set; }
        public string? TaskDesc { get; set; }
        public TimeSpan TaskBeginTime { get; set; }
        public TimeSpan TaskEndTime { get; set; }
        public DateTime TaskBeginDate { get; set; }
        public DateTime TaskEndDate { get; set; }
    }
    public class TaskViewModel
    {
        public TaskViewModel(List<SingleTask> tasks, List<DateTime> dates)
        {
            Tasks = tasks;
            Dates = dates;
        }
        public List<SingleTask>? Tasks { get; set; }
        public List<DateTime>? Dates { get; set; }
    }
    public class SingleTask
    {
        public SingleTask() { }
        public SingleTask(List<TaskSchedule> tasks)
        {
            Tasks = new List<SingleTask>();

            foreach(var t in tasks)
            {
                Tasks.Add(new SingleTask() { 
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
}
