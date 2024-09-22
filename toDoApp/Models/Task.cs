using System;
using System.Collections.Generic;

namespace toDoApp.Models;

public partial class Task
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string Task1 { get; set; } = null!;

    public bool? Isfinish { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
