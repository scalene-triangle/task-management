﻿using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using System.Text.Json.Serialization;

namespace task_management.Models
{
    public class Project: GenericRecord
    {
        [StringLength(40, MinimumLength = 1)]
        [Required]
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        [JsonIgnore] public virtual List<MyTask> MyTasks { get; set; } = new List<MyTask>();
    }
}
